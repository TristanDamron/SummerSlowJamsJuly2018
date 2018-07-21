using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField]
	public bool IsShadow;
	public int PlayerNumber; // 1-indexed

	// Use this for initialization
	void Start () {
		if (PlayerNumber < 1 || PlayerNumber > Config.NumberOfPlayers) {
			Debug.LogError(
				"The max number of players is " + 
				Config.NumberOfPlayers + 
        " and player numbers start at 1"
			);
		}
	}
	
	// Update is called once per frame
	void Update () {
		var xVelocity = Input.GetAxis("HorizontalPlayer" + PlayerNumber);
		var yVelocity = Input.GetAxis("VerticalPlayer" + PlayerNumber);

		gameObject.GetComponent<Rigidbody>().velocity = new Vector3(
			xVelocity * Config.MovementSpeed,
      0,
			yVelocity * Config.MovementSpeed
		);

		Vector3 direction = (Vector3.right * xVelocity) + (Vector3.forward * yVelocity);
		if (direction != Vector3.zero) {
      transform.rotation=Quaternion.LookRotation(direction);
		}
	}
}
