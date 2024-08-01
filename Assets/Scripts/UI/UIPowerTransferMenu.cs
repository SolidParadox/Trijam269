using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPowerTransferMenu : MenuCore {
  public TMP_Text label1, label2;
  public Slider slider;

  public float ptButtonAmount;

  public float basePower;

  public void ButtonIC ( int direction ) {
    // Change current power tranfer level
    slider.value += ptButtonAmount * direction;
  }

  public override void Incoming ( bool a ) {
    base.Incoming ( a );
    if ( !status ) {
      basePower = slider.value * basePower / 100.0f;
    }
  }

  public override void Update () {
    base.Update ();
    if ( status ) {
      label1.text = ( slider.value * basePower / 100.0f ).ToString ( "F2" );
      label2.text = basePower.ToString ( "F2" );
      // Focus the slider so it can be moved with left and right
    }

    if ( Input.GetKey ( KeyCode.R ) ) {
      Backflow ( true );
    }
    
    if ( Input.GetKey ( KeyCode.Space ) || Input.GetKey ( KeyCode.Return ) ) {
      Backflow ( false );
    }
  }
}
