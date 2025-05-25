using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownPanelSlider : MonoBehaviour
{
    [Header("Panel Movement")]
    public RectTransform panel;
    public Vector2 offsetSlide = new Vector2(-500f, 0f); // Slide distance in pixels
    public float slideDuration = 0.5f;
    public AudioSource slideSound;

    [Header("Countdown Settings")]
    public float countdownTime = 90f;
    public TMP_Text countdownText;

    [Header("Audio Swap at 13s")]
    public AudioSource audioToStopAt13;
    public AudioSource audioToPlayAt13;

    private bool timerStarted = false;
    private bool hasSwitchedAudio = false;
    private bool hasSlid = false;
    private Coroutine countdownCoroutine = null;

    private void Start()
    {
        if (countdownText != null)
            countdownText.gameObject.SetActive(false);
    }

    public void StartCountdown()
    {
        if (timerStarted) return;
        timerStarted = true;

        if (countdownText != null)
            countdownText.gameObject.SetActive(true);

        countdownCoroutine = StartCoroutine(CountdownAndSlide());
    }

    private IEnumerator CountdownAndSlide()
    {
        float timeLeft = countdownTime;

        while (timeLeft > 0f)
        {
            if (!hasSwitchedAudio && timeLeft <= 13f)
            {
                hasSwitchedAudio = true;
                if (audioToStopAt13 != null && audioToStopAt13.isPlaying)
                    audioToStopAt13.Stop();
                if (audioToPlayAt13 != null)
                    audioToPlayAt13.Play();
            }

            if (countdownText != null)
            {
                int m = Mathf.FloorToInt(timeLeft / 60f);
                int s = Mathf.FloorToInt(timeLeft % 60f);
                countdownText.text = $"{m:00}:{s:00}";
            }

            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }

        if (countdownText != null)
        {
            countdownText.text = "00:00";
            yield return new WaitForSeconds(0.5f);
            countdownText.gameObject.SetActive(false);
        }

        if (!hasSlid)
        {
            hasSlid = true;
            if (slideSound != null)
                slideSound.Play();

            yield return StartCoroutine(SlideAnchored());
        }
    }

    private IEnumerator SlideAnchored()
    {
        Vector2 startPos = panel.anchoredPosition;
        Vector2 endPos = startPos + offsetSlide;

        float elapsed = 0f;
        while (elapsed < slideDuration)
        {
            panel.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsed / slideDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        panel.anchoredPosition = endPos;
    }

    public void OnClickHideAndStopTimer()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }

        if (countdownText != null)
            countdownText.gameObject.SetActive(false);

        timerStarted = false;
    }
}
