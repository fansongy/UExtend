/*
 * 
 *This file show the way to use SoundManager 
 * 
 * 
 */

using UnityEngine;
using System.Collections;

public class SoundMain : MonoBehaviour {

	int BGMCount = 1;

	void Awake()
	{
		Singleton.getInstance(ClassName.SOUND_MANAGER);
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect (350,100,200,80),"Play Sound"))
		{
			SoundManager sm = Singleton.getInstance(ClassName.SOUND_MANAGER) as SoundManager;
			sm.playSound("bumb");
		}
		if(GUI.Button(new Rect (350,200,200,80),"Play Random Sound"))
		{
			SoundManager sm = Singleton.getInstance(ClassName.SOUND_MANAGER) as SoundManager;
			sm.playSound("act_hurry_up");
		}	
		if(GUI.Button(new Rect (350,300,200,80),"Play BGM"))
		{
			SoundManager sm = Singleton.getInstance(ClassName.SOUND_MANAGER) as SoundManager;
			if(BGMCount == 1)
			{
				sm.playBGM("act_hurry_up_1");
				BGMCount = 2;
			}
			else
			{
				sm.playBGM("act_hurry_up_2");
				BGMCount = 1;
			}
		}
	}
}
