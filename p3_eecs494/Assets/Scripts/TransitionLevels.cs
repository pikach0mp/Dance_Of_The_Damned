using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLevels : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Here");
            
            Scene scene = SceneManager.GetActiveScene();
            Debug.Log(scene.name);
            if(scene.name == "Level0")
            {
                SceneManager.LoadScene("Level1");
            }
            if(scene.name == "Level1")
            {
                SceneManager.LoadScene("Level2");
            }
            if(scene.name == "Level2")
            {

            }
        }
    }
}
