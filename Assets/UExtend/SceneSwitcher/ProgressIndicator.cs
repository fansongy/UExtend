/*
 * This a indicator for progress.
 * 
 * You can use startProgress to init this indicator.
 * 
 * Then you could call moveNext() to increase indicator, the callBack will be called each time.
 * 
 * When the indicator hit the end, finishCallBack will be called.
 * 
 */

using UnityEngine;
using System.Collections.Generic;

public class ProgressIndicator {
	int curPercent = 0;
	int totalPercent = 0;
	System.Action<float> progressCallBack;
	System.Action finishCallBack;

	public void startProgress(int total,System.Action<float> callBack,System.Action finish)
	{
		Debug.Log("start total is "+total);
		curPercent = 0;
		totalPercent  = total;
		progressCallBack = callBack;
		finishCallBack = finish;
	}

	public void moveNext()
	{
		curPercent++;
		if(curPercent < totalPercent)
		{
			float percent = (float)curPercent/totalPercent;
			if(progressCallBack != null)
			{
				progressCallBack(percent);
			}
		}
		else
		{
			if(finishCallBack != null)
			{
				finishCallBack();
			}

			curPercent = 0;
			totalPercent = 0;
			progressCallBack = null;
			finishCallBack = null;
		}
	}

}
