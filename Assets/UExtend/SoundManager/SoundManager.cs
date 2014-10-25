using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {


	Dictionary<string,AudioClip> soundData = new Dictionary<string, AudioClip>();

	void Awake()
	{

	}

	public void addSoundClip(string key,AudioClip clip)
	{
		if(key != "" && clip != null)			
		{
			soundData.Add(key,clip);
		}
		else 
		{
			Debug.LogError("SoundManager::addSoundClip() - Add Error key : "+key+" sound :"+clip);
		}
	}

	public void playSound(string key)
	{
		if(!soundData.ContainsKey(key))
		{
			Debug.LogError("SoundManager::playSound() - can't find the sound of key: "+key);
			return;
		}
		else
		{
			audio.PlayOneShot(soundData[key]);
		}
	}

}
