/*
 * The Asset Class is used to record then class info
 * 
 * v0.1 by fansy
 * 		the dictionary set the directory of resource type
 * 
 * 
 */ 

using System;
using System.Collections.Generic;
using System.IO;
public class Asset
{
	private class AssetFamilyPathInfo
	{
		public string bundleDir;  	// dir of bundle
		public string sourceDir; //dir in program
		public string resourceDir; 	//dir in resource 
		public string[] exts;   
	}

	public enum LoadModel 
	{
		FromResources,
		FromBundle
	}

	LoadModel _loadModel = LoadModel.FromResources;

	public LoadModel loadModel() {return _loadModel;}

	private static readonly Dictionary<AssetFamily, Asset.AssetFamilyPathInfo> paths;
	private Dictionary<LoadModel,string> m_path = new Dictionary<LoadModel, string>();
	public string Name
	{
		get;
		private set;
	}
	public AssetFamily Family
	{
		get;
		private set;
	}
	public bool Persistent
	{
		get;
		private set;
	}
	public bool LoadAsync
	{
		get;
		private set;
	}
	public bool PreloadOnly
	{
		get;
		private set;
	}

	public string SourceDir
	{
		get
		{
			return Asset.paths[this.Family].sourceDir;
		}
	}
	public string[] Extensions
	{
		get
		{
			return Asset.paths[this.Family].exts;
		}
	}
	private Asset(string name, AssetFamily family, bool persistent, bool loadAsync, bool preloadOnly)
	{
		this.Name = name;
		this.Family = family;
		this.Persistent = persistent;
		this.LoadAsync = loadAsync;
		this.PreloadOnly = preloadOnly;
	}
	static Asset()
	{
		Dictionary<AssetFamily, Asset.AssetFamilyPathInfo> dictionary = new Dictionary<AssetFamily, Asset.AssetFamilyPathInfo>();
		dictionary.Add(AssetFamily.Config, new Asset.AssetFamilyPathInfo
		{
			bundleDir = FileUtils.PersistentDataPath + "{0}.unity3d",
			sourceDir = "Configs",
			resourceDir = "Configs/{0}",
			exts = new string[]
			{
				"config"
			}
		});
		dictionary.Add(AssetFamily.Sound, new Asset.AssetFamilyPathInfo
		{
			bundleDir = FileUtils.PersistentDataPath + "Sounds/{0}.unity3d",
			sourceDir = "Sounds",
			resourceDir = "Sounds/{0}",
			exts = new string[]
			{
				"prefab"
			}
		});
		dictionary.Add(AssetFamily.Texture, new Asset.AssetFamilyPathInfo
		{
			bundleDir = FileUtils.PersistentDataPath + "Textures/{0}.unity3d",
			sourceDir = "Textures",
			resourceDir = "Textures/{0}",
			exts = new string[]
			{
				"psd"
			}
		});
		dictionary.Add(AssetFamily.Screen, new Asset.AssetFamilyPathInfo
		{
			bundleDir = FileUtils.PersistentDataPath + "UIScreens/{0}.unity3d",
			sourceDir = "UIScreens",
			resourceDir = "UIScreens/{0}",
			exts = new string[]
			{
				"prefab"
			}
		});
		dictionary.Add(AssetFamily.GameObject, new Asset.AssetFamilyPathInfo
		{
			bundleDir = FileUtils.PersistentDataPath + "{0}.unity3d",
			sourceDir = "GamePrefab",
			resourceDir = "GamePrefab/{0}",
			exts = new string[]
			{
				"prefab"
			}
		});
		Asset.paths = dictionary;
	}
	public static Asset Create(string assetName, AssetFamily family, bool persistent = false, bool loadAsync = false, bool preloadOnly = false)
	{
		return new Asset(assetName, family, persistent, loadAsync, preloadOnly)
		{
			m_path = new Dictionary<LoadModel, string>()
			{
				{LoadModel.FromBundle,string.Format(Asset.paths[family].bundleDir, family.ToString())},
				{LoadModel.FromResources,string.Format(Asset.paths[family].resourceDir, assetName)}
			}
		};
	}

	public string GetPath()
	{
		return this.m_path[_loadModel];
	}

	public string GetBundlePath()
	{
		return this.m_path[LoadModel.FromBundle];
	}

//	public void Save(byte[] bytes, string url, string cachePath)
//	{
//		string text = "Data/cache/" + cachePath;
//		try
//		{
//			System.IO.Directory.CreateDirectory(Path.GetDirectoryName(text));
//			File.WriteAllBytes(text, bytes);
////			StreamingLog.LogAssetSaved(this, url, cachePath);
//		}
//		catch (Exception ex)
//		{
////			StreamingLog.LogAssetSaveFailed(this, url, cachePath, ex.get_Message());
//		}
//	}

//	public static bool IsLocal(string cachePath)
//	{
//		if (cachePath == null)
//		{
//			return false;
//		}
//		string text = string.Format("{0}/{1}", "Data/cache/", cachePath);
//		return File.Exists(text);
//	}
}
