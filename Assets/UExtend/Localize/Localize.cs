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
}
