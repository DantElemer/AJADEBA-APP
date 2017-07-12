﻿using UnityEngine;
using System.Collections;

public class BarrackOption : BuildOption {

	// Use this for initialization
	void Start () 
	{
		GameObject flag = gameObject.transform.Find ("Flag").gameObject;
		SpriteRenderer flagSR = flag.GetComponent<SpriteRenderer> ();
		flagSR.sprite = PlayerHandler.instance.currentPlayer.flag;
	}
	
	void OnMouseOver()
	{
		if (Input.GetMouseButtonUp (0)) 
			MapHandler.instance.chosenField.addBarrack (PlayerHandler.instance.currentPlayer);
	}
}
