using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	[SerializeField]
	float movementSpeed;
	public static float MovementSpeed { get { return config.movementSpeed; } }

	[SerializeField]
	int numberOfPlayers;
	public static int NumberOfPlayers { get { return config.numberOfPlayers; } }

	[SerializeField]
  float bulletSpeed;
	public static float BulletSpeed { get { return config.bulletSpeed; } }

}
