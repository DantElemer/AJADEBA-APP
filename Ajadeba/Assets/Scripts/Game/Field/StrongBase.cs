using UnityEngine;
using System.Collections;

public class StrongBase : FieldPart {

	public Player builder;

	public int strength (Player chosen) 
	{
		int str=0;
		foreach (Field[] row in MapHandler.instance.fields)
			foreach (Field f in row)
				if (f!=null)
				if (f.HasPart (Field.BARRACK))
				if (f.myBarrack.owner==chosen)
				if (Connection.CanGo (f.myBarrack, this))
					str += f.myBarrack.strength;
		return str;
	}
}
