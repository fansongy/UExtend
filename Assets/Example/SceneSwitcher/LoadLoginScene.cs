using UnityEngine;
using System.Collections;

public class LoadLoginScene : MonoBehaviour {


	bool isLoading = false;
	int process = 0;

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
		if(GUI.Button(new Rect(200,300,200,80),"SceneProgress"))
		{
			SceneSwitcher.getInstance().toSceneProgress("LobbyScene",changeProgress,loadFinish);
			isLoading = true;
		}
		if(isLoading)
		{
			GUI.Label(new Rect(200,400,200,80),"Load Precent:"+process);
		}

	}

	void changeProgress(float percent)
	{
		Debug.Log("change Progress:"+percent);
		process = (int)(percent*100);
	}

	void loadFinish()
	{
		isLoading = false;
		SceneSwitcher.getInstance().toSceneDirectly("LobbyScene");
	}
}
