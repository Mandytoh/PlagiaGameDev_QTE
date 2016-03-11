using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputManager : MonoBehaviour {

	public Player player;
	public RectTransform scoreUI;
	public RectTransform qteUI;

	public QTEManager qteManager;

	private string waitingInput;

	public bool isWaitingForInput = false;

	public void ChangeWaitingInput(string newInput){
		this.waitingInput = newInput;
		this.isWaitingForInput = true;
	}

	public void addScore(float newScore){
		float totalScore = this.player.score + newScore;
		this.player.score = totalScore;

		string totalScoreTxt = totalScore.ToString ("0000000000");
		this.scoreUI.GetComponent<Text> ().text = totalScoreTxt;
	}

	void Update(){
		if(this.isWaitingForInput){
			if (Input.anyKeyDown) {
				Debug.Log ("Ah bah enfin ! Tu as cliqué sur : " + Input.inputString.ToString());
				if (this.waitingInput == Input.inputString.ToString()) {
					this.qteManager.Validate (true);
					this.isWaitingForInput = false;
				} else {
					this.qteManager.Validate (false);
					this.isWaitingForInput = false;
				}
			}
		}
	}

}
