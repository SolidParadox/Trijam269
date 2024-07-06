using UnityEngine;

public class MMCore : MonoBehaviour {
  public Transform mousePosition;

  public float maxSpeed;
  public float aimingDistance;

  public bool detached;
  private Vector2 target;

  public float strengthR;
  public float strengthLD;

  private void LateUpdate () {
    if ( detached || ( transform.position - (Vector3)target ).sqrMagnitude > aimingDistance ) {
      float fuckingAngle = Vector3.SignedAngle ( Vector2.up, mousePosition.position - transform.position , Vector3.forward );
      transform.rotation = Quaternion.Lerp ( transform.rotation, Quaternion.Euler ( 0, 0, fuckingAngle ), strengthR * Time.deltaTime );
    }
    detached = Input.GetAxis ( "Fire1" ) > 0;
    if ( !detached ) {
      target = mousePosition.position;
    }
    Vector3 omegaPos = (Vector3) target - transform.position;
    omegaPos = omegaPos.normalized * Mathf.Clamp ( omegaPos.magnitude * strengthLD * Time.deltaTime, 0, maxSpeed * Time.deltaTime );
    transform.position += omegaPos;
  }
}
