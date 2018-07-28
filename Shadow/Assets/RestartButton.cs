using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {
	public void Restart() {
		SoundManager.Inst.PlaySound(SoundManager.Inst.ButtonPressClip);				
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
