using UnityEngine;
using System.Collections;

public class Stakhanovite : Character { //sztahanovista

	public Stakhanovite()
	{
		cost = 6;
		name = STAKHANOVITE;
		hasTurnEndBoost = true;
	}

	public override void TurnPassed ()
	{
		base.TurnPassed ();
		hasTurnEndBoost = true;
	}

	public override void UseUpTurnEndBoost ()
	{
		base.UseUpTurnEndBoost ();
		myPlayer.stepsLeft++;
	}
}
