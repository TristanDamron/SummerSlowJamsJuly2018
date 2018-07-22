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
			case "Bullet pickup":
				Destroy(col);
				Debug.Log("You shot a bullet pickup");
				break;
			default:
				Debug.Log("Hit building geometry or floor");
				break;
		}

		Destroy(gameObject);
	}

	private void OnDestroy() {
		// Prevent errors on scene end
		if (SoundManager.Inst != null) {
  			SoundManager.Inst.PlaySound(SoundManager.Inst.BulletDiesClip);
		}
	}
}
