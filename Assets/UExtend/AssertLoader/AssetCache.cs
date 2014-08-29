/*
 * 
 * This class is a package of really asset object.
 * 
 * v0.1 by fansy
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;
public class AssetCache
{
	public class CachedAsset
	{
		private UnityEngine.Object m_assetObject;
		private Asset m_asset;
		public bool Persistent
		{
			get;
			set;
		}
		public UnityEngine.Object GetAssetObject()
		{
			return this.m_assetObject;
		}
		public void SetAssetObject(UnityEngine.Object asset)
		{
			this.m_assetObject = asset;
		}
		public Asset GetAsset()
		{
			return this.m_asset;
		}
		public void SetAsset(Asset asset)
		{
			this.m_asset = asset;
		}
		public void UnloadAssetObject()
		{
			Log.Asset.Print("CachedAsset.UnloadAssetObject() - unloading name={0} family={1} persistent={2}", new object[]
			{
				this.m_asset.Name,
				this.m_asset.Family,
				this.Persistent
			});
			this.m_assetObject = null;
		}
	}
	
	private Dictionary<string, AssetCache.CachedAsset> m_assetMap = new Dictionary<string, AssetCache.CachedAsset>();

	public AssetCache.CachedAsset GetItem(Asset asset)
	{
		if(m_assetMap.ContainsKey(asset.Name))
		{
			return m_assetMap[asset.Name];
		}
		else
		{
			return null;
		}
	}
	public AssetCache.CachedAsset GetItem(string key)
	{
		AssetCache.CachedAsset cachedAsset;
		return (!this.m_assetMap.TryGetValue(key, out cachedAsset)) ? null : cachedAsset;
	}

	public bool HasItem(string key)
	{
		return this.m_assetMap.ContainsKey(key);
	}

	public void AddItem(string name, AssetCache.CachedAsset item)
	{
		this.m_assetMap.Add(name, item);
	}

	//------------------------------------------------------------
	public void PrintInfo(string name)
	{
		string text = string.Concat(new object[]
		{
			"Asset Cache ",
			name,
			" contains ",
			this.m_assetMap.Count,
			" objects\n"
		});
		using (Dictionary<string, AssetCache.CachedAsset>.Enumerator enumerator = this.m_assetMap.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, AssetCache.CachedAsset> current = enumerator.Current;
				text = text + "  " + current.Key + "\n";
			}
		}
		Debug.Log(text);
	}

	public void Clear()
	{
		Dictionary<string, AssetCache.CachedAsset> dictionary = new Dictionary<string, AssetCache.CachedAsset>();
		using (Dictionary<string, AssetCache.CachedAsset>.Enumerator enumerator = this.m_assetMap.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, AssetCache.CachedAsset> current = enumerator.Current;
				string key = current.Key;
				AssetCache.CachedAsset value = current.Value;
				if (value.Persistent)
				{
					dictionary.Add(key, value);
				}
				else
				{
					value.UnloadAssetObject();
				}
			}
		}
		this.m_assetMap = dictionary;
	}
	public void ForceClear()
	{
		using (Dictionary<string, AssetCache.CachedAsset>.Enumerator enumerator = this.m_assetMap.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, AssetCache.CachedAsset> current = enumerator.Current;
				AssetCache.CachedAsset value = current.Value;
				value.UnloadAssetObject();
			}
		}
		this.m_assetMap.Clear();
	}
	public bool ClearItem(string key)
	{
		bool flag = false;
		AssetCache.CachedAsset cachedAsset;
		if (this.m_assetMap.TryGetValue(key, out cachedAsset))
		{
			cachedAsset.UnloadAssetObject();
			this.m_assetMap.Remove(key);
			flag = true;
		}
		if (!flag)
		{
			Log.Asset.Print(string.Format("AssetCache.ClearItem() - there is no loaded asset or request for key {0} in {1}", key, this));
		}
		return flag;
	}
	public void ClearItems(IEnumerable<string> itemsToRemove)
	{
		using (IEnumerator<string> enumerator = itemsToRemove.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.Current;
				this.ClearItem(current);
			}
		}
	}
}
