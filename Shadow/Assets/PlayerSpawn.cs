using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {
	[SerializeField]
	private GameObject _player; 
	[SerializeField]
	private int _playerNum;
	[SerializeField]
	private GameObject _shadow;

	void Update () {
		if (Input.GetAxis("ShootPlayer" + _playerNum) != 0f) {
			Config.ReadyPlayers++;

			// TODO(tdamron): Change this from 1 to 4 for playtesting 
			if (Config.ReadyPlayers >= Config.NumberOfPlayers) {
				var p = Instantiate(_shadow, transform.position, transform.rotation);
				p.GetComponent<PlayerController>().PlayerNumber = _playerNum;

				Config.StartGame();
			} else {
				var p = Instantiate(_player, transform.position, transform.rotation);
				p.GetComponent<PlayerController>().PlayerNumber = _playerNum;
			}

			Destroy(gameObject);
		}
	}
}
