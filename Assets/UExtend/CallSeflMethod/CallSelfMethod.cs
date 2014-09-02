/*
 * This Component can set a method called after few second calling Start()
 * 
 * It is designed to use in UntiyTestTools,to tragger the self call
 * 
 * Usage:
 *     1.the avaliable method will appare under as button,click to choose the method you want to call
 * 	   2.you can set delay call by delayCall
 *     3.you can except someComponent by change exceptCompName
 * 
 */ 

using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class CallSelfMethod : MonoBehaviour {
	
	public string functionName = "no function";
	public float delayCall=0;

	[HideInInspector]
	public int functionIndex = 0;

	public string[] exceptCompName = 
	{
		"AssertionComponent"
	};

	void Start()
	{
		StartCoroutine(runMethod());
	}

	IEnumerator runMethod()
	{
		yield return new WaitForSeconds(delayCall);
		List<MethodFinder.Entry> info = MethodFinder.GetMethods(gameObject,exceptCompName);
		object[] param = new object[0];
		if(info.Count ==0)
		{
			Debug.LogError("CallSelfMethod.runMethod() - the Method info list is empty");
		}
		else
		{
			MethodFinder.Entry entry = info[functionIndex];
			if(entry != null)
			{
				entry.method.Invoke(gameObject.GetComponent(entry.typeName),param);
			}
			else
			{
				Debug.LogError("CallSelfMethod.runMethod() - can't find Entry at"+functionIndex);
			}
		}
	}
}
