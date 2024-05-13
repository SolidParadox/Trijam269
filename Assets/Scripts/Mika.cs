using Unity.VisualScripting;
using UnityEngine;

public class Mika : MonoBehaviour {
  public  Rigidbody2D     rgb;

  public  float           acc;
  public  float           mxv;
    private float umv;

  public  float           strengthDOS;      // DRAG for OVERSPEED
  public  float           strengthD;

  protected Vector2       delta;
  protected Vector2       backupDelta;
  protected int               ticks;

  protected bool              vLock;

  public static float VClip ( float a, float b, float m ) {
    if ( Mathf.Abs ( b + a ) >= m ) {
      if ( Mathf.Sign ( a ) != Mathf.Sign ( b ) ) { return Mathf.Sign ( a ) * Mathf.Min ( m, Mathf.Abs ( a ) ); }
      if ( Mathf.Abs ( b ) > m ) { b = Mathf.Sign ( b ) * m; }
      return Mathf.Sign ( a ) * ( m - Mathf.Abs ( b ) );
    }
    return a;
  }

  protected Vector2 Cutter ( Vector2 a, Vector2 b, float m ) {
    if ( a.magnitude < 0.0001f ) return Vector2.zero;
    if ( ( a + b ).magnitude >= m ) {
      if ( Vector2.Dot ( a, b ) < 0 ) { return a.normalized * Mathf.Min ( a.magnitude, m ); }
      if ( b.magnitude > m ) { b = b.normalized * m; }
      return ( b + a ).normalized * m - b;
    }
    return a;
  }

  virtual public void AddThrusterOutput ( Vector2 a ) {
    delta += a;
  }

  virtual public void SetThrusterOutput ( Vector2 a ) {
    backupDelta = a;
    vLock = true;
  }

  virtual public void Start () {
    if ( rgb == null ) {
      rgb = GetComponent<Rigidbody2D> ();
    }
  }

  virtual public void Update () {
    ticks++;
  }

  virtual public void FixedUpdate () {
    if ( ticks == 0 || vLock ) { delta = backupDelta; } else { delta /= ticks; backupDelta = delta; }
    vLock = false;

    if ( delta.sqrMagnitude > 1 ) {
      delta.Normalize ();
    }

    umv = Mathf.Max(0, Mathf.Abs(delta.magnitude)) * mxv;

    Vector2 deltaSpeed = Vector2.zero;

    if ( delta.sqrMagnitude > 0.05f || rgb.velocity.sqrMagnitude < 0.05f ) {
      deltaSpeed = Vector3.Project ( rgb.velocity, rgb.transform.up );
    }

    rgb.velocity -= rgb.velocity * strengthD * Time.fixedDeltaTime;
    if ( rgb.velocity.magnitude > umv ) {
      rgb.velocity -= ( rgb.velocity - rgb.velocity.normalized * umv ) * strengthDOS * Time.fixedDeltaTime;
    }

    Vector2 deltaProcessed = Cutter( delta.normalized * acc * Time.fixedDeltaTime, rgb.velocity, umv * delta.magnitude );
    
    rgb.AddForce ( deltaProcessed * rgb.mass / Time.fixedDeltaTime );

    ResetDeltas ();
  }

  protected virtual void ResetDeltas () {
    delta = Vector2.zero;
    ticks = 0;
  }

  protected virtual void OnEnable () {
    ResetDeltas ();
  }
}
