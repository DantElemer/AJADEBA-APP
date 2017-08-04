using UnityEngine;
using System.Collections;

public class ARoadOp : BuildOption {

	public string direction;

	void Start()
	{
		scale = 1;
	}

	protected override void BuildIt ()
	{
		MapHandler.instance.chosenField.AddRoad (direction);
		if (PlayerHandler.instance.currentPlayer.roadBoost > 0) //use roadboost if has
			PlayerHandler.instance.currentPlayer.roadBoost --;
		else
			PlayerHandler.instance.currentPlayer.stepsLeft --;

		base.BuildIt ();
	}

	protected override bool CanBuild (Field where)
	{
		return !where.HasPart(Field.RoadName(direction));
	}
}
