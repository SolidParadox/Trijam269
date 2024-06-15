using UnityEngine;

public class EQWireCutters : EQBase {
  public float cutLength;
  public RadarCore cableRadar;

  public override void OnStartFire () {
    if ( cableRadar.breached ) {
      ENGCable deltaCable;
      for ( int i = 0; i < cableRadar.contacts.Count; i++ ) {
        if ( cableRadar.contacts[ i ].transform.parent.TryGetComponent( out deltaCable ) ) {
          deltaCable.DealWithIntrerupt ( transform.position, cutLength );
          break;
        }
      }
      base.OnStartFire ();
    }
  }
}
