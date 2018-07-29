using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour {
	[SerializeField]
	private VideoPlayer _player;
	[SerializeField]
	private RawImage _image;
	[SerializeField]
	private bool _autoStart;

	void Start() {
		if (_autoStart) {
			StartCoroutine(PlayVideo());
		}
	}
	
	IEnumerator PlayVideo() {
		WaitForSeconds wait = new WaitForSeconds(1f);
		while (!_player.isPrepared) {
			yield return wait;
			break;
		}
		
		_image.texture = _player.texture;
		_player.Play();
	}

	public void Reset(VideoClip _clip) {
		_player.clip = _clip;
		StartCoroutine(PlayVideo());
	}
}
