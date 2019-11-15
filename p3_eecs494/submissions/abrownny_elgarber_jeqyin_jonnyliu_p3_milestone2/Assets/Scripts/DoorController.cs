﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private bool isLerp;
    Vector3 target_pos;

    // Start is called before the first frame update
    void Start()
    {
        target_pos = transform.position;
    }

    private void Update()
    {
        if (isLerp)
        {
            StartCoroutine(MoveToPosition());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target_pos.y = 1.25f;
            isLerp = true;
            //AudioSource.PlayClipAtPoint();
        }
    }

    public void OpenDoor()
    {
        isLerp = false;
        target_pos.y = 3.75f;
        StartCoroutine(MoveToPosition());
     }

    private IEnumerator MoveToPosition()
    {
        float elapsedTime = 0f;
        float time = 2.5f;
        Vector3 startingPos = transform.position;
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, target_pos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return 0;
        }
        if(target_pos.y > 1.25f)
        {
            this.gameObject.SetActive(false);
        }
    }

}