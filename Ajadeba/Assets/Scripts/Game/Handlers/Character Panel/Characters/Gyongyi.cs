using UnityEngine;
using System.Collections;

public class Gyongyi : Character {

	public Gyongyi()
	{
		cost = 5;
		name = GYONGYI;
	}

	public override void AddedToPlayer(Player toWhom)
	{
		base.AddedToPlayer (toWhom);
		myPlayer.mainStrong.myField.AddVillage ();
	}
}
