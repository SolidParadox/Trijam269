using UnityEngine;

public class TRCSampler : MonoBehaviour {
  public Vector2 currentHeading;
  public RadarCore radar;

  private void FixedUpdate () {
    if ( radar.contacts.Count > 0 ) {
      Vector2 delta = Random.insideUnitCircle.normalized;
      for ( int i = 0; i < radar.contacts.Count; i++ ) { 
        try {
          if ( radar.contacts[i].GetComponent<TRCNode> ().touched ) { }
          delta += radar.contacts[i].GetComponent<TRCNode> ().GetLastTrace ();
          break;
        } catch {

        }
      }
      currentHeading = delta; 
    }
    Debug.DrawLine ( transform.position, (Vector3)currentHeading, Color.red );
  }
}
