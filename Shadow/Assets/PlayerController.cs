using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
	[Header("GameObjects set in Unity Inspector")]
	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
	private Material _energyRadial;

	[Header("Player information")]
	public bool IsShadow;
	public int PlayerNumber; // 1-indexed
	public float hp;

	private float _energy = 3f;
	[SerializeField]
	private int numberOfBullets;
	[SerializeField]
	private GameObject _shadowWeapon;
	[SerializeField]
	private Animator _animator;
	private Slider _healthBar;
	private float _boost;
	private bool _canAttack;
	public bool frozen;


	// Use this for initialization
	void Start () {
		if (GetComponent<Animator>() != null)
			_animator = GetComponent<Animator>();

		hp = 5;

		if (PlayerNumber < 1 || PlayerNumber > Config.NumberOfPlayers) {
			Debug.LogError(
				"The max number of players is " + 
				Config.NumberOfPlayers + 
        " and player numbers start at 1"
			);
		}

		_healthBar = GameObject.Find("HealthPlayer" + PlayerNumber).GetComponent<Slider>();
		
		if (IsShadow)
			_boost = 3f;
		else 
			_boost = 1f;
		
		_canAttack = true;
		frozen = false;
		_energyRadial.SetFloat("_Fill", 0f); 
	}
	
	// Update is called once per frame
	void Update () {
		_healthBar.value = hp;

		if (hp <= 0f) {
			Destroy(gameObject);
		}

		var xVelocity = Input.GetAxisRaw("HorizontalPlayer" + PlayerNumber);
		var yVelocity = Input.GetAxisRaw("VerticalPlayer" + PlayerNumber);
		var sprint = Input.GetAxis("Sprint" + PlayerNumber);

		if (frozen) {
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		}

		if (!IsShadow && !frozen) {
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(
				xVelocity * Config.MovementSpeed * _boost,
				0,
				yVelocity * Config.MovementSpeed * _boost
			);
		} else if (IsShadow) {
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(
				xVelocity * Config.ShadowMovementSpeed,
      			0,
				yVelocity * Config.ShadowMovementSpeed
			);
		}

		if ((xVelocity != 0f || yVelocity != 0f) && !frozen) {
			_animator.Play("walking");
		} else {
			_animator.Play("default");
		}

		if (!IsShadow) {
			_energyRadial.SetFloat("_Fill", _energy / 3f); 
			if (sprint != 0f && _energy >= 3f) {
				_energy = 0f;
				_boost = 3f;
			} else if (_energy < 3f) {
				_energy += Time.deltaTime;
			} else {
				_boost = 1f;
				_energyRadial.SetFloat("_Fill", 0f); 
			}
		}

		Vector3 direction = (Vector3.right * xVelocity) + (Vector3.forward * yVelocity);
		if (direction != Vector3.zero) {
      		transform.rotation = Quaternion.LookRotation(direction);
		}

		if (Input.GetButtonDown("ShootPlayer" + PlayerNumber) && IsShadow && _canAttack) {
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
		_canAttack = false;
		_shadowWeapon.SetActive(true);
		StartCoroutine(SheathWeapon());
	}

	IEnumerator SheathWeapon() {
		yield return new WaitForSeconds(0.1f);		
		_shadowWeapon.SetActive(false);	
		StartCoroutine(CheckAttack());			
	}

	IEnumerator CheckAttack() {
		yield return new WaitForSeconds(1f);
		_canAttack = true;
	}

    private void OnDestroy() {
        CameraAdjuster.ScanForPlayersAndShadow();
    }
	
	void OnTriggerEnter(Collider c) {
		if (!IsShadow && c.tag == "Shadow") {
			if (!frozen) {
				Config.FrozenKids++;			
				hp -= 1f;
			}

			frozen = true;	
			GetComponent<ParticleSystem>().Play();		
		}

	}

	void OnCollisionEnter(Collision c) {
		if (!IsShadow && c.gameObject.tag == "Player") {
			if (frozen)
				Config.FrozenKids--;		
			frozen = false;
			GetComponent<ParticleSystem>().Clear();			
			GetComponent<ParticleSystem>().Stop();	
			
		}

	}
}
