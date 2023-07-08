using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicAnimationScript : MonoBehaviour
{
    public float DURATION = 0.5f;
    public float HANG_TIME = 0.25f;

    private float timer = 0;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        timer += Time.deltaTime;

        if (timer > (DURATION + HANG_TIME))
        {
            finish();
            return;
        }

        if (timer > DURATION)
        {
            draw(1f);
            return;
        }

        draw(timer / DURATION);
    }

    protected virtual void finish()
    {
        Destroy(gameObject);
    }

    abstract protected void draw(float percentage);
}
