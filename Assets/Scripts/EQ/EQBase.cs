using UnityEngine;

public class EQBase : MonoBehaviour {
  public  bool  continuousFire;
  public  float fireCooldown;

  private bool  searCatch, fireStatus;
  private float deltaTime;

  private void Update () {
    deltaTime -= Time.deltaTime;
    if ( Input.GetAxis ( "Fire1" ) > 0 ) {
      if ( searCatch && deltaTime <= 0 ) {
        if ( !fireStatus ) {
          OnStartFire();
        }
        DuringFire();
        if ( fireStatus ) {
          deltaTime = fireCooldown;
          searCatch = continuousFire;
        }
      }
    } else {
      searCatch = true;
      if ( fireStatus ) {
        OnStopFire();
      }
    }
  }

  public virtual void OnStartFire () {
    fireStatus = true;
  }

  public virtual void DuringFire () {

  }

  public virtual void OnStopFire () {
    fireStatus = false;
  }
}
