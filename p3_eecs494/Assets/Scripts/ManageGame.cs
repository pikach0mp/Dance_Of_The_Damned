﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour
{
    // Start is called before the first frame update
    static ManageGame instance;
    GameObject doors;
    public AudioSource hiding;
    public AudioSource escape;

    private int level;

    bool playerSeenCurtains;
    bool beginGame = true;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += BeginAtCheckpoint;
        level = 1;
        playerSeenCurtains = false;
        beginGame = true;
        DontDestroyOnLoad(gameObject);
    }

    void BeginAtCheckpoint(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "FinalLayout")
        {
            Debug.Log(scene.name);
            if (beginGame)
            {
                instance.escape.Play();
                beginGame = false;
            }

            doors = GameObject.Find("Doors");
            Transform t = doors.transform;

            switch (level)
            {
                case 1:
                    Debug.Log("starting at level 1");

                    GameObject.Find("Level1Door").GetComponent<DoorController>().startsOpen = true;
                    t.Find("Level1BackDoor").gameObject.SetActive(true);
                    break;
                case 2:
                    Debug.Log("starting at level 2");
                    GameObject.Find("Level2Door").GetComponent<DoorController>().startsOpen = true;
                    t.Find("Level2BackDoor").gameObject.SetActive(true);
                    break;
                case 3:
                    Debug.Log("starting at level 3");
                    GameObject.Find("Level3Door").GetComponent<DoorController>().startsOpen = true;
                    t.Find("Level3BackDoor").gameObject.SetActive(true);
                    break;
                default:
                    Debug.Log("something is very, very wrong");
                    break;
            }
        }
    }

    public static void IncrementLevel()
    {
        instance.level += 1;
        Debug.Log(instance.level);
    }

    public static void ResetLevel()
    {
        instance.level = 1;
        instance.beginGame = true;
        instance.playerSeenCurtains = false;
    }

    public static void FirstTimeCurtain()
    {
        if (instance.playerSeenCurtains)
        {
            return;
        }
        else
        {
            instance.hiding.Play();
            Debug.Log("tried playing");
            instance.playerSeenCurtains = true;
        }
    }
}
