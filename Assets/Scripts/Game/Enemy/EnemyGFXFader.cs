using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGFXFader : MonoBehaviour
{
    LineRenderer visionRenderer;
    SpriteRenderer modelRenderer;

    public float fadeSpeed = 2f;
    public float topLimit = 0.7f;
    private void Awake()
    {
        visionRenderer = GetComponent<LineRenderer>();
        modelRenderer = GetComponent<SpriteRenderer>();

    }
    void FixedUpdate()
    {
        float alpha = (Mathf.Sin(Time.time * fadeSpeed) + 1) * 0.5f * topLimit; 
        Color modelColor = modelRenderer.color;
        modelColor.a = alpha;
        modelRenderer.color = modelColor;

        Color visionColor = visionRenderer.material.color;
        visionColor.a = alpha;
        visionRenderer.startColor = visionColor;
        visionRenderer.endColor = visionColor;


    }
}
