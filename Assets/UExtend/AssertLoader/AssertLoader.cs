/*
 * Asset Loader is the interface class to manage the Loaded AssetBundle
 * 
 * v0.1 by fansy
 * Feature:
 * 		1.load the prefab from StreamAssets directory. The AssetBundle should be splite packaged.
 *      2.load the config file in StreamAssets.The file must be unpackaged. 
 *        Because It should be use the second Level Read. It maybe implement in next version.
 * 	
 * Usage:
 * 		1.You can use LoadGameObject() to load the resource from the AssetBundle:
 *        	 AssetLoader.Get().LoadGameObject("sp1",new AssetLoader.GameObjectCallback(this.onLloaded));
 *      2.Then to implement the callback:
 * 			void onLloaded(string name, GameObject go, object callbackData)
 *			{
 *				Log.fansy.ScreenPrint ("load call back!");
 *			}
 *      3.You can use LoadConfig to load config File
 * 			AssetLoader.Get().LoadConfig("log",new AssetLoader.ObjectCallback(this.onConfig));
 * 		4. Then to implement the callback:
 * 			void onConfig(string name, System.Object obj, object callbackData)
 *			{
 *				Log.fansy.ScreenPrint ("config call back!");
 *				WWW bundle = obj as WWW;
 *				Log.fansy.ScreenPrint (bundle.text);
 *			}
 * Tip:
 * When it load error by "<url> malformed",it cause by wrong plafrom
 * 
 * 
 */

//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
	public delegate void ObjectCallback(string name, UnityEngine.Object obj, object callbackData);
//	public delegate void GameObjectCallback(string name, GameObject go, object callbackData);
//	public delegate void FileCallback(string path, WWW file, object callbackData);
	public delegate void ProgressCallback(string name, float progress, object callbackData);
	private static Dictionary<AssetFamily, AssetCache> cacheTable = new Dictionary<AssetFamily, AssetCache>();
	private static Dictionary<AssetFamily, WWW> loadedBundle = new Dictionary<AssetFamily, WWW>();

	private static AssetLoader s_instance;

	public void PreloadBundles()
	{
		//deside which should be proload
	}

	private void Start()
	{
		this.PreloadBundles();
	}
	private void OnApplicationQuit()
	{
		ForceClearAllCaches();
	}
	public static AssetLoader Get()
	{
		if(!s_instance)
		{
			GameObject loader = new GameObject();
			loader.name = "AssetLoader";
			s_instance = loader.AddComponent<AssetLoader>();
			return s_instance;
		}
		return AssetLoader.s_instance;
	}
	
//	private IEnumerator LoadResInBundle(Asset asset, WWW bundle, bool usePrefabPosition, AssetLoader.GameObjectCallback callback, object callbackData)
//	{
//		AssetCache.CachedAsset cache =  cacheTable[asset.Family].GetItem(asset);
//		UnityEngine.Object target = null;
//		if (cache != null) 
//		{
//			target = cache.GetAssetObject();
//		}
//		else
//		{
//			if(bundle.assetBundle == null)
//			{
//				Debug.LogError("Can't find the bundle");
//				yield return null;
//			}
//			target = bundle.assetBundle.Load (asset.Name);
//		}
//		CacheAsset (asset, target);
//		GameObject go = Instantiate (target) as GameObject;
//		if(usePrefabPosition == false)
//		{
//			go.transform.position = new Vector3(0,0,0);
//		}
//		if (callback != null)
//		{
//			callback (asset.Name, go, null);
//		}
//		yield return null;
//	}
//
//	private bool LoadGameObject(string assetName, AssetFamily family, bool usePrefabPosition, AssetLoader.GameObjectCallback callback, AssetLoader.ProgressCallback progressCallback, object callbackData, bool persistent = false, bool loadAsync = false, bool preloadOnly = false)
//	{
//		if (string.IsNullOrEmpty(assetName))
//		{
//			Debug.LogWarning("AssetLoader.LoadGameObject() - name was null or empty");
//			return false;
//		}
//		Asset asset = Asset.Create(assetName, family, persistent, loadAsync, preloadOnly);
//		LoadCachedGameObject(asset, usePrefabPosition, callback, progressCallback, callbackData);
//		return true;
//	}
//
//
//	private void LoadCachedObject(Asset asset, AssetLoader.ObjectCallback callback, AssetLoader.ProgressCallback progressCallback, object callbackData)
//	{
//		if (asset.Name == null)
//		{
//			if (callback != null)
//			{
//				callback(null, null, callbackData);
//			}
//			return;
//		}
//
//		if(asset.loadModel() == Asset.LoadModel.FromBundle)
//		{
//			//find the AssetBundle loaded
//			WWW bundle = GetBundle(asset);
//			if (bundle != null)
//			{
//				if (callback != null)
//				{
//					callback(asset.Name, bundle, callbackData);
//				}
//				return;
//			}
//			else
//			{
//				//load the resource
//				StartCoroutine(this.CreateCachedObject(asset,callback,callbackData));
//			}
//		}
//		else if(asset.loadModel() == Asset.LoadModel.FromResources)
//		{
//			Debug.Log("Load Obj by Resources,Path is:"+asset.GetPath());
//			System.Object obj = Resources.Load(asset.GetPath());
//			callback(asset.Name,obj,callbackData);
//		}
//	}
//
//	private void LoadCachedGameObject(Asset asset, bool usePrefabPosition, AssetLoader.GameObjectCallback callback, AssetLoader.ProgressCallback progressCallback, object callbackData)
//	{
//		if (asset.Name == null)
//		{
//			if (callback != null)
//			{
//				callback(null, null, callbackData);
//			}
//			return;
//		}
//		WWW bundle = GetBundle(asset);
//		if (bundle != null) // AssetBundle is loaded
//		{
//			if (asset.PreloadOnly)
//			{
//				return;
//			}
//			base.StartCoroutine(this.LoadResInBundle(asset, bundle, usePrefabPosition, callback, callbackData));
//			return;
//		}
//		else
//		{
//			StartCoroutine(CreateCachedGameObject(asset,usePrefabPosition,callback, callbackData));
//			return;
//		}
//	}
//
//	private IEnumerator CreateCachedBundle( Asset asset)
//	{
//		//Load assertBudle
//		WWW bundle = new WWW(asset.GetPath());
//		Log.fansy.Print ("bundle dir:" + asset.BundleDir+"bundle Path:"+asset.GetPath());
//		yield return bundle;
//		CacheBundle(asset,bundle);
//	}
//
//
//
//	private IEnumerator CreateCachedObject(Asset asset, AssetLoader.ObjectCallback callback, object callbackData)
//	{
//		yield return StartCoroutine (CreateCachedBundle (asset));
//		WWW bundle = GetBundle(asset);
//		if (bundle != null)
//		{
//			if (callback != null)
//			{
//				callback(asset.Name, bundle, callbackData);
//			}
//			yield return null;
//		}
//	}
//	
//
//	private IEnumerator CreateCachedGameObject(Asset asset, bool usePrefabPosition, AssetLoader.GameObjectCallback callback, object callbackData)
//	{
//		yield return StartCoroutine(CreateCachedBundle(asset));
//		WWW bundle = GetBundle(asset);
//		if(bundle == null)
//		{
//			Debug.LogError("create Bundle error! the path is :"+asset.GetPath());
//		}
//		yield return StartCoroutine (LoadResInBundle (asset, bundle, usePrefabPosition, callback, callbackData));
//	}



	public static void ClearTextures(IEnumerable<string> names)
	{
		if (names == null)
		{
			return;
		}
		cacheTable[AssetFamily.Texture].ClearItems(names);
	}
	public void ClearTexture(string name)
	{
		cacheTable[AssetFamily.Texture].ClearItem(name);
	}
	public void ClearSound(string name)
	{
		cacheTable[AssetFamily.Sound].ClearItem(name);
	}
	public void ClearGameObject(string name)
	{
		cacheTable[AssetFamily.GameObject].ClearItem(name);
	}

	private void ClearAllCaches()
	{
		using (Dictionary<AssetFamily, AssetCache>.Enumerator enumerator = cacheTable.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<AssetFamily, AssetCache> current = enumerator.Current;
				current.Value.Clear();
			}
		}
	}

	private void ForceClearAllCaches()
	{
		using (Dictionary<AssetFamily, AssetCache>.Enumerator enumerator = cacheTable.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<AssetFamily, AssetCache> current = enumerator.Current;
				current.Value.ForceClear();
			}
		}
		
		foreach(var it in loadedBundle)
		{
			Log.Asset.Print("loadBundleClear - family="+it.Key );
		}
		loadedBundle.Clear ();
	}

	public void PrintAllObjects()
	{
		using (Dictionary<AssetFamily, AssetCache>.Enumerator enumerator = cacheTable.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<AssetFamily, AssetCache> current = enumerator.Current;
				current.Value.PrintInfo(current.Key.ToString());
			}
		}
	}

//----------------------------------------------------------
	Object getInCache(Asset asset)
	{
		if(!cacheTable.ContainsKey(asset.Family))
		{
//			Log.Asset.Print("AssetLoader.getInCache() - cacheTable haven't the Key:"+asset.Family);
			return null;
		}
		else
		{
			AssetCache.CachedAsset cache = cacheTable[asset.Family].GetItem(asset);
			if(cache != null)
			{
				return cache.GetAssetObject();
			}
			else
			{
				return null;
			}
		}
	}

	private bool LoadObject(string assetName, AssetFamily family, AssetLoader.ObjectCallback callback, AssetLoader.ProgressCallback progressCallback, object callbackData, bool persistent = false, bool loadAsync = false)
	{
		if (string.IsNullOrEmpty(assetName))
		{
			Log.Asset.Print("AssetLoader.LoadObject() - name was null or empty");
			return false;
		}
		Asset asset = Asset.Create(assetName, family, persistent, loadAsync, false);
		Object target = getInCache (asset);
		if(target != null) //got in cache
		{
			if(callback != null)
			{
				callback(asset.Name,target,callbackData);
			}
			return true;
		}
		else
		{
			if(asset.loadModel() == Asset.LoadModel.FromBundle)
			{
				StartCoroutine(createBundleAsset(asset,callback,progressCallback,callbackData,persistent,loadAsync));
			}
			else if(asset.loadModel() == Asset.LoadModel.FromResources)
			{
				Object obj = Resources.Load(asset.GetPath());
				if(obj == null)
				{
					Log.Asset.Print("AssetLoader.LoadObject() - load object from Resources was failed ,path is:"+asset.GetPath());
				}
				else
				{
					CacheAsset(asset,obj);
					if(callback != null)
					{
						callback(asset.Name,obj,callbackData);
					}
				}
			}
		}
		return true;
	}

	private void CacheAsset(Asset asset, UnityEngine.Object assetObject)
	{
		AssetCache.CachedAsset cachedAsset = new AssetCache.CachedAsset();
		cachedAsset.SetAsset(asset);
		cachedAsset.SetAssetObject(assetObject);
		cachedAsset.Persistent = asset.Persistent;
		if(!cacheTable.ContainsKey(asset.Family))
		{
			cacheTable.Add(asset.Family,new AssetCache());
		}
		Add(asset, cachedAsset);
	}
	private void Add(Asset asset, AssetCache.CachedAsset item)
	{
		cacheTable[asset.Family].AddItem(asset.Name, item);
	}

	IEnumerator createBundleAsset(Asset asset,AssetLoader.ObjectCallback callback, AssetLoader.ProgressCallback progressCallback, object callbackData, bool persistent = false, bool loadAsync = false)
	{
		yield return StartCoroutine (makeBundleCached (asset));
		WWW bundle = GetBundle (asset);
		if(bundle == null)
		{
			Log.Asset.Print("AssetLoader.createBundleAsset() - bundle is not in cache");
			yield return null;
		}
		else
		{
			Object target = bundle.assetBundle.Load (asset.Name);
			if(target == null)
			{
				Log.Asset.Print("AssetLoader.createBundleAsset() - load res from bundle error,asset name is:"+asset.Name);
				yield return null;
			}
			else
			{
				CacheAsset(asset,target);
				if(callback != null)
				{
					callback(asset.Name,target,callbackData);
				}
			}
		}
	}

	IEnumerator makeBundleCached(Asset asset)
	{
		WWW bundle = GetBundle (asset);
		if(bundle == null)
		{
			bundle = new WWW(asset.GetPath());
			yield return bundle;
			if(!System.String.IsNullOrEmpty(bundle.error))
			{
				Log.Asset.Print("AssetLoader.makeBundleCached() - www new error: "+bundle.error);
			}
			else
			{
				CacheBundle(asset,bundle);
			}
		}
		yield return null;
	}

	private WWW GetBundle(Asset asset)
	{
		if(loadedBundle.ContainsKey(asset.Family))
		{
			return loadedBundle[asset.Family];
		}
		else
		{
			return null;
		}
	}
	private void CacheBundle(Asset asset, WWW bundle)
	{
		loadedBundle.Add (asset.Family, bundle);
	}



//---------------OutSide Interface--------------------------

	//------------------------------------------------
	public bool LoadConfig(string fileName)
	{
		return this.LoadConfig(fileName, null);
	}
	public bool LoadConfig(string fileName, AssetLoader.ObjectCallback callback)
	{
		return this.LoadConfig(fileName, callback, null);
	}
	public bool LoadConfig(string fileName, AssetLoader.ObjectCallback callback, object callbackData)
	{
		return this.LoadConfig(fileName, callback, null, callbackData, false);
	}
	public bool LoadConfig(string fileName, AssetLoader.ObjectCallback callback, AssetLoader.ProgressCallback progressCallback)
	{
		return this.LoadConfig(fileName, callback, progressCallback, null, false);
	}
	public bool LoadConfig(string fileName, AssetLoader.ObjectCallback callback, AssetLoader.ProgressCallback progressCallback, object callbackData, bool persistent)
	{
		return this.LoadObject(fileName, AssetFamily.Config, callback, progressCallback, callbackData, persistent, false);
	}	
	//------------------------------------------------
	public bool LoadSound(string soundName)
	{
		return this.LoadSound(soundName, null);
	}
	public bool LoadSound(string soundName, AssetLoader.ObjectCallback callback)
	{
		return this.LoadSound(soundName, callback, null);
	}
	public bool LoadSound(string soundName, AssetLoader.ObjectCallback callback, object callbackData)
	{
		return this.LoadSound(soundName, callback, null, callbackData, false);
	}
	public bool LoadSound(string soundName, AssetLoader.ObjectCallback callback, AssetLoader.ProgressCallback progressCallback)
	{
		return this.LoadSound(soundName, callback, progressCallback, null, false);
	}
	public bool LoadSound(string soundName, AssetLoader.ObjectCallback callback, AssetLoader.ProgressCallback progressCallback, object callbackData, bool persistent)
	{
		return this.LoadObject(soundName, AssetFamily.Config, callback, progressCallback, callbackData, persistent, false);
	}
	//------------------------------------------------
	public bool LoadTexture(string textureName)
	{
		return this.LoadTexture(textureName, null);
	}
	public bool LoadTexture(string textureName, AssetLoader.ObjectCallback callback)
	{
		return this.LoadTexture(textureName, callback, null);
	}
	public bool LoadTexture(string textureName, AssetLoader.ObjectCallback callback, object callbackData)
	{
		return this.LoadTexture(textureName, callback, null, callbackData);
	}
	public bool LoadTexture(string textureName, AssetLoader.ObjectCallback callback, AssetLoader.ProgressCallback progressCallback)
	{
		return this.LoadTexture(textureName, callback, progressCallback, null);
	}
	public bool LoadTexture(string textureName, AssetLoader.ObjectCallback callback, AssetLoader.ProgressCallback progressCallback, object callbackData)
	{
		return this.LoadObject(textureName, AssetFamily.Texture, callback, progressCallback, callbackData, false, false);
	}
	//------------------------------------------------
	public bool LoadUIScreen(string screenName)
	{
		return this.LoadUIScreen(screenName, null);
	}
	public bool LoadUIScreen(string screenName, AssetLoader.ObjectCallback callback)
	{
		return this.LoadUIScreen(screenName, callback, null);
	}
	public bool LoadUIScreen(string screenName, AssetLoader.ObjectCallback callback, object callbackData)
	{
		return this.LoadUIScreen(screenName, callback, null, callbackData);
	}
	public bool LoadUIScreen(string screenName, AssetLoader.ObjectCallback callback, AssetLoader.ProgressCallback progressCallback)
	{
		return this.LoadUIScreen(screenName, callback, progressCallback, null);
	}
	public bool LoadUIScreen(string screenName, AssetLoader.ObjectCallback callback, AssetLoader.ProgressCallback progressCallback, object callbackData)
	{
		return this.LoadObject(screenName, AssetFamily.Texture, callback, progressCallback, callbackData, false, false);
	}
	//------------------------------------------------
	public bool LoadGameObject(string name)
	{
		return this.LoadGameObject(name, null);
	}
	public bool LoadGameObject(string name, AssetLoader.ObjectCallback callback)
	{
		return this.LoadGameObject(name, callback, null);
	}
	public bool LoadGameObject(string name, AssetLoader.ObjectCallback callback, object callbackData)
	{
		return this.LoadGameObject(name, callback, null, callbackData);
	}
	public bool LoadGameObject(string name, AssetLoader.ObjectCallback callback, AssetLoader.ProgressCallback progressCallback)
	{
		return this.LoadGameObject(name, callback, progressCallback, null);
	}
	public bool LoadGameObject(string name, AssetLoader.ObjectCallback callback, AssetLoader.ProgressCallback progressCallback, object callbackData)
	{
		return this.LoadGameObject(name, callback, progressCallback, callbackData, false, false);
	}
	public bool LoadGameObject(string name, AssetLoader.ObjectCallback callback, AssetLoader.ProgressCallback progressCallback, object callbackData, bool persistent)
	{
		return this.LoadGameObject(name, callback, progressCallback, callbackData, persistent, false);
	}
	public bool LoadGameObject(string name, AssetLoader.ObjectCallback callback, AssetLoader.ProgressCallback progressCallback, object callbackData, bool persistent, bool loadAsync)
	{
		return this.LoadObject(name, AssetFamily.GameObject, callback, progressCallback, callbackData, persistent, loadAsync);
	}
	public void PreloadGameObject(string name, bool persistent, bool loadAsync)
	{
		this.LoadObject(name, AssetFamily.GameObject, null, null, null, persistent, loadAsync);
	}
}
