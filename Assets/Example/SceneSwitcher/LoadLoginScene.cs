using UnityEngine;
using System.Collections;

public class LoadLoginScene : MonoBehaviour {


	void Awake()
	{
	
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(200,200,100,80),"Login"))
		{
			Debug.Log("switch scene");
			SceneSwitcher.getInstance().toScene("LobbyScene");
		}

	}
}
