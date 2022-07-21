using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class GameManager : MonoBehaviour
{
    //public static GameManager singleton;

    public static bool isGameOver;
    public static bool levelCompleted;
    public static bool isGameStarted;
    public static bool isContinued;
    public static bool isRestarted;
    public static bool muted = false;

    public static int currentLevel;  
    public static int currentScore;
    public static int  platformsPassed;  

    // Start is called before the first frame update
    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        
        //if (singleton != null && singleton != this)
        //{
        //    Destroy(this.gameObject);
        //}
        //else
        //{
        //    singleton = this;
        //}
    }
    private void Start()
    {
        PlatformCheck.IsPassed += ScoreCheck_IsPassed;
        StartTask();   
    }

    private void StartTask()
    {
        isGameStarted = false;
        isGameOver = false;
        isRestarted = false;
        currentScore = 0;
        platformsPassed = 0;
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);

        HelixManager.singleton.GenerateHelix();
    }

    private void OnDestroy()
    {
        PlatformCheck.IsPassed -= ScoreCheck_IsPassed;
    }

    private void ScoreCheck_IsPassed()
    {
        currentScore += 3;
        platformsPassed++;
    }

    public void StartGame()
    {
        if (Input.GetMouseButton(0) && !isGameStarted)
        {
            isGameStarted = true;
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            else
            {
                UIManager.singleton.ShowStartMenuPanel(false);
                UIManager.singleton.ShowGameplayPanel(true);               
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isGameStarted)
        {
            isGameStarted = true;
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;
            else
            {
                UIManager.singleton.ShowStartMenuPanel(false);
                UIManager.singleton.ShowGameplayPanel(true);
            }
        }
    }

    public void GameState()
    {
        if (currentScore > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
        }

        if (isGameOver)
        {
            currentScore = 0;
            //Time.timeScale = 0;
            UIManager.singleton.ShowGameOverPanel(true);

            if (Input.GetMouseButton(0))
            {
                isRestarted = true;
                if (isRestarted)
                    Invoke ("RestartGame", 1f);          
            }           

            //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            //    UIManager.singleton.ShowGameOverPanel(false);
        }
        else if (levelCompleted)
        {
            UIManager.singleton.ShowLevelCompletePanel(true);
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                SceneManager.LoadScene("Level");

            if (Input.GetMouseButton(0))
            {
                isContinued = true;
                if (isContinued)
                    Invoke("NextLevel", 1f);
            }     
        }
    }

    public void RestartGame()
    {                 
        isGameOver = false;
        currentScore = 0;
        platformsPassed = 0;
        UIManager.singleton.ShowGameOverPanel(false);
        Player.singleton.RestartPosition();
        HelixManager.singleton.RestartRotation();              
    }

    private void NextLevel()
    {
        if (isContinued)
        {
            Player.singleton.RestartPosition();
            PlayerPrefs.SetInt("CurrentLevel", currentLevel + 1);         
            UIManager.singleton.ShowLevelCompletePanel(false);
            HelixManager.singleton.RemoveAllChildren();
            HelixManager.singleton.GenerateHelix();

            isContinued = false; 
            levelCompleted = false;
            currentScore = 0;
            platformsPassed = 0;
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        } 
    }

    public void GameProgress()
    {
        UIManager.singleton.currentScoreText.text = currentScore.ToString();
        UIManager.singleton.bestScoreText.text = "BEST: " + PlayerPrefs.GetInt("BestScore", 0).ToString();

        UIManager.singleton.currentLevelText.text = currentLevel.ToString();
        UIManager.singleton.nextLevelText.text = (PlayerPrefs.GetInt("CurrentLevel") + 1).ToString();

        int totalPlatforms = HelixManager.singleton.numberOfPlatforms - 1;
        int progress = (platformsPassed * 100) / totalPlatforms;
        
        UIManager.singleton.gameProgress.value = progress;
    }

    void Update()
    {
        StartGame();
        GameProgress();
        GameState();
    }
}
