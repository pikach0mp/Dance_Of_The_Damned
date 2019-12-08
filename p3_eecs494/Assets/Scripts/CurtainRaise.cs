using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainRaise : MonoBehaviour {
	public CurtainRaiseTrigger raiseTrigger;
	private float yCutoff = 2.7F;

	private float timeToStartRaising = 4F;
	private float time;

	void FixedUpdate() {
		if(raiseTrigger.IsTriggered()) {

			if(time < timeToStartRaising) {
				time += Time.deltaTime;
			} else {
				transform.position += Vector3.up * Time.fixedDeltaTime * 0.1F;

				if(transform.position.y > yCutoff) {
					gameObject.SetActive(false);
				}
			}
		} else {
			time = 0;
		}
	}
}
