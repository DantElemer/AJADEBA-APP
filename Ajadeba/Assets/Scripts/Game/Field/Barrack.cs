﻿using UnityEngine;
using System.Collections;

public class Barrack : MonoBehaviour {

	public Player owner;

	Field myField 
	{
		get {
			return (transform.parent.gameObject.GetComponent<Field>() as Field);
		}
	}

	/*public Barrack(Player owner)
		: owner (owner
	{

	}*/

	public void Inic()
	{
		//setting flag
		GameObject flag = gameObject.transform.Find ("Flag").gameObject;
		SpriteRenderer flagSR = flag.GetComponent<SpriteRenderer> ();
		flagSR.sprite = owner.flag;

		Debug.Log (myField.index);
	}

	public int strength
	{
		get
		{
			return 1; //TODO ...
		}
	}
}
