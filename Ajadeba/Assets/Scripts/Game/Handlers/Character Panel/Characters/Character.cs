using UnityEngine;
using System.Collections;

public class Character {

	public int cost = 0; //number of turns you have to wait until choosing an other if you choose this one
	public int level = 1;
	int turnsSpent = 0;
	protected int evolveTime = 5;
	public string name = "Anonymus";
	protected Player myPlayer;

	public const string MASON = "Mason";
	public const string PROLIFIC = "Prolific";
	public const string ROMAN = "Roman";
	public const string GYONGYI = "Gyöngyi";

	public virtual void AddedToPlayer(Player toWhom)
	{
		myPlayer = toWhom;
		Debug.Log ("Hi man");
	}

	protected virtual void Evolve()
	{
		level++;
	}

	public void TurnPassed ()
	{
		turnsSpent++;
		if (turnsSpent % evolveTime == 0)
			Evolve ();
	}
}
