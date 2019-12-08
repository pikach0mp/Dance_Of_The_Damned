using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainRaise : MonoBehaviour {
	public CurtainRaiseTrigger raiseTrigger;
	private float yCutoff = 2.7F;

	void FixedUpdate() {
		if(raiseTrigger.IsTriggered()) {
			transform.position += Vector3.up * Time.fixedDeltaTime * 0.1F;

			if(transform.position.y > yCutoff) {
				gameObject.SetActive(false);
			}
		}
	}
}
