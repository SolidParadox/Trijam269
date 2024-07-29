using UnityEngine;

public class PUCK : MonoBehaviour {
  public int healthPoints;
  public int remainingHP;

  public Mika powerTrain;
  public RadarCore damageRadar;
  
  // Also invulnerable while stunned
  public float stunDuration;
  private float stunTimer;

  public Animator animator;
  public LightZone flashlight;
  public PIDCore avPIDCore; // Angular velocity PID Core
  public float avMaxDelta;

  private void Start () {
    remainingHP = healthPoints;
    stunTimer = 0;

    animator.SetBool ( "Stunned", false );
    animator.SetInteger ( "HPSTATUS", remainingHP );
  }

  private Vector2 direction;

  void Update () {
    if ( damageRadar.contacts.Count != 0 && stunTimer <= 0 ) { // hurt
      remainingHP--;
      stunTimer = stunDuration;

      direction =  powerTrain.rgb.position - ( Vector2)damageRadar.contacts[0].transform.position;

      animator.SetBool ( "Stunned", true );
      animator.SetInteger ( "HPSTATUS", remainingHP );
    }
    if ( stunTimer > 0 ) {
      stunTimer -= Time.deltaTime;
      if ( stunTimer <= 0 ) {
        animator.SetBool ( "Stunned", false );
      }
    } else {
      Vector2 deltaD =  new Vector2 ( Input.GetAxis ( "Horizontal" ), Input.GetAxis ( "Vertical" ) );
      if ( deltaD.magnitude > 0.01f ) {
        direction = deltaD;
      }
    }
    powerTrain.AddThrusterOutput ( direction );
  }

  private float AngleNormalizer ( float past, float current ) {
    int maxC = 10;
    while ( Mathf.Abs ( current - past ) > 180 && maxC > 0 ) {
      current += 360 * Mathf.Sign ( past );
      maxC--;
    }
    return current;
  }
  private float deltaAT = 0;
  private void FixedUpdate () {
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
  }
}
