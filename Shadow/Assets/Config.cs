using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

	public static void StartGame() {
		CameraAdjuster.Init();
	}
}

[CustomEditor(typeof(Config))]
public class ConfigEditor : Editor
{
  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();

		Config myScript = (Config)target;
    if(GUILayout.Button("Simulate game start"))
    {
			Config.StartGame();
    }
  }
}