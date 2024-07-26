using UnityEngine;

public class TRCSampler : MonoBehaviour {
  public Vector2 currentHeading;
  public RadarCore radar;

  private void FixedUpdate () {
    if ( radar.contacts.Count > 0 ) {
      Vector2 delta = Vector2.zero;
      for ( int i = 0; i < radar.contacts.Count; i++ ) { 
        try {
          delta += radar.contacts[i].GetComponent<TRCNode> ().GetLastTrace ();
        } catch {

        }
      }
      delta /= radar.contacts.Count;
      currentHeading = delta; 
    }
    Debug.DrawLine ( transform.position, (Vector3)currentHeading, Color.red );
  }
}
