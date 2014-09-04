using UnityEngine;
using System.Collections;


public abstract class CurveTween<T> :MonoBehaviour {

	public delegate void AnimRunningCallBack(float progress);
	public delegate void AnimFinishCallBack(bool isFinished);

	[SerializeField]
	AnimationCurve curve;

	[SerializeField]
	float duration = 0;


	bool isPlaying = false;
	float startTime = 0;
	bool revFlag = false;

	AnimRunningCallBack runningCallBack = null;
	AnimFinishCallBack finishCallBack = null;



	protected void setData(float duration)
	{
		if(isPlaying)
		{
			forceStop();
		}
		this.startTime = Time.time;
		this.duration = duration;
	}
		
	public void play(AnimRunningCallBack runCallBack,AnimFinishCallBack finishCallBack = null)
	{
		this.runningCallBack = runCallBack;
		this.finishCallBack = finishCallBack;
		startTime = Time.time;
		isPlaying = true;
		revFlag = false;
	}

	public void revPlay(AnimRunningCallBack runCallback,AnimFinishCallBack endCallback = null)
	{
		this.runningCallBack = runCallback;
		this.finishCallBack = endCallback;
		startTime = Time.time;
		isPlaying = true;
		revFlag = true;
	}

	public void forceStop(AnimFinishCallBack callback = null)
	{	if(isPlaying)
		{
			isPlaying = false;
			revFlag = false;
			startTime = 0;
			duration = 0;
		}
		if(callback != null)
		{
			callback(true);
		}
	}

	void FixedUpdate () {
		if(isPlaying)
		{
			if (Time.time > startTime + duration) {		
				isPlaying = false;
				if(!revFlag)
				{
					runningCallBack(curve.Evaluate(1));
				}
				else
				{
					runningCallBack(curve.Evaluate(0));
				}
				if(finishCallBack != null)
				{
					finishCallBack(true);
				}
			}
			else
			{
				if(runningCallBack != null)
				{
					if(!revFlag)
					{
						runningCallBack(curve.Evaluate((Time.time - startTime)/ duration));
					}
					else
					{
						runningCallBack(curve.Evaluate((duration-(Time.time - startTime))/ duration));
					}
				}
			}
		}
	}

	public bool isRunning()
	{
		return isPlaying;
	}
}
