using System.Collections;
using UnityEngine;

public class BeatLevelSystem : MonoBehaviour {

	public int streakNeeded = 20;
	public int scoreToLevelDown;
	public int capScore = 0;

	private int score;
	private int streak;

	public void OnBeatHit((KeyCode key, BeatInfo) info) {
		score++;
		streak++;
		OnUpdated();
	}

	public void OnBadInput(KeyCode key) {
		score -= 2;
		streak = 0;
		OnUpdated();
	}

	public void OnBeatMiss(BeatInfo info) {
		score -= 2;
		streak = 0;
		OnUpdated();
	}

	private void OnUpdated() {
		bool resetScore = false;
		if(score <= scoreToLevelDown) {
			resetScore = BeatGenerator.SetLevel(BeatGenerator.GetLevel() - 1);
		}
		if(streak == streakNeeded) {
			resetScore = BeatGenerator.SetLevel(BeatGenerator.GetLevel() + 1);
		}
		if(resetScore) {
			score = capScore;
			streak = 0;
		} else {
			score = Mathf.Clamp(score, scoreToLevelDown, capScore);
		}
	}
}