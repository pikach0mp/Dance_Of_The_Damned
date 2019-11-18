using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    public GameObject directionTextObject;
    Text directionText;
    // Start is called before the first frame update
    void Start()
    {
        directionText = directionTextObject.GetComponent<Text>();
        directionText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayText(string directions)
    {
        directionText.text = directions;
    }

    public void ClearText()
    {
        directionText.text = "";
    }
}
