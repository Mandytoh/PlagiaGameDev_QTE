using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerManager : MonoBehaviour {

	public RectTransform timerUI;
	public float timer;
	public float timerInSeconds;

	public bool isActive = true;

	// Update is called once per frame
	void Update () {
		if (isActive) {
			this.timer += Time.deltaTime;
			this.timerInSeconds = Mathf.Floor (timer % 60);
				
			string seconds = this.timerInSeconds.ToString ("000");
			timerUI.GetComponent<Text> ().text = seconds;
		}
	}
}
