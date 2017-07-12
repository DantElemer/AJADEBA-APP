using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stronghold : FieldPart {

	public Player owner;
	List<Field> territory = new List<Field> ();

	public void Inic()
	{
		SetFlag ();
		SetTerritory ();
	}

	void SetFlag()
	{
		GameObject flag = gameObject.transform.Find ("Flag").gameObject;
		SpriteRenderer flagSR = flag.GetComponent<SpriteRenderer> ();
		flagSR.sprite = owner.flag;
	}

	void SetTerritory()
	{

	}

	public int strength
	{
		get
		{
			return 1; //TODO ...
		}
	}
}
