using UnityEngine;

public class TComp : MonoBehaviour {
  public  Transform   trackedTransform;
  public  Rigidbody2D trackedRigidbody;

  public  Vector3     linearOffset;
  [Tooltip("Velocity")]
  public  float       strengthV;
  [Tooltip("Rotation")]
  public  float       strengthR; 
  [Tooltip("Linear Distance")]
  public  float       strengthLD; 

  private Vector3     delta;
  public  bool        switchRT = false;   // Real Time

  public float        teleportRange = 10;

  public void LoadTracked ( Transform target ) {
    trackedTransform = target;
    trackedTransform.TryGetComponent ( out trackedRigidbody );
    if ( trackedTransform == null ) {   // Just a safety ...
      enabled = false;
      Debug.LogError ( "MISSING TRACKED" );
    }
  }

  void LateUpdate () {
    delta = trackedTransform.position;

    if ( trackedRigidbody != null ) {
      delta += ( Vector3 )trackedRigidbody.velocity * strengthV;
    }

    delta += linearOffset;

    float deltaTSF = switchRT ? Time.deltaTime : Time.unscaledDeltaTime;

    Vector3 omegaPos = Vector3.Lerp ( transform.position - linearOffset, delta, strengthLD * deltaTSF ) + linearOffset;
    if ( ( omegaPos - transform.position ).sqrMagnitude > teleportRange ) {
      omegaPos = delta;
    }
    transform.position = omegaPos;
    transform.rotation = Quaternion.Lerp ( transform.rotation, trackedTransform.rotation, strengthR * deltaTSF );
  }
}
