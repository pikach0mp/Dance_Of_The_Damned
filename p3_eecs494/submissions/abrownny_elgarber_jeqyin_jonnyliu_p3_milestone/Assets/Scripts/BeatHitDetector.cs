using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.SerializableAttribute]
public class UnityEventKeyCode : UnityEvent<KeyCode> {}

[System.SerializableAttribute]
public class UnityEventKeyCodeBeatInfo : UnityEvent<(KeyCode, BeatInfo)> {}


public class BeatHitDetector : MonoBehaviour {
	public float leeway = 0.1F;

	public UnityEventKeyCodeBeatInfo OnBeatHit;
	public UnityEventKeyCode OnBadInput;
	public UnityEventBeatInfo OnBeatMissed;

	private (KeyCode, float) lastHit;
	private bool lastHitResolved;

	private bool lastBeatAvailable;
	private (BeatInfo, float) lastBeat;

	void Awake() {
		lastHitResolved = true;
		lastBeatAvailable = false;
	}

	public void OnBeat(BeatInfo info) {
		if(!lastHitResolved) {
			OnBeatHit.Invoke((lastHit.Item1, info));
			lastHitResolved = true;
		} else {
			lastBeat = (info, BeatGenerator.GetTime());
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

			OnBeatHit.Invoke((key, lastBeat.Item1));
			lastHitResolved = true;
		} else {
			lastHitResolved = false;
			lastHit = (key, BeatGenerator.GetTime());
		}
	}

	void Update() {
		// Checks if some past input didn't hit anything
		if(!lastHitResolved && BeatGenerator.GetTime() - lastHit.Item2 > leeway) {
			lastHitResolved = true;
			OnBadInput.Invoke(lastHit.Item1);
		}

		// Checks if last beat was ever hit
		if(lastBeatAvailable && BeatGenerator.GetTime() - lastBeat.Item2 > leeway) {
			lastBeatAvailable = false;
			OnBeatMissed.Invoke(lastBeat.Item1);
		}
	}
}
