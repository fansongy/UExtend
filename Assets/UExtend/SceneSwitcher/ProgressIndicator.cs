using UnityEngine;
using System.Collections.Generic;

public class ProgressIndicator {
	float curPercent = 0;
	float totalPercent = 0;
	System.Action<float> progressCallBack;

	public void startProgress(float total,System.Action<float> callBack)
	{
		curPercent = 0;
		totalPercent  = total;
	}

	public void moveNext()
	{
		if(curPercent < totalPercent)
		{
			curPercent++;
			float percent = curPercent/totalPercent;
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
