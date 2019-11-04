﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//create 
public class EndGoal : MonoBehaviour
{
    public GameObject player;
    public GameObject BeatGenerator;

    // Update is called once per frame
    void Update()
    {
        //press r to restart game
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoopGame();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.gameObject.tag == "Player")
        {
            Debug.Log("touched end");
            player.SetActive(false);
            BeatGenerator.SetActive(false);
        }
    }

    public void LoopGame()
    {
        SceneManager.LoadScene("Level0");
    }
}
