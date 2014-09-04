/*
 * It's an enter of game struct.
 * I couldn't find a way to run it when the scene is loaded,so it should be attach to the main camera
 * 
 * by fansy
 */

using UnityEngine;
using System.Collections;

public class StructureGame : MonoBehaviour {

	void Awake()
	{
		GameObject view = new GameObject("view");
		view.AddComponent<TestView> ();

		Singleton.getInstance (ClassName.GAME_CONTROLLER);
	}

}
