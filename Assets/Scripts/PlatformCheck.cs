using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCheck : MonoBehaviour
{
    public static event Action IsPassed = delegate { };

    private void OnTriggerEnter(Collider other)
    {
        IsPassed();
    }
}
