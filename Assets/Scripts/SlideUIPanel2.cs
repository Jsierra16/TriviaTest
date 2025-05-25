using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideUIPanel2 : MonoBehaviour
{
    public RectTransform panel;         
    public float distance = 300f;         
    public float duration = 0.5f;        
    public bool lockDuringSlide = true;   

    private bool isSliding = false;

    public void SlideUp()
    {
        if (lockDuringSlide && isSliding) return;

        Vector2 target = panel.anchoredPosition + Vector2.up * distance;
        StartCoroutine(SlideTo(target));
    }

    public void SlideDown()
    {
        if (lockDuringSlide && isSliding) return;

        Vector2 target = panel.anchoredPosition + Vector2.down * distance;
        StartCoroutine(SlideTo(target));
    }

    public void SlideLeft()
    {
        if (lockDuringSlide && isSliding) return;

        Vector2 target = panel.anchoredPosition + Vector2.left * distance;
        StartCoroutine(SlideTo(target));
    }

    public void SlideRight()
    {
        if (lockDuringSlide && isSliding) return;

        Vector2 target = panel.anchoredPosition + Vector2.right * distance;
        StartCoroutine(SlideTo(target));
    }

    private IEnumerator SlideTo(Vector2 target)
    {
        isSliding = true;
        Vector2 start = panel.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            panel.anchoredPosition = Vector2.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        panel.anchoredPosition = target;
        isSliding = false;
    }
}
