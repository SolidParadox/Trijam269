using UnityEngine;

public class PUCK : MonoBehaviour {
  public int healthPoints;
  public int remainingHP;

  public Mika powerTrain;
  public RadarCore damageRadar;
  public RadarCore collisionRadar;

  public float stunMinSpeedDelta; // Minimum speed required to stun on collision w/terrain
  public float stunDuration;
  private float stunTimer;

  public Animator animator;
  public LightZone flashlight;

  public PIDCore avPIDCore; // Angular velocity PID Core
  public float avMaxDelta;  // Angular Velocity Max Change In Velocity

  public Color[] flashlightOptions;
  private float foIndex = 0;

  private Vector2 direction;
  private float deltaAT = 0;
  private float pastSpeed; // Saved past speed of rgb to detect fast decelerations

  // PUBLIC METHODS

  public void StartStun () {
    stunTimer = stunDuration;
    animator.SetBool ( "Stunned", true );
    animator.SetInteger ( "HPSTATUS", remainingHP );

    direction = direction.normalized * 0.01f;

    powerTrain.rgb.rotation %= 360;
    deltaAT %= 360;

    avPIDCore.ResetError ( powerTrain.rgb.rotation, powerTrain.rgb.rotation );
  }

  // UNITY METHODS

  private void Start () {
    remainingHP = healthPoints;
    stunTimer = 0;

    animator.SetBool ( "Stunned", false );
    animator.SetInteger ( "HPSTATUS", remainingHP );
  }

  void Update () {
    // STUN ON OVERSPEED COLLISION
    if ( collisionRadar.breached && collisionRadar.changeLastFrame && stunTimer <= 0 && pastSpeed - powerTrain.rgb.velocity.magnitude > stunMinSpeedDelta ) {
      StartStun ();
      collisionRadar.changeLastFrame = false;
      //Debug.Log ( "HIT!!!!" );
    }
    //Debug.Log ( powerTrain.rgb.velocity.magnitude );
    
    // GET DAMAGED 
    if ( damageRadar.contacts.Count != 0 && stunTimer <= 0 ) { // hurt
      remainingHP--;
      //direction = powerTrain.rgb.position - (Vector2) damageRadar.contacts[0].transform.position;
      StartStun ();
    }

    // IF STUNNED, LOSE CONTROL
    if ( stunTimer > 0 ) {
      stunTimer -= Time.deltaTime;
      if ( stunTimer <= 0 ) {
        animator.SetBool ( "Stunned", false );
        flashlight.spectrum = flashlightOptions[(int) foIndex];
      } else {
        deltaAT = powerTrain.rgb.rotation;
        direction = powerTrain.rgb.transform.up * 0.01f;
        flashlight.spectrum = Color.black;
      }
    } else {
      Vector2 deltaD =  new Vector2 ( Input.GetAxis ( "Horizontal" ), Input.GetAxis ( "Vertical" ) );
      if ( deltaD.magnitude > 0.01f ) {
        direction = deltaD;
      }
    }
    
    // CYCLE FLASHLIGHT OPTIONS
    if ( Input.GetAxis("Fire2") > 0 && stunTimer <= 0 ) {
      foIndex += Time.deltaTime * 5;
      foIndex = Mathf.Repeat ( foIndex, flashlightOptions.Length );
      flashlight.spectrum = flashlightOptions[(int) foIndex];
    } else {
      foIndex = (int) foIndex;
    }

    // ONLY APPLY THRUST FORWARD
    Vector2 thrusters = Vector3.Project ( direction, powerTrain.rgb.transform.up );
    if ( Vector2.Dot ( thrusters, powerTrain.rgb.transform.up ) < 0 ) {
      thrusters = Vector2.zero;
    }
    powerTrain.AddThrusterOutput ( thrusters );
  }

  private void FixedUpdate () {
    // AUTOROTATE TO AT
    if ( stunTimer <= 0 ) {
      float aT = Vector2.SignedAngle ( Vector2.up, direction );
      float aC = powerTrain.rgb.rotation;

      aT = AngleNormalizer ( deltaAT, aT );

      //sDebug.Log ( aT + " " + aC + " FROM : " + Vector2.SignedAngle( Vector2.up, direction ) + " " + powerTrain.rgb.rotation );

      float deltaA = avPIDCore.WorkFunction ( aT, aC, Time.fixedDeltaTime );
      deltaA = Mathf.Clamp ( deltaA, -avMaxDelta, avMaxDelta );
      powerTrain.rgb.angularVelocity += deltaA;

      deltaAT = aT;

      if ( Mathf.Abs ( deltaAT ) > 1080 ) {
        deltaAT -= 360 * Mathf.Sign ( deltaAT );
        powerTrain.rgb.rotation -= 360 * Mathf.Sign ( deltaAT );
        avPIDCore.ResetError ( deltaAT, powerTrain.rgb.rotation );
      }
    }
    pastSpeed = powerTrain.rgb.velocity.magnitude;
  }

  // PRIVATE METHODS

  private float AngleNormalizer ( float past, float current ) {
    int maxC = 10;
    while ( Mathf.Abs ( current - past ) > 180 && maxC > 0 ) {
      current += 360 * Mathf.Sign ( past );
      maxC--;
    }
    return current;
  }
}
