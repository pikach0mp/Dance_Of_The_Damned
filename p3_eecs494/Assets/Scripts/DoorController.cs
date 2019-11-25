using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject StopLevel;
    private bool isLerp;
    Vector3 target_pos;
    private AudioSource audioS;
    public bool IsBackDoor = false;

    // Start is called before the first frame update
    void Start()
    {
        target_pos = transform.position;
        audioS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isLerp)
        {
            StartCoroutine(MoveToPosition(false));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target_pos.y = 1.25f;
            isLerp = true;
            other.gameObject.GetComponent<Health>().ResetHealth();
            if (!IsBackDoor)
                StopLevel.SetActive(false);
            gameObject.layer = 8;
        }
    }

    public void OpenDoor()
    {
        isLerp = false;
        target_pos.y = 3.75f;
        gameObject.layer = 0;
        StartCoroutine(MoveToPosition(true));
     }

    private IEnumerator MoveToPosition(bool opening)
    {
        isLerp = false;
        float elapsedTime = 0f;
        float time = 2.5f;
        Vector3 startingPos = transform.position;
        if (!opening)
        {
            audioS.Play();
            audioS.time = 2;
        }
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, target_pos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return 0;
        }
        if(target_pos.y > 1.25f)
        {
            this.gameObject.SetActive(false);
            gameObject.layer = 0;
        }

    }

}
