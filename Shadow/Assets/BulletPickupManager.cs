using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class BulletPickupManager : MonoBehaviour {
	public static BulletPickupManager Inst;
	public static int NumberBullets;

	[Header("Set in Unity Inspector")]
	[SerializeField]
	private List<Transform> pickupSpawnLocations;
	[SerializeField]
	private GameObject pickupPrefab;
	[SerializeField]
	private TextMeshProUGUI bulletNumberCounter;

  // Tracks which spots things have spawned in.
  // If false, the spot is available
	private Dictionary<int, bool> pickupSpawnDict;
	private float lastPickupSpawnTime = 0;

	private void Start() {
		Inst = this;
		SetNumberBullets(Config.StartingBulletNumber);

		pickupSpawnDict = new Dictionary<int, bool>();
		for (int i = 0; i < pickupSpawnLocations.Count; i++) {
			pickupSpawnDict.Add(i, false);
		}

		for (int i = 0; i < Config.StartingPickupNumber; i++) {
			SpawnPickup();
		}
	}

	private void Update() {
		if (Config.PickupSpawnInterval < 1f) {
			Debug.LogError("Spawn interval time is way too low");
		} else {
  		if (Time.time - Config.PickupSpawnInterval > lastPickupSpawnTime) {
  			lastPickupSpawnTime = Time.time;
  			SpawnPickup();
  		}
		}
	}

	private void SpawnPickup() {
		List<int> possibleSpawnIndices = new List<int>();

		for (int i = 0; i < pickupSpawnLocations.Count; i++) {
			if (!pickupSpawnDict[i]) {
				possibleSpawnIndices.Add(i);
			}
		}

		print(possibleSpawnIndices.Count);

		if (possibleSpawnIndices.Count == 0) {
			Debug.Log("can't spawn any more, no spots available");
			return;
		}

    // Spawn pickup in this location and mark as taken up
		int spawnIndex = possibleSpawnIndices[Random.Range(0, possibleSpawnIndices.Count-1)];
		var pprefab = GameObject.Instantiate(pickupPrefab, pickupSpawnLocations[spawnIndex]);
		pprefab.transform.localPosition = Vector3.zero;
		pprefab.GetComponent<BulletPickup>().PickupSpawnLocationIndex = spawnIndex;
		pickupSpawnDict[spawnIndex] = true;
	}

  // Called from pickup, frees up pickup spot
	public void SetPickupIndexAsAvailable(int idx) {
		print("picking up " + idx);
		if (!pickupSpawnDict[idx]) {
			Debug.LogError("Something went wrong in the pickup spawn dictionary logic");
		}

		pickupSpawnDict[idx] = false;
	}

	private void SetNumberBullets(int newNumber) {
    NumberBullets = newNumber;
		bulletNumberCounter.text = newNumber.ToString();
  }

	public void IncrementNumberBullets() {
		SetNumberBullets(NumberBullets + 1);
	}

	public bool DecrementNumberBullets() {
		if (NumberBullets > 0) {
      SetNumberBullets(NumberBullets - 1);
			return true;
		} else {
			return false;
		}
  }
}
