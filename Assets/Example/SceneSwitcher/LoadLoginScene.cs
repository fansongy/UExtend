/*
 * This file show how to use SceneSwticher
 * 
 * you can change the targetScene name and loading scene name to your file.
 * 
 */

using UnityEngine;
using System.Collections;

public class LoadLoginScene : MonoBehaviour {

	bool isLoading = false;
	int process = 0;

	public string targetScene = "LobbyScene";
	public string loadingScene = "Loading";

	void OnGUI()
	{
		if(GUI.Button(new Rect(200,100,200,80),"SceneDirectly"))
		{
			SceneSwitcher.getInstance().toSceneDirectly(targetScene);
		}
		if(GUI.Button(new Rect(200,200,200,80),"SceneStatic"))
		{
			SceneSwitcher.getInstance().toSceneStatic(targetScene,loadingScene);
		}
		if(GUI.Button(new Rect(200,300,200,80),"SceneProgress"))
		{
			SceneSwitcher.getInstance().toSceneProgress(targetScene,changeProgress,loadFinish);
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
		SceneSwitcher.getInstance().toSceneDirectly(targetScene);
	}
}
