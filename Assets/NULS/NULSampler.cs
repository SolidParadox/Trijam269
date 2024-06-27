using UnityEngine;

public class NULSampler : MonoBehaviour {
  public float lightLevel;
  public SpriteRenderer spriteRenderer;
  private void Update () {
    spriteRenderer.color = NULCore.Instance.GetLL ( transform.position );
  }
}
