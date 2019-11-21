using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerBeats : MonoBehaviour
{
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
}
