using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatAnimations : MonoBehaviour
{
    bool pressed = false;
    private Vector3 initScale;

    void Awake() {
        initScale = transform.localScale;
    }

    void Update()
    {
        //Successful press animation
        if(pressed)
        {
            gameObject.transform.localScale += new Vector3(Time.deltaTime * 6, Time.deltaTime * 6, Time.deltaTime * 6);
        }
    }

    //Animation for correct beat
    //Could possibly put something in here for movement of the player?
    public IEnumerator hit()
    {
        pressed = true;
        yield return new WaitForSeconds(.1f);
        pressed = false;
        gameObject.GetComponent<RawImage>().enabled = false;
        // yield return new WaitForSeconds(1.0f);
        // gameObject.GetComponent<RawImage>().enabled = true;
    }

    //Animation for missed beat
    public IEnumerator miss()
    {
        //gameObject.GetComponent<Material>().SetColor("_TintColor", Color.red);
        yield return new WaitForSeconds(.5f);
    }

    public void Reset() {
        transform.localScale = initScale;
        gameObject.GetComponent<RawImage>().enabled = true;
    }
}

