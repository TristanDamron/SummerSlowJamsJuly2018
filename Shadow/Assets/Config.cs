using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

/// <summary>
/// Singleton class that allows us to set constants in the inspector.
/// </summary>
public class Config : MonoBehaviour {
	// It's a singleton
	static Config config;
	void Awake() {
    config = this;
  }

  // Bunch of publicly accessible variables in the singleton style
	[Header("Game config settings")]
	[SerializeField]
	float movementSpeed;
	public static float MovementSpeed { get { return config.movementSpeed; } }

	[SerializeField]
	float shadowMovementSpeed;
	public static float ShadowMovementSpeed { get { return config.shadowMovementSpeed; } }

	[SerializeField]
	int numberOfPlayers;
	public static int NumberOfPlayers { get { return config.numberOfPlayers; } }

	[SerializeField]
  float bulletSpeed;
	public static float BulletSpeed { get { return config.bulletSpeed; } }

	[SerializeField]
  int startingBulletNumber;
	public static int StartingBulletNumber { get { return config.startingBulletNumber; } }

  [SerializeField]
  int startingPickupNumber;
	public static int StartingPickupNumber { get { return config.startingPickupNumber; } }

  [SerializeField]
  int pickupSpawnInterval;
	public static int PickupSpawnInterval { get { return config.pickupSpawnInterval; } }
	
	[SerializeField]
	int readyPlayers;
	public static int ReadyPlayers { get { return config.readyPlayers; } set  { config.readyPlayers = value; } }

	[SerializeField]
  float cameraMovementSpeed;
	public static float CameraMovementSpeed { get { return config.cameraMovementSpeed; } }
	[SerializeField]
  int frozen;
	public static int FrozenKids { get { return config.frozen; } set { config.frozen = value; } }

	[SerializeField]
  bool paused;
	public static bool Paused { get { return config.paused; } set { config.paused = value; } }
	[SerializeField]
  float boostResetTime;
	public static float ShadowBoostResetTime { get { return config.boostResetTime; } }
	[SerializeField]
  float boostLength;
	public static float ShadowBoostLength { get { return config.boostLength; } }	
	[SerializeField]
  float shadowBoostSpeed;
	public static float ShadowBoostSpeed { get { return config.shadowBoostSpeed; } }
    public GameObject EndGameObject;
    public TextMeshProUGUI EndGameWinnerText;

	public static void StartGame() {
			CameraAdjuster.ScanForPlayersAndShadow();
	}

    public static void EndGame(bool playersWin) {
        print("The shadow is dead? " + playersWin);
        config.EndGameObject.SetActive(true);
        config.EndGameWinnerText.text = playersWin ? "RAD LADDS WIN!" : "MIDNIGHT MAN WINS!";
    }
}

// @TODO(tdamron): Turn off this block for production build
// [CustomEditor(typeof(Config))]
// public class ConfigEditor : Editor
// {
//   public override void OnInspectorGUI()
//   {
//     DrawDefaultInspector();

// 		Config myScript = (Config)target;
//     if(GUILayout.Button("Simulate game start"))
//     {
// 			Config.StartGame();
//     }
//   }
// }