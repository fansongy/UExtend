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

		string sceneName = (string)SceneSwitcher.getInstance().getDict()[SceneSwitcher.NEXTSCENE];
		async = Application.LoadLevelAsync(sceneName);

		yield return StartCoroutine(endAction());

		yield return async;
		
	}

	protected virtual IEnumerator preAction()
	{
		yield return null;
	}


	protected virtual IEnumerator loadAction()
	{
		yield return null;
	}
	protected virtual IEnumerator endAction()
	{
		yield return null;
	}

}
