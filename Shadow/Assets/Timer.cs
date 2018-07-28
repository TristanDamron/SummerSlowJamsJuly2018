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

	void Start() {
		_hours = 12;
		_minutes = 1;
	} 
	
	void Update () {
		if (Config.ReadyPlayers >= Config.NumberOfPlayers) {
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

			if ((_hours >= 3 && _hours != 12) && _minutes >= 33) {
				Config.EndGame(true);
			}

			if (_hours > 12) {
				_hours = 1;
			}
		}
	}
}
