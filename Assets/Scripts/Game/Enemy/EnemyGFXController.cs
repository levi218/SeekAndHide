using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGFXController : MonoBehaviour
{
    LineRenderer lineRenderer;

    Vector2 lookingDirection;

    Enemy baseScript;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        baseScript = GetComponentInParent<Enemy>();
        InitVisionRenderer();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        DrawFieldOfVision();
    }

    void InitVisionRenderer()
    {
        lineRenderer.positionCount = 3;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        Color visionColor = lineRenderer.material.color;
        visionColor.a = 0.1f;
        lineRenderer.startColor = visionColor;
        lineRenderer.endColor = visionColor;
    }
    void DrawFieldOfVision()
    {
        Vector3[] points = new Vector3[3];

        lookingDirection = Vector2.Lerp(lookingDirection, baseScript.direction, 0.05f);
        points[0] = transform.position + Quaternion.AngleAxis(baseScript.visionAngle, Vector3.forward) * lookingDirection * baseScript.visionSize;
        points[1] = transform.position;
        points[2] = transform.position + Quaternion.AngleAxis(360 - baseScript.visionAngle, Vector3.forward) * lookingDirection * baseScript.visionSize;
        lineRenderer.SetPositions(points);
    }
}
