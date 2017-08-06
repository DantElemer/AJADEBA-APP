using UnityEngine;
using System.Collections;

public class Character { //TODO esetleg: különböző tulajdonságok kezelésére alosztályok (végén bónuszt adó, kiválasztáskor bónuszt adó, fejlődő...)

	public int cost = 0; //number of turns you have to wait until choosing an other if you choose this one
	public int level = 1; //Kőműves, Rádiusz... fejlődnek, később talán a többiek is
	public bool hasTurnEndBoost = false; //(still) has it...
	int turnsSpent = 0; //added to a player
	protected int evolveTime = 5; //number of turns needed for levelup
	public string name = "Anonymus"; 
	protected Player myPlayer; //the player to which it's added

	public const string MASON = "Kőműves";
	public const string PROLIFIC = "Szapora";
	public const string ROMAN = "Római";
	public const string GYONGYI = "Gyöngyi";
	public const string TRESPASSER = "Birtokháborító";
	public const string STAKHANOVITE = "Sztahanovista";
	public const string BJB = "BJB";

	public virtual void AddedToPlayer(Player toWhom) //fired right after it's attached to a player
	{
		myPlayer = toWhom;
		Debug.Log ("Hi man");
	}

	protected virtual void Evolve()
	{
		level++;
	}

	public virtual void TurnPassed () //fired when its player finished its turn
	{
		turnsSpent++;
		if (turnsSpent % evolveTime == 0)
			Evolve ();
	}

	public virtual void UseUpTurnEndBoost() //fired when its player finished "normal" acts
	{
		hasTurnEndBoost = false;
	}
}
