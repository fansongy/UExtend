using UnityEngine;
using System.Collections;

public class testLocalize : MonoBehaviour {

	string title = "???";
	string desc = "???";
	string[] langs = { "CN","EN","FR","RU","Error" };
	int curSelected = 0;


	// Use this for initialization
	void Start () {
		Localize.Get().setCurLang(langs[curSelected]);
	}
	
	void OnGUI()
	{
		curSelected = GUI.SelectionGrid(new Rect(100,100,300,80),curSelected,langs,5);
		Localize.Get().setCurLang(langs[curSelected]);
		title = Localize.Get().getStringByTID("TID_TEST_LANGUAGE_TITLE");
		desc = Localize.Get().getStringByTID("TID_TEST_LANGUAGE_DES");
		GUILayout.Label(title);
		GUILayout.Label(desc);
	}

}
