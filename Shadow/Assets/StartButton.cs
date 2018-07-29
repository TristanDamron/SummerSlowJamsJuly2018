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
	private bool _fall;
	[SerializeField]
	private GameObject _instructions;
	[SerializeField]
	private bool _start;
	[SerializeField]
	private bool _ready;

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
		_image.gameObject.SetActive(false);
		_instructions.SetActive(true);
		gameObject.SetActive(false);		
	}

	public void ReadyButton() {
		SoundManager.Inst.PlaySound(SoundManager.Inst.ButtonPressClip);		
		Config.Paused = false;		
		_fall = true;
	}

	void Update() {
		if (Input.GetAxis("Submit") != 0f) {
			if (_ready) {
				ReadyButton();
			} else if (_start) {
				StartGame();
			}
		}

		if (_fall) {
			transform.parent.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -800, transform.position.z), Time.deltaTime);
			if (transform.parent.position.y <= -800) {
				transform.parent.gameObject.SetActive(false);
			}
		}
	}
}
