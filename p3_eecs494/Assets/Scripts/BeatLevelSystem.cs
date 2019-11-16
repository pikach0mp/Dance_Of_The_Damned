using System.Collections;
using UnityEngine;

public class BeatLevelSystem : MonoBehaviour {

	public int streakNeeded = 20;
	public int scoreToLevelDown;
	public int capScore = 0;

	private int score;
	private int streak;

	private bool update;

	void Start() {
		update = true;
	}

	public void SetUpdate(bool update) {
		Debug.Log(update);
		this.update = update;
	}

	public void OnBeatHit((ButtonPress press, BeatInfo) info) {
		if(!update) {
			return;
		}
		score++;
		streak++;
		OnUpdated();
	}

	public void OnBadInput(ButtonPress press) {
		if(!update) {
			return;
		}
		score -= 2;
		streak = 0;
		OnUpdated();
	}

	public void OnBeatMiss(BeatInfo info) {
		if(!update) {
			return;
		}
		score -= 2;
		streak = 0;
		OnUpdated();
	}

	private void OnUpdated() {
		// bool resetScore = false;
		// if(score <= scoreToLevelDown) {
		// 	resetScore = BeatGenerator.SetLevel(BeatGenerator.GetLevel() - 1);
		// }
		// if(streak == streakNeeded) {
		// 	resetScore = BeatGenerator.SetLevel(BeatGenerator.GetLevel() + 1);
		// }
		// if(resetScore) {
		// 	score = capScore;
		// 	streak = 0;
		// } else {
		// 	score = Mathf.Clamp(score, scoreToLevelDown, capScore);
		// }
	}
}