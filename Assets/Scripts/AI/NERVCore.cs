using UnityEngine;

public class NERVCore : MonoBehaviour {
  public enum StateMachine { Dormant, Feral, Attacking, Stunned, Dead }
  public StateMachine state;

  public NULSampler lightSampler;
  public TRCSampler traceSampler;
  public RadarCore collisionRadar;
  public Mika powerTrain;

  public RRays lineRadar;
  public LineRenderer line;

  public Vector2    cHeading;

  private float internalDuration = 1, deltaStun;
  public float paramThawDuration, paramFeralDuration, paramStunDuration;

  public Animator statusAnimator;

  private void Awake () {
    //SceneCore.Instance.EnemyAutoSubscribe ( this );
    internalDuration = paramThawDuration;
  }

  public float Status () {
    if ( state == StateMachine.Dormant) {
      return paramThawDuration / internalDuration;
    } else {
      return paramThawDuration / internalDuration;
    }
  }

  private void FixedUpdate () {
    statusAnimator.SetBool ( "InLight", lightSampler.inLight );

    if ( !lightSampler.inLight ) {
      if ( state == StateMachine.Dormant ) {
        paramThawDuration -= Time.fixedDeltaTime;
        if ( paramThawDuration <= 0 ) {
          state = StateMachine.Feral;
          internalDuration = paramFeralDuration;
        }
      } else {
        paramFeralDuration -= Time.fixedDeltaTime;
        if ( paramFeralDuration <= 0 ) {

          // Make it slidey so they continue if they are dashing -> horror
          powerTrain.strengthD = 0.01f;
          powerTrain.strengthDOS = 0.01f;

          line.SetPosition ( 0, Vector2.zero );
          line.SetPosition ( 1, Vector2.zero );

          state = StateMachine.Dead;
          enabled = false;
        }
      }
    }
    switch ( state ) {
      case StateMachine.Feral:
        cHeading = traceSampler.currentHeading - powerTrain.rgb.position;

        powerTrain.rgb.angularVelocity = 0;
        powerTrain.rgb.rotation = Vector2.SignedAngle ( Vector2.up, cHeading );
        cHeading.Normalize ();

        state = StateMachine.Attacking;
        break;
      case StateMachine.Attacking:
        powerTrain.SetThrusterOutput ( cHeading );

        if ( collisionRadar.contacts.Count != 0 ) {
          state = StateMachine.Stunned;
          deltaStun = paramStunDuration;
        }

        lineRadar.WorkFunction ();
        line.SetPosition ( 1, (Vector2)powerTrain.rgb.position );
        line.SetPosition ( 0, powerTrain.rgb.position + lineRadar.points[0] );
        Debug.DrawLine ( powerTrain.rgb.position, powerTrain.rgb.position + lineRadar.points[0], Color.blue );  

        break;
      case StateMachine.Stunned:
        deltaStun -= Time.fixedDeltaTime;
        powerTrain.rgb.angularVelocity = 0;
        powerTrain.SetThrusterOutput ( -powerTrain.rgb.transform.up * 0.1f );
        if ( deltaStun <= 0 ) {
          state = StateMachine.Feral;
        }
        line.SetPosition ( 0, Vector2.zero );
        line.SetPosition ( 1, Vector2.zero );
        break;
      default:
        break;
    }

    statusAnimator.SetInteger ( "Status", (int) state );
  }
}
