using UnityEditor;
using UnityEngine;

[CustomEditor ( typeof ( SUPTraceHexLayer ) )]
public class SUPTRHLayer : Editor {
  public override void OnInspectorGUI () {
    DrawDefaultInspector ();

    SUPTraceHexLayer myComponent = (SUPTraceHexLayer)target;
    if ( GUILayout.Button ( "PLACE TRACES" ) ) {
      myComponent.Place ();
    }
  }
}
