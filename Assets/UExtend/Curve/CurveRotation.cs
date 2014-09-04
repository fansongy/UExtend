using UnityEngine;
using System.Collections;

public class CurveRotation : CurveTween<Vector3> {

	public Vector3 rotion;
	private Quaternion srcRotation;
	
	public void play()
	{
		srcRotation = transform.localRotation;
		base.play (onRoation, onFinish);
	}

	public void revPlay()
	{
		srcRotation = transform.localRotation;
		base.revPlay(onRoation,onRevFinish);
	}
	void onRoation(float progress)
	{
		transform.localRotation = srcRotation * Quaternion.Euler (rotion * progress);
	}


	void onFinish(bool isFinish)
	{
//		Debug.Log ("result is "+isFinish);
	}

	void onRevFinish(bool isFinish)
	{
		rotion *= -1;
	}
}
