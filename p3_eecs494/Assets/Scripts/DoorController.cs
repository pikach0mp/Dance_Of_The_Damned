﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject StopLevel;
    private bool isLerp;
    Vector3 target_pos;
    public bool startsOpen = false;
    const float minHeight = 1.25f;
    const float maxHeight = 4f;

    private AudioSource audioS;
    public bool IsBackDoor = false;

    // Start is called before the first frame update
    void Start()
    {
        target_pos = transform.position;
        audioS = GetComponent<AudioSource>();
        if (startsOpen)
        {
            OpenDoor();
        }
    }

    private void Update()
    {
        if (isLerp)
        {
            StartCoroutine(MoveToPosition(false));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target_pos.y = minHeight;
            isLerp = true;
            other.gameObject.GetComponent<Health>().ResetHealth();
            if (!IsBackDoor)
            {
                ManageGame.IncrementLevel();
                BeatGenerator.CrossFadeAudio(0);
                StopLevel.SetActive(false);
            }
            gameObject.layer = 8;
        }
    }

    public void OpenDoor()
    {
        isLerp = false;
        target_pos.y = maxHeight;
        gameObject.layer = 0;
        StartCoroutine(MoveToPosition(true));
    }

    public void CloseDoor()
    {
        target_pos.y = minHeight;
        isLerp = true;
        gameObject.layer = 8;
    }

    private IEnumerator MoveToPosition(bool opening)
    {
        isLerp = false;
        float elapsedTime = 0f;
        float time = 2.5f;
        Vector3 startingPos = transform.position;
        if (!opening)
        {
            audioS.Play();
            audioS.time = 2;
        }
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, target_pos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return 0;
        }
        if(target_pos.y > minHeight)
        {
            this.gameObject.SetActive(false);
            gameObject.layer = 0;
        }
    }
}
