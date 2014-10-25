using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

	const int MAX_SOUNDS = 3;

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
