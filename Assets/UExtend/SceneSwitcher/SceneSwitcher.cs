using UnityEngine;
using System.Collections.Generic;

public class SceneSwitcher : MonoBehaviour {

	static SceneSwitcher s_instance;
	[SerializeField]
	private Dictionary<string,object> strDict = new Dictionary<string, object>();

	private string currentSceneName ;


	public const string NEXTSCENE = "nextScene";
	const string PRELOAD = "PreLoadObject";
	const string LOADINGSCENE = "Loading";

	ProgressIndicator progress = new ProgressIndicator();

	public static SceneSwitcher getInstance()
	{
		if(s_instance == null)
		{
			GameObject obj = new GameObject();
			obj.name = "SceneSwitcher";
			s_instance = obj.AddComponent<SceneSwitcher>();
			DontDestroyOnLoad(obj);
		}
		return s_instance;
	}

	public ProgressIndicator getIndicator()
	{
		return progress;
	}

	public Dictionary<string,object> getDict()
	{
		return strDict;
	}

	public void toScene(string target)
	{
		loadScene(target);
		initIndicator(null);
	}

	public void toScene(string target,System.Action<float> changeProcess)
	{
		loadScene(target);
		initIndicator(changeProcess);
	}

	void loadScene(string target)
	{
		strDict[NEXTSCENE] = target;
		currentSceneName = target;
		Application.LoadLevel(LOADINGSCENE);
	}

	void initIndicator(System.Action<float> changeProcess)
	{
		AssetLoader.Get().LoadConfig("sceneConfig",(string name, UnityEngine.Object obj, object callbackData)=>{
			ConfigDict cfg = ConfigHelper.ParseJsonByString(obj.ToString());
			Dictionary<string,object> dict = cfg[currentSceneName] as Dictionary<string,object>;
			List<object> list = dict[PRELOAD] as List<object>;
			progress.startProgress(list.Count,changeProcess);
		}); 	
	}

}
