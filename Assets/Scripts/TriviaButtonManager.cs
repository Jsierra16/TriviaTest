using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriviaButtonManager : MonoBehaviour
{
    public static TriviaButtonManager Instance; 
    public int score = 0;
    public TMP_Text scoreText;

    public AudioSource correctSound;
    public AudioSource incorrectSound;

    private void Awake()
    {
        Instance = this; 
    }

    public void OnCorrectAnswer()
    {
        score += 1;
        UpdateScore();

        if (correctSound != null)
            correctSound.Play();
    }

    public void OnIncorrectAnswer()
    {
        if (incorrectSound != null)
            incorrectSound.Play();
    }

    void UpdateScore()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}
