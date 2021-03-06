﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitions : MonoBehaviour
{
	public GameObject player;
	private Animator anim;

	private Vector3 target_pos;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = player.transform.position;
        //transform.position.y += 1;
	}
	
    public void TransitionFrom(int level)
    {
        switch (level)
        {
            case 1:
                GetComponent<Animator>().SetTrigger("Level1Transition");
                break;
            case 2:
                GetComponent<Animator>().SetTrigger("Level2Transition");
                break;
            default:
                break;
        }
    }

    public void OnDisable()
    {
    	Debug.Log("END");
        BeatGenerator.ToggleBeatSystem(true);
        this.gameObject.SetActive(false);
    }
}
