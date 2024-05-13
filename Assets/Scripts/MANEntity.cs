using UnityEngine;

public class MANEntity : MonoBehaviour {
  protected Mika mika;

  virtual protected void Start () {
    mika = GetComponent<Mika>();
  }
}
