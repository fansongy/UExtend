using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

public static class MethodFinder
{
	public class Entry
	{
		public MonoBehaviour target;
		public MethodInfo method;
		public string typeName;
	}
	
	public static List<Entry> GetMethods (GameObject target,string[] exceptCompName)
	{
		MonoBehaviour[] comps = target.GetComponents<MonoBehaviour>();
		
		List<Entry> list = new List<Entry>();

		for (int i = 0, imax = comps.Length; i < imax; ++i)
		{
			MonoBehaviour mb = comps[i];
			if (mb == null) continue;
			
			string type = mb.GetType().ToString();
			int period = type.LastIndexOf('.');
			if (period > 0) type = type.Substring(period + 1);
			
			MethodInfo[] methods = mb.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public);

			for (int b = 0; b < methods.Length; ++b)
			{
				MethodInfo mi = methods[b];
				bool isExcept = false;
				for(int j = 0;j<exceptCompName.Length;++j)
				{
					if(type == exceptCompName[j])
					{
						isExcept = true;
						break;
					}
				}
				if(isExcept)
				{
					continue;
				}

				if (mi.GetParameters().Length == 0 && mi.ReturnType == typeof(void))
				{
					if (mi.Name != "StopAllCoroutines" && mi.Name != "CancelInvoke")
					{
						Entry ent = new Entry();
						ent.target = mb;
						ent.method = mi;
						ent.typeName = type;
						list.Add(ent);
					}
				}
			}
		}
		return list;
	}
	
	public static string[] GetMethodNames (List<Entry> list)
	{
		string[] names = new string[list.Count];
		
		for (int i = 0; i < list.Count; ++i)
		{
			Entry ent = list[i];
			string type = ent.target.GetType().ToString();
			int period = type.LastIndexOf('.');
			if (period > 0) type = type.Substring(period + 1);
			
			string del = type + "." + ent.method.Name;
			names[i] = del;
		}
		return names;
	}
}

