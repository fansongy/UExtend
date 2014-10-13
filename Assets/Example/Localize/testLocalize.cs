using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testLocalize : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AssetLoader.Get ().LoadConfig ("localize",this.onLoadText);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onLoadText(string name, Object obj, object callbackData)
	{
		string json = obj.ToString();
		Log.fansy.Print (json);
		ObjectConfig config = new ObjectConfig();
		config.initialize(json);
		Dictionary<string,object> title =(Dictionary<string,object>)config.getConfig("TID_TEST_LANGUAGE_TITLE");
		Log.fansy.Print(title.ToString());
		Log.fansy.Print(title["EN"].ToString());

	}
}
