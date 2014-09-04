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
using System.IO;
using System.Collections;


public class BundleMaker
{
	[MenuItem("UExtend/AssetLoader/Copy to Resources")]
	static void copyToResources ()
	{
		Caching.CleanCache ();
		string resDir = FileUtils.GetFullPathOfAssets("Resources");
		if(!Directory.Exists(resDir))
		{
			Directory.CreateDirectory(resDir);
			AssetDatabase.Refresh();
		}
		foreach( string types in  System.Enum.GetNames(typeof(AssetFamily)))
		{
			AssetFamily family = EnumUtils.GetEnum<AssetFamily>(types);
			Asset asset = Asset.Create("public", family, false, false, false);
			copyAssetToResources(asset,false);
		}

	}

	[MenuItem("UExtend/AssetLoader/Create AssetBunldes/Android")]
	static void CreateAndroid ()
	{
		CreateAssetBunldesALL(BuildTarget.Android);
	}

	[MenuItem("UExtend/AssetLoader/Create AssetBunldes/IOS")]
	static void CreateIOS ()
	{
		CreateAssetBunldesALL(BuildTarget.iPhone);
	}

	[MenuItem("UExtend/AssetLoader/Create AssetBunldes/WebPlayer")]
	static void CreateEditor ()
	{
		CreateAssetBunldesALL(BuildTarget.WebPlayer);
	}


	static void CreateAssetBunldesALL (BuildTarget target)
	{
		
		Caching.CleanCache ();

		string resDir = FileUtils.GetFullPathOfAssets("StreamingAssets");
		if(!Directory.Exists(resDir))
		{
			Directory.CreateDirectory(resDir);
			AssetDatabase.Refresh();
		}

		foreach( string types in  System.Enum.GetNames(typeof(AssetFamily)))
		{
			AssetFamily family = EnumUtils.GetEnum<AssetFamily>(types);
			Asset asset = Asset.Create("public", family, false, false, false);
			loadAsset(asset,target);
		}
		string tmpPath = FileUtils.GetFullBundleTempPath("");
		FileUtil.DeleteFileOrDirectory(tmpPath);
		AssetDatabase.Refresh();
	}

	static void copyAssetToResources(Asset asset,bool isBundle)
	{
		string fullSrcPath = FileUtils.GetFullPathOfAssets(asset.SourceDir);
		if(!Directory.Exists(fullSrcPath))
		{
//			Log.Asset.Print("BundleMaker.loadAsset() - sourceDir of {0} is not exist",fullSrcPath);
			return;
		}
		
		string tarPath = FileUtils.GetFullBundleTempPath(asset.SourceDir);
		if(!isBundle)
		{
			string tail = FileUtils.GetPathUnderAssets(fullSrcPath);
			tarPath = FileUtils.GetFullResourcesPath(tail);
		}
		if(Directory.Exists(tarPath))
		{
			if(EditorUtility.DisplayDialog("Warning","the "+FileUtils.GetPathUnderAssets(tarPath)+" is Exist. Are your sure to overwrite it?","Yes","No"))
			{
				FileUtil.DeleteFileOrDirectory(tarPath);
				FileUtil.CopyFileOrDirectory(fullSrcPath,tarPath);
				AssetDatabase.Refresh ();
			}
		}
		else
		{
			if(isBundle)
			{
				string tmpPath = FileUtils.GetFullBundleTempPath("");
				if(!Directory.Exists(tmpPath))
				{
					Directory.CreateDirectory(tmpPath);
				}
			}
			FileUtil.CopyFileOrDirectory(fullSrcPath,tarPath);
			AssetDatabase.Refresh ();
			if(!isBundle)
			{
				EditorUtility.DisplayDialog("Success","Copy "+asset.Family+" to Resources Success","OK");
			}
		}

	}

	static void loadAsset(Asset asset,BuildTarget platform)
	{
		copyAssetToResources(asset,true);
		string tarPath = FileUtils.GetFullBundleTempPath(asset.SourceDir);
		string tail = FileUtils.GetPathUnderResources(tarPath);
		Object[] files = Resources.LoadAll(tail);
		if(files.Length >0)
		{
			var bundleOptions =	BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets;
			string streamPath = Application.dataPath +"/"+ FileUtils.GetPathUnderAssets(asset.GetBundlePath());
			if (BuildPipeline.BuildAssetBundle (null, files,streamPath, bundleOptions,platform)) {
				AssetDatabase.Refresh ();
				EditorUtility.DisplayDialog("Success","The "+platform+"::"+asset.Family+" bundled success","OK");
			} else {
				Log.Asset.Print("BundleMaker.loadAsset() - make bandle: {0} failed",asset.Family);
			}
		}
	}

}