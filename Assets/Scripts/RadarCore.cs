using UnityEngine;
using System.Collections.Generic;

public abstract class RadarCore : MonoBehaviour {
  public List<GameObject> contacts = new List<GameObject>();
  public bool breached { get; protected set; }

  public string targetTag;
  public enum TagMode { Free, Exclude, Include };
  public TagMode tagMode;

  public bool changeLastFrame = false;

  private void LateUpdate () {
    for ( int i = 0; i < contacts.Count; i++ ) {
      if ( contacts [i] == null ) {
        contacts.RemoveAt ( i );
        breached = contacts.Count != 0;
        changeLastFrame = true;
        i--;
      }
    }
  }

  public void AddContact ( GameObject contact ) {
    if ( tagMode == TagMode.Free || ( tagMode == TagMode.Exclude && contact.tag != targetTag ) || ( tagMode == TagMode.Include && contact.tag == targetTag ) ) {
      if ( !contacts.Contains ( contact ) ) {
        breached = true;
        contacts.Add ( contact );
        changeLastFrame = true;
      }
    }
  }

  public void RemoveContact ( GameObject contact ) {
    if ( contacts.Contains ( contact ) ) {
      contacts.Remove ( contact );
      breached = contacts.Count != 0;
      changeLastFrame = true;
    }
  }
}
