using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Another singleton because theres only one camera
/// </summary>
public class CameraAdjuster : MonoBehaviour {
	static Camera cam;
	static TV_Effect tvEffect;
	static VHS_Effect vhsEffect;

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
		tvEffect = gameObject.GetComponent<TV_Effect>();
		vhsEffect = gameObject.GetComponent<VHS_Effect>();
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
			largerDifference = Mathf.Min(1f, largerDifference);
			targetCameraHeight = (largerDifference * targetCameraHeight) + 40;

			// TODO(tdamron): Change this from 0.5f -> 1f
			DoVhsShaders(0.5f - largerDifference);

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
        .Select(x => x.transform)
        .ToList();
			playerTransforms.Add(GameObject.FindGameObjectWithTag("Shadow").transform);
		}
	}

  // Max is 1f, min is ~0.07f
	void DoVhsShaders(float magnitude) {
		// Square effect so it's way more pronounced when shadow is nearby
		magnitude = magnitude * magnitude;

    // Scanlines. The inspector slider goes from -8f to -16f but it gets set to 0 by default
    // -16 is basically impossible to see, but 0f makes everything too bright
    // Maybe take this out
		//tvEffect._hardScan = -4f * magnitude * 4f;
		//tvEffect._resolution = (magnitude + 1f);

    // 3d glasses style color offset
		// 0.005 = almost none, 0.0128 = extreme amount
		vhsEffect.offsetColor = magnitude * .01f;

    // Camera jumping.
    // 0 = none, 0.05 = extreme
		vhsEffect._verticalOffset = magnitude * .012f;

    // Overlaid "grainy" texture
    // 1 = none, .9 = very noticeable, .8 = way too intrusive
		vhsEffect._textureIntensity = .95f + -(magnitude) * .1f;
	}
}

