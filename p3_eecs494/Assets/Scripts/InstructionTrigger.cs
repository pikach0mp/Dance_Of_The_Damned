using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionTrigger : MonoBehaviour
{
    public float detection_distance = 2;
    public GameObject instructions;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * detection_distance, Color.green);
        if (Physics.Raycast(transform.position, transform.forward, out hit, detection_distance))
        {
            if(hit.transform.gameObject.tag == "Player")
            {
                StartCoroutine(TimedPrompt());
            }
        }
    }

    IEnumerator TimedPrompt()
    {
        instructions.SetActive(true);
        yield return new WaitForSeconds(10f);
        instructions.SetActive(false);
    }
}
