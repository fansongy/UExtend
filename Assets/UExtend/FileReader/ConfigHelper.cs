using UnityEngine;
using System.Collections.Generic;

public class ConfigHelper  {
	
	public static ConfigDict ParseJsonByResourcePath(string pathInResources)
	{
		var obj = Resources.Load(pathInResources);
		return ParseJsonByString(obj.ToString());
	}

	public static ConfigDict ParseJsonByString(string json)
	{
		Dictionary<string,object> jsonData = MiniJSON.Json.Deserialize(json) as Dictionary<string,object>;
		ConfigDict data = new ConfigDict(jsonData);
		return data;
	}

}