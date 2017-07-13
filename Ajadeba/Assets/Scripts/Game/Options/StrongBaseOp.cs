using UnityEngine;
using System.Collections;

public class StrongBaseOp : BuildOption {

	void OnMouseOver()
	{
		if (Input.GetMouseButtonUp (0)) 
			MapHandler.instance.chosenField.addStrongBase ();
	}

	protected override bool CanBuild (Field where)
	{
		return base.CanBuild(where) && !where.hasOtherOwner(null);
	}
}
