using UnityEngine;
using System.Collections;

public class testSp : MonoBehaviour {
	[SerializeField]
	private string testName;
	[SerializeField]
	private Rect rect;
	
	void Awake () 
	{
		Debug.Log("my name is "+ testName);
	}
	void OnGUI()
	{
		GUI.Label (rect, testName);
	}
}
