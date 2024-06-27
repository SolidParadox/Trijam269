using UnityEngine;

public class NULSampler : MonoBehaviour {
  public Color target;
  public float sensitivity;

  public bool inLight;

  private void Update () {
    Color delta = NULCore.Instance.GetLL ( transform.position );
    Vector3 delta2 = new Vector3 ( delta.r, delta.g, delta.b ) * sensitivity;
    inLight = delta2.x >= target.r && delta2.y >= target.g && delta2.z >= target.b;
  }
}
