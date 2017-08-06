using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class Field : MonoBehaviour {

	public int index = -1; //it's set in MapHandler, and never used elsewhere yet... actually it doesn't seem too useful
	public int xCoord; //first index in MapHandler's fields
	public int yCoord; //second index in MapHandler's fields
	public const float WIDTH = 0.3f; //for graphics...

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

	public bool selectable=false; //in MapHandler's CHOOSER_STATE the selectable fields are shown, this bool stores whether this field is currently selectable or not

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

	List<Player> owners = new List<Player> (); //pls do not modify it randomly


	public static string RoadName (string direction) //returns the PropertyDrawer namespace of the given direction
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

	public void AddVillage () //adds a village to the field
	{
		Village vill = Instantiate (villagePref);
		vill.transform.SetParent (gameObject.transform);
		vill.transform.position = gameObject.transform.position;
	}

	public void AddRuin () //adds a ruin to the field
	{
		Ruin ruin = Instantiate (ruinPref);
		ruin.transform.SetParent (gameObject.transform);
		ruin.transform.position = gameObject.transform.position;
	}

	public void AddRoad (string dir) //adds a road to the field
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

	public void AddBarrack (Player owner) //adds a barrack to the field
	{
		Barrack bar = Instantiate (barrackPref);
		bar.transform.SetParent (gameObject.transform);
		bar.transform.position = gameObject.transform.position;
		bar.owner = owner;
		bar.Inic();
	}

	public void RemoveBarrack () //removes the barrack from the field
	{
		DestroyImmediate (transform.Find (BARRACK).gameObject); //a mocsok Unity amúgy késlelteti és a láncszabály miatt esetleg más erőviszonyok lennének
	}

	public void AddStrongBase (Player builder) //adds a stronghold base to the field
	{
		StrongBase strongBase = Instantiate (strongBasePref);
		strongBase.transform.SetParent (gameObject.transform);
		strongBase.transform.position = gameObject.transform.position;
		strongBase.builder = builder;
	}

	public void RemoveStrongBase () //removes the stronghold base from the field
	{
		DestroyImmediate (transform.Find (STRONGHOLD_BASE).gameObject); //a mocsok Unity amúgy késlelteti és a láncszabály miatt a baseCheck végtelen ciklusba kerül
	}

	public void AddStronghold (Player owner) //adds a stronghold to the field
	{
		Stronghold stronghold = Instantiate (strongholdPref);
		stronghold.transform.SetParent (gameObject.transform);
		stronghold.transform.position = gameObject.transform.position;
		stronghold.owner = owner;
		if (owner.mainStrong == null)
			owner.mainStrong = stronghold;
		stronghold.Inic();
	}

	public void RemoveStronghold () //removes the stronghold from the field
	{
		myStronghold.Die ();
	}

	public bool IsOwner (Player who) //returns if given player has territory on this field
	{
		return owners.Contains (who);
	}

	public bool HasOtherOwner (Player than) //returns if other player has territory on this field
	{
		foreach (Player p in owners)
			if (p != than)
				return true;
		return false;
	}

	public bool HasEnemyOwner (Player enemyToWhom) //returns if enemy player has territory on this field
	{
		return HasOtherOwner (enemyToWhom); //TODO (teams!)
	}

	public void AddOwner (Player owner) //...
	{
		//visual stuff
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

	public void RemoveOwner(Player owner) //...
	{
		string TERRITORY_NAME = "Territory (" + owner.myName + ")"; // ok it's not a constant, but it shouldn't change (cannot make it const)
		Destroy (gameObject.transform.Find (TERRITORY_NAME).gameObject); 

		owners.Remove (owner);
	}

	public bool HasPart (string part) //return if field contains given part
	{
		Transform c=gameObject.transform.Find (part);
		if (c == null)
			return false;
		return true;
	}

	public bool IsBlankForBuilding() //...basically IsBlank (except channels shouldn't count)
	{
		if (HasPart (BARRACK) || HasPart (STRONGHOLD_BASE) || HasPart (STRONGHOLD) || HasPart (VILLAGE) || HasPart (NORTH_ROAD) || HasPart (EAST_ROAD) 
			|| HasPart (SOUTH_ROAD) || HasPart (WEST_ROAD) || HasPart (RUIN))
			return false;
		return true;
	}

	public bool IsFullForBuilding() //returns true if you cannot build anything on it (TODO: channels)
	{
		if (HasPart (BARRACK) || HasPart (STRONGHOLD) || HasPart (STRONGHOLD_BASE) || HasPart (VILLAGE) || HasPart (RUIN) ||
		    HasPart (NORTH_ROAD) && HasPart (EAST_ROAD) && HasPart (SOUTH_ROAD) && HasPart (WEST_ROAD))
			return true;
		return false;
	}

	public int BarrackStrength() //if field contains barrack, returns its strength
	{
		if (HasPart (BARRACK))
			return myBarrack.strength;
		else
			return 0;
	}

	public Village myVillage //returns village part (if has)
	{
		get
		{
			if (HasPart (VILLAGE))
				return GetComponentInChildren<Village> () as Village;
			return null;
		}
	}

	public Barrack myBarrack //returns barrack part (if has)
	{
		get
		{
			if (HasPart (BARRACK))
				return GetComponentInChildren<Barrack> () as Barrack;
			return null;
		}
	}

	public Stronghold myStronghold //returns stronghold part (if has)
	{
		get
		{
			if (HasPart (STRONGHOLD))
				return GetComponentInChildren<Stronghold> () as Stronghold;
			return null;
		}
	}

	public StrongBase myStrongholdBase //returns stronghold base part (if has)
	{
		get
		{
			if (HasPart (STRONGHOLD_BASE))
				return GetComponentInChildren<StrongBase> () as StrongBase;
			return null;
		}
	}

	void OnMouseDown() //fired when mouse pressed on this field
	{
		MapHandler.instance.FieldPressed(this);
	}

	void OnMouseUp() //fired when mouse is released (and was pressed on this field)
	{
		MapHandler.instance.FieldReleased(this);
	}

	public void ActivateSelection() //becomes selectable
	{
		gameObject.transform.FindChild ("selectable").gameObject.SetActive(true);
		selectable = true;
	}

	public void DeactivateSelection() //...
	{
		gameObject.transform.FindChild ("selectable").gameObject.SetActive(false);
		selectable = false;
	}

	public List<Field> Borders() //returns a list of border fields
	{
		List<Field> borders = new List<Field> ();
		if (MapHandler.instance.InMap (xCoord, yCoord + 1))
			borders.Add (MapHandler.instance.fields [xCoord] [yCoord + 1]);
		if (MapHandler.instance.InMap (xCoord, yCoord - 1))
			borders.Add (MapHandler.instance.fields [xCoord] [yCoord - 1]);
		if (MapHandler.instance.InMap (xCoord + 1, yCoord))
			borders.Add (MapHandler.instance.fields [xCoord + 1] [yCoord]);
		if (MapHandler.instance.InMap (xCoord - 1, yCoord))
			borders.Add (MapHandler.instance.fields [xCoord - 1] [yCoord]);
		return borders;
	}

	public void DestroyIfMust() //destroys undefended lazies, undefendeds in enemy territory
	{
		if (HasPart (Field.STRONGHOLD_BASE) && HasOtherOwner (null)) {
			RemoveStrongBase ();
			AddRuin ();
		}
		if (HasPart (Field.BARRACK) && !IsOwner (myBarrack.owner) && HasEnemyOwner (myBarrack.owner)) {
			RemoveBarrack ();
			AddRuin ();
		}
		if (HasPart (Field.BARRACK) && !IsOwner (myBarrack.owner) && myBarrack.isLazy) {
			RemoveBarrack ();
			AddRuin ();
		}	
	}
}
