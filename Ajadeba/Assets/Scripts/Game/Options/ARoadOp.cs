using UnityEngine;
using System.Collections;

public class ARoadOp : BuildOption {

	public string direction;

	void Start()
	{
		scale = 1;
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonUp (0))
			MapHandler.instance.chosenField.addRoad (direction);
	}

	protected override bool CanBuild (Field where)
	{
		return !where.hasPart(Field.RoadName(direction));
	}
}
