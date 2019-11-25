using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatStreakTracker : MonoBehaviour {

	private Text text;
	private int streak;

	void Awake() {
		streak = 0;
		text = GetComponent<Text>();
		text.text = "x0";
	}

	public void OnBeatHit((ButtonPress press, BeatInfo) info) {
		streak++;
		text.text = "x"+streak.ToString();
	}

	public void OnBadInput(ButtonPress press) {
		streak = 0;
		text.text = "x0";
	}

	public void OnBeatMiss(BeatInfo info) {
		streak = 0;
		text.text = "x0";
	}
}
