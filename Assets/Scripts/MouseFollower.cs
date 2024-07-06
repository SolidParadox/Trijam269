using UnityEngine;

public class MouseFollower : MonoBehaviour {
  public Camera cam;
  public bool tracking = true;
  
  private void LateUpdate () {
    if ( Input.GetKeyDown ( KeyCode.Escape ) ) { tracking = !tracking; }
    if ( !tracking ) return;

    transform.position = (Vector2)cam.ScreenToWorldPoint ( Input.mousePosition ); 
  }
}
