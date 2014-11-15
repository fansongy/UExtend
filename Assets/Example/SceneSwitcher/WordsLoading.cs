/*
 * This file show the children class of loading, which should attach our loading scene.
 * 
 */ 

using UnityEngine;
using System.Collections;

public class WordsLoading : Loading {


	bool  prepare = true;

	protected override IEnumerator preAction()
	{
		yield return new WaitForSeconds(1);
		prepare = false;

	}

	void OnGUI()
	{
		if(prepare)
		{
			GUI.Label(new Rect(200,200,200,200),"Welcome~");
		}
		else
		{
			GUI.Label(new Rect(200,200,200,200),"Loading......");
		}
	}
}
