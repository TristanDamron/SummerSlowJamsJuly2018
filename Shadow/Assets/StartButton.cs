using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {
	[SerializeField]
	private RawImage _image;
	[SerializeField]
	private VideoClip _active;

	void Start () {
		Config.Paused = true;
	}

	public void StartGame() {
		GetComponent<StreamVideo>().Reset(_active);
		SoundManager.Inst.PlaySound(SoundManager.Inst.ButtonPressClip);		
		StartCoroutine(CleanUp());
	}	

	IEnumerator CleanUp() {
		yield return new WaitForSeconds(5f);		
		Config.Paused = false;
		_image.gameObject.SetActive(false);
		gameObject.SetActive(false);

	}
}
