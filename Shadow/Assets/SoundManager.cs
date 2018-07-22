using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	public static SoundManager Inst;
	private AudioSource source;

	[Header("Set in Unity Inspector")]
	public AudioClip ShootClip;
	public AudioClip BulletDiesClip;

	private void Awake() {
		source = gameObject.GetComponent<AudioSource>();
		Inst = this;
	}

	public void PlaySound(AudioClip sound) {
		source.PlayOneShot(sound);
	}
}
