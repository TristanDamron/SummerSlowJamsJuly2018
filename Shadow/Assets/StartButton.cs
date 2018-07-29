using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using UnityEngine.UI;

public class StartButton : Selectable {
	public BaseEventData data;
	[SerializeField]
	private RawImage _image;
	[SerializeField]
	private VideoClip _activeClip;
	[SerializeField]
	private VideoClip _highlightedClip;
	private bool _fall;
	[SerializeField]
	private GameObject _instructions;

	[SerializeField]
	private Texture _idleTexture;

	void Start () {
		Config.Paused = true;
		GetComponent<RawImage>().texture = _idleTexture;
	}

	public void OnHighLight() {
		if (!GetComponent<StreamVideo>().enabled)
			GetComponent<StreamVideo>().enabled = true;
		GetComponent<StreamVideo>().Reset(_highlightedClip);
	}

	public void OnNotHighLight() {
		GetComponent<StreamVideo>().enabled = false;
		GetComponent<RawImage>().texture = _idleTexture;
	}

	public void StartGame() {
		GetComponent<StreamVideo>().Reset(_activeClip);
		SoundManager.Inst.PlaySound(SoundManager.Inst.ButtonPressClip);		
		StartCoroutine(CleanUp());
	}	

	public void ShowInstructions() {
		_instructions.SetActive(true);
	}

	IEnumerator CleanUp() {
		yield return new WaitForSeconds(1f);		
		_image.gameObject.SetActive(false);
		gameObject.SetActive(false);
		Config.Paused = false;		
	}

	public void ReadyButton() {
		SoundManager.Inst.PlaySound(SoundManager.Inst.ButtonPressClip);		
		Config.Paused = false;		
		_fall = true;
	}

	public void ShowCredits() {
		_credits.SetActive(true);
	}

	void Update() {
		if (IsHighlighted(data) == true) {			
			OnHighLight();
		} else {
			OnNotHighLight();
		}
	}
}
