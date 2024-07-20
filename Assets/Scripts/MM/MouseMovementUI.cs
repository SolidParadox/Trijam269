using UnityEngine;

public class MouseMovementUI : MonoBehaviour {
  public LineRenderer lr1;
  public LineRenderer lr2;
  public MMCore mm;
  public LayerMask layerMask;

  private Vector3 lpOffset = new Vector3 (0,0,-9);

  private void LateUpdate () {
    lr1.SetPosition ( 0, mm.transform.position + new Vector3( 0, 0, -9 - mm.transform.position.z ) );
    lr2.SetPosition ( 1, (Vector3)mm.target + lpOffset );
    RaycastHit2D rc = new RaycastHit2D();
    float length = ( (Vector2)mm.transform.position - mm.target ).magnitude;
    rc = Physics2D.Raycast ( mm.transform.position, (Vector3)mm.target - mm.transform.position, length, layerMask );
    if ( rc ) {
      lr1.SetPosition ( 1, (Vector3) rc.point + lpOffset );
      lr2.SetPosition ( 0, (Vector3) rc.point + lpOffset );
    } else {
      lr1.SetPosition ( 1, (Vector3) mm.target + lpOffset );
      lr2.SetPosition ( 0, (Vector3) mm.target + lpOffset );
    }
  }
}
