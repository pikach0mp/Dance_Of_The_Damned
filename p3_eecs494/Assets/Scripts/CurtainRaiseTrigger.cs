using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainRaiseTrigger : MonoBehaviour {

	private bool triggered = false;

	public bool IsTriggered() {
		return triggered;
	}

	void OnTriggerEnter(Collider collider) {
		if(collider.GetComponent<PlayerMovement>() != null) {
			triggered = true;
		}
	}

	void OnTriggerExit(Collider collider) {
		if(collider.GetComponent<PlayerMovement>() != null) {
			triggered = false;
		}
	}
}
