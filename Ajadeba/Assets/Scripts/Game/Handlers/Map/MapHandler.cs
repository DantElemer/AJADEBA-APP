using UnityEngine;
using System.Collections;

public class MapHandler : MonoBehaviour { 

	private static MapHandler sInstance = null;


	public GameObject map;
	public Field fieldPrefab;
	public Field[][] fields; 

	public Field chosenField; //the field currently pressed
	public Field assaultBase; // the field from which the assault can start
	float timeSincePress = 0; // if a a field is being pressed it stores how much time passed since the start of the press, otherwise zero

	public bool checkMode = false;
	public Field fromF;




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

	void Update()
	{
		if (!Input.GetMouseButton (0)) //left button is not pressed
			chosenField = null;
		if (chosenField!=null)
			timeSincePress += Time.deltaTime;
	}

	void GenerateMap()
	{
		fields = new Field[10][];
		for (int i = 0; i < 10; i++) {
			fields [i] = new Field[10];
			for (int j = 0; j < 10; j++) {
				if (Random.value < 0.95) { //néhányat kihagyunk 
					fields [i] [j] = Instantiate (fieldPrefab);
					fields [i] [j].index = 10 * i + j;
					fields [i] [j].xCoord = i;
					fields [i] [j].yCoord = j;
					fields [i] [j].transform.Translate (new Vector3 (Field.WIDTH * (i - 5), Field.WIDTH * (j - 5), 0));
					fields [i] [j].transform.SetParent (map.transform);
					if (Random.value < 0.1)
						fields [i] [j].AddVillage ();
					else {
						if (Random.value < 0.1)
							fields [i] [j].AddRoad (Field.NORTH);
						if (Random.value < 0.1)
							fields [i] [j].AddRoad (Field.EAST);
						if (Random.value < 0.1)
							fields [i] [j].AddRoad (Field.SOUTH);
						if (Random.value < 0.1)
							fields [i] [j].AddRoad (Field.WEST);
					}
				} 
			}
		}
	}

	void KillUndefendedLazies()
	{
		foreach (Field[] row in fields)
			foreach (Field f in row)
				if (f != null) //could use inMap as well
				if (f.HasPart (Field.BARRACK))
				if (f.myBarrack.isLazy && !f.IsOwner (f.myBarrack.owner))
					f.RemoveBarrack ();
					
	}

	void FieldClicked()
	{
		Debug.Log ("CLICKED!");

		if (assaultBase != null) // one stronghold is chosen
		{
			if (chosenField.HasPart (Field.STRONGHOLD))  // and the field clicked also has a stronghold
			if (PlayerHandler.instance.areEnemies (assaultBase.myStronghold.owner, chosenField.myStronghold.owner)) // and they are enemies
			if (assaultBase.myStronghold.attStrength > chosenField.myStronghold.defStrength) // and attacker is stronger
			if (Connection.CanGo (assaultBase.myStronghold, chosenField.myStronghold)) // and attacker reaches defender
			{
				chosenField.RemoveStronghold ();
				KillUndefendedLazies ();
			}
			assaultBase=null;
		}
		else if (chosenField.HasPart (Field.STRONGHOLD)) {
			if (chosenField.myStronghold.owner == PlayerHandler.instance.currentPlayer)
				assaultBase = chosenField;
		}
		else {
			assaultBase = null;
			Debug.Log ("sajna");
		}
			
	}

	public void FieldPressed (Field field) //mouse down on field
	{
		if (checkMode) {
			if (fromF == null)
				fromF = field;
			else {
				Debug.Log (Connection.IsConnected (fromF, field, PlayerHandler.instance.currentPlayer));
				fromF = null;
			}
		} else {
			chosenField = field;
			if (BuildHandler.instance.CanBuild (field, PlayerHandler.instance.currentPlayer))
				BuildHandler.instance.OpenBuildOptions (field);
		}
	}

	public void FieldReleased (Field field) //mouse down on field
	{
		if (BuildHandler.instance.open)
			BuildHandler.instance.CloseBuildOptions();
		
		const float MAX_TIME_FOR_CLICK = 0.2f;
		if (timeSincePress < MAX_TIME_FOR_CLICK)
			FieldClicked ();
		else
			assaultBase = null;
		timeSincePress = 0;

		BaseConquering.ConquerCheck (); //TODO értelmesebb helyre kéne rakni, így eggyel "késik"
	}

	public bool InMap(int x, int y)
	{
		if (x < 0 || y < 0)
			return false;
		if (fields.Length <= x)
			return false;
		if (fields [x].Length <= y)
			return false;
		if (fields [x] [y] == null)
			return false;
		return true;
	}
}
