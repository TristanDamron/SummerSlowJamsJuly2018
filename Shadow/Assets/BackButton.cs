using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour {
	void Update() {
		if (Input.GetAxisRaw("Cancel") != 0f) {
			gameObject.SetActive(false);
		}
	}
}
