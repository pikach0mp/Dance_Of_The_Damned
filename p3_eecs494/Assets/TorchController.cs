using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    public Material extinguishedFire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Turning off");
        StartCoroutine(turnOff());
    }

    IEnumerator turnOff()
    {
        yield return new WaitForSeconds(.25f);
        transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).GetComponent<Renderer>().material = extinguishedFire;
        transform.GetChild(1).GetComponent<Renderer>().material = extinguishedFire;



    }

}
