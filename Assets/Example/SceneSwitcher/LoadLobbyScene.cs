using UnityEngine;
using System.Collections;

public class LoadLobbyScene : MonoBehaviour {

	void Start()
	{
		StartCoroutine( LoadObj());
	}

	IEnumerator LoadObj()
	{
		ProgressIndicator indicator = SceneSwitcher.getInstance().getIndicator();
		var obj = Resources.Load("GamePrefab/sp1");
		for(int i = 0;i<5000;++i)
		{
			Instantiate(obj);
			if(i%10 == 0)
			{
				indicator.moveNext();
			}
		}
		yield return null;
	}

	void OnGUI()
	{
		GUI.Label(new Rect(300,200,100,100),"This is LobbyScene");
	}

}
