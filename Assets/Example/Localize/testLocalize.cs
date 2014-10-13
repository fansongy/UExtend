using UnityEngine;
using System.Collections;

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
		Log.fansy.Print (obj.ToString ());
	}
}
