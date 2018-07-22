﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
	[Header("GameObjects set in Unity Inspector")]
	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
  private Slider _energySlider;

	[Header("Player information")]
	public bool IsShadow;
	public int PlayerNumber; // 1-indexed
	public float hp;

	private float _energy = 3f;
	[SerializeField]
	private int numberOfBullets;
	[SerializeField]
	private GameObject _shadowWeapon;
	private Slider _healthBar;


	// Use this for initialization
	void Start () {
		hp = 5;

		if (PlayerNumber < 1 || PlayerNumber > Config.NumberOfPlayers) {
			Debug.LogError(
				"The max number of players is " + 
				Config.NumberOfPlayers + 
        " and player numbers start at 1"
			);
		}

		_healthBar = GameObject.Find("HealthPlayer" + PlayerNumber).GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		_healthBar.value = hp;

		if (hp <= 0f) {
			Destroy(gameObject);
		}

		_energySlider.value = _energy;
		var xVelocity = Input.GetAxis("HorizontalPlayer" + PlayerNumber);
		var yVelocity = Input.GetAxis("VerticalPlayer" + PlayerNumber);
		var sprint = Input.GetAxisRaw("Sprint" + PlayerNumber);

		gameObject.GetComponent<Rigidbody>().velocity = new Vector3(
			xVelocity * Config.MovementSpeed * ((sprint * 2) + 1) * _energy,
      		0,
			yVelocity * Config.MovementSpeed * ((sprint * 2) + 1) * _energy
		);

		if (sprint != 0f && _energy > 0f) {
			_energy -= Time.deltaTime;
		} else if (_energy < 3f){
			_energy += Time.deltaTime;
		}

		Vector3 direction = (Vector3.right * xVelocity) + (Vector3.forward * yVelocity);
		if (direction != Vector3.zero) {
      transform.rotation = Quaternion.LookRotation(direction);
		}

		if (Input.GetButtonDown("ShootPlayer" + PlayerNumber) && IsShadow) {
      		Swipe();
    	}		
		else if (Input.GetButtonDown("ShootPlayer" + PlayerNumber)) {
      		Shoot();
    	}
	}
       
	void Shoot() {
		if (BulletPickupManager.Inst.DecrementNumberBullets()) {
  		var bullet = GameObject.Instantiate(bulletPrefab, transform);
      
  		bullet.transform.localPosition = new Vector3(0, 2, 3);
  		bullet.transform.SetParent(null);

  		bullet.GetComponent<Rigidbody>().velocity = (transform.forward * Config.BulletSpeed);

  		Destroy(bullet, 2.0f);

  		SoundManager.Inst.PlaySound(SoundManager.Inst.ShootClip);
		} else {
			SoundManager.Inst.PlaySound(SoundManager.Inst.NoBulletsClip);
		}
	}

	void Swipe() {
		_shadowWeapon.SetActive(true);
		StartCoroutine(SheathWeapon());
	}

	IEnumerator SheathWeapon() {
		yield return new WaitForSeconds(0.1f);		
		_shadowWeapon.SetActive(false);				
	}

	void OnCollisionEnter(Collision c) {
		if (IsShadow && c.gameObject.name == "Bullet") {
			hp -= 1f;
		}
	}
}
