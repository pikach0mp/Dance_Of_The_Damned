using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPressAnimation : MonoBehaviour {

	private Image image;

	void Start() {
		image = GetComponent<Image>();
	}

	public void OnBeat(BeatInfo info) {
		StartCoroutine(DoAnimation());
	}

	private IEnumerator DoAnimation() {
		image.color = new Color(0.7F, 0.7F, 0.7F);
		yield return new WaitForSeconds(0.1F);
		image.color = Color.white;
	}
}
