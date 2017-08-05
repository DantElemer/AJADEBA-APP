using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Barrack : FieldPart {

	public Player owner;
	public bool isLazy = false;

	public void Inic()
	{
		//setting flag
		GameObject flag = gameObject.transform.Find ("Flag").gameObject;
		SpriteRenderer flagSR = flag.GetComponent<SpriteRenderer> ();
		flagSR.sprite = owner.flag;
		if (myField.IsOwner (owner))
			isLazy = true;
	}

	public int strength
	{
		get
		{
			int str=0;
			foreach (Field[] row in MapHandler.instance.fields)
				foreach (Field f in row)
					if (f!=null)
					if (f.HasPart (Field.VILLAGE))
					if (Connection.CanGo (f.myVillage, this))
						str += f.myVillage.capacity;
			return str;
		}
	}
}
