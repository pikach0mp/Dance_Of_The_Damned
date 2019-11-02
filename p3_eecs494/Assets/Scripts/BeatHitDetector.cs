using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.SerializableAttribute]
public class UnityEventKeyCode : UnityEvent<KeyCode> {}

public class BeatHitDetector : MonoBehaviour {
	public float leeway = 0.1F;

	public UnityEventKeyCode OnBeatHit;
	public UnityEventKeyCode OnBadInput;
	public UnityEvent OnBeatMissed;
	public UnityEvent OnBeatIgnored;

	private (KeyCode, float) lastHit;
	private bool lastHitResolved;

	private bool lastBeatAvailable;
	private float lastBeat;

	void Awake() {
		lastHitResolved = true;
		lastBeatAvailable = false;
	}

	public void OnBeat() {
		if(!lastHitResolved) {
			OnBeatHit.Invoke(lastHit.Item1);
			lastHitResolved = true;
		} else {
			lastBeat = Time.timeSinceLevelLoad;
			lastBeatAvailable = true;
		}
	}

	public void PressButton(KeyCode key) {
		// Prevents spamming
		if(!lastHitResolved) {
			OnBadInput.Invoke(lastHit.Item1);
		}

		if(lastBeatAvailable) {
			lastBeatAvailable = false;

			OnBeatHit.Invoke(key);
			lastHitResolved = true;
		} else {
			lastHitResolved = false;
			lastHit = (key, Time.timeSinceLevelLoad);
		}
	}

	void Update() {
		// Checks if some past input didn't hit anything
		if(!lastHitResolved && Time.timeSinceLevelLoad - lastHit.Item2 > leeway) {
			lastHitResolved = true;
			OnBadInput.Invoke(lastHit.Item1);
		}

		// Checks if last beat was ever hit
		if(lastBeatAvailable && Time.timeSinceLevelLoad - lastBeat > leeway) {
			lastBeatAvailable = false;
			OnBeatMissed.Invoke();
		}
	}
}
