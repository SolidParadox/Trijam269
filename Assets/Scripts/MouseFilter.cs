using UnityEngine;

public class MouseFilter : MonoBehaviour {
  public MouseFollower mf;

  public float spaceScale;
  public float rotationStrength;

  public void LateUpdate () {
    transform.position = mf.transform.position * spaceScale;
    transform.rotation = mf.transform.rotation;
  }
}
