using UnityEngine;

public class MouseFollower : MonoBehaviour {
  public Camera cam;
  public Transform follower;
  public float strenght = 1;
  public float rotationDistance = 0.1f;

  public bool tracking;

  private void Start () {
    tracking = true;
  }

  private void LateUpdate () {
    if ( Input.GetKeyDown(KeyCode.Escape) ) { tracking = !tracking; }
    if ( !tracking ) return;

    transform.position = strenght * cam.ScreenToWorldPoint ( Input.mousePosition );
    if ( follower != null && ( (Vector2) transform.position - (Vector2) follower.position ).sqrMagnitude > rotationDistance ) {
      float fuckingAngle = Vector3.SignedAngle ( Vector2.up,(Vector2) transform.position - (Vector2) follower.position, Vector3.forward );
      transform.rotation = Quaternion.Euler ( 0, 0, fuckingAngle );
    }
  }
}
