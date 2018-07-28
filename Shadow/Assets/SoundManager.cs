using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	public static SoundManager Inst;
	private AudioSource source;

	[Header("Set in Unity Inspector")]
	public AudioClip ShootClip;
	public AudioClip BulletDiesClip;
	public AudioClip NoBulletsClip;
	public AudioClip PickupClip;
	public AudioClip PlayerFrozenClip;
	public AudioClip PlayerRevivedClip;
	public AudioClip ShadowHitClip;
	public AudioClip ShadowDiedClip;
	public AudioClip ButtonPressClip;

	private void Awake() {
		source = gameObject.GetComponent<AudioSource>();
		Inst = this;
	}

	public void PlaySound(AudioClip sound) {
		source.PlayOneShot(sound);
	}
}
