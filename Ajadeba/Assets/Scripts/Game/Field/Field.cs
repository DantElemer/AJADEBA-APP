using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class Field : MonoBehaviour {

	public int index = -1; 
	public const float WIDTH = 0.3f;

	public GameObject villagePref;
	public Barrack barrackPref;
	public GameObject strongBasePref;
	public Stronghold strongholdPref;
	public GameObject northRoadPref;
	public GameObject eastRoadPref;
	public GameObject southRoadPref;
	public GameObject westRoadPref;

	public GameObject territoryPref;

	public const string NORTH = "north";
	public const string EAST = "east";
	public const string SOUTH = "south";
	public const string WEST = "west";

	public const string BARRACK = "Barrack(Clone)";
	public const string VILLAGE = "Village(Clone)";
	public const string STRONGHOLD_BASE = "StrongholdBase(Clone)";
	public const string NORTH_ROAD = "NorthRoad(Clone)";
	public const string EAST_ROAD = "EastRoad(Clone)";
	public const string SOUTH_ROAD = "SouthRoad(Clone)";
	public const string WEST_ROAD = "WestRoad(Clone)";

	List<Player> owners = new List<Player>();

	// Use this for initialization
	void Start () {
	
	}

	public void addVillage()
	{
		GameObject vill = Instantiate (villagePref);
		vill.transform.SetParent (gameObject.transform);
		vill.transform.position = gameObject.transform.position;
	}

	public void addRoad(string dir)
	{
		GameObject road = null;
		if (dir==NORTH)
			road = Instantiate (northRoadPref);
		else if (dir==EAST)
			road = Instantiate (eastRoadPref);
		else if (dir==SOUTH)
			road = Instantiate (southRoadPref);
		else if (dir==WEST)
			road = Instantiate (westRoadPref);
		
		road.transform.SetParent (gameObject.transform);
		road.transform.position = gameObject.transform.position;
	}

	public void addBarrack (Player owner)
	{
		Barrack bar = Instantiate (barrackPref);
		bar.transform.SetParent (gameObject.transform);
		bar.transform.position = gameObject.transform.position;
		bar.owner = owner;
		bar.Inic();
	}

	public void addStrongBase () //TODO
	{
		GameObject strongBase = Instantiate (strongBasePref);
		strongBase.transform.SetParent (gameObject.transform);
		strongBase.transform.position = gameObject.transform.position;
		//TODO
	}

	public void addStronghold (Player owner) //TODO
	{
		Stronghold stronghold = Instantiate (strongholdPref);
		stronghold.transform.SetParent (gameObject.transform);
		stronghold.transform.position = gameObject.transform.position;
		stronghold.owner = owner;
		stronghold.Inic();
		//TODO
	}

	public bool isOwner(Player who)
	{
		return owners.Contains (who);
	}

	public bool hasOtherOwner (Player than)
	{
		foreach (Player p in owners)
			if (p != than)
				return true;
		return false;
	}

	public void addOwner(Player owner)
	{
		GameObject newTer = Instantiate (territoryPref);
		newTer.transform.SetParent (gameObject.transform);
		newTer.transform.position = gameObject.transform.position;
		SpriteRenderer terSR = newTer.GetComponent<SpriteRenderer> ();
		terSR.sprite = Resources.Load<Sprite> ("Nations/" + owner.nation + "/TerritoryPattern");
		owners.Add (owner);
	}

	public bool hasPart(string part)
	{
		Transform c=gameObject.transform.Find (part);
		if (c == null)
			return false;
		return true;
	}

	public bool isBlankForBuilding() //alagútak nem számítanak
	{
		if (hasPart (BARRACK) || hasPart (STRONGHOLD_BASE) || hasPart (VILLAGE) || hasPart (NORTH_ROAD) || hasPart (EAST_ROAD) 
			|| hasPart (SOUTH_ROAD) || hasPart (WEST_ROAD))
			return false;
		return true;
	}

	public bool isFullForBuilding() //alagútak nem számítanak
	{
		if (hasPart (BARRACK) || hasPart (STRONGHOLD_BASE) || hasPart (VILLAGE) ||
		    hasPart (NORTH_ROAD) && hasPart (EAST_ROAD) && hasPart (SOUTH_ROAD) && hasPart (WEST_ROAD))
			return true;
		return false;
	}

	public int BarrackStrength()
	{
		if (hasPart (BARRACK))
			return myBarrack.strength;
		else
			return 0;
	}

	Barrack myBarrack
	{
		get
		{
			if (hasPart (BARRACK))
				return GetComponentInChildren<Barrack> () as Barrack;
			return null;
		}
	}

	void OnMouseDown()
	{
		MapHandler.instance.FieldPressed(this);
	}

	void OnMouseUp()
	{
		MapHandler.instance.FieldReleased(this);
	}
}
