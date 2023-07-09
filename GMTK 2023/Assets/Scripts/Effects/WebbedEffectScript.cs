using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebbedEffectScript : BasicAnimationScript
{
    public float FINAL_SIZE = 0.5f;
    public float DISTANCE = 0.5f;
    public int COUNT = 3;

    private int currentCount = 0;
    private bool propogated = false;

    private float fullDuration = 0;
    private Vector2 centerPosition;

    private void Awake()
    {
        currentCount = COUNT;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void draw(float percentage)
    {
        Vector3 scale = new Vector3(FINAL_SIZE, FINAL_SIZE, 0);
        scale *= percentage;
        scale.z = 1;
        transform.localScale = scale;

        if (!propogated && (currentCount > 0) && (percentage >= 0.5f))
        {
            propogated = true;

            WebbedEffectScript webbedEffect = Instantiate(TowerManager.WebbedEffectObject).GetComponent<WebbedEffectScript>();
            webbedEffect.setCenter(centerPosition);
            webbedEffect.setEffectDuration(fullDuration);
            webbedEffect.setCount(currentCount - 1);
        }   
    }

    public void setCount(int count)
    {
        currentCount = count;
    }

    public void setEffectDuration(float duration)
    {
        fullDuration = duration;
        HANG_TIME = duration - DURATION;
    }

    public void setCenter(Vector3 position)
    {
        centerPosition = position;
        float randomAngle = Random.Range(0, 2 * Mathf.PI);
        Vector2 offset = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        offset *= DISTANCE;
        transform.position = new Vector3(centerPosition.x + offset.x, centerPosition.y + offset.y, transform.position.z);
    }
}
