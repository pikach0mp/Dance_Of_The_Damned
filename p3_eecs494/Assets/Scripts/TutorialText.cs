using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    public GameObject can;
    public Text directions;

    private void Start()
    {
        BeatGenerator.ToggleBeatSystem(false);
        can.SetActive(false);
    }

    public void DisplayTextComplete()
    {
        can.SetActive(true);
        BeatGenerator.ToggleBeatSystem(true);
    }


    public void ChangeText(string text)
    {
        directions.text = text;
    }

}
