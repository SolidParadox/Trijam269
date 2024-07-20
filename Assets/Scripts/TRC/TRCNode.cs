using UnityEngine;

public class TRCNode : MonoBehaviour {
  public RadarCore radar;
  public Vector2 heading;

  private void FixedUpdate () {
    if ( radar.contacts.Count > 0 ) {
      RaycastHit2D rc = new RaycastHit2D();
      // DEFAULT layer only layer mask
      Vector2 rayHeading = radar.contacts[ 0 ].transform.position - transform.position;
      rc = Physics2D.Raycast ( transform.position, rayHeading, rayHeading.magnitude, 1 );
      if ( rc.collider == null ) {
        heading = radar.contacts[0].transform.position - transform.position;
      }
    }
    Debug.DrawLine ( transform.position, transform.position + (Vector3)heading, Color.cyan );
  }

  public Vector2 GetLastTrace () {
    return transform.position + (Vector3)heading;
  }
}
