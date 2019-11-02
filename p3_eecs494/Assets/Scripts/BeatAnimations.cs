using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatAnimations : MonoBehaviour
{
    public Material base_material;

    bool pressed = false;
    bool missed = false;
    bool dissolving = true;
	public bool moving = false;
    float dissolve = .1f;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        //Successful press animation
        if(pressed)
        {
            gameObject.transform.localScale *= 1.10f;

        }

        //If the indicator is moving
        if (moving)
		{
            Vector3 new_pos = new Vector3(transform.position.x - 10 * speed, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, new_pos, .1f);
		}

        //Dissolving animation when beat spawns
        if (dissolving)
        {
            dissolve -= .025f;
            GetComponent<Renderer>().material.SetFloat("Vector1_B0ED71B0", dissolve);
        }
    }

    //Animation for correct beat
    //Could possibly put something in here for movement of the player?
    public IEnumerator hit()
    {
        pressed = true;
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
    }

    //Animation for missed beat
    public IEnumerator miss()
    {
        //gameObject.GetComponent<Material>().SetColor("_TintColor", Color.red);
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }

    public IEnumerator changeMaterial()
    {
        yield return new WaitForSeconds(1.5f);
        GetComponent<Renderer>().material = base_material;
    }
}

