using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixPlatform : MonoBehaviour
{
    public static bool isFirstPlatform = false;
    public static bool isLastPlatform = false;

    public List<Transform> platformParts;

    public List<int> assignedIndex = new List<int>(); 

    public Material unsafePart;
    public Material safePart;

    public int unsafeParts;
    public int deactivateParts;
    public int safeParts;
    public int numberOfParts;

    private void OnEnable()
    {
        GeneratePlatform();
    }

    public void GetAllParts()
    {
        foreach (Transform child in transform)
        {
            platformParts.Add(child);
        }
        platformParts.RemoveAt(platformParts.Count - 1);
    }

    public void GeneratePlatform()
    {
        GetAllParts();

        numberOfParts = platformParts.Count; 
        if (isFirstPlatform)
        {       
            unsafeParts = 0;
            deactivateParts = Random.Range(3, 6);
            //isFirstPlatform = false;
        }
        else if (isLastPlatform)
        {
            unsafeParts = 0;
            deactivateParts = 0;
        }
        else
        {
            unsafeParts = Random.Range(2, 4);
            deactivateParts = Random.Range(3, 6);
        }    
        safeParts = numberOfParts - unsafeParts - deactivateParts;

        RandomUnsafeParts(unsafeParts);
        RandomDeactivateParts(deactivateParts);
        RandomSafeParts(safeParts);
    }

    public int RandomIndexInList(List<int> list)
    {     
        bool alreadyExist = true;
        int randomIndex = 0;
        
        do
        {
            randomIndex = Random.Range(0, numberOfParts);
            alreadyExist = list.Contains(randomIndex);
            if (!alreadyExist)
            {
                list.Add(randomIndex);
            }
        }
        while (alreadyExist);
        return randomIndex;
    }

    private void RandomUnsafeParts(int unsafeParts)
    {
        for (int i = 0; i < unsafeParts; i++)
        {
            int randomIndex = RandomIndexInList(assignedIndex);           
            GameObject platformPart = platformParts[randomIndex].gameObject;
            platformPart.GetComponent<Renderer>().material = unsafePart;
            platformPart.tag = "UnsafePart";
        }              
    }

    private void RandomDeactivateParts(int deactivateParts)
    {
        if (isFirstPlatform)
        {
            for (int i = 0; i < deactivateParts; i++)
            {
                int randomIndex = RandomIndexInList(assignedIndex);
                if (randomIndex != 0 && randomIndex != 1)
                {
                    GameObject platformPart = platformParts[randomIndex].gameObject;
                    platformPart.SetActive(false);
                    platformPart.tag = "DeactivatePart";
                }
            }
            isFirstPlatform = false;
        }
        else
        {
            for (int i = 0; i < deactivateParts; i++)
            {
                int randomIndex = RandomIndexInList(assignedIndex);
                    GameObject platformPart = platformParts[randomIndex].gameObject;
                    platformPart.SetActive(false);
                    platformPart.tag = "DeactivatePart";
            }
        }
    }    

    private void RandomSafeParts(int safeParts)
    {
        if (isLastPlatform)
        {
            for (int i = 0; i < safeParts; i++)
            {
                int randomIndex = RandomIndexInList(assignedIndex);
                GameObject platformPart = platformParts[randomIndex].gameObject;
                platformPart.tag = "LastPlatform";
            }
            isLastPlatform = false;
        }
        else
        {
            for(int i = 0; i < safeParts; i++)
        {
                int randomIndex = RandomIndexInList(assignedIndex);
                GameObject platformPart = platformParts[randomIndex].gameObject;    
                platformPart.tag = "SafePart";
            }
        }
    }
}
