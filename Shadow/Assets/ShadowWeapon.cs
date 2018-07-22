using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowWeapon : MonoBehaviour {
	void OnTriggerEnter(Collider c) {
		if (c.tag == "Player") {
			c.GetComponent<PlayerController>().hp -= 1f; 
		}
	}
}
