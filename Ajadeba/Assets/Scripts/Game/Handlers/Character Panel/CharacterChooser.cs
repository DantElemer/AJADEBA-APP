using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterChooser : MonoBehaviour {

	Character myChar;

	public void Inic(Character chToChoose)
	{
		myChar = chToChoose;
		Debug.Log ("I'm a " + myChar.name + " chooser!");
		gameObject.GetComponentInChildren<Text> ().text = myChar.name + " (" + myChar.cost.ToString() + ")";
	}

	public void Chosen()
	{
		PlayerHandler.instance.currentPlayer.AddCharacter (myChar);
		gameObject.GetComponentInParent<CharacterPanelHandler> ().CloseChChoosers ();
	}
}
