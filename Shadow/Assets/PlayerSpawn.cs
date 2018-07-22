using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {
	[SerializeField]
	private GameObject _player; 
	[SerializeField]
	private int _playerNum;

	void Update () {
		if (Input.GetAxis("ShootPlayer" + _playerNum) != 0f) {
			Config.ReadyPlayers++;
			var p = Instantiate(_player, transform.position, transform.rotation);
			p.GetComponent<PlayerController>().PlayerNumber = _playerNum;

			// TODO(tdamron): Change this from 1 to 4 for playtesting 
			if (Config.ReadyPlayers >= 2) {
				GameObject.Find("Ready Up Text").SetActive(false);
				// TODO(tdamron): Set the 4th player as the shadow
				p.GetComponent<PlayerController>().IsShadow = true;
				p.tag = "Shadow";
			}

			Destroy(gameObject);
		}
	}
}
