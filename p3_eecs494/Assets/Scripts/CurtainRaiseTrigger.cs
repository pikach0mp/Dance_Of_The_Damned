using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainRaiseTrigger : MonoBehaviour {

	private bool triggered = false;

	public bool IsTriggered() {
		return triggered;
	}

	void OnTriggerEnter(Collider col) {
		if(col.GetComponent<PlayerMovement>() != null) {
            ManageGame.FirstTimeCurtain();
            triggered = true;
		}
	}

	void OnTriggerExit(Collider col) {
		if(col.GetComponent<PlayerMovement>() != null) {
			triggered = false;
		}
	}
}
