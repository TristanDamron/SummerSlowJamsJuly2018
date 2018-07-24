using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour {
	public int PickupSpawnLocationIndex;

	void OnTriggerEnter(Collider col) {
    switch (col.tag) {
      case "Shadow":
        Debug.Log("Shadow picked up bullet!");
        break;
      case "Player":
        Debug.Log("Player picked up bullet");
				BulletPickupManager.Inst.IncrementNumberBullets();
        break;
			case "Bullet":
				// Bullet logic handles destroying bullet pickups
				return;
      default:
        Debug.Log("This shouldn't happen");
        break;
    }

    Destroy(gameObject);
  }

	private void OnDestroy() {
		// Prevent errors on scene end
		if (SoundManager.Inst != null) {
  		SoundManager.Inst.PlaySound(SoundManager.Inst.PickupClip);

  		BulletPickupManager.Inst.SetPickupIndexAsAvailable(PickupSpawnLocationIndex);
		}
	}

  void Update() {
    transform.Rotate(Vector3.up * 100f * Time.deltaTime);
  }
}
