using UnityEngine;

[RequireComponent ( typeof ( Collider2D ) )]
public class RZone : RadarCore {
  public bool includeTriggers = false;
  private void OnTriggerEnter2D ( Collider2D collision ) {
    if ( !includeTriggers && collision.isTrigger ) { return; }
    AddContact ( collision.gameObject );
  }

  private void OnTriggerExit2D ( Collider2D collision ) {
    if ( !includeTriggers && collision.isTrigger ) { return; }
    RemoveContact ( collision.gameObject );
  }
}
