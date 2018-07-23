using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {
	private Light _light;
	[SerializeField]
	private float _min;
	[SerializeField]	
	private float _max;
	private float _default;

	void Start() {
		_light = GetComponent<Light>();
		_default = _light.intensity;
	}

	void OnTriggerStay(Collider c) {
		if (c.tag == "Shadow") {
			_light.intensity = Random.Range(_min, _max);		
		}
	}

	void OnTriggerExit(Collider c) {
		if (c.tag == "Shadow") {
			_light.intensity = _default;
		}
	}
}
