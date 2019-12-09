using System.Collections;
using UnityEngine;

public class BeatLevelSystem : MonoBehaviour {

	public int healthCap = 30;
	public int level1Cutoff = 15;
	public int level2Cutoff = 25;
	private int health;
	private bool update;
    bool isUpgrade;
	public RectTransform healthTransfrom;
	private float maxHeight;

	void Start() {
		maxHeight = healthTransfrom.rect.height;
		healthTransfrom.sizeDelta = new Vector2(healthTransfrom.sizeDelta.x, 0);
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
        health += 2;

        isUpgrade = true;
        OnUpdated();
	}

	public void OnBadInput(ButtonPress press) {
		if(!update) {
			return;
		}
		health -= 2;

        isUpgrade = false;
        OnUpdated();
	}

	public void OnBeatMiss(BeatInfo info) {
		if(!update) {
			return;
		}

        isUpgrade = false;
        health -= 2;
		OnUpdated();
	}


	private void OnUpdated() {
        if (health >= level2Cutoff) {
			BeatGenerator.SetLevel(2, 0);
		} else if (health >= level1Cutoff) {
            int isDown = isUpgrade ? 0 : 1;
            BeatGenerator.SetLevel(1, isDown);
		} else {
			BeatGenerator.SetLevel(0, 1);
		}
		
        health = Mathf.Clamp(health, 0, healthCap);
		healthTransfrom.sizeDelta = new Vector2(healthTransfrom.sizeDelta.x, Mathf.Lerp(0, maxHeight, (float)health/(float)healthCap));
    }
}
