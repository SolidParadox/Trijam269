using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDimmer : MonoBehaviour
{
    public SpriteRenderer[] sprites;
    public LightSensor lightSensor;
    private Color[] baseColor, targetColor;
    private void Start()
    {
        baseColor = new Color[ sprites.Length];
        targetColor = new Color[ sprites.Length ];
        for (int i = 0; i < sprites.Length; i++)
        {
            baseColor[i] = sprites[i].color;
            targetColor[i] = sprites[i].color;
            targetColor[i].a = 0;
        }

    }

    void Update()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = Color.Lerp( baseColor [ i ], targetColor [ i ], 1 - lightSensor.GetRelativeBrightness() );
        }
    }
}
