using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitOrMiss : MonoBehaviour
{
    Color colorStart = Color.red;
    Color colorEnd = Color.green;
    //float duration = 1.0f;
    public KeyCode key;
    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        //rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

  //      if (Input.GetKeyDown(key))
		//{
  //          StartCoroutine(presskey());
		//}
    }

    //Check if they press the key while colliding
    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(key))
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            // StartCoroutine(other.gameObject.GetComponent<BeatAnimations>().hit());
        }
    }

    //Missed
    private void OnTriggerExit(Collider other)
    {
        //rend.material.SetColor("_Color", Color.red);
        // StartCoroutine(other.gameObject.GetComponent<BeatAnimations>().miss());
    }

    //Changes indicator color when pressing the key
 //   IEnumerator presskey()
	//{
 //       var block = new MaterialPropertyBlock();
 //       Color current_color = GetComponent<Renderer>().material.color;
 //       current_color.a = .5f;
 //       Color new_color = new Color(1, .5f, .5f, .5f);
 //       block.SetColor("_BaseColor", new_color);
 //       rend.SetPropertyBlock(block);
 //       yield return new WaitForSeconds(.05f);
 //       block.SetColor("_BaseColor", current_color);
 //       rend.SetPropertyBlock(block);


 //   }
}
