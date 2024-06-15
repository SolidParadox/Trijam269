using UnityEngine;

public class MANDoor : MonoBehaviour {
  public int gateID;
  private bool currentStatus;

  public bool powerPolarity;
  public float reqPower;
  public ENGPDS pds;

  public Animator animator;

  public Collider2D col;

  private void Recheck () {
    SceneCore.Instance.nav.gates[gateID].closed = !currentStatus;
    SceneCore.Instance.nav.GenerateNodes ();
    animator.SetBool ( "DoorOpen", currentStatus );
  }

  private void Update () {
    if ( reqPower != 0 ) {
      col.enabled = pds.throughput * ( powerPolarity ? 1 : -1 ) > reqPower * ( powerPolarity ? 1 : -1 );
      if ( col.enabled != currentStatus ) {
        currentStatus = col.enabled;
        Recheck ();
      }
    } else {
      col.enabled = powerPolarity;
      if ( col.enabled != currentStatus ) {
        currentStatus = col.enabled;
        Recheck ();
      }
    }
  }
}
