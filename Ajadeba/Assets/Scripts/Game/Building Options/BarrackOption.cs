using UnityEngine;
using System.Collections;

public class BarrackOption : BuildOption {

	// Use this for initialization
	void Start () 
	{
		GameObject flag = gameObject.transform.Find ("Flag").gameObject;
		SpriteRenderer flagSR = flag.GetComponent<SpriteRenderer> ();
		flagSR.sprite = PlayerHandler.instance.currentPlayer.flag;
	}

	protected override void BuildIt ()
	{
		MapHandler.instance.chosenField.AddBarrack (PlayerHandler.instance.currentPlayer);
		base.BuildIt ();
	}
}
