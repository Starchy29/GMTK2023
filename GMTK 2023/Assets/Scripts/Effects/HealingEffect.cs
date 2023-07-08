using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingEffect : BasicAnimationScript
{
    public float FINAL_SIZE = 0.5f;
    public float MAX_DISTANCE = 2f;
    public int INITAL_COUNT = 10;

    private int remainingCount = 0;
    private bool spawnedChild = false;

    private void Awake()
    {
        remainingCount = INITAL_COUNT;
    }

    protected override void Start()
    {
        base.Start();
        float randomX = Random.Range(-MAX_DISTANCE, MAX_DISTANCE);
        float randomY = Random.Range(-MAX_DISTANCE, MAX_DISTANCE);
        transform.position = new Vector3(randomX + transform.parent.position.x, randomY + transform.parent.position.y, transform.position.z);
    }

    protected override void Update()
    {
        base.Update();
        transform.rotation = Quaternion.identity;
    }

    protected override void draw(float percentage)
    {
        Vector3 scale = new Vector3(FINAL_SIZE, FINAL_SIZE, 0);
        scale *= percentage;
        transform.localScale = scale;

        if (!spawnedChild && (remainingCount > 0) && (percentage >= 0.5f))
        {
            HealingEffect next = Instantiate(EnemyManager.HealingEffectObject).GetComponent<HealingEffect>();
            next.setCount(remainingCount - 1);
            next.transform.parent = transform.parent;
            spawnedChild = true;
        }
    }

    public void setCount(int count)
    {
        remainingCount = count;
    }

    protected override void finish()
    {

        base.finish();
    }
}
