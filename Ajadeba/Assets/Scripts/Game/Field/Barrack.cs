using UnityEngine;
using System.Collections;

public class Barrack : FieldPart {

	public Player owner;

	public void Inic()
	{
		//setting flag
		GameObject flag = gameObject.transform.Find ("Flag").gameObject;
		SpriteRenderer flagSR = flag.GetComponent<SpriteRenderer> ();
		flagSR.sprite = owner.flag;
	}

	public int strength
	{
		get
		{
			return 1; //TODO ...
		}
	}
}
