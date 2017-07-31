using UnityEngine;
using System.Collections;

public class ChPFlag : MonoBehaviour {

	public Player player;

	void OnMouseDown()
	{
		if (PlayerHandler.instance.currentPlayer==player)
			Debug.Log ("It's your flag " + player.myName + ".");
		else
			Debug.Log (player.myName + "'s flag.");
	}
}
