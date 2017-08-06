using UnityEngine;
using System.Collections;

public class MapHandler : MonoBehaviour { 

	private static MapHandler sInstance = null; //the only instance of MapHandler (singleton)


	public Field fieldPrefab; //for creating fields...
	public Field[][] fields {get; private set;} //the map, "holes" are null elements

	public Field chosenField; //the field currently pressed, after releasing the field, it's available until next tick (that's why we can use it in FieldClicked for example)
	public Field assaultBase {get; private set;} // the field from which the assault can start
	bool assaultBaseJustChosen=false;

	float timeSincePress = 0; // if a a field is being pressed it stores how much time passed since the start of the press, otherwise it's zero
	bool mousePressed = false; 

	public bool checkMode = false; // for debugging Connection
	public Field fromF; // for debugging Connection

	const float MAX_TIME_FOR_CLICK = 0.15f; //if you do not release the field after this amount of time it will be considered a "long press", not a click
	bool longPress = false; //...


	//status
	public const string NORMAL_STATE = "normal state"; //you can build, start assaults etc.
	public const string CHOOSER_STATE = "chooser state"; //used for choosing a field (Prolific, BJB...)
	public const string FROZEN_STATE = "frozen state"; //not interactable, you can't do anything on the map
	string stat; //never use directly!
	public string status {
		get { 
			return stat;
		}
		set 
		{
			if (value != stat) 
			{
				stat = value;
				Debug.Log ("[MapHandler] Switched to " + stat);
			}
		}
	} 
		//chooser state
		bool prolificChoosing = false; //currently searching for a village to prolify
		bool bJBing = false; //currently bjb acting is taking place

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
		status = NORMAL_STATE;
		GenerateMap(); //TODO: generate map normally
	}

	void Update() //part of mouse event handling
	{
		if (status==FROZEN_STATE)
			return;

		if (Input.GetMouseButton (0) && !mousePressed) { //mouse has just being pressed
			mousePressed = true;
			if (chosenField == null)
				MousePressedOutOfMap ();
		}
		if (!Input.GetMouseButton (0)) { //left button is not pressed
			if (mousePressed) //mouse has been released rigth now
				MouseReleased();
			chosenField = null;
			longPress = false;
		}
		if (chosenField != null) //so mouse is pressed (if it wasn't chosenField had been set to null up there)
		{
			timeSincePress += Time.deltaTime;
			if (timeSincePress > MAX_TIME_FOR_CLICK && !longPress) {
				LongPress ();
				longPress = true;
			}
		}
		assaultBaseJustChosen = false;
	}

	void FieldClicked()
	{
		if (status != CHOOSER_STATE) {
			if (chosenField.HasPart (Field.STRONGHOLD)) { //start an assault?
				if (chosenField.myStronghold.owner == PlayerHandler.instance.currentPlayer)
				if (PlayerHandler.instance.currentPlayer.stepsLeft == PlayerHandler.instance.currentPlayer.maxSteps) {
					AssaultOn (); 
					Debug.Log ("Attack: " + chosenField.myStronghold.attStrength);
				} else
					Debug.Log ("Assault takes a full turn!");
			}	
		} 
		else {
			if (prolificChoosing) {
				if (chosenField.HasPart (Field.VILLAGE)) {
					chosenField.myVillage.Prolificate ();
					prolificChoosing = false;
					status = NORMAL_STATE;
					Selection.SelectionTimeOver (Selection.VILLAGES);
					PlayerHandler.instance.currentPlayer.MayFinishedTurn ();
				} else
					Debug.Log ("Idiot, it's not a village!");
			} else if (bJBing) {
				if (chosenField.selectable)
				if (!(PlayerHandler.instance.currentPlayer.MyChar (Character.BJB) as BJB).ContinueActing ()) { //acting terminated
					bJBing = false;
					PlayerHandler.instance.currentPlayer.MayFinishedTurn ();
				} 
			} else if (assaultBase != null) { //so assault choosing...
				if (chosenField.selectable) {
					AssaultOff ();
					chosenField.RemoveStronghold ();
					chosenField.AddRuin ();
					KillCheckOnFullMap (); //a bit of waste (TODO)
					PlayerHandler.instance.currentPlayer.MayFinishedTurn ();
				}
			}
		}
	}

	void MousePressedOutOfMap() //...
	{
		AssaultOff ();
	}

	void MouseReleased() //anywhere
	{
		if (!assaultBaseJustChosen)
			AssaultOff ();
		mousePressed = false;
	}

	public void FieldPressed (Field field) //mouse down on field, it sets chosenField
	{
		if (status==FROZEN_STATE)
			return;
		
		if (checkMode) { //TODO kiszedni, ha már nem kell
			if (fromF == null)
				fromF = field;
			else {
				Debug.Log (Connection.IsConnected (fromF, field, PlayerHandler.instance.currentPlayer));
				fromF = null;
			}
		} else 
			chosenField = field;
	}

	public void FieldReleased (Field field) //...
	{
		if (status==FROZEN_STATE)
			return;
		
		if (BuildHandler.instance.open) //if build options were open, close them
			BuildHandler.instance.CloseBuildOptions();
		if (!longPress) //if you were fast enough, you get a click!
			FieldClicked ();
		timeSincePress = 0;
	}

	void LongPress() //main point: opening build options
	{
		if (status != CHOOSER_STATE) 
		{
			AssaultOff ();
			if (BuildHandler.instance.CanBuild (chosenField, PlayerHandler.instance.currentPlayer))
				BuildHandler.instance.OpenBuildOptions (chosenField);
		}
	}

	void AssaultOn() //a stronghold is chosen to start assault from it
	{
		if (Selection.SelectionTime (Selection.ASSAULT_AIMS)) {
			assaultBase = chosenField;
			assaultBase.myStronghold.transform.Find ("AssaultOn").gameObject.SetActive (true);
			assaultBaseJustChosen = true;
			Selection.SelectionTime (Selection.ASSAULT_AIMS);
			status = CHOOSER_STATE;
		} else
			Debug.Log ("You cannot attack from here anything");
	}

	void AssaultOff() //assault was done, or canceled
	{
		if (assaultBase == null)
			return;
		assaultBase.myStronghold.transform.Find ("AssaultOn").gameObject.SetActive (false);
		assaultBase = null;
		Selection.SelectionTimeOver(Selection.ASSAULT_AIMS);
		status = NORMAL_STATE;
	}

	public bool InMap(int x, int y) //is there a field?
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

	public void Prolification() //a player just choosed prolific, so get a village...
	{
		bool atLeastOneVillage = Selection.SelectionTime (Selection.VILLAGES);

		if (atLeastOneVillage) {
			status = CHOOSER_STATE;
			prolificChoosing = true; 
			Debug.Log ("Choose a village to prolify!");
		} else {
			Debug.Log ("No village you cretin");
			PlayerHandler.instance.currentPlayer.MayFinishedTurn ();
		}
	}

	public void BJBing() //BJB choosing procedure has started...
	{
		status = CHOOSER_STATE;
		bJBing = true;
		Debug.Log ("Choose one of your strongholds!");
	}

	void GenerateMap() //quite good for testing I think :)
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
					fields [i] [j].transform.SetParent (gameObject.transform);
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

	public void KillCheckOnFullMap()
	{
		foreach (Field[] row in fields)
			foreach (Field f in row)
				if (f != null) //could use inMap as well
					f.DestroyIfMust(); 				
	}

}