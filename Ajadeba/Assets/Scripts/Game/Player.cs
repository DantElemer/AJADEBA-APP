using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public Stronghold mainStrong; //the player's first stronghold (set in Field/AddStronghold)
	public string myName;
	public string nation;
	public Sprite flag { get; private set;}

	public int maxSteps = 4; //basically how many roads he can build in a turn
	public int stepsLeft; 
	public int roadBoost; //second player may get 1 or 2 extra roads in the fist round (for balancing)

	bool active = false; //has setter, getter
	public bool alive { get; private set; }

	List<Character> myChars = new List<Character> (); //watch out, you need casting for specified stuff!

	void Start()
	{
		alive = true; 
	}

	public void SetFlag()
	{
		flag = Resources.Load<Sprite> ("Nations/"+nation+"/Flag");
		if (flag == null)
			Debug.Log ("gebasz a zászló betöltésénél");
	}

	public void SetActive (bool act)
	{
		active = act;
		if (active) { //activated => new turn => new steps! :)
			stepsLeft = maxSteps;
			roadBoost = 0;
		}
	}

	public bool IsActive ()
	{
		return active;
	}

	public bool HasChar(string character) //searches by name
	{
		foreach (Character c in myChars)
			if (c.name == character)
				return true;
		return false;
	}

	public Character MyChar(string which) //searches by name, returns null if hasn't got such char
	{
		foreach (Character c in myChars)
			if (c.name == which)
				return c;
		return null; //doesn't have such character
	}

	public int CharLevel (string character) //searches by name, returns 0 if hasn't got such char
	{
		foreach (Character c in myChars)
			if (c.name == character)
				return c.level;
		return 0; //if player doesn't have the character
	}

	public void AddCharacter(Character newCh) //yay!
	{
		myChars.Add(newCh);
		newCh.AddedToPlayer (this);
		Debug.Log (newCh.name + " added to " + myName);
		if (!(newCh is Prolific))
			MayFinishedTurn ();
	}

	public void TurnFinished()
	{
		foreach (Character c in myChars)
			c.TurnPassed ();
	}


	//TODO bármilyen sorrendben használhassa a boostjait?
	public void FinishedBuilding()
	{
		if (HasChar (Character.STAKHANOVITE))
			if (MyChar (Character.STAKHANOVITE).hasTurnEndBoost) //it's here because stakhanovite's boost is available only after building
				MyChar (Character.STAKHANOVITE).UseUpTurnEndBoost ();
			else
				MayFinishedTurn ();
		else
			MayFinishedTurn ();
	}

	public void MayFinishedTurn() //only turn end boosts left
	{
		stepsLeft = 0;
		bool hasBoost = false;

		if (HasChar (Character.BJB))
		if (MyChar (Character.BJB).hasTurnEndBoost) {
			MyChar (Character.BJB).UseUpTurnEndBoost ();
			hasBoost = true;
		}

		if (!hasBoost)
			PlayerHandler.instance.NextPlayer ();
	}

}
