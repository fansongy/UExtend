/*
 * Bundle Maker by fansy
 * 
 *   v0.1 only can bundle the prefab as single assetbundle. 
 *        config file not use buddle just copy to the target directory.  -_-b
 * 
 * 
 */ 

using UnityEngine;
using UnityEditor;

public class BundleMaker
{

	static BuildTarget buildTarget = BuildTarget.Android;

	static void makeBundle(AssetFamily type,Object[] SelectedAsset)
	{
		if (SelectedAsset.Length == 1) 
		{
			Object obj = SelectedAsset[0];
			Asset asset = Asset.Create(obj.name, type, false, false, false);
			Debug.Log ("打包资源:" + type);
//			string targetPath = asset.GetPath();
			string targetPath = Application.dataPath + "/StreamingAssets/" + obj.name + ".unity3d";
			if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies,buildTarget)) {
				Debug.Log(obj.name +"资源打包成功");
			} 
			else 
			{
				Debug.Log(obj.name +"资源打包失败");
			}
		}
		else
		{
			//遍历所有的游戏对象
			foreach (Object obj in SelectedAsset) 
			{
				Asset asset = Asset.Create(obj.name, type, false, false, false);
				Debug.Log ("打包资源:" + type);
//				string targetPath = asset.GetPath();
				string targetPath = Application.dataPath + "/StreamingAssets/" + obj.name + ".unity3d";
//				string sourcePath = AssetDatabase.GetAssetPath (obj);
				//本地测试：建议最后将Assetbundle放在StreamingAssets文件夹下，如果没有就创建一个，因为移动平台下只能读取这个路径
				//StreamingAssets是只读路径，不能写入
				//服务器下载：就不需要放在这里，服务器上客户端用www类进行下载。
				if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies,buildTarget)) {
					Debug.Log(obj.name +"资源打包成功");
				} 
				else 
				{
					Debug.Log(obj.name +"资源打包失败");
				}
			}
		}
		//刷新编辑器
		AssetDatabase.Refresh (); 
	}

	static void makeIntoBundle(AssetFamily type,Object[] SelectedAsset,string bundleName)
	{
		Asset asset = Asset.Create(bundleName, type, false, false, false);
		Debug.Log ("打包资源:" + type);
		string targetPath = asset.GetPath();
		if (BuildPipeline.BuildAssetBundle (null, SelectedAsset, targetPath, BuildAssetBundleOptions.CollectDependencies,buildTarget)) {
			Debug.Log("集成资源打包成功");
		} 
		else 
		{
			Debug.Log("集成资源打包失败");
		}
		//刷新编辑器
		AssetDatabase.Refresh (); 
	}


	static void CreateAssetBunldesConfig ()
	{
		//获取在Project视图中选择的所有游戏对象
		Object[] SelectedAsset = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);
		makeBundle (AssetFamily.Config, SelectedAsset);
	}

	[MenuItem("Fansy Bundle/Create AssetBunldes Prefabs")]
	static void CreateAssetBunldesPrefabs ()
	{
		
		Caching.CleanCache ();		
		Object[] SelectedAsset = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);
		
		foreach (Object obj in SelectedAsset) 
		{
			Debug.Log ("Create AssetBunldes name :" + obj);
		}
		// still bundle split
		makeBundle (AssetFamily.GameObject, SelectedAsset);
//		makeIntoBundle (AssetFamily.GameObject, SelectedAsset,"GameObject");
	}

	[MenuItem("Fansy Bundle/Create AssetBunldes ALL(From MOMO)")]
	static void CreateAssetBunldesALL ()
	{
		
		Caching.CleanCache ();
		
		string Path = Application.dataPath + "/StreamingAssets/ALL.assetbundle";
		
		Object[] SelectedAsset = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);
		
		foreach (Object obj in SelectedAsset) 
		{
			Debug.Log ("Create AssetBunldes name :" + obj);
		}
		
		//这里注意第二个参数就行
		if (BuildPipeline.BuildAssetBundle (null, SelectedAsset, Path, BuildAssetBundleOptions.CollectDependencies)) {
			AssetDatabase.Refresh ();
		} else {
			
		}
	}
	//	[MenuItem("Fansy Bundle/Create Scene")]
	//	static void CreateSceneALL ()
	//	{
//		//清空一下缓存
//		Caching.CleanCache();
//		string Path = Application.dataPath + "/StreamingAssets/MyScene.unity3d";
//		string  []levels = {"Assets/Bundle/Bundle.unity"};
//		//打包场景
//		BuildPipeline.BuildPlayer( levels, Path,buildTarget, BuildOptions.BuildAdditionalStreamedScenes);
//		AssetDatabase.Refresh ();
//	}
}