using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour {

	public int index = -1; 
	public const float WIDTH = 0.3f;

	public GameObject villagePref;
	public GameObject northRoadPref;
	public GameObject eastRoadPref;
	public GameObject southRoadPref;
	public GameObject westRoadPref;

	public const string NORTH = "north";
	public const string EAST = "east";
	public const string SOUTH = "south";
	public const string WEST = "west";

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

	void OnMouseDown()
	{
		MapHandler.instance.FieldPressed(index);
	}

	void OnMouseUp()
	{
		MapHandler.instance.FieldReleased(index);
	}
}
