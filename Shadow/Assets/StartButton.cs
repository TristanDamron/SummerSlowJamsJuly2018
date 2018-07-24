using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {
	[SerializeField]
	private RawImage _image;

	void Start () {
		Config.Paused = true;
	}

	public void StartGame() {
		Config.Paused = false;
		_image.gameObject.SetActive(false);
		gameObject.SetActive(false);
	}	
}
