/*
 * This is a class which offer the way to switch different scene
 * 
 * Usage:
 * 
 *     1.switch directily(without effect). you can call toSceneDirectly
 * 	    	SceneSwitcher.getInstance().toSceneDirectly(targetScene);
 *     2.switch scene transit by a static loading Scene. 
 *       I found the scene will block when you load the next scene,so I suggest it is a static Loading Scene.You can add some Animation at preloading.
 *       	SceneSwitcher.getInstance().toSceneStatic(targetScene,loadingScene);
 * 		 The two parameter is the scene name which should be added to the Scene In Build.
 *     3.This way may be not exactly switch scene.If you want to make the loading action with some animation,you should load the GameObject in memory,and switch directily.
 *       For loading the GameObject you can call:
 *         SceneSwitcher.getInstance().toSceneProgress(targetScene,changeProgress,loadFinish);
 *       then add function:
 *         void changeProgress(float percent){}
 *		   void loadFinish() {}
 */ 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneSwitcher : MonoBehaviour {

	static SceneSwitcher s_instance;
	[SerializeField]
	private Dictionary<string,object> strDict = new Dictionary<string, object>();

	private string currentSceneName ;


	public const string NEXTSCENE = "nextScene";
	const string PRELOAD = "PreLoadObject";

	ProgressIndicator progress = new ProgressIndicator();

	public static SceneSwitcher getInstance()
	{
		if(s_instance == null)
		{
			GameObject obj = new GameObject();
			obj.name = "SceneSwitcher";
			s_instance = obj.AddComponent<SceneSwitcher>();
			DontDestroyOnLoad(obj);
		}
		return s_instance;
	}

	public ProgressIndicator getIndicator()
	{
		return progress;
	}

	public Dictionary<string,object> getDict()
	{
		return strDict;
	}
	public void toSceneDirectly(string target)
	{
		strDict[NEXTSCENE] = target;
		currentSceneName = target;
		Application.LoadLevel(target);
	}

	public void toSceneStatic(string target,string loadingSceneName)
	{
		strDict[NEXTSCENE] = target;
		currentSceneName = target;
		Application.LoadLevel(loadingSceneName);
	}

	public void toSceneProgress(string target,System.Action<float> changeProccess,System.Action loadFinish)
	{
		strDict[NEXTSCENE] = target;
		currentSceneName = target;
		initIndicator(changeProccess,loadFinish);
	}

	void initIndicator(System.Action<float> changeProcess,System.Action finish)
	{
		AssetLoader.Get().LoadConfig("sceneConfig",(string name, UnityEngine.Object obj, object callbackData)=>{
			ConfigDict cfg = ConfigHelper.ParseJsonByString(obj.ToString());
			Dictionary<string,object> dict = cfg[currentSceneName] as Dictionary<string,object>;
			List<object> list = dict[PRELOAD] as List<object>;
			progress.startProgress(list.Count,changeProcess,finish);
			loadRes(list);
		}); 	
	}

	void loadRes(List<object> list)
	{
		for(int i = 0;i<list.Count;++i)
		{
			AssetLoader.Get().LoadGameObject((string)list[i],loadFinish);
		}
	}

	void loadFinish(string name, UnityEngine.Object obj, object callbackData)
	{
		Debug.Log("Load "+name+" finished");
		progress.moveNext();
	}

}
