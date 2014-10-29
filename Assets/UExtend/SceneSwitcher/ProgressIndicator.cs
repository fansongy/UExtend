using UnityEngine;
using System.Collections.Generic;

public class ProgressIndicator {
	int curPercent = 0;
	int totalPercent = 0;
	System.Action<float> progressCallBack;

	public void startProgress(int total,System.Action<float> callBack)
	{
		Debug.Log("start total is "+total);
		curPercent = 0;
		totalPercent  = total;
	}

	public void moveNext()
	{
		if(curPercent < totalPercent)
		{
			curPercent++;
			float percent = (float)curPercent/totalPercent;
			if(progressCallBack != null)
			{
				progressCallBack(percent);
			}
		}
	}

	public void setCallBack(System.Action<float> callBack)
	{
		progressCallBack = callBack;
	}
}
