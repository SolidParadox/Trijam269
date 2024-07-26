using UnityEngine;

public class NULSampler : MonoBehaviour {
  public Color target;
  public float sensitivity;

  public float mass;
  private float deltaT;

  public bool inLight;

  private void Update () {
    Color delta = NULCore.Instance.GetLL ( transform.position );
    //Debug.Log ( delta );
    Vector3 delta2 = new Vector3 ( delta.r, delta.g, delta.b ) * sensitivity;
    bool sensorLight = delta2.x >= target.r && delta2.y >= target.g && delta2.z >= target.b;
    if ( !sensorLight ) {
      deltaT -= Time.deltaTime;
      if ( deltaT <= 0 ) {
        inLight = false;
      }
    } else {
      inLight = true;
      deltaT = mass;
    }
  }
}
