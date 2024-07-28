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

  private void Start () {
    remainingHP = healthPoints;
    stunTimer = 0;

    animator.SetBool ( "Stunned", false );
    animator.SetInteger ( "HPSTATUS", remainingHP );
  }

  void Update () {
    if ( damageRadar.contacts.Count != 0 && stunTimer <= 0 ) { // hurt
      remainingHP--;
      stunTimer = stunDuration;

      powerTrain.AddThrusterOutput ( powerTrain.rgb.position - ( Vector2)damageRadar.contacts[0].transform.position );

      animator.SetBool ( "Stunned", true );
      animator.SetInteger ( "HPSTATUS", remainingHP );
    }
    if ( stunTimer > 0 ) {
      stunTimer -= Time.deltaTime;
      if ( stunTimer <= 0 ) {
        animator.SetBool ( "Stunned", false );
      }
    } else {
      powerTrain.AddThrusterOutput ( new Vector2 ( Input.GetAxis ( "Horizontal" ), Input.GetAxis ( "Vertical" ) ) );
    }
  }

  private void FixedUpdate () {
    if ( stunTimer <= 0 ) {
      powerTrain.rgb.rotation = Vector2.SignedAngle ( Vector2.up, powerTrain.rgb.velocity );
    }
  }
}
