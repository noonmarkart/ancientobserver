using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteFlashController : MonoBehaviour
{
    private bool isTransitioning = false;
    private Color color;
    private float timer = 0f;
    public float whiteDuration = 1f;
    public float fadeDuration = 1f;
    public Image image;

    private void Start()
    {
        color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        image.color = new Color(color.r, color.g, color.b, 0f);
    }

    private void Update()
    {
        if (isTransitioning)
        {
            if (timer <= whiteDuration) {
                image.color = color;
            } else if (timer <= whiteDuration + fadeDuration) {
                image.color = new Color(color.r, color.g, color.b, (fadeDuration - (timer - whiteDuration)) / fadeDuration);
            } else {
                image.color = new Color(color.r, color.g, color.b, 0f);
                isTransitioning = false;
            }
            timer += Time.deltaTime;
        }
    }

    public void ExecuteTransition()
    {
        timer = 0f;
        isTransitioning = true;
    }
}
