using UnityEngine;
using System.Collections;

public class StrongBaseOp : BuildOption {

	protected override bool CanBuild (Field where)
	{
		return base.CanBuild(where) && !where.HasOtherOwner(null);
	}

	protected override void BuildIt ()
	{
		MapHandler.instance.chosenField.AddStrongBase (PlayerHandler.instance.currentPlayer);
		base.BuildIt ();
	}
}
