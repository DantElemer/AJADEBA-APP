using UnityEngine;
using System.Collections;

public class BJB : Character {


	const string RESTING = "resting";
	const string CHOOSING_STRONG = "choosing stronghold";
	const string CHOOSING_TER = "choosing territoy";
	const string CHOOSING_AIM = "choosing aim field";
	string actingStatus;
	Field fromField;

	public BJB()
	{
		cost = 4;
		name = BJB;
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
		StartActing ();
	}

	public void StartActing()
	{
		actingStatus = CHOOSING_STRONG;
		Selection.SelectionTime (Selection.MY_STRONGS);
		MapHandler.instance.BJBing ();
	}

	public bool ContinueActing()
	{
		if (actingStatus == CHOOSING_STRONG) {
			actingStatus = CHOOSING_TER;
			Selection.SelectionTimeOver (Selection.MY_STRONGS);
			Selection.SelectionTime (Selection.STRONG_TER);
			return true;
		} else if (actingStatus == CHOOSING_TER) {
			fromField = MapHandler.instance.chosenField;
			actingStatus = CHOOSING_AIM;
			Selection.SelectionTimeOver (Selection.STRONG_TER);
			Selection.SelectionTime (Selection.BORDERS);
			return true;
		}
		else if (actingStatus == CHOOSING_AIM) { //TODO territory modifying
			fromField.RemoveOwner(myPlayer);
			fromField.DestroyIfMust ();
			MapHandler.instance.chosenField.AddOwner (myPlayer);
			MapHandler.instance.chosenField.DestroyIfMust ();
			actingStatus = RESTING;
			Selection.SelectionTimeOver (Selection.BORDERS);
			return false;
		}
		return false;
	}

}
