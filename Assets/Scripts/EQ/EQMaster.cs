using UnityEngine;

public class EQMaster : MonoBehaviour {
  public PUCK puck;
  public RadarCore pickupRadar;
  public EQBase currentEquipment;

  void Update () {
    if ( pickupRadar.breached && currentEquipment == null && Input.GetKey ( KeyCode.E ) ) {
      currentEquipment = pickupRadar.contacts[0].GetComponent<EQBase> ();
      currentEquipment.Attach ( transform.GetChild ( 0 ) );  
    }
    if ( Input.GetKey ( KeyCode.G ) && currentEquipment != null ) {
      currentEquipment.Detach ();
      currentEquipment = null;
    }
    if ( puck.isStunned && currentEquipment != null ) {
      currentEquipment.Detach ();
      currentEquipment = null;
    }
  }
}
