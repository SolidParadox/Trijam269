using UnityEngine;

public class MANAud : MonoBehaviour {
  public AudioSource droneAS;

  public float strengthBDS; // BASE DRONE SOUND LEVEL
  public float detectionRadius;

  private Transform    player;
  private RadarCore    enemyProximityRadar;

  void Start () {
    droneAS.volume = strengthBDS;
    player = SceneCore.Instance.playerTransform;
    enemyProximityRadar = player.GetChild(2).GetComponent<RadarCore>();
  }

  private void LateUpdate () {
    float ce = detectionRadius; // Closest enemy
    float delta;
    if ( enemyProximityRadar.breached ) {
      for ( int i = 0; i < enemyProximityRadar.contacts.Count; i++ ) {
        delta = ( enemyProximityRadar.contacts[i].transform.position - player.position ).magnitude;
        if ( delta < ce ) {
          ce = delta;
        }
      }
    }
    droneAS.volume = Mathf.Max ( strengthBDS, 1 - ( ce / detectionRadius ) * ( 1 - strengthBDS) );
  }
}
