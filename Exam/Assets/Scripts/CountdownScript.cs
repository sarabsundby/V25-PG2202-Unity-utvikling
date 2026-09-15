using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountdownScript : MonoBehaviour
{
    public float timeRemaining = 60f;
    public TextMeshProUGUI timerText;
    private bool timerIsRunning = true;
    private RectTransform textRect;
    private bool hasMovedToCenter = false;

    void Start()
    {
        textRect = timerText.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                timerText.text = "Time's up!\n Score: " + ScoreManager.Instance.GetScore().ToString() + "\n\nReturning to main menu...";
                StartCoroutine(LoadSceneAfterDelay(5f));
                MoveTextToCenter();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void MoveTextToCenter()
    {
        if (hasMovedToCenter) return;

        textRect.anchorMin = new Vector2(0.5f, 0.5f);
        textRect.anchorMax = new Vector2(0.5f, 0.5f);
        textRect.pivot = new Vector2(0.5f, 0.5f);
        textRect.anchoredPosition = Vector2.zero;

        hasMovedToCenter = true;
    }
    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);

    }
}
