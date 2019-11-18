using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//create 
public class EndGoal : MonoBehaviour
{
    public GameObject Canvas;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("in here");
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
            BeatGenerator.ToggleBeatSystem(false);
            StartCoroutine(CompleteGame());
        }
    }

    private IEnumerator CompleteGame()
    {
        Animator anim = Canvas.GetComponent<Animator>();
        anim.Play("EndGame");
        yield return new WaitForSeconds(12.5f);
        SceneManager.LoadScene("MainMenu");
    }

    public void LoopGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
