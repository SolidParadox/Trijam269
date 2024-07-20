using UnityEngine;

public class NULSamplerBulb : MonoBehaviour {
  public NULSampler sampler;
  public SpriteRenderer spriteRenderer;

  public Color a, b;

  // Update is called once per frame
  void Update () {
    spriteRenderer.color = sampler.inLight ? a : b;
  }
}
