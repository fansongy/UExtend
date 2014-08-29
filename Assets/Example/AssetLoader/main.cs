
using UnityEngine;
using System.Collections;
using System.IO;

public class main : MonoBehaviour {
		
	void OnGUI()
	{
		if(GUI.Button(new Rect(100,100,200,80),"Main Assetbundle"))
		{			
			Log.fansy.ScreenPrint("Application.streamingAssetsPath:"+Application.streamingAssetsPath);
			Log.fansy.ScreenPrint("log path:"+FileUtils.PersistentDataPath);
			StartCoroutine(LoadMainGameObject(FileUtils.PersistentDataPath + "sp1.unity3d"));
			StartCoroutine(LoadMainGameObject(FileUtils.PersistentDataPath + "sp2.unity3d"));
		}
			
		if(GUI.Button(new Rect(100,300,200,80),"ALL Assetbundle"))
		{
			Log.fansy.ScreenPrint("Basic Path is:"+FileUtils.PersistentDataPath);
			StartCoroutine(LoadALLGameObject(FileUtils.PersistentDataPath + "ALL.assetbundle"));
		}
		if (GUI.Button (new Rect (350, 100, 200, 80), "Load GameObject")) 
		{
			AssetLoader.Get().LoadGameObject("sp1",new AssetLoader.GameObjectCallback(this.onLoaded));
		}
		if(GUI.Button(new Rect (350, 300, 200, 80),"Load File"))
		{
			AssetLoader.Get().LoadConfig("log",new AssetLoader.ObjectCallback(this.onConfig));
		}
			
	}
	void onLoaded(string name, GameObject go, object callbackData)
	{
			Log.fansy.ScreenPrint ("load call back!");
	}

	void onConfig(string name, System.Object obj, object callbackData)
	{
		Log.fansy.ScreenPrint ("config call back!");
		WWW bundle = obj as WWW;
		Log.fansy.ScreenPrint (bundle.text);
	}

	//读取一个资源
	
	private IEnumerator LoadMainGameObject(string path)
	{
		WWW bundle = new WWW(path);
		 
		yield return bundle;
	Log.fansy.ScreenPrint("Main Bundle is in:"+path);
	//加载到游戏中
		yield return Instantiate(bundle.assetBundle.mainAsset);
		
		bundle.assetBundle.Unload(false);
	}
		
	//读取全部资源
	
	private IEnumerator LoadALLGameObject(string path)
	{
		WWW bundle = new WWW(path);
		
		yield return bundle;
		
	Log.fansy.ScreenPrint("ALL Bundle is in:"+path);
	//通过Prefab的名称把他们都读取出来
		Object  obj0 =  bundle.assetBundle.Load("sp1");
		Object  obj1 =  bundle.assetBundle.Load("sp2");
	Log.fansy.ScreenPrint("obj0 is"+obj0);
	Log.fansy.ScreenPrint("obj1 is"+obj1);
	
	//加载到游戏中    
		yield return Instantiate(obj0);
		yield return Instantiate(obj1);
		bundle.assetBundle.Unload(false);
	}			
}