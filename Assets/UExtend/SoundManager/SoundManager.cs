/*
 * SoundManager is design to play sound in 2D Game
 * 
 * It is a Singleton and the AudioSource is attached to camera
 * 
 * It support mutiply play sound which is managed by a AudioSource pool
 * 
 * Usage:
 * 	1.Make the sounds copyed to the project , and change the path of sound in Asset.cs
 *  2.Run the python script 'SoundMake.py' which is in the same dirctory. 
 * 		It will export a sound config file which are all files supported in sound dir to config dir
 *  3.call follow code to play sound:
 * 		SoundManager sm = Singleton.getInstance(ClassName.SOUND_MANAGER) as SoundManager;
 *		sm.playSound("bumb");
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

	const int MAX_SOUNDS = 5;

	Dictionary<string,AudioClip> soundData = new Dictionary<string, AudioClip>();
	List<AudioSource> player = new List<AudioSource>();

	void Awake()
	{
		loadAllSound();

		if(Camera.main.audio == null)
		{
			Camera.main.gameObject.AddComponent<AudioSource>();
		}
		player.Add(Camera.main.audio);
	}

	AudioSource getFreeSource()
	{
		//GC
		if(player.Count > MAX_SOUNDS )
		{
//			Debug.Log("Run Sound GC");
			List<AudioSource> newPlayer = new List<AudioSource>();
			foreach(var sound in player)
			{
				if(sound.isPlaying)
				{
					newPlayer.Add(sound);
				}
				else
				{
					Destroy(sound);
				}
			}
			player = newPlayer;

		}
		foreach(var sound in player)
		{
			if(!sound.isPlaying)
			{
				return sound;
			}
		}

		AudioSource newAudio = Camera.main.gameObject.AddComponent<AudioSource>();
		player.Add(newAudio);
		return newAudio;
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
			var source = getFreeSource();
			source.clip = soundData[key];
			source.Play();
		}
	}

	public void loadAllSound()
	{
		AssetLoader.Get().LoadConfig("soundlist",(string name, Object go, object callbackData)=>{
			string data = go.ToString();
			ObjectConfig cfg = new ObjectConfig();
			cfg.initialize(data);
			List<string>list = cfg.getAllKeys();
			foreach(string sound in list)
			{
				AssetLoader.Get().LoadSound(sound,this.onSound);
			}
		});
	}

    void onSound(string name, Object go, object callbackData)
    {
		soundData.Add(name,(AudioClip)go);
	}
}
