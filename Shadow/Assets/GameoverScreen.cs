using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverScreen : MonoBehaviour {
	void Update () {		
		transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0f, 0f, 0f), Time.deltaTime * 4f);

		if (Input.GetAxisRaw("Submit") != 0f) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);			
		} else if (Input.GetAxisRaw("Cancel") != 0) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
 	}
}
