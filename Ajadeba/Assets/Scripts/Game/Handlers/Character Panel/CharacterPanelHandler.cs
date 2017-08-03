using UnityEngine;
using System.Collections;

public class CharacterPanelHandler : MonoBehaviour {

	public SpriteRenderer firstFlag;
	public SpriteRenderer secondFlag;

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
		PlayerHandler.instance.currentPlayer.AddCharacter ("Mason");
		//(PlayerHandler.instance.currentPlayer.myChars [0] as Mason).activated();
			
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
