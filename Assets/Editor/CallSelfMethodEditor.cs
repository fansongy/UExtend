/*
 * Editor of CallSelfMethod
 *
 * save the selected method to CallSelfMethod
 * 
 */ 

using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

[CustomEditor(typeof(CallSelfMethod))]
public class CallSelfMethodEditor : Editor {
	

	public override void OnInspectorGUI ()
	{

		base.OnInspectorGUI();

		GUILayout.Space(20f);
		CallSelfMethod call = target as CallSelfMethod;

		int curSelect = call.functionIndex;

		List<MethodFinder.Entry> info = MethodFinder.GetMethods(call.gameObject,call.exceptCompName);
		string[] names = MethodFinder.GetMethodNames(info);
		if(names.Length == 0)
		{
			return;
		}
		else if(names.Length <curSelect)
		{
			curSelect = names.Length-1;
		}
		curSelect = GUILayout.SelectionGrid(curSelect,names,1);
		call.functionName = names[curSelect];
		call.functionIndex = curSelect;
	}
}
