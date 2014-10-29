using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {

	AsyncOperation async;
	
	//读取场景的进度，它的取值范围在0 - 1 之间。
	int progress = 0;
	
	void Start()
	{
		StartCoroutine(loadScene());
	}

	IEnumerator loadScene()
	{
		//异步读取场景。
		//Globe.loadName 就是A场景中需要读取的C场景名称。
		string sceneName = (string)SceneSwitcher.getInstance().getDict()[SceneSwitcher.NEXTSCENE];
		async = Application.LoadLevelAsync(sceneName);
//		Application.LoadLevelAsync(sceneName);

		ProgressIndicator indicator = SceneSwitcher.getInstance().getIndicator();
		indicator.setCallBack(changeProccess);

		//读取完毕后返回， 系统会自动进入C场景
		yield return async;
		
	}

	void Update()
	{
		
		//在这里计算读取的进度，
		//progress 的取值范围在0.1 - 1之间， 但是它不会等于1
		//也就是说progress可能是0.9的时候就直接进入新场景了
		//所以在写进度条的时候需要注意一下。
		//为了计算百分比 所以直接乘以100即可
		progress =  (int)(async.progress *100);
//		progress++;
		//有了读取进度的数值，大家可以自行制作进度条啦。
		Debug.Log("xuanyusong" +progress);
		
	}

	void changeProccess(float percent)
	{
		Debug.Log("enter CallBack "+percent);
		progress = (int)percent*100;
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect( 100,180,300,60), "lOADING!!!!!" + progress);	
	}

}
