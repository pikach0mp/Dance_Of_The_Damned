﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeController : MonoBehaviour
{
    static CameraShakeController instance;

    bool should_shake = false;
    float magnitude = 1.0f;

    float timestamp_next_stable = 0.0f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void DoShake()
    {
        if (!should_shake)
            return;

        if (Time.time > timestamp_next_stable)
        {
            should_shake = false;
            transform.localPosition = Vector3.zero;
            return;
        }

        transform.localPosition = UnityEngine.Random.insideUnitSphere * magnitude;
    }

    public static void RequestScreenShake(float magnitude, float duration)
    {
        instance.timestamp_next_stable = Time.time + duration;
        instance.should_shake = true;
        instance.magnitude = magnitude;
    }
}

