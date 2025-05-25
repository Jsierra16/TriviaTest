using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ScoreboardManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField nameInputField;
    public TMP_Text scoreboardText;

    [Header("Score Settings")]
    public TriviaButtonManager triviaManager; 

    private const int maxEntries = 10;
    private const string saveKey = "ScoreboardData";

    [System.Serializable]
    public class ScoreEntry
    {
        public string playerName;
        public int score;
    }

    [System.Serializable]
    public class Scoreboard
    {
        public List<ScoreEntry> entries = new List<ScoreEntry>();
    }

    private Scoreboard scoreboard = new Scoreboard();

    private void Start()
    {
        LoadScoreboard();
        DisplayScoreboard();
    }

    public void SaveScore()
    {
        string playerName = nameInputField.text.Trim();
        if (string.IsNullOrEmpty(playerName)) return;

        int currentScore = triviaManager.score; 
        Debug.Log("Saving score: " + currentScore + " for " + playerName);

        ScoreEntry newEntry = new ScoreEntry { playerName = playerName, score = currentScore };

        scoreboard.entries.Add(newEntry);
        scoreboard.entries = scoreboard.entries
            .OrderByDescending(e => e.score)
            .Take(maxEntries)
            .ToList();

        SaveToPrefs();
        DisplayScoreboard();

        nameInputField.text = ""; 
    }

    void SaveToPrefs()
    {
        string json = JsonUtility.ToJson(scoreboard);
        PlayerPrefs.SetString(saveKey, json);
        PlayerPrefs.Save();
    }

    void LoadScoreboard()
    {
        if (PlayerPrefs.HasKey(saveKey))
        {
            string json = PlayerPrefs.GetString(saveKey);
            scoreboard = JsonUtility.FromJson<Scoreboard>(json);
        }
        else
        {
            scoreboard = new Scoreboard();
        }
    }

    void DisplayScoreboard()
    {
        if (scoreboardText == null) return;

        scoreboardText.text = "<b>High Scores</b>\n";
        int rank = 1;
        foreach (var entry in scoreboard.entries)
        {
            scoreboardText.text += $"{rank}. {entry.playerName} - {entry.score}\n";
            rank++;
        }
    }

    public void ClearScoreboard()
    {
        PlayerPrefs.DeleteKey(saveKey);
        scoreboard = new Scoreboard();
        DisplayScoreboard();
    }
}
