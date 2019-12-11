using System.Collections;
using UnityEngine;

public class BeatLevelSystem : MonoBehaviour {

	public int healthCap = 60;
	public int level1Cutoff = 10;
	public int level2Cutoff = 30;
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
		health -= 4;

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

    private Vector2 CalcHeight(float curr_h, float startH, float endH, float levelBeg, float levelEnd)
    {
        return new Vector2(healthTransfrom.sizeDelta.x, startH + ((curr_h - levelBeg) / (levelEnd - levelBeg)) * (endH - startH));
    }


    private void OnUpdated() {

        health = Mathf.Clamp(health, 0, healthCap);
        if (health >= level2Cutoff) {
            healthTransfrom.sizeDelta = CalcHeight(health, maxHeight * (2.0f / 3), maxHeight, level2Cutoff, healthCap);
            BeatGenerator.SetLevel(2, 0);
		} else if (health >= level1Cutoff) {
            healthTransfrom.sizeDelta = CalcHeight(health, maxHeight * (1.0f / 3), maxHeight * (2.0f / 3), level1Cutoff, level2Cutoff);
            int isDown = isUpgrade ? 0 : 1;
            BeatGenerator.SetLevel(1, isDown);
		} else {
            healthTransfrom.sizeDelta = CalcHeight(health, 0, maxHeight * (1.0f / 3), 0, level1Cutoff);
            BeatGenerator.SetLevel(0, 1);
		}


        //healthTransfrom.sizeDelta = new Vector2(healthTransfrom.sizeDelta.x, Mathf.Lerp(0, maxHeight, (float)health / (float)healthCap));
    }

}
