using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void loadSync()
    {
        SceneManager.LoadScene("Sync");
    }

    public void loadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void loadLevel()
    {
        SceneManager.LoadScene("FinalLayout");
    }
}
