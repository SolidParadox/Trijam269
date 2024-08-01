using UnityEngine;

public class EQBase : MonoBehaviour {
  private Rigidbody2D rgb;

  public  bool  continuousFire;
  public  float fireCooldown;

  private bool  searCatch, fireStatus;
  private float deltaTime;

  private Transform originalParent;

  protected virtual void Start() {
    rgb = GetComponent<Rigidbody2D>();
    originalParent = transform.parent;
    Detach ();
  }

  private void Update () {
    deltaTime -= Time.deltaTime;
    if ( Input.GetAxis ( "Fire1" ) > 0 ) {
      if ( searCatch && deltaTime <= 0 ) {
        if ( !fireStatus ) {
          OnStartFire ();
        }
        DuringFire ();
        if ( fireStatus ) {
          deltaTime = fireCooldown;
          searCatch = continuousFire;
        }
      }
    } else {
      searCatch = true;
      if ( fireStatus ) {
        OnStopFire ();
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

  public virtual void Attach ( Transform alpha ) {
    enabled = true;
    rgb.simulated = false;
    
    transform.SetParent ( alpha );

    transform.localPosition = Vector3.zero;
    transform.localRotation = Quaternion.identity;
    // Make some animator calls, who knows
  }

  public virtual void Detach () {
    enabled = false;

    transform.SetParent ( originalParent );

    rgb.velocity = transform.up * 10;
    rgb.angularVelocity = 15;
    rgb.simulated = true;
  }
}
