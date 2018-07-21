using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {
	[SerializeField]
	private Light _light;

	void OnTriggerStay(Collider c) {
		if (c.name == "Shadow") {
			_light.intensity = Random.Range(10f, 80f);		
		}
	}

	void OnTriggerExit(Collider c) {
		if (c.name == "Shadow") {
			_light.intensity = 100f;
		}
	}
}
