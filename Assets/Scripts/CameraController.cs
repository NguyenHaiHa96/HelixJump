using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Vector3 currentPosition;
    private float offset;

    // Start is called before the first frame update
    void Awake()
    {
        offset = transform.position.y - target.position.y;
        currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isGameStarted)
            transform.position = currentPosition;
        else
        {
            currentPosition.y = target.position.y + offset;
            transform.position = currentPosition;
        }
    }
}
