using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour {
	[SerializeField]
	private float _timer;
	[SerializeField]
	private TMP_Text _timerText;
	[SerializeField]
	private float _delay;
	private int _hours;
	private int _minutes; 
	
	void Update () {
		if (!Config.Paused) {
			_timer += Time.deltaTime;
			if (_timer > _delay) {
				_minutes++;
				if (_minutes >= 60f) {
					_hours++;
					_minutes = 0;
				}

				var strHours = _hours.ToString();
				var strMinutes = _minutes.ToString();

				if (strHours.Length < 2) {
					strHours = "0" + _hours.ToString();
				} 
				
				if (strMinutes.Length < 2) {
					strMinutes = "0" + _minutes.ToString();
				}

				_timerText.text = strHours + ":" + strMinutes;
				_timer = 0f;
			}
		}
	}
}
