using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPulseScript : BasicAnimationScript
{
    
    private float endRadius = 0;

    public void setEndRadius(float radius)
    {
        endRadius = radius;
    }

    protected override void draw(float percentage)
    {
        Vector3 scale = new Vector3(endRadius, endRadius, 0);
        scale *= percentage;
        transform.localScale = scale;
    }
}
