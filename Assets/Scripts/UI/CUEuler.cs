using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// CONTROL UNIT EULER
public class CUEuler : MonoBehaviour {
  public MANEnemy target;
  public Animator animator;

  public Slider s1, s2;
  private float offset = 0.3f;

  private float deltaStatus;

  public float acc, mv, drag;
  private float cVel, cPos;
  public Image i1, i2;
  private Material m1, m2;

  public float blendLS;// Lerp Speed
  private float deltaBlend;

  private void Start () {
    m1 = new Material ( i1.material );
    m2 = new Material ( i2.material );
    i1.material = m1;
    i2.material = m2;
    deltaBlend = 0;
  }

  void Update () {
    if ( target != null ) {
      animator.SetBool ( "feral", target.Feral () );
      bool isDraining = deltaStatus != target.Status ();
      animator.SetBool ( "drain", isDraining );
      //animator.SetBool ( "drain", true );

      s1.value = offset + ( 1 - offset ) * target.Status ();
      s2.value = s1.value;
      if ( isDraining ) {
        cVel += Mika.VClip ( acc, cVel, mv );
      } else {
        cVel -= cVel * drag * Time.deltaTime;
      }
      cPos += cVel * Time.deltaTime;

      i1.material.SetVector ( "_OFS", new Vector2 ( cPos, 2 ) );
      i2.material.SetVector ( "_OFS", new Vector2 ( cPos, 2 ) );

      deltaBlend = Mathf.Lerp ( deltaBlend, isDraining ? 1 : 0, blendLS * Time.deltaTime );
      i1.material.SetFloat ( "_BLEND", deltaBlend );
      i2.material.SetFloat ( "_BLEND", deltaBlend );

      deltaStatus = target.Status ();
    } else {
      animator.SetBool ( "dead", true );
    }
  }
}
