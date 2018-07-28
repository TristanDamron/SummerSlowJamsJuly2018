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
    [SerializeField]
    private GameObject _healthBar;
	[SerializeField]
	private RenderTexture _texture;

	void Update () {
		if (Input.GetAxis("ShootPlayer" + _playerNum) != 0f && !Config.Paused) {
			Config.ReadyPlayers++;

			// TODO(tdamron): Change this from 1 to 4 for playtesting 
			if (Config.ReadyPlayers >= Config.NumberOfPlayers) {
				var p = Instantiate(_shadow, transform.position, transform.rotation);
				p.GetComponent<PlayerController>().PlayerNumber = _playerNum;
				p.transform.Find("Profile Camera").GetComponent<Camera>().targetTexture = _texture;				
				Config.StartGame();
			} else {
				var p = Instantiate(_player, transform.position, transform.rotation);
				p.GetComponent<PlayerController>().PlayerNumber = _playerNum;
				p.transform.Find("Profile Camera").GetComponent<Camera>().targetTexture = _texture;
			}

            _healthBar.SetActive(true);

			Destroy(gameObject);
		}
	}
}
