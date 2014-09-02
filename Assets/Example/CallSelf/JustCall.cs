/*
 * test self call ,see log 
 */

using UnityEngine;
using System.Collections;

public class JustCall : MonoBehaviour {

	public void canYouCallMe()
	{
		Debug.Log("woo real call me!");
	}
	public void dontCallMe()
	{
		Debug.Log("Don't call me again");
	}
}
