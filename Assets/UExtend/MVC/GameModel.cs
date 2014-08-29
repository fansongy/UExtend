/*
 * GameModel class in MVC.
 * It store all global data.So it must use with Singleton
 * 
 * Usage Example
 *   1.GameModel gm = Singleton.getInstance(ClassName.GAME_MODEL) as GameModel;
 *     gm.clickNum++;
 * 
 * by fansy
 */ 

using UnityEngine;
using System.Collections;

public class GameModel : MonoBehaviour {

	public int clickNum {
		get;set;
	}
	
}
