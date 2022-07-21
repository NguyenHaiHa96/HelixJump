using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixManager : MonoBehaviour
{
    public static List<GameObject> helixPlatforms = new List<GameObject>();
    public static HelixManager singleton;

    public int numberOfPlatforms;

    public GameObject helixPlatformPrefab;

    private Vector2 lastTapPosition;

    private float platformYSpawn = 0f;
    private float platformDistance = 6.2f;

    private int platformsToAdd = 3;

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

    void Update()
    {
        if (GameManager.isGameStarted || GameManager.levelCompleted)
            Invoke("RotateHelix", 1f);
    }

    public void RotateHelix()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTapPosition = Input.mousePosition;

            float delta = lastTapPosition.x - currentTapPosition.x;
            lastTapPosition = currentTapPosition;

            transform.Rotate(Vector3.up * delta);
        }
        if (Input.GetMouseButtonUp(0))
            lastTapPosition = Vector2.zero;
    }

    public int GetNumberOfPlatforms()
    {
        return numberOfPlatforms;
    }

    public void RemoveAllChildren()
    {
        if (GameManager.isContinued)
        {
            foreach (GameObject platform in helixPlatforms)
            {
                Destroy(platform);
            }
        }         
    }

    public void GenerateHelix()
    {
        platformYSpawn = 0f;
        numberOfPlatforms = PlayerPrefs.GetInt("CurrentLevel") + platformsToAdd;
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            if (i == 0)
                HelixPlatform.isFirstPlatform = true;
            else if (i == numberOfPlatforms - 1)
                HelixPlatform.isLastPlatform = true;         
            var newPlatform = Instantiate(helixPlatformPrefab, Vector3.up * platformYSpawn, Quaternion.identity, transform);     
            platformYSpawn -= platformDistance;
            helixPlatforms.Add(newPlatform);
        }
    }

    public void RestartRotation()
    {
        transform.eulerAngles = Vector3.zero;
    }
}

