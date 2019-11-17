using System.Collections;
using UnityEngine;

public class BeatLevelSystem : MonoBehaviour {

	public int healthCap = 30;
	public int healthNeeded = 20;
	private int health;

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
		health++;
		OnUpdated();
	}

	public void OnBadInput(ButtonPress press) {
		if(!update) {
			return;
		}
		health -= 3;
		OnUpdated();
	}

	public void OnBeatMiss(BeatInfo info) {
		if(!update) {
			return;
		}
		health -= 3;
		OnUpdated();
	}

	private void OnUpdated() {
		BeatGenerator.SetLevel(health >= healthNeeded ? 1 : 0);
		health = Mathf.Clamp(health, 0, healthCap);
	}
}