/*
 * Read Json from string
 * get common data and leveled data
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectConfig {

	Dictionary<string,object> configTable = new Dictionary<string, object>();

	public ObjectConfig() {}
	~ObjectConfig() {}
		
	public virtual void initialize(string jsonString)
	{
		configTable = MiniJSON.Json.Deserialize(jsonString) as Dictionary<string,object>;
	}

	public object getConfig(string key,int level = 0)
	{
		if(configTable.Count == 0)
		{
			Log.Asset.Print("ObjectConfig::getConfig() - configTable is empty");
			return null;
		}
		if(!configTable.ContainsKey(key))
		{
			Log.Asset.Print("ObjectConfig::getConfig() - don't have {0} key ",key);
			return null;
		}
		else
		{
			object value = configTable[key];
			if( value is List<object>)
			{
				var asDict = value as List<object>;
				//leveled
				if(asDict.Count-1 <level)
				{
					Log.Asset.Print("ObjectConfig::getConfig() - level: {0} is larger than Dict count: {1},key is {2} ",level,asDict.Count,key);
					return null;
				}
				else
				{
					return asDict[level];
				}
			}
			else
			{
				return configTable[key];
			}
		}
	}
		
}
