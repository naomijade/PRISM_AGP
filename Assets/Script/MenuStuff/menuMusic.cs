﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuMusic : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
}