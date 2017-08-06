using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Stronghold : FieldPart {

	public Player owner;
	public List<Field> territory = new List<Field> ();
	bool extendedTer=false;
	bool reducedTer=false;
	int radius=3;

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
				if (Math.Abs (myField.xCoord - i) + Math.Abs (myField.yCoord - j) <= 3 && Math.Abs (myField.xCoord - i) + Math.Abs (myField.yCoord - j) != 0) 
						AddTerritoryBit (MapHandler.instance.fields [i] [j]);
		//BJB
		if (owner.HasChar (Character.BJB))
			ExtendTerritory ();
		else 
			foreach (Player p in PlayerHandler.instance.players)
				if (PlayerHandler.instance.areEnemies (p, owner) && p.HasChar (Character.BJB))
					ReduceTerritory ();
	}

	void RemoveTerritory()
	{
		foreach (Field f in territory)
			f.RemoveOwner (owner);
		territory.Clear ();
	}

	public void ExtendTerritory() //BJB
	{
		if (extendedTer)
			return;
		extendedTer = true;
		int x = myField.xCoord;
		int y = myField.yCoord;
		if (reducedTer) 
		{
			if (MapHandler.instance.InMap(x+radius, y))
				AddTerritoryBit(MapHandler.instance.fields[x+radius][y]);
			if (MapHandler.instance.InMap(x-radius, y))
				AddTerritoryBit(MapHandler.instance.fields[x-radius][y]);
			if (MapHandler.instance.InMap(x, y+radius))
				AddTerritoryBit(MapHandler.instance.fields[x][y+radius]);
			if (MapHandler.instance.InMap(x, y-radius))
				AddTerritoryBit(MapHandler.instance.fields[x][y-radius]);
		}
		if (MapHandler.instance.InMap(x+radius+1, y))
			AddTerritoryBit(MapHandler.instance.fields[x+radius+1][y]);
		if (MapHandler.instance.InMap(x-radius-1, y))
			AddTerritoryBit(MapHandler.instance.fields[x-radius-1][y]);
		if (MapHandler.instance.InMap(x, y+radius+1))
			AddTerritoryBit(MapHandler.instance.fields[x][y+radius+1]);
		if (MapHandler.instance.InMap(x, y-radius-1))
			AddTerritoryBit(MapHandler.instance.fields[x][y-radius-1]);
	}

	public void ReduceTerritory() //BJB
	{
		if (reducedTer)
			return;
		reducedTer = true;
		int x = myField.xCoord;
		int y = myField.yCoord;
		if (extendedTer)
		{
			if (MapHandler.instance.InMap(x+radius+1, y))
				RemoveTerritoryBit(MapHandler.instance.fields[x+radius+1][y]);
			if (MapHandler.instance.InMap(x-radius-1, y))
				RemoveTerritoryBit(MapHandler.instance.fields[x-radius-1][y]);
			if (MapHandler.instance.InMap(x, y+radius+1))
				RemoveTerritoryBit(MapHandler.instance.fields[x][y+radius+1]);
			if (MapHandler.instance.InMap(x, y-radius-1))
				RemoveTerritoryBit(MapHandler.instance.fields[x][y-radius-1]);
		}
		if (MapHandler.instance.InMap(x+radius, y))
			RemoveTerritoryBit(MapHandler.instance.fields[x+radius][y]);
		if (MapHandler.instance.InMap(x-radius, y))
			RemoveTerritoryBit(MapHandler.instance.fields[x-radius][y]);
		if (MapHandler.instance.InMap(x, y+radius))
			RemoveTerritoryBit(MapHandler.instance.fields[x][y+radius]);
		if (MapHandler.instance.InMap(x, y-radius))
			RemoveTerritoryBit(MapHandler.instance.fields[x][y-radius]);
	}

	public void RemoveTerritoryBit (Field bit)
	{
		foreach (Field f in territory)
			if (f == bit)
				f.RemoveOwner (owner);
		territory.Remove (bit);
	}

	public void AddTerritoryBit (Field bit)
	{
		bit.AddOwner (owner);
		territory.Add (bit);
	}

	public void Die()
	{
		RemoveTerritory ();
		DestroyImmediate (gameObject);
	}

	public int strength
	{
		get
		{
			int str=0;
			foreach (Field[] row in MapHandler.instance.fields)
				foreach (Field f in row)
					if (f!=null)
					if (f.HasPart (Field.BARRACK))
					if (Connection.CanGo (f.myBarrack, this))
						str += f.myBarrack.strength;
			return str;
		}
	}

	public int defStrength
	{
		get
		{
			return strength + owner.CharLevel(Character.MASON);
		}
	}

	public int attStrength
	{
		get
		{
			return strength;
		}
	}
}
