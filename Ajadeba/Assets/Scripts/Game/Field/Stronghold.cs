using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
		for (int i = myField.xCoord - 3; i <= myField.xCoord + 3; i++)
			for (int j = myField.yCoord - 3; j <= myField.yCoord + 3; j++)
				if (MapHandler.instance.InMap (i, j))
					if (Math.Abs(myField.xCoord-i)+Math.Abs(myField.yCoord-j)<=3)
							MapHandler.instance.fields [i] [j].addOwner (owner);
	}

	public int strength
	{
		get
		{
			return 1; //TODO ...
		}
	}
}
