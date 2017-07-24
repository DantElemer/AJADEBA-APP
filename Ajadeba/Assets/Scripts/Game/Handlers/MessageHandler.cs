using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Message : MonoBehaviour
{
	public const string YELLOW = "yellow";
	public const string DEFAULT_COLOR = YELLOW;

	string myText; //TODO getter
	string myColorName; //TODO getter
	float myTime; //time since creation //TODO getter

	public Message(string text, string color = DEFAULT_COLOR)
	{
		myText = text;
		myColorName = color;
		myTime = 0;
	}

	void Update()
	{
		myTime += Time.deltaTime;
	}
};

public class MessageHandler : MonoBehaviour //Balázs, ha csinálod, az én (!) elképzelésem vmi olyasmi, hogy van mondjuk 5 Text-je (UI cucc), amiket lehetőleg Scriptben gyárt le és helyez el (Start), ha nem lehet, akkor a Canvasre rápakolsz ennyi Text-et, beállítod a dolgaikat és drag-drop dologgal behúzogatod a publicus (egyelőre nem public) tömbbe. Namármost alapból ezek mind üres szövegek, de ha már vannak message-ek, akkor az utolsó 5-öt kiírod (ha kevesebb van, akkor annyit) a megfelelő Text-ekbe. Mivel List-ekről van szó, ezért megcsinálhatod üres Text-ek nélkül is, ha szorgosan hozod létre és gyilkolod le őket. Egyéb: lehet időzített pusztulás is.
{
	static List<Message> messages = new List<Message>();
	static List<Text> mesTexts = new List<Text>();

	void Start()
	{
		//TODO generate mesTexts
	}

	public static void NewMessage(string text, string color = Message.DEFAULT_COLOR)
	{
		messages.Add (new Message (text, color));
	}

	public static void NewMessage(Message newMes)
	{
		messages.Add (newMes);
	}

	public static void Refresh()
	{
		//TODO: update the panel... 
	}

}
