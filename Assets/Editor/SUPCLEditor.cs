using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor ( typeof ( SUPCableLayer ) )]
public class SUPCLEditor : Editor {
  public override void OnInspectorGUI () {
    DrawDefaultInspector ();

    SUPCableLayer myComponent = (SUPCableLayer)target;
    if ( GUILayout.Button ( "LAY CABLE" ) ) {
      myComponent.Create ();
    }
  }
}
