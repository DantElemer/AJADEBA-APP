using UnityEngine;
using System.Collections;

public class StrongBaseOp : BuildOption {

	void OnMouseOver()
	{
		if (Input.GetMouseButtonUp (0)) {
			MapHandler.instance.chosenField.AddStrongBase (PlayerHandler.instance.currentPlayer);
			BaseConquering.ConquerCheck ();
		}
	}

	protected override bool CanBuild (Field where)
	{
		return base.CanBuild(where) && !where.HasOtherOwner(null);
	}
}
