using UnityEngine;
using System.Collections;

public class ChPFlag : MonoBehaviour {

	public Player player;

	void OnMouseUp()
	{
		if (PlayerHandler.instance.currentPlayer == player) {
			Debug.Log ("It's your flag " + player.myName + ".");
			gameObject.GetComponentInParent<CharacterPanelHandler> ().OpenChChoosers ();
		}
		else
			Debug.Log (player.myName + "'s flag.");
	}
}
