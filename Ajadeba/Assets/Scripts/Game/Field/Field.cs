using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class Field : MonoBehaviour {

	public int index = -1; 
	public int xCoord;
	public int yCoord;
	public const float WIDTH = 0.3f;

	public Village villagePref;
	public Barrack barrackPref;
	public StrongBase strongBasePref;
	public Stronghold strongholdPref;
	public Ruin ruinPref;
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
	public const string STRONGHOLD = "Stronghold(Clone)";
	public const string RUIN = "Ruin(Clone)";
	public const string NORTH_ROAD = "NorthRoad(Clone)";
	public const string EAST_ROAD = "EastRoad(Clone)";
	public const string SOUTH_ROAD = "SouthRoad(Clone)";
	public const string WEST_ROAD = "WestRoad(Clone)";

	List<Player> owners = new List<Player>();

	void Start () {
	
	}

	public static string RoadName (string direction)
	{
		if (direction == NORTH)
			return NORTH_ROAD;
		if (direction == EAST)
			return EAST_ROAD;
		if (direction == SOUTH)
			return SOUTH_ROAD;
		if (direction == WEST)
			return WEST_ROAD;
		return null;
	}

	public void AddVillage()
	{
		Village vill = Instantiate (villagePref);
		vill.transform.SetParent (gameObject.transform);
		vill.transform.position = gameObject.transform.position;
	}

	public void AddRuin()
	{
		Ruin ruin = Instantiate (ruinPref);
		ruin.transform.SetParent (gameObject.transform);
		ruin.transform.position = gameObject.transform.position;
	}

	public void AddRoad(string dir)
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

	public void AddBarrack (Player owner)
	{
		Barrack bar = Instantiate (barrackPref);
		bar.transform.SetParent (gameObject.transform);
		bar.transform.position = gameObject.transform.position;
		bar.owner = owner;
		bar.Inic();
	}

	public void RemoveBarrack () 
	{
		DestroyImmediate (transform.Find (BARRACK).gameObject); //a mocsok Unity amúgy késlelteti és a láncszabály miatt esetleg más erőviszonyok lennének
		AddRuin();	
	}

	public void AddStrongBase (Player builder) 
	{
		StrongBase strongBase = Instantiate (strongBasePref);
		strongBase.transform.SetParent (gameObject.transform);
		strongBase.transform.position = gameObject.transform.position;
		strongBase.builder = builder;
		//AddStronghold (PlayerHandler.instance.currentPlayer); //for testing reasons
	}

	public void RemoveStrongBase () 
	{
		DestroyImmediate (transform.Find (STRONGHOLD_BASE).gameObject); //a mocsok Unity amúgy késlelteti és a láncszabály miatt a baseCheck végtelen ciklusba kerül
		AddRuin();
	}

	public void AddStronghold (Player owner) 
	{
		Stronghold stronghold = Instantiate (strongholdPref);
		stronghold.transform.SetParent (gameObject.transform);
		stronghold.transform.position = gameObject.transform.position;
		stronghold.owner = owner;
		stronghold.Inic();
	}

	public void RemoveStronghold () 
	{
		myStronghold.Die ();
		AddRuin ();
	}

	public bool IsOwner(Player who)
	{
		return owners.Contains (who);
	}

	public bool HasOtherOwner (Player than)
	{
		foreach (Player p in owners)
			if (p != than)
				return true;
		return false;
	}

	public bool HasEnemyOwner (Player enemyToWhom)
	{
		return HasOtherOwner (enemyToWhom); //TODO
	}

	public void AddOwner(Player owner)
	{
		GameObject newTer = Instantiate (territoryPref);
		newTer.transform.SetParent (gameObject.transform);
		newTer.transform.position = gameObject.transform.position;
		newTer.name = "Territory (" + owner.myName + ")";
		SpriteRenderer terSR = newTer.GetComponent<SpriteRenderer> ();
		terSR.sprite = Resources.Load<Sprite> ("Nations/" + owner.nation + "/TerritoryPattern");
		terSR.sortingOrder = 5;
		owners.Add (owner);
		if (HasPart (BARRACK))
			myBarrack.isLazy = true; // if a barrack becomes defended, it becomes lazy
	}

	public void RemoveOwner(Player owner)
	{
		string TERRITORY_NAME = "Territory (" + owner.myName + ")"; // ok it's not a constant, but it shouldn't change (cannot make it const)
		Destroy (gameObject.transform.Find (TERRITORY_NAME).gameObject); 

		owners.Remove (owner);
	}

	public bool HasPart(string part)
	{
		Transform c=gameObject.transform.Find (part);
		if (c == null)
			return false;
		return true;
	}

	public bool IsBlankForBuilding() //alagútak nem számítanak
	{
		if (HasPart (BARRACK) || HasPart (STRONGHOLD_BASE) || HasPart (STRONGHOLD) || HasPart (VILLAGE) || HasPart (NORTH_ROAD) || HasPart (EAST_ROAD) 
			|| HasPart (SOUTH_ROAD) || HasPart (WEST_ROAD) || HasPart (RUIN))
			return false;
		return true;
	}

	public bool IsFullForBuilding() //alagútak nem számítanak
	{
		if (HasPart (BARRACK) || HasPart (STRONGHOLD) || HasPart (STRONGHOLD_BASE) || HasPart (VILLAGE) || HasPart (RUIN) ||
		    HasPart (NORTH_ROAD) && HasPart (EAST_ROAD) && HasPart (SOUTH_ROAD) && HasPart (WEST_ROAD))
			return true;
		return false;
	}

	public int BarrackStrength()
	{
		if (HasPart (BARRACK))
			return myBarrack.strength;
		else
			return 0;
	}

	public Village myVillage
	{
		get
		{
			if (HasPart (VILLAGE))
				return GetComponentInChildren<Village> () as Village;
			return null;
		}
	}

	public Barrack myBarrack
	{
		get
		{
			if (HasPart (BARRACK))
				return GetComponentInChildren<Barrack> () as Barrack;
			return null;
		}
	}

	public Stronghold myStronghold
	{
		get
		{
			if (HasPart (STRONGHOLD))
				return GetComponentInChildren<Stronghold> () as Stronghold;
			return null;
		}
	}

	public StrongBase myStrongholdBase
	{
		get
		{
			if (HasPart (STRONGHOLD_BASE))
				return GetComponentInChildren<StrongBase> () as StrongBase;
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
