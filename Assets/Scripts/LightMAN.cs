using UnityEngine;

public class LightMAN : MonoBehaviour {
  public RadarCore radar;
  public AudioSource breakAS;

  public LightZone lz;

  public ENGCable cable;
  public float reqPower;
  public float maxLightPower;

  public GameObject[] dead;

  void Update () {
    if ( radar.breached ) {
      breakAS.Play ();
      for ( int i = 0; i < dead.Length; i++ ) {
        Destroy ( dead[i] );  
      }
      lz.enabled = false;
      enabled = false;
    }
    if ( reqPower != 0 ) {
      lz.baseLightPower = maxLightPower * Mathf.Clamp01 ( cable.throughput / reqPower );
    } else {
      lz.baseLightPower = maxLightPower;
    }
  }
}
