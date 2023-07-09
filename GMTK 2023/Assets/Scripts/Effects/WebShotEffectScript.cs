using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebShotEffectScript : BasicAnimationScript
{
    private Vector2 start;
    private Vector2 end;

    protected override void draw(float percentage)
    {
        float xPos = ((end.x - start.x) * percentage) + start.x;
        float yPos = ((end.y - start.y) * percentage) + start.y;
        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }

    public void setTravel(Vector2 start, Vector2 end)
    {
        this.start = start;
        this.end = end;
    }
}
