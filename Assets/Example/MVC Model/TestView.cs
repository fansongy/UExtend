/*
 * This is view class.
 * It listen the message from controller,which maybe sync or ansync.
 * After that,doing animation or moving  reflect the program receive the operate by user. 
 * 
 * by fansy
 */ 

using UnityEngine;
using System.Collections;

public class TestView : MonoBehaviour {

	private string text = "no msg";
	private GameModel gm = null;
	void Awake()
	{
		Messenger.AddListener<string> (MsgType.TEST_CLICK_MSG_1,OnTestMessage);
		Messenger.AddListener<string> (MsgType.TEST_NOTIFY_MSG_1, onNotifyMessage);
	}

	void Start()
	{
		gm = Singleton.getInstance (ClassName.GAME_MODEL) as GameModel;
	}

	void OnGUI()
	{
		GUI.TextArea (new Rect (300, 50, 100, 50), "" + gm.clickNum);
		GUI.TextArea (new Rect (300, 150,100, 50), text);
	}

	void OnTestMessage(string str)	       
	{
		text = str;
	}

	void onNotifyMessage(string str)
	{
		Debug.Log ("recv Notify press"+" says:"+str);
	}
}
