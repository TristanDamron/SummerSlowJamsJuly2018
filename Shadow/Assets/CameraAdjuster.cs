using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Another singleton because theres only one camera
/// </summary>
public class CameraAdjuster : MonoBehaviour {
	static Camera cam;
	static CameraAdjuster cameraAdjuster;
	static bool initialized;
	static List<Transform> playerTransforms;
	public float furthestEast = 0f;
	public float furthestWest = 0f;
	public float furthestNorth = 0f;
	public float furthestSouth = 0f;
	Vector3 cameraTargetPosition = Vector3.zero;

  // Largest possible x and z 
	float maxXPosition = 200f;
	float maxZPosition = 100f;
  
	void Start() {
		cameraAdjuster = this;
		playerTransforms = new List<Transform>();
		cam = gameObject.GetComponent<Camera>();
	}

	void Update() {
		if (playerTransforms.Count > 0) {

      // Calculate furthest cardinal position of all players + shadow
			furthestEast = -maxXPosition;
			furthestWest = maxXPosition; 
			furthestNorth = -maxZPosition; 
      furthestSouth = maxZPosition;
			foreach (Transform t in playerTransforms) {
				if (t.position.x > furthestEast) furthestEast = t.position.x;
				if (t.position.x < furthestWest) furthestWest = t.position.x;
				if (t.position.z < furthestSouth) furthestSouth = t.position.z;
				if (t.position.z > furthestNorth) furthestNorth = t.position.z;
			}

      // Calculate ideal camera height to capture whole game area
			float targetCameraHeight = 180; // default
			float xMaxDifference = (furthestEast - furthestWest) / maxXPosition;
			float zMaxDifference = (furthestNorth - furthestSouth) / maxZPosition;
			float largerDifference = xMaxDifference > zMaxDifference ? xMaxDifference : zMaxDifference;
			targetCameraHeight = (largerDifference * targetCameraHeight) + 40;

			cameraTargetPosition = new Vector3(
				(furthestEast + furthestWest) / 2,
				targetCameraHeight,
				(furthestNorth + furthestSouth) /2
			);

			cam.transform.position = Vector3.Lerp(cam.transform.position, cameraTargetPosition, Config.CameraMovementSpeed * Time.deltaTime);
		}
	}

	public static void Init() {
		if (!initialized) {
      GameObject.Find("Ready Up Text").SetActive(false);
			playerTransforms = GameObject.FindGameObjectsWithTag("Player")
			                             .Select(x => { print(x.name); return x.transform; })
        .ToList();
			playerTransforms.Add(GameObject.FindGameObjectWithTag("Shadow").transform);
		}
	}
}

