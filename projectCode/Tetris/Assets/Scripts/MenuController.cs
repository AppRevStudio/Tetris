using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] frames;
    [SerializeField]
    private float frameDelay = 0.1f;
    [SerializeField]
    private float repeatDelay = 2f;
    private float nextFrameTime;
    private float nextCycleTime;
    private int frameIndex = 0;

    [SerializeField]
    private Image titleImage;

    [SerializeField]
    private GameObject homePanel;
    [SerializeField]
    private GameObject statsPanel;

    [SerializeField]
    private TMPro.TMP_Text highScoreText;
    [SerializeField]
    private TMPro.TMP_Text highRowText;
    [SerializeField]
    private TMPro.TMP_Text topTimeText;
    [SerializeField]
    private TMPro.TMP_Text timeSpentText;

    int seconds;
    int minutes;
    int hours;


    // Start is called before the first frame update
    void Start()
    {
        nextFrameTime = Time.time + frameDelay;
        nextCycleTime = Time.time;

        homePanel.SetActive(true);
        statsPanel.SetActive(false);

        SetupStats();
    }

    // Update is called once per frame
    void Update()
    {
        RenderTitleGif();
    }

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void EnableStatsPanel()
    {
        homePanel.SetActive(false);
        statsPanel.SetActive(true);
    }

    public void DisableStatsPanel()
    {
        statsPanel.SetActive(false);
        homePanel.SetActive(true);
    }

    void SetupStats()
    {
        if (!PlayerPrefs.HasKey("TetrisHighScore"))
        {
            PlayerPrefs.SetInt("TetrisHighScore", 0);
        }
        if (!PlayerPrefs.HasKey("TetrisHighRow"))
        {
            PlayerPrefs.SetInt("TetrisHighRow", 0);
        }
        if (!PlayerPrefs.HasKey("TetrisTopTime"))
        {
            PlayerPrefs.SetInt("TetrisTopTime", 0);
        }
        if (!PlayerPrefs.HasKey("TetrisTimeSpent"))
        {
            PlayerPrefs.SetInt("TetrisTimeSpent", 0);
        }

        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("TetrisHighScore");
        highRowText.text = "Rows Cleared: " + PlayerPrefs.GetInt("TetrisHighRow");

        minutes = (PlayerPrefs.GetInt("TetrisTimeSpent") / 60);
        hours = minutes / 60;
        minutes = (minutes % 60);

        string minuteCounter;

        if (minutes < 10)
        {
            minuteCounter = "0" + minutes;
        }
        else
        {
            minuteCounter = minutes.ToString();
        }

        timeSpentText.text = "Time Spent: " + hours + ":" + minuteCounter;

        minutes = (PlayerPrefs.GetInt("TetrisTopTime") / 60);
        seconds = (PlayerPrefs.GetInt("TetrisTopTime") % 60);

        string secondCounter;

        if (seconds < 10)
        {
            secondCounter = "0" + seconds;
        }
        else
        {
            secondCounter = seconds.ToString();
        }

        topTimeText.text = "Longest Game: " + minutes + ":" + secondCounter;
    }

    // Since Unity does not support gifs, I created this method that loops through all of the sprite frames, then waits for a delay before repeating
    private void RenderTitleGif()
    {
        if (Time.time < nextCycleTime)
        {
            return;
        }

        if (Time.time >= nextFrameTime)
        {
            frameIndex++;
            if (frameIndex >= frames.Length)
            {
                frameIndex = 0;
                nextCycleTime = Time.time + repeatDelay;
            }
            else
            {
                titleImage.sprite = frames[frameIndex];
                nextFrameTime = Time.time + frameDelay;
            }
        }
    }
}
