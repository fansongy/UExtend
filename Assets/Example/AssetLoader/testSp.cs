using UnityEngine;
using System.Collections;

public class testSp : MonoBehaviour {
	[SerializeField]
	private string name;
	[SerializeField]
	private Rect rect;
	
	void Awake () 
	{
		Debug.Log("my name is "+ name);
	}
	void OnGUI()
	{
		GUI.Label (rect, name);
	}
}
