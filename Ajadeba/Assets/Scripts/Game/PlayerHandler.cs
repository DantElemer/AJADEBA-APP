using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour {

	public Text txtCurrPlayer;
	public Player playerPref;
	Player[] players;
	int currPlayer;

	// Use this for initialization
	void Start () 
	{
		CreatingPlayers ();
		ActivatePlayer (0);
		NextPlayer (); NextPlayer ();
	}
	
	void NextPlayer()
	{
		DeactivatePlayer ();
		ActivatePlayer (FindNextAlive (currPlayer));
	}

	int FindNextAlive (int index)
	{
		int nextPlayer = index + 1;
		if (nextPlayer < players.Length)
		if (players [nextPlayer].IsAlive ())
			return nextPlayer;
		else 
			return FindNextAlive (nextPlayer);
		else //it was the last
			return FindNextAlive (-1);
	}

	void ActivatePlayer(int itsIndex)
	{
		currPlayer = itsIndex;
		players [itsIndex].SetActive (true);
		Debug.Log (currPlayer.ToString () + ". player's turn!");
		txtCurrPlayer.text = "Current player: " + players [currPlayer].myName;
	}

	void DeactivatePlayer() //the current one
	{
		players [currPlayer].SetActive (false);
	}

	void CreatingPlayers()
	{
		players = new Player[2]; //TODO: number of players can depend on Settings.

		Player firstPlayer = Instantiate (playerPref);
		Player secondPlayer = Instantiate (playerPref);

		firstPlayer.name = "First Player";
		firstPlayer.myName = "Domi";
		secondPlayer.name = "Second Player";
		secondPlayer.myName = "Balázs";

		players [0] = firstPlayer;
		players [1] = secondPlayer;
	}
}
