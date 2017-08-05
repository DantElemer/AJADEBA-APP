using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterPanelHandler : MonoBehaviour { //TODO staticolás/singletoná varázsolás? 2v2-nél, hogy legyen? Lehet kettő lesz belőle?

	public SpriteRenderer firstFlag;
	public SpriteRenderer secondFlag;
	public CharacterChooser ChChooserPref;
	List<CharacterChooser> chChoosers = new List<CharacterChooser>();
	bool mouseDownWhenOpen = false; //it's true if chChoosers are shown and mouse is pressed

	//turn markers' data:
	public SpriteRenderer TurnMarkerPref;
	Vector3 upperStartPoint=new Vector3(0.17f, -0.06f, 0f);
	Vector3 bottomStartPoint=new Vector3(0.17f, -0.22f, 0f);
	int turnsFinished=0; //each players' turn counts, not "whole turns"







	// Use this for initialization
	void Start () 
	{ 
		//setting Player's flags
		firstFlag.sprite = PlayerHandler.instance.players [0].flag;
		secondFlag.sprite = PlayerHandler.instance.players [1].flag;
		Component[] chPFlags = GetComponentsInChildren<ChPFlag> ();
		int i = 0;
		foreach (ChPFlag flag in chPFlags) {
			flag.player = PlayerHandler.instance.players [i];
			i++;
		}
	}

	void Update()
	{
		if (Input.GetMouseButton (0) && chChoosers.Count > 0)
			mouseDownWhenOpen = true;
		else if (mouseDownWhenOpen && !Input.GetMouseButton (0)) {
			CloseChChoosers ();
			mouseDownWhenOpen = false;
		}
	}

	/*public void ClickedElsewhere()
	{
		CloseChChoosers ();
	}*/

	public void OpenChChoosers()
	{
		MapHandler.instance.SetStatus(MapHandler.FROZEN_STATE);
		//gameObject.transform.FindChild ("ClickElsewhereBtn").gameObject.SetActive (true);

		int numOfChars = 7;
		for (int i = 0; i < numOfChars; i++) //hát nem gyönyönrű?
		{
			CharacterChooser newChooser = Instantiate (ChChooserPref);
			newChooser.gameObject.transform.SetParent (gameObject.transform);
			chChoosers.Add (newChooser);
			newChooser.transform.position = newChooser.transform.position + (new Vector3 (0, (i - (float)(numOfChars-1) / 2f) * 0.75f, 0));

			if (i == 0) 
				newChooser.Inic (new Mason ());
			else if (i==1) 
				newChooser.Inic (new Prolific ());
			else if (i==2) 
				newChooser.Inic (new Roman ());
			else if (i==3) 
				newChooser.Inic (new Gyongyi ());
			else if (i==4) 
				newChooser.Inic (new Trespasser ());
			else if (i==5) 
				newChooser.Inic (new Stakhanovite ());
			else if (i==6) 
				newChooser.Inic (new BJB ());

			if (PlayerHandler.instance.currentPlayer.HasChar (newChooser.myChar.name))
				newChooser.gameObject.GetComponentInChildren<Button> ().interactable = false;
		}
	}

	public void CloseChChoosers()
	{
		if (MapHandler.instance.pStatus == MapHandler.FROZEN_STATE)
			MapHandler.instance.SetStatus (MapHandler.NORMAL_STATE);
		//gameObject.transform.FindChild ("ClickElsewhereBtn").gameObject.SetActive (false);

		foreach (CharacterChooser chCh in chChoosers)
			Destroy (chCh.gameObject);
		chChoosers.Clear ();
	}
	
	public void TurnFinished()
	{
		SpriteRenderer tMarker=Instantiate (TurnMarkerPref);
		tMarker.gameObject.transform.SetParent (gameObject.transform);
		tMarker.gameObject.transform.position = gameObject.transform.position;
		float markerDist = tMarker.bounds.size.x + 0.01f;
		if (turnsFinished % 2 == 0 ) //upper player
			tMarker.gameObject.transform.position += upperStartPoint + (new Vector3 (turnsFinished / 2 * markerDist, 0, 0));
		else //bottom player
			tMarker.gameObject.transform.position += bottomStartPoint + (new Vector3 (turnsFinished / 2 * markerDist, 0, 0));
		turnsFinished++;
	}
}
