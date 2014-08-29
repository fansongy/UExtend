using UnityEngine;
using System.Collections;

public class TestLog : MonoBehaviour {

	void OnGUI()
	{
		if(GUI.Button(new Rect(100,100,100,50),"yo"))
		{
			Log.fansy.Print("yoyo check now");
			Log.fansy.LogLevelPrint("yes",LogLevel.Error);
			Log.fansy.ScreenPrint("Screen"+1);
		}
	}
}
