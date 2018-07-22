using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		switch (col.tag) {
			case "Shadow":
				Debug.Log("Hit shadow!");
				break;
			case "Player":
				Debug.Log("Hit a player");
				break;
			default:
				Debug.Log("Hit building geometry or floor");
				break;
		}

		Destroy(gameObject);
	}

	private void OnDestroy() {
		SoundManager.Inst.PlaySound(SoundManager.Inst.BulletDiesClip);
	}
}
