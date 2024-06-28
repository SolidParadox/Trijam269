using UnityEngine;

// Mouse Double Background Focus
public class UIMDBF : MonoBehaviour {
  public Transform target;
  public float strength;
  public Camera cam;

  void LateUpdate () {
    Vector2 delta = cam.ScreenToWorldPoint ( Input.mousePosition ) * strength;
    target.position = delta;
  }
}
