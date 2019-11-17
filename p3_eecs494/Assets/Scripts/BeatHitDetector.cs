using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ButtonPress {
	MOVE, ATTACK
}

[System.SerializableAttribute]
public class UnityEventButtonPress : UnityEvent<ButtonPress> {}

[System.SerializableAttribute]
public class UnityEventButtonPressBeatInfo : UnityEvent<(ButtonPress, BeatInfo)> {}


public class BeatHitDetector : MonoBehaviour {
	public float leeway = 0.1F;

	public UnityEventButtonPressBeatInfo OnBeatHit;
	public UnityEventButtonPress OnBadInput;
	public UnityEventBeatInfo OnBeatMissed;

	private (ButtonPress, float) lastHit;
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

	public void PressButton(ButtonPress buttonPress) {
		// Prevents spamming
		if(!lastHitResolved) {
			OnBadInput.Invoke(lastHit.Item1);
		} else if(lastBeatAvailable) {
			lastBeatAvailable = false;

			OnBeatHit.Invoke((buttonPress, lastBeat.Item1));
			lastHitResolved = true;
		} else {
			lastHitResolved = false;
			lastHit = (buttonPress, BeatGenerator.GetTime());
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
