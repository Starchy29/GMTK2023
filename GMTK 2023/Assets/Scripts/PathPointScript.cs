using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPointScript : MonoBehaviour
{
    public PathPointScript prevPoint = null;
    public PathPointScript nextPoint = null;
    public string name = "";

    // Start is called before the first frame update
    void Start()
    {
        if (prevPoint == null)
        {
            PathManager.addPath(name, this);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (prevPoint != null)
        {
            Vector2 midpoint = (prevPoint.transform.position + transform.position) / 2;
            Gizmos.DrawLine(midpoint, transform.position);
        }

        if (nextPoint != null)
        {
            Vector2 midpoint = (nextPoint.transform.position + transform.position) / 2;
            Gizmos.DrawLine(transform.position, midpoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
