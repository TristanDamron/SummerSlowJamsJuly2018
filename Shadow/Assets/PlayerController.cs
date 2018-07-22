using System.Collections;
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

	private float _energy = 3f;
	[SerializeField]
	private int numberOfBullets;

	// Use this for initialization
	void Start () {
		if (PlayerNumber < 1 || PlayerNumber > Config.NumberOfPlayers) {
			Debug.LogError(
				"The max number of players is " + 
				Config.NumberOfPlayers + 
        " and player numbers start at 1"
			);
		}
	}
	
	// Update is called once per frame
	void Update () {
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

		if (Input.GetButtonDown("ShootPlayer" + PlayerNumber)) {
      Shoot();
    }
	}
       
	void Shoot() {
		var bullet = GameObject.Instantiate(bulletPrefab, transform);
    
		bullet.transform.localPosition = new Vector3(0, 2, 3);
		bullet.transform.SetParent(null);

		bullet.GetComponent<Rigidbody>().velocity = (transform.forward * Config.BulletSpeed);

		Destroy(bullet, 2.0f);

		SoundManager.Inst.PlaySound(SoundManager.Inst.ShootClip);
	}
}
