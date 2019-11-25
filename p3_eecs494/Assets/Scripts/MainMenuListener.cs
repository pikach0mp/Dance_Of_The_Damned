using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenuListener : MonoBehaviour
{
    void Update()
    {
    	Gamepad pad = Gamepad.current;

     	if(Input.GetKeyDown(KeyCode.Escape) || (pad != null && pad.selectButton.wasPressedThisFrame))  {
     		SceneManager.LoadScene("MainMenu");
     	}
    }
}
