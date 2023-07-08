using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float secondsPerSprite;
    private int currentSprite;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0) {
            timer += secondsPerSprite;
            currentSprite++;
            if(currentSprite >= sprites.Length) {
                currentSprite = 0;
            }
            GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];
        }
    }
}
