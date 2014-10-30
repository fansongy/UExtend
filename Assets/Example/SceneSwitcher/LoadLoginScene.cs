using UnityEngine;
using System.Collections;

public class LoadLoginScene : MonoBehaviour {


	void Awake()
	{
	
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(200,100,200,80),"SceneDirectly"))
		{
			SceneSwitcher.getInstance().toSceneDirectly("LobbyScene");
		}
		if(GUI.Button(new Rect(200,200,200,80),"SceneStatic"))
		{
			SceneSwitcher.getInstance().toSceneStatic("LobbyScene");
		}

	}
}
