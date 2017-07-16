using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseConquering : MonoBehaviour {

	static void DestroyBasedOnTerritory() //destroys undefended barracks in enemy territory, and strongBases in territory
	{
		foreach (Field[] row in MapHandler.instance.fields)
			foreach (Field f in row)
				if (f != null) //could use inMap as well
				{
					if (f.HasPart (Field.STRONGHOLD_BASE) && f.HasOtherOwner (null))
						f.RemoveStrongBase ();
					if (f.HasPart (Field.BARRACK) && !f.IsOwner (f.myBarrack.owner) && f.HasEnemyOwner (f.myBarrack.owner))
						f.RemoveBarrack ();
				}
	}

	static void Conquer (Field baseField, Player conqueror)
	{
		baseField.RemoveStrongBase ();
		baseField.AddStronghold (conqueror);
		Debug.Log (baseField.HasPart(Field.STRONGHOLD_BASE));
	}

	public static void ConquerCheck()
	{
		bool newStrongCreated = false; 
		foreach (Field[] row in MapHandler.instance.fields)
			foreach (Field f in row)
				if (f != null) //could use inMap as well
				if (f.HasPart (Field.STRONGHOLD_BASE)) {
					int[] strengths = new int[PlayerHandler.instance.players.Length];
					int i = 0;
					foreach (Player p in PlayerHandler.instance.players) { //yeah using foreach and use a counter...
						strengths [i] = f.myStrongholdBase.strength (p);
						i++;
					}
					List<int> maxes = new List<int>();
					int max = -1; //assuming there is no negative strength...
					for (int j = 0; j < strengths.Length; j++) { //finding the max strengths
						if (strengths [j] > max) {
							max = strengths [j];
							maxes = new List<int> ();
							maxes.Add (j);
						} else if (strengths [j] == max) {
							maxes.Add (j);
						}
					}
					if (max > 0) { //valakié mostmár lesz
						newStrongCreated=true;
						if (maxes.Count == 1) //the normal case...
						{
							Debug.Log ("a legtöbbet küldőé");//TODO kapja meg
							Conquer (f, PlayerHandler.instance.players[maxes[0]]);
						}
						else { 
							bool oneIsBuilder = false;
							foreach (int playerIndex in maxes)
								if (PlayerHandler.instance.players [playerIndex] == f.myStrongholdBase.builder) { //az egyik maxos az építő
									Debug.Log ("a legtöbbet küldő építőé"); 
									oneIsBuilder = true;
									Conquer (f, PlayerHandler.instance.players[maxes[playerIndex]]);
								}
							if (!oneIsBuilder) {
								Debug.Log ("az aktuális játékosé");
								Conquer (f, PlayerHandler.instance.currentPlayer);
							}
						}
							
					}
				}

		if (newStrongCreated) {
			DestroyBasedOnTerritory ();
			ConquerCheck (); //láncszabály
		}
		
		/// végigmenni az összes mezőn, megvizsgálni a base-ket, hogy
		/// van-e játékos aki megkaphatja
		/// ha igen
		/// ha egy, az megkapja *
		/// ha több, melyik küld a legtöbbet,
		/// ha egyenlőség, melyik építette,
		/// ha egyik se az aktuális kapja *
		/// * a Base-t el kell távolítani, Strong-ot adni
		/// beállítani az owner-ét
		/// Inic (ezzel kap területet)
						// tesztelni, hogy vannak-e lemészárolandó dolgok (lemészárolni őket)
		/// újra, ha lett új strong (a lánc lehetőség miatt)
	}
}
