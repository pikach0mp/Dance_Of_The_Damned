using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//create 
public class EndGoal : MonoBehaviour
{
    public BeatGenerator beatGenerator;
    public GameObject Canvas;

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
            Debug.Log("ending game");
            StartCoroutine(CompleteGame());
            BeatGenerator.ToggleBeatSystem(false);
        }
    }

    private IEnumerator CompleteGame()
    {
        Animator anim = Canvas.GetComponent<Animator>();
        anim.SetTrigger("EndGame");
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("MainMenu");
    }

    public void LoopGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
