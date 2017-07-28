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
		if (Input.GetMouseButtonUp (0)) {
			MapHandler.instance.chosenField.AddRoad (direction);
			BaseConquering.ConquerCheck ();
		}
	}

	protected override bool CanBuild (Field where)
	{
		return !where.HasPart(Field.RoadName(direction));
	}
}
