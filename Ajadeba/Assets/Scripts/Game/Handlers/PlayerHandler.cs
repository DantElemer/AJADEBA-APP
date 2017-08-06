using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour {

	private static PlayerHandler sInstance; //the only instance

	public Text txtCurrPlayer; //shows current player's name
	public Player playerPref;
	public Player[] players { get; private set; }
	public Player currentPlayer { get; private set; }
	public CharacterPanelHandler chPanel; 
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


	void Awake () 
	{
		CreatingPlayers ();
		ActivatePlayer (0);
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.N)) //cheat code :D
			NextPlayer ();
	}
	
	public void NextPlayer() //finishes current player's turn, and starts the next one's
	{
		currentPlayer.TurnFinished ();
		DeactivatePlayer ();
		ActivatePlayer (FindNextAlive (currPlayer));
		chPanel.TurnFinished (); //TODO nem ide tenni, hanem mindkettőt valamilyen központból hívni?
		SceneManager.LoadScene ("NewTurn", LoadSceneMode.Additive);
	}

	int FindNextAlive (int index) //finds the next player (not dead) and returns its index (in players); don't call when one player lives!
	{
		int nextPlayer = index + 1;
		if (nextPlayer < players.Length) 
			if (players [nextPlayer].alive)
				return nextPlayer;
			else 
				return FindNextAlive (nextPlayer);
		else //it was the last, so start from beginning
			return FindNextAlive (-1); //as (-1)+1=0, which is the start index
	}

	void ActivatePlayer(int itsIndex)
	{
		currPlayer = itsIndex;
		currentPlayer = players [currPlayer];
		currentPlayer.SetActive (true);
		txtCurrPlayer.text = "Current player: " + currentPlayer.myName;
	}

	void DeactivatePlayer() //the current one (there shouldn't be more active at once)
	{
		currentPlayer.SetActive (false);
	}

	void CreatingPlayers() 
	{
		players = new Player[2]; //TODO: number of players, their properties should depend on Settings.

		Player firstPlayer = Instantiate (playerPref);
		Player secondPlayer = Instantiate (playerPref);

		firstPlayer.name = "First Player";
		firstPlayer.myName = "Domi";
		firstPlayer.nation = "Test1";
		firstPlayer.SetFlag ();
		secondPlayer.name = "Second Player";
		secondPlayer.myName = "Balázs";
		secondPlayer.nation = "Test2";
		secondPlayer.SetFlag ();

		players [0] = firstPlayer;
		players [1] = secondPlayer;
	}

	public bool areEnemies(Player one, Player two)
	{
		return one != two; //if there will be teams, change this
	}
}
