using System;
using System.IO;
using UnityEngine;
public class FileUtils
{
//	private const string CONFIG_DIRECTORY_NAME = "Resources/Configs";

	public delegate void ConfigFileEntryParseCallback(string baseKey, string subKey, string val, object userData);
	public static string PersistentDataPath
	{
		get
		{
			string PathURL = //Application.streamingAssetsPath;
			#if UNITY_ANDROID
				Application.streamingAssetsPath+"/";
			#elif UNITY_IPHONE
				Application.dataPath + "/Raw/";
			#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
				"file://" + Application.dataPath + "/StreamingAssets/";
			#else
				string.Empty;
			#endif

			return PathURL;
		}
	}
	public static string ConfigDataPath
	{
		get
		{
			return Application.dataPath+"/Configs/";
		}
	}
	//-----------------------------------------------------------------
	public static string GetPathUnderAssets(DirectoryInfo folder)
	{
		return FileUtils.GetPathUnderAssets(folder.FullName);
	}
	public static string GetPathUnderAssets(FileInfo fileInfo)
	{
		return FileUtils.GetPathUnderAssets(fileInfo.FullName);
	}
	public static string GetPathUnderAssets(string path)
	{
		string text = path.Replace("\\", "/");
		int num = text.IndexOf("/Assets", 5);
		return text.Remove(0, num + 8);
	}
	
	public static string GetFullPathOfAssets(string addPath)
	{
		return Application.dataPath+"/"+addPath;
	}

	public static string GetFullBundleTempPath(string underPath)
	{
		return GetFullPathOfAssets("Resources/Temp/"+underPath);
	}

	public static string GetFullResourcesPath(string underPath)
	{
		return GetFullPathOfAssets("Resources/"+underPath);
	}

	public static string GetPathUnderResources(string path)
	{
		string text = path.Replace("\\", "/");
		int num = text.IndexOf("/Resources", 5);
		return text.Remove(0, num + 11);
	}

	//----------------------------------------------------------------
	public static string MakeMetaPathFromSourcePath(string path)
	{
		return string.Format("{0}.meta", path);
	}
	public static string MakeSourceAssetMetaPath(string path)
	{
		string path2 = FileUtils.GetPathUnderAssets(path);
		return FileUtils.MakeMetaPathFromSourcePath(path2);
	}
	//----------------------------------------------------------------
	public static string GameToSourceAssetPath(string path, string ext = "prefab")
	{
		return string.Format("{0}.{1}", path, ext);
	}
	public static string GameToSourceAssetName(string folder, string name, string ext = "prefab")
	{
		return string.Format("{0}/{1}.{2}", folder, name, ext);
	}
	//----------------------------------------------------------------
	public static string SourceToGameAssetPath(string path)
	{
		int num = path.LastIndexOf('.');
		return path.Substring(0, num);
	}
	public static string SourceToGameAssetName(string path)
	{
		int num = path.LastIndexOf('/');
		int num2 = path.LastIndexOf('.');
		return path.Substring(num + 1, num2);
	}
	//----------------------------------------------------------------
	public static string GetAssetPath(string fileName)
	{
		return fileName;
	}

	//----------------------------------------------------------------
	public static void GetLastFolderAndFileFromPath(string path, out string folderName, out string fileName)
	{
		folderName = null;
		fileName = null;
		if (string.IsNullOrEmpty(path))
		{
			return;
		}
		path = path.Replace("\\", "/");
		int length = path.Length;
		int num = path.LastIndexOf("/");
		if (num < 0)
		{
			fileName = path;
			return;
		}
		if (length == 1)
		{
			return;
		}
		if (num == length - 1)
		{
			folderName = path.Remove(length - 1, 1);
			return;
		}
		int num2 = num + 1;
		fileName = path.Substring(num2, length - num2);
		if (num == 0)
		{
			return;
		}
		folderName = path.Substring(0, num);
		int num3 = folderName.LastIndexOf("/");
		if (num3 < 0)
		{
			return;
		}
		int num4 = num3 + 1;
		folderName = folderName.Substring(num4, folderName.Length - num4);
	}
	//----------------------------------------------------------------
	public static bool ParseConfigFile(string filePath, FileUtils.ConfigFileEntryParseCallback callback)
	{
		return FileUtils.ParseConfigFile(filePath, callback, null);
	}
	public static bool ParseConfigFile(string filePath, FileUtils.ConfigFileEntryParseCallback callback, object userData)
	{
		if (callback == null)
		{
			Debug.LogWarning("FileUtils.ParseConfigFile() - no callback given");
			return false;
		}
		if (!File.Exists(filePath))
		{
			Debug.LogWarning(string.Format("FileUtils.ParseConfigFile() - file {0} does not exist", filePath));
			return false;
		}
		int num = 1;
		using (StreamReader streamReader = File.OpenText(filePath))
		{
			string baseKey = string.Empty;
			while (streamReader.Peek() != -1)
			{
				string text = streamReader.ReadLine().Trim();
				if (text.Length >= 1)
				{
					if (text.ToCharArray()[0] != ';')
					{
						if (text.ToCharArray()[0] == '[')
						{
							if (text.ToCharArray()[(text.Length - 1)] != ']')
							{
								Debug.LogWarning(string.Format("FileUtils.ParseConfigFile() - bad key name \"{0}\" on line {1} in file {2}", text, num, filePath));
							}
							else
							{
								baseKey = text.Substring(1, text.Length - 2);
							}
						}
						else
						{
							if (!text.Contains("="))
							{
								Debug.LogWarning(string.Format("FileUtils.ParseConfigFile() - bad value pair \"{0}\" on line {1} in file {2}", text, num, filePath));
							}
							else
							{
								string[] array = text.Split(new char[]
								{
									'='
								});
								string subKey = array[0].Trim();
								string text2 = array[1].Trim();
								if (text2.ToCharArray()[0] == '"' && text2.ToCharArray()[(text2.Length - 1)] == '"')
								{
									text2 = text2.Substring(1, text2.Length - 2);
								}
								callback(baseKey, subKey, text2, userData);
							}
						}
					}
				}
			}
		}
		return true;
	}
}
