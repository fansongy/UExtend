using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testLocalize : MonoBehaviour {

	ObjectConfig languageText = null;

	// Use this for initialization
	void Start () {
		AssetLoader.Get ().LoadConfig ("localize",this.onLoadText);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onLoadText(string name, Object obj, object callbackData)
	{
		languageText = new ObjectConfig();
		languageText.initialize(obj.ToString());
		Log.fansy.Print(getStringByTID("TID_TEST_LANGUAGE_TITLE","N"));
	}

	string getStringByTID(string tid,string lang)
	{
		if(languageText != null)
		{
			Dictionary<string,object> title =(Dictionary<string,object>)languageText.getConfig(tid);
			Log.fansy.Print(title.ToString());
			object result;
			if(title.TryGetValue(lang,out result))
			{
				return result.ToString();
			}
			else
			{
				Log.Asset.Print("getStringByTID() - can't find the lang {0} in {1},check the config file",lang,tid); 
				return "???";
			}

		}
		else
		{
			Log.Asset.Print("getStringByTID() - can't find the languageText,makesure it's loaded"); 
			return "???";
		}
	}
}
