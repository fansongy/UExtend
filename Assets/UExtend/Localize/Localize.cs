/*
 * This class is used to control localize text
 * 
 * Usage:
 * 		1.set current language
 * 			Localize.Get().setCurLang(langs[curSelected]);
 * 		2.call func to get the text
 * 			Localize.Get().getStringByTID("TID_TEST_LANGUAGE_TITLE");
 * Script:	
 * 	In order to add TID easily, I wrote a python script to manage TID.
 *  It's named TIDMake which is in the same level with this file.
 *  It can add,change TID, and also can export one kind of language for translating.
 * 
 */ 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Localize : MonoBehaviour {

	string curLang = "CN";
	ObjectConfig languageText = null;

	private static Localize s_instance;

	public static Localize Get()
	{
		if(!s_instance)
		{
			GameObject loc = new GameObject();
			loc.name = "Localize";
			s_instance = loc.AddComponent<Localize>();
			s_instance.initData();
			return s_instance;
		}
		return Localize.s_instance;
	}

	void initData()
	{
		setDefaultLang();
		AssetLoader.Get ().LoadConfig ("localize",this.onLoadText);
	}

	void setDefaultLang()
	{
		setCurLang("CN");
	}

	public void setCurLang(string lang)
	{
		curLang = lang;
	}

	void onLoadText(string name, Object obj, object callbackData)
	{
		languageText = new ObjectConfig();
		languageText.initialize(obj.ToString());
	}

	public string getStringByTID(string tid)
	{
		if(languageText != null)
		{
			Dictionary<string,object> title =(Dictionary<string,object>)languageText.getConfig(tid);
			if(title == null)
			{
				Log.Asset.Print("getStringByTID() - can't find the tid {0} in lang {1}",tid,curLang);
				return "???";
			}
			object result;
			if(title.TryGetValue(curLang,out result))
			{
				return result.ToString();
			}
			else
			{
				Log.Asset.Print("getStringByTID() - can't find the lang {0} in {1},check the config file",curLang,tid); 
				return "???";
			}
		}
		else
		{
			Log.Asset.Print("getStringByTID() - can't find the languageText,makesure it's loaded"); 
			return "???";
		}
	}

	public string getStringByTID(string tid,object arg0)
	{
		string str = getStringByTID(tid);
		if(str != "???")
		{
			str = string.Format(str,arg0); 
		}
		return str;
	}
}
