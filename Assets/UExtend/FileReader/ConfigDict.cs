/*
 * Read Json from string
 * get common data and leveled data
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;

public class ConfigDict :Dictionary<string,object>{
	
	public ConfigDict(Dictionary<string,object> json):base(json)
	{
	}
	
	public object getConfig(string key,int level = 0)
	{
		if(this.Count == 0)
		{
			Log.Asset.Print("ObjectConfig::getConfig() - configTable is empty");
			return null;
		}
		if(!this.ContainsKey(key))
		{
			Log.Asset.Print("ObjectConfig::getConfig() - don't have key "+key);
			return null;
		}
		else
		{
			object value = this[key];
			if( value is List<object>)
			{
				var asDict = value as List<object>;
				//leveled
				if(asDict.Count-1 <level)
				{
					Log.Asset.Print("ObjectConfig::getConfig() - level:"+level+"  is larger than Dict count: "+asDict.Count+",key is "+key);
					return null;
				}
				else
				{
					return asDict[level];
				}
			}
			else
			{
				return this[key];
			}
		}
	}
}
