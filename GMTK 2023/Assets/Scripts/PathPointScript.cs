using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPointScript : MonoBehaviour
{
    public PathPointScript nextPoint = null;
    public Color color = Color.white;

    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        if (nextPoint != null)
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;

            lineRenderer.startWidth = transform.localScale.x;
            lineRenderer.endWidth = transform.localScale.y;

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, nextPoint.transform.position);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (nextPoint != null)
        {
            Gizmos.DrawLine(transform.position, nextPoint.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
