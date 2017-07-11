using UnityEngine;
using System.Collections;

public class MapHandler : MonoBehaviour { //TODO add to Map, not to GameHandler?

	private static MapHandler sInstance = null;


	public GameObject map;
	public RoadOption roadOptionPref; 
	public BarrackOption barrackOptionPref;
	public StrongBaseOp strongBaseOpPref;
	public Field fieldPrefab;
	public Field[] fields; 

	public int chosenField;

	RoadOption rO;
	BarrackOption bO;
	StrongBaseOp sO;


	public static MapHandler instance //singleton magic
	{
		get 
		{
			if (sInstance == null)
				sInstance = FindObjectOfType (typeof(MapHandler)) as MapHandler;
			if (sInstance == null)
				sInstance = new MapHandler ();
			return sInstance;
		}
	}

	void Start () {
		//TODO: generate map normally
		GenerateMap();
	}

	void GenerateMap()
	{
		fields = new Field[100];
		for (int i = 0; i < 10; i++)
			for (int j = 0; j < 10; j++) 
			{
				fields [10*i+j] = Instantiate(fieldPrefab);
				fields [10*i+j].index = 10*i+j;
				fields [10 * i + j].transform.Translate(new Vector3 (Field.WIDTH * i, Field.WIDTH * j, 0));
				fields [10 * i + j].transform.SetParent (map.transform);
				if (Random.value < 0.1)
					fields [10 * i + j].addVillage ();
				else 
				{
					if (Random.value < 0.1)
						fields [10 * i + j].addRoad (Field.NORTH);
					if (Random.value < 0.1)
						fields [10 * i + j].addRoad (Field.EAST);
					if (Random.value < 0.1)
						fields [10 * i + j].addRoad (Field.SOUTH);
					if (Random.value < 0.1)
						fields [10 * i + j].addRoad (Field.WEST);
				}
			}
	}

	public void FieldPressed (int fieldInd) //mouse down on field
	{
		//TODO tesztelni, h tud-e bármit csinálni ott
		OpenBuildOptions(fieldInd);
	}

	void OpenBuildOptions (int fieldInd)
	{
		rO = Instantiate (roadOptionPref); //TODO show around the clicked field
		rO.transform.position = fields[fieldInd].transform.position+Vector3.right;
		bO = Instantiate (barrackOptionPref); //TODO show around the clicked field
		bO.transform.position = fields[fieldInd].transform.position+Vector3.left;
		sO = Instantiate (strongBaseOpPref); //TODO show around the clicked field
		sO.transform.position = fields[fieldInd].transform.position+Vector3.up;
		chosenField = fieldInd;
	}

	public void FieldReleased (int fieldInd) //mouse down on field
	{
		CloseBuildOptions(fieldInd);
	}

	void CloseBuildOptions (int fieldInd)
	{
		rO.CloseRoadOps ();
		Destroy (rO.gameObject);
		Destroy (bO.gameObject);
		Destroy (sO.gameObject);
		//TODO
	}
}
