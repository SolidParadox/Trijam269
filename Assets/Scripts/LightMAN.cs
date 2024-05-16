using UnityEngine;

public class LightMAN : MonoBehaviour {
  public RadarCore radar;
  public AudioSource breakAS;

  public GameObject[] dead;

  void Update () {
    if ( radar.breached ) {
      breakAS.Play ();
      for ( int i = 0; i < dead.Length; i++ ) {
        Destroy ( dead[i] );  
      }
      GetComponent<LightZone> ().enabled = false;
      enabled = false;
    }
  }
}
