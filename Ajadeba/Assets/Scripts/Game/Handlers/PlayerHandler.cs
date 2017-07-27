using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour {

	private static PlayerHandler sInstance;

	public Text txtCurrPlayer;
	public Player playerPref;
	public Player[] players;
	public Player currentPlayer;
	int currPlayer;


	public static PlayerHandler instance //singleton magic
	{
		get 
		{
			if (sInstance == null)
				sInstance = FindObjectOfType (typeof(PlayerHandler)) as PlayerHandler;
			if (sInstance == null)
				sInstance = new PlayerHandler ();
			return sInstance;
		}
	}


	void Start () 
	{
		CreatingPlayers ();
		ActivatePlayer (0);
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.N))
			NextPlayer ();
	}
	
	void NextPlayer()
	{
		DeactivatePlayer ();
		ActivatePlayer (FindNextAlive (currPlayer));
		SceneManager.LoadScene ("NewTurn", LoadSceneMode.Additive);
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
		currentPlayer = players [currPlayer];
		currentPlayer.SetActive (true);
		txtCurrPlayer.text = "Current player: " + currentPlayer.myName;
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
		firstPlayer.nation = "Test1";
		secondPlayer.name = "Second Player";
		secondPlayer.myName = "Balázs";
		secondPlayer.nation = "Test2";

		players [0] = firstPlayer;
		players [1] = secondPlayer;
	}

	public bool areEnemies(Player one, Player two)
	{
		return one != two;
	}
}
