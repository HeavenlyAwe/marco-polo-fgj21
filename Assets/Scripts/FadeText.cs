using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FadeText : MonoBehaviour
{
    public float waitStartDuration = 2.0f;
    public float fadeInDuration = 10.0f;
    public float fadeOutDuration = 10.0f;
    public float waitDuration = 3.0f;

    private SpriteRenderer spriteRenderer;

    private bool fadeInDone = false;

    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= waitStartDuration)
        {
            if (fadeInDone && fadeOutDuration < 0) return;

            float targetValue = fadeInDone ? 0.0f : 1.0f;
            float duration = fadeInDone ? fadeOutDuration : fadeInDuration;

            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(color.a, targetValue, timer / duration);
            spriteRenderer.color = color;

            if (color.a >= 0.99f && fadeInDone == false)
            {
                Debug.Log(color.a);

                if (timer >= waitDuration + waitStartDuration)
                {
                    fadeInDone = true;
                    timer = 0.0f;
                }
            }
        }

        timer += Time.deltaTime;
    }
}
