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
		public string format;
		public string sourceDir;
		public string[] exts;
	}
	private static readonly Dictionary<AssetFamily, Asset.AssetFamilyPathInfo> paths;
	private string m_path;
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
	public string BundleDir
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
		// Note: this type is marked as 'beforefieldinit'.
		Dictionary<AssetFamily, Asset.AssetFamilyPathInfo> dictionary = new Dictionary<AssetFamily, Asset.AssetFamilyPathInfo>();
		dictionary.Add(AssetFamily.Config, new Asset.AssetFamilyPathInfo
		{
			format = "Configs/{0}.config",
			sourceDir = FileUtils.PersistentDataPath,
			exts = new string[]
			{
				"config"
			}
		});
		dictionary.Add(AssetFamily.Sound, new Asset.AssetFamilyPathInfo
		{
			format = "Sounds/{0}.unity3d",
			sourceDir = "Assets/Game/Sounds",
			exts = new string[]
			{
				"prefab"
			}
		});
		dictionary.Add(AssetFamily.Texture, new Asset.AssetFamilyPathInfo
		{
			format = "Textures/{0}.unity3d",
			sourceDir = "Assets/Game/Textures",
			exts = new string[]
			{
				"psd"
			}
		});
		dictionary.Add(AssetFamily.Screen, new Asset.AssetFamilyPathInfo
		{
			format = "UIScreens/{0}.unity3d",
			sourceDir = "Assets/Game/UIScreens",
			exts = new string[]
			{
				"prefab"
			}
		});
		dictionary.Add(AssetFamily.GameObject, new Asset.AssetFamilyPathInfo
		{
			format = "{0}.unity3d",
			sourceDir = FileUtils.PersistentDataPath,
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
			m_path = Asset.paths[family].sourceDir+string.Format(Asset.paths[family].format, assetName)
		};
	}

	public string GetPath()
	{
		return this.m_path;
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
