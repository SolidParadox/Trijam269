using UnityEngine;

public class MouseMovementUI : MonoBehaviour {
  public LineRenderer lr1;
  public LineRenderer lr2;
  public MMCore mm;
  public LayerMask layerMask;

  private void LateUpdate () {
    lr1.SetPosition ( 0, mm.transform.position );
    lr2.SetPosition ( 1, mm.target );
    RaycastHit2D rc = new RaycastHit2D();
    float length = ( (Vector2)mm.transform.position - mm.target ).magnitude;
    rc = Physics2D.Raycast ( mm.transform.position, (Vector3)mm.target - mm.transform.position, length, layerMask );
    if ( rc ) {
      lr1.SetPosition ( 1, rc.point );
      lr2.SetPosition ( 0, rc.point );
    } else {
      lr1.SetPosition ( 1, mm.target );
      lr2.SetPosition ( 0, mm.target );
    }
  }
}
