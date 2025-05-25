using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 3f;

    private void Start()
    {
        if (fadeImage != null)
            StartCoroutine(FadeInAndDisable());
    }

    private IEnumerator FadeInAndDisable()
    {
        Color color = fadeImage.color;
        float time = 0f;

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, 0f);

        fadeImage.gameObject.SetActive(false);
    }
}
