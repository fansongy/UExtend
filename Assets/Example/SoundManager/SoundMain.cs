/*
 * 
 *This file show the way to use SoundManager 
 * 
 * 
 */

using UnityEngine;
using System.Collections;

public class SoundMain : MonoBehaviour {

	void Awake()
	{
		Singleton.getInstance(ClassName.SOUND_MANAGER);
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect (350,100,200,80),"Load Sound"))
		{
			SoundManager sm = Singleton.getInstance(ClassName.SOUND_MANAGER) as SoundManager;
			sm.playSound("bumb");
		}
		
	}
}
