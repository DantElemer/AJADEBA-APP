using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public Stronghold mainStrong; //the player's first stronghold (set in Field/AddStronghold)
	public string myName;
	public string nation;
	public Sprite flag;

	public int maxSteps = 4;
	public int stepsLeft; 
	public int roadBoost;

	bool active = false;
	bool alive = true;

	List<Character> myChars = new List<Character> (); //watch out, you need casting for specified stuff!


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

	public bool IsAlive()
	{
		return alive;
	}

	public bool HasChar(string character)
	{
		foreach (Character c in myChars)
			if (c.name == character)
				return true;
		return false;
	}

	public Character MyChar(string which)
	{
		foreach (Character c in myChars)
			if (c.name == which)
				return c;
		return null; //doesn't have such character
	}
	public int CharLevel (string character)
	{
		foreach (Character c in myChars)
			if (c.name == character)
				return c.level;
		return 0; //if player doesn't have the character
	}

	public void AddCharacter(Character newCh)
	{
		myChars.Add(newCh);
		newCh.AddedToPlayer (this);
		Debug.Log (newCh.name + " added to " + myName);
		PlayerHandler.instance.NextPlayer ();
	}

	public void TurnFinished()
	{
		foreach (Character c in myChars)
			c.TurnPassed ();
	}

	public void FinishedBuilding()
	{
		if (HasChar (Character.STAKHANOVITE))
			if (MyChar (Character.STAKHANOVITE).hasTurnEndBoost) {
				stepsLeft++;
				MyChar (Character.STAKHANOVITE).hasTurnEndBoost = false;
			}
			else
				PlayerHandler.instance.NextPlayer ();
		else
			PlayerHandler.instance.NextPlayer ();
	}

}
