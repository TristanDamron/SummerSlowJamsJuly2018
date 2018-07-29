using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Bumper : MonoBehaviour {
	private VideoPlayer _player;
	private float _playbackTime;		
	[SerializeField]
	private GameObject _next;

	void Start() {
		_player = GetComponent<VideoPlayer>();
	}

	void Update () {
		_playbackTime += Time.deltaTime;			
		if (_playbackTime >= _player.clip.length + 2f) {
			_next.SetActive(true);
			gameObject.SetActive(false);
		}		
	}
}
