using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextComponent
{
    [SerializeField]
    public string text = "";

    [SerializeField, Range(0.0f, 1.0f)]
    public float startFadeValue = 0.0f;

    [SerializeField, Range(0.0f, 1.0f)]
    public float endFadeValue = 1.0f;

    [SerializeField]
    public float fadeDuration = 1.0f;

    [SerializeField]
    public float pauseDurationBefore = 0.0f;

    [SerializeField]
    public float pauseDurationAfter = 0.0f;

    [SerializeField]
    public bool shouldGoToNext = true;

}

public class TextRenderer : MonoBehaviour
{
    [SerializeField]
    public TextComponent[] components;

    private int currentIndex = -1;

    private float totalTimer = 0.0f;
    private float timer = 0.0f;

    private TMPro.TextMeshPro textMesh;

    private void SetupComponent()
   {
        textMesh.text = components[currentIndex].text;
        textMesh.alpha = components[currentIndex].startFadeValue;
    }

    // Start is called before the first frame update
    void Awake()
    {
        textMesh = GetComponent<TMPro.TextMeshPro>();

        currentIndex = 0;
        SetupComponent();
    }

    private bool fading = false;
    private bool done = false;

    // Update is called once per frame
    void Update()
    {
        if (done) return;

        if (fading)
        {
            timer += Time.deltaTime;
            if (timer <= components[currentIndex].fadeDuration)
            {
                print(timer / components[currentIndex].fadeDuration);
                textMesh.alpha = Mathf.Lerp(components[currentIndex].startFadeValue, components[currentIndex].endFadeValue, timer/components[currentIndex].fadeDuration);
            }
            else
            {
                textMesh.alpha = components[currentIndex].endFadeValue;
            }
        }

        if (totalTimer >= components[currentIndex].pauseDurationBefore + components[currentIndex].fadeDuration + components[currentIndex].pauseDurationAfter)
        {
            if (components[currentIndex].shouldGoToNext)
            {
                currentIndex += 1;

                if (currentIndex >= components.Length)
                {
                    done = true;
                }
                else
                {
                    SetupComponent();
                    fading = false;
                    timer = 0.0f;
                    totalTimer = 0.0f;
                }
            } else
            {
                done = true;
            }
        }
        else if (totalTimer >= components[currentIndex].pauseDurationBefore + components[currentIndex].fadeDuration)
        {
            // print("Fade complete");
            fading = false;
        }
        else if (totalTimer >= components[currentIndex].pauseDurationBefore)
        {
            fading = true;
            // print("Start fade");
        }

        totalTimer += Time.deltaTime;
    }

    public void TutorialDone()
    {
        components[components.Length - 2].shouldGoToNext = true;
        done = false;
    }
}
