using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BeatAnimations))]
public class BeatIndicator : MonoBehaviour
{
    public float speed = 3;

    private float hitTime;
    private Vector3 targetPosition;
    private BeatAnimations anims;
    private bool hit;

    private RectTransform trans;

    void Awake() {
        anims = GetComponent<BeatAnimations>();
        trans = GetComponent<RectTransform>();
        hit = false;
    }

    void Update() {
        if(!hit) {
            UpdatePos();
        }
    }

    public void SetHitTime(float hitTime) {
        this.hitTime = hitTime;
    }

    public void SetTarget(Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }

    public void UpdatePos () {
        trans.position = targetPosition + (hitTime - BeatGenerator.GetTime()) * Vector3.right * speed; 
    }

    public void Hit() {
        hit = true;
        StartCoroutine(anims.hit());
    }

    public void Missed() {
        StartCoroutine(anims.miss());
    }

    public void Reset() {
        anims.Reset();
        hit = false;
    }
    
}
