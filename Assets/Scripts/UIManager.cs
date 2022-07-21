using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager singleton;

    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;

    public Slider gameProgress;

    public GameObject startMenuPanel;
    public GameObject gamePlayPanel;
    public GameObject gameOverPanel;
    public GameObject levelCompletePanel;

    void Awake()
    {
        if (singleton != null && singleton != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            singleton = this;
        }
    }

    public void ShowGameplayPanel(bool isShow)
    {
        gamePlayPanel.SetActive(isShow);
    }

    public void ShowGameOverPanel(bool isShow)
    {
        gameOverPanel.SetActive(isShow);
    }

    public void ShowLevelCompletePanel(bool isShow)
    {
        levelCompletePanel.SetActive(isShow);
    }

    public void ShowStartMenuPanel(bool isShow)
    {
        startMenuPanel.SetActive(isShow);
    }
}
