using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerBeats : MonoBehaviour
{
    public AudioSource playGame;
    public AudioSource follow;
    public AudioSource joystick;
    public AudioSource pressX;

    // Start is called before the first frame update
    void Start()
    {
        BeatGenerator.ToggleBeatSystem(false);
    }

    // Update is called once per frame
    public void StartBeats()
    {
        BeatGenerator.ToggleBeatSystem(true);
    }

    public void WannaPlay()
    {
        playGame.Play();
    }

    public void Follow()
    {
        follow.Play();
    }

    public void Joystick()
    {
        joystick.Play();
    }

    public void PressX()
    {
        pressX.Play();
    }
}
