using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//create 
public class EndGoal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
            Debug.Log("touched end");
            LoopGame();
        }
    }


    public void LoopGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
