using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {

	AsyncOperation async;

	void Start()
	{
		StartCoroutine(loadScene());
		StartCoroutine(loadAction());
	}

	IEnumerator loadScene()
	{
		yield return StartCoroutine(preAction());
		//异步读取场景。
		//Globe.loadName 就是A场景中需要读取的C场景名称。
		string sceneName = (string)SceneSwitcher.getInstance().getDict()[SceneSwitcher.NEXTSCENE];
		async = Application.LoadLevelAsync(sceneName);

		yield return StartCoroutine(endAction());

		//读取完毕后返回， 系统会自动进入C场景
		yield return async;
		
	}

	protected virtual IEnumerator preAction()
	{
		Debug.Log("Begin Loading");
//		yield return new WaitForSeconds(0.1f);
		yield return null;
	}


	protected virtual IEnumerator loadAction()
	{
		Debug.Log("Loading...");
		yield return null;
	}
	protected virtual IEnumerator endAction()
	{
		Debug.Log("End Load");
		yield return null;
	}

	void OnGUI()
	{
		GUI.Label(new Rect(100,200,200,200),"Loading...");
	}
}
