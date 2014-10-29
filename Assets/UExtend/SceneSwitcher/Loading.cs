using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {


	
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
		AsyncOperation async = Application.LoadLevelAsync(sceneName);
//		Application.LoadLevelAsync(sceneName);

		ProgressIndicator indicator = SceneSwitcher.getInstance().getIndicator();
		indicator.setCallBack(changeProccess);
		jamTest();
		indicator.moveNext();
		jamTest();
		indicator.moveNext();
		jamTest();
		indicator.moveNext();

		//读取完毕后返回， 系统会自动进入C场景
		yield return async;
		
	}
	void jamTest()
	{
		for(int i = 0;i<10;++i)
		{
			i=i+1;
			if(i<65535)
			{
				i = 0;
			}
		}
	}

	void changeProccess(float percent)
	{
		progress = (int)percent*100;
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect( 100,180,300,60), "lOADING!!!!!" + progress);	
	}

}
