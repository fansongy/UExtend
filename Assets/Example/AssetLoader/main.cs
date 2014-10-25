
using UnityEngine;
using System.Collections;
using System.IO;

public class main : MonoBehaviour {

	void Awake()
	{
		Singleton.getInstance(ClassName.SOUND_MANAGER);
	}
		
	void OnGUI()
	{
		if (GUI.Button (new Rect(350,100,200,80), "Load GameObject")) 
		{
			AssetLoader.Get().LoadGameObject("sp1",this.onLoaded);
		}
		if(GUI.Button(new Rect (350, 300, 200, 80),"Load File"))
		{
			AssetLoader.Get().LoadConfig("log",this.onConfig);
		}
		if(GUI.Button(new Rect (350,500,200,80),"Load Sound"))
		{
//			AssetLoader.Get().LoadSound("bumb",this.onSound);
			SoundManager sm = Singleton.getInstance(ClassName.SOUND_MANAGER) as SoundManager;
			sm.playSound("bumb");
		}

	}
	void onLoaded(string name, Object go, object callbackData)
	{
		Log.fansy.ScreenPrint ("load call back!");
		Instantiate(go);
	}

	void onConfig(string name, Object obj, object callbackData)
	{
		Log.fansy.ScreenPrint ("config call back!");
		Log.fansy.ScreenPrint (obj.ToString());
	}

}