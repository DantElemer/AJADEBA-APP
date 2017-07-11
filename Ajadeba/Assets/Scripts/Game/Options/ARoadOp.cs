using UnityEngine;
using System.Collections;

public class ARoadOp : MonoBehaviour {

	public string direction;

	void OnMouseOver()
	{
		if (Input.GetMouseButtonUp (0))
			MapHandler.instance.fields [MapHandler.instance.chosenField].addRoad (direction);
	}
}
