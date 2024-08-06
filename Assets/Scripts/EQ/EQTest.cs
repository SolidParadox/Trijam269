using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EQTest : EQBase {
  public override void DuringFire () {
    gameObject.layer = LayerMask.NameToLayer ( "Display" );
  }

  public override void OnStopFire () {
    base.OnStopFire ();
    gameObject.layer = LayerMask.NameToLayer ( "Default" );
  }
}
