using System.Collections;
using UnityEngine;

public class SlideUIPanel : MonoBehaviour
{
    public RectTransform panel;
    public float distance = 300f;
    public float duration = 0.5f;
    public AudioSource clickSound;
    public bool lockDuringSlide = true;

    [Header("Optional Audio Control")]
    public AudioSource audioToStop;
    public AudioSource audioToPlayAfterSlide;

    private bool isSliding = false;

    public void SlideUp() => TrySlide(Vector2.up);
    public void SlideDown() => TrySlide(Vector2.down);
    public void SlideLeft() => TrySlide(Vector2.left);
    public void SlideRight() => TrySlide(Vector2.right);

    private void TrySlide(Vector2 direction)
    {
        if (lockDuringSlide && isSliding) return;

        if (clickSound != null)
            clickSound.Play();

        if (audioToStop != null && audioToStop.isPlaying)
            audioToStop.Stop();

        Vector2 target = panel.anchoredPosition + direction * distance;
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

        if (audioToPlayAfterSlide != null)
            audioToPlayAfterSlide.Play();

        isSliding = false;
    }
}
