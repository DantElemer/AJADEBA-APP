using UnityEngine;
using System.Collections;

public class StrongBaseOp : MonoBehaviour {

	void OnMouseOver()
	{
		if (Input.GetMouseButtonUp (0)) 
			MapHandler.instance.chosenField.addStrongBase ();
	}
}
