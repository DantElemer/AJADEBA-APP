using UnityEngine;
using System.Collections;

public class Prolific : Character //Szapora
{
	public Prolific()
	{
		cost = 5;
		name=PROLIFIC;
	}

	public override void AddedToPlayer (Player toWhom)
	{
		MapHandler.instance.Prolification ();
	}
}
