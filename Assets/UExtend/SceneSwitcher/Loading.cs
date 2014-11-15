/*
 * This is a Basic class of Loading Scene which is designed to be used in Asyn switching.
 * 
 * You should write a class to override the virtual funcion below. 
 * 
 * Then Attach your own class to your own LoadingScene.
 * 
 */ 

using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {

	AsyncOperation async;

	void Start()
	{
		StartCoroutine(loadScene());
		StartCoroutine(loadAction());
	}

	IEnumerator loadScene()
	{
		yield return StartCoroutine(preAction());

		if(!SceneSwitcher.getInstance().getDict().ContainsKey(SceneSwitcher.NEXTSCENE))
		{
			Debug.LogError("Loading::loadScene() - didn't find the nextScene key.Maybe you shouldn't start from the Loading Scene");
		}
		else
		{
			string sceneName = (string)SceneSwitcher.getInstance().getDict()[SceneSwitcher.NEXTSCENE];
			async = Application.LoadLevelAsync(sceneName);
		}

		yield return StartCoroutine(endAction());

		yield return async;
		
	}

	protected virtual IEnumerator preAction()
	{
		yield return null;
	}


	protected virtual IEnumerator loadAction()
	{
		yield return null;
	}
	protected virtual IEnumerator endAction()
	{
		yield return null;
	}

}
