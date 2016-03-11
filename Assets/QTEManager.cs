using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QTEManager : MonoBehaviour {

	public RectTransform qteUI;
	public TimerManager timerManager;
	public InputManager inputManager;

	public bool waitingInputIsSend = false;
	public bool isValid = false;

	public List<QTEData> listQTE = new List<QTEData> ();

	public int indexQTE = 0;

	public void ReinitQTEUI(){
		this.qteUI.GetComponent<Text> ().color = new Color (1, 1, 1);
	}

	public void ErrorQTEUI(){
		this.qteUI.GetComponent<Text> ().color = new Color (1, 0, 0);
	}

	public void ValidQTEUI(){
		this.qteUI.GetComponent<Text> ().color = new Color (0, 1, 0);
	}

	public void RemoveFirstQTE(){
		this.indexQTE++;
	}

	public void Validate(bool isValid){
		if (isValid) {
			this.ValidQTEUI ();
			this.isValid = true;

			Debug.Log ("Oui, c'est ça !");

			float totalAvailableTime = listQTE [indexQTE].timerEnd - listQTE [indexQTE].timerStart;
			float availableTime = listQTE [indexQTE].timerEnd - timerManager.timer;

			float score = Mathf.Round (listQTE [indexQTE].score * availableTime / totalAvailableTime);
			this.inputManager.addScore (score);

			Debug.Log ("Tu as gagné " + score + " points ");
		} else {
			this.ErrorQTEUI ();
			this.isValid = false;
			Debug.Log ("Tu te fous de ma gueule ?!");
		}
	}

	void Update(){
		if (indexQTE < listQTE.Count) {
			if (timerManager.timerInSeconds >= listQTE [indexQTE].timerStart && timerManager.timerInSeconds < listQTE [indexQTE].timerEnd) {
				if (!this.waitingInputIsSend) {
					//Mettre à jour l'UI QTE
					this.qteUI.GetComponent<Text> ().text = listQTE [indexQTE].input.ToUpper ();

					ReinitQTEUI ();

					//Envoyer l'ordre à l'InputManager
					this.inputManager.ChangeWaitingInput (listQTE [indexQTE].input);

					//Attendre l'input
					this.waitingInputIsSend = true;
					this.isValid = false;
					Debug.Log ("Lettre : " + listQTE [indexQTE].input);
				}

				if (timerManager.timerInSeconds == listQTE [indexQTE].timerEnd - 1) {
					//Mettre à jour l'UI QTE
					if (!this.isValid) {
						this.ErrorQTEUI ();
					}

					Debug.Log ("Etttt non, c'est fini !");

					//Remove l'ordre à l'InputManager
					this.inputManager.ChangeWaitingInput ("");

					//On n'attend plus d'input
					this.waitingInputIsSend = false;

					this.qteUI.GetComponent<Text> ().text = "";

					//Suppression du QTE en tête de liste
					RemoveFirstQTE ();
				}
			}
		} else {
			ReinitQTEUI ();
			this.qteUI.GetComponent<Text> ().text = "Bravo !";
			timerManager.isActive = false;
		}
	}
}

[System.Serializable]
public class QTEData {
	public float timerStart;
	public float timerEnd;
	public string input;
	public float score = 5000;
}