using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInAndOut : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    private IEnumerator Fades()
    {
        anim.SetTrigger("fade_out");
        yield return 0;
    }

    public void PerformFades()
    {
        StartCoroutine(Fades());
    }
}
