using UnityEngine;
using System.Collections;

public class ChPFlag : MonoBehaviour {

	public Player player;

	void OnMouseUp()
	{
		if (PlayerHandler.instance.currentPlayer == player) {
			Debug.Log ("It's your flag " + player.myName + ".");
			if (PlayerHandler.instance.currentPlayer.stepsLeft == PlayerHandler.instance.currentPlayer.maxSteps)
				gameObject.GetComponentInParent<CharacterPanelHandler> ().OpenChChoosers ();
			else
				Debug.Log ("Choosing a new character requires a full turn!");
		}
		else
			Debug.Log (player.myName + "'s flag.");
	}
}
