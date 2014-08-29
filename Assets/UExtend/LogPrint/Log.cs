/*
 * Log System Main class  
 * 
 * v1.0
 * 
 *Usage:
 *    1.define yourself as a Logger :
 *      public static Logger fansy = new Logger("fansy");
 *    2.define logger in DEFAULT_LOG_INFOS :
 *     new LogInfo("fansy", true,true,true,LogLevel.Debug)
 *    3.call Log:
 * 		Log.fansy.Print("yoyo check now");
 *		Log.fansy.LogLevelPrint("yes",LogLevel.Error);
 *		Log.fansy.ScreenPrint("Screen"+1);
 *
 * If you want to print log on screen ,you should call this:
 *     Log.fansy.ScreenPrint("Screen"+1);
 * It will attach a component to MainCamera. Anyway,you can attach to main camera yourself to set the config
 * 
 * The config directory is define in FileUtils.cs default is "Configs",the config name is log.config
 * The rule is:
 * 		;注册用户 需要保证在Log.cs中存在
 *		[fansy]
 *		ConsolePrinting = "true"
 *		ScreenPrinting = "true"
 *		FilePrinting = "true"
 *		LogLevel = "Debug"
 *
 * 
 */ 

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
public class Log
{
	private const string CONFIG_FILE_NAME = "log.config";
	private const string OUTPUT_DIRECTORY_NAME = "Logs";
	private const string OUTPUT_FILE_EXTENSION = "log";

	public static Logger Asset = new Logger("Asset");
	public static Logger fansy = new Logger("fansy");
	private readonly LogInfo[] DEFAULT_LOG_INFOS = new LogInfo[]
	{
		new LogInfo("Asset", false,false,true,LogLevel.Debug)
//		new LogInfo("fansy", true,true,false,LogLevel.Debug)
	};
	private static Log s_instance;
	private Dictionary<string, LogInfo> m_logInfos = new Dictionary<string, LogInfo>();

	public static Log Get() {
		if (Log.s_instance == null) {
			Log.s_instance = new Log();
			Log.s_instance.Initialize();
		}
		return Log.s_instance;
	}

	public static Log getInstance() {
		if (Log.s_instance == null) {
			Log.s_instance = new Log();
			Log.s_instance.Initialize();
		}
		return Log.s_instance;
	}
	
	public void Load() {
		string text = string.Format("{0}/{1}", FileUtils.ConfigDataPath, CONFIG_FILE_NAME);
		if (File.Exists(text)) {
			this.m_logInfos.Clear();
			FileUtils.ParseConfigFile(text, new FileUtils.ConfigFileEntryParseCallback(this.OnConfigFileEntryParsed));
		}
		LogInfo[] dEFAULT_LOG_INFOS = this.DEFAULT_LOG_INFOS;
		for (int i = 0; i < dEFAULT_LOG_INFOS.Length; i++) {
			LogInfo logInfo = dEFAULT_LOG_INFOS[i];
			if (!this.m_logInfos.ContainsKey(logInfo.GetName())) {
				this.m_logInfos.Add(logInfo.GetName(), logInfo);
			}
		}
	}
	public bool IsPrintingEnabled(string name)
	{
		LogInfo logInfo;
		return this.m_logInfos.TryGetValue(name, out logInfo) && logInfo.IsPrintingEnabled();
	}
	public bool IsConsolePrintingEnabled(string name)
	{
		LogInfo logInfo;
		return this.m_logInfos.TryGetValue(name, out logInfo) && logInfo.IsConsolePrintingEnabled();
	}
	public bool IsScreenPrintingEnabled(string name)
	{
		LogInfo logInfo;
		return this.m_logInfos.TryGetValue(name, out logInfo) && logInfo.IsScreenPrintingEnabled();
	}
	public bool IsFilePrintingEnabled(string name)
	{
		LogInfo logInfo;
		return this.m_logInfos.TryGetValue(name, out logInfo) && logInfo.IsFilePrintingEnabled();
	}
	public void Print(string name, string message, LogLevel logLevel)
	{
		LogInfo logInfo;
		if (!this.m_logInfos.TryGetValue(name, out logInfo)) {
			return;
		}
		if (logInfo.GetLogLevel() > logLevel) {
			return;
		}
		if (logInfo.IsFilePrintingEnabled() && !logInfo.DidFileCreateFail()) {
			StreamWriter streamWriter = logInfo.GetFileWriter();
			if (streamWriter == null) {
				string dir = string.Format("{0}/Assets/{1}", Environment.CurrentDirectory,OUTPUT_DIRECTORY_NAME);
				bool flag = Directory.Exists(dir);
				if (!flag) {
					DirectoryInfo directoryInfo = Directory.CreateDirectory(dir);
					flag = directoryInfo.Exists;
				}
				if (flag) {
					try {
						string text = string.Format("{0}/{1}.{2}", dir, name, OUTPUT_FILE_EXTENSION);
						streamWriter = new StreamWriter(new FileStream(text, FileMode.OpenOrCreate));
						logInfo.SetFileWriter(streamWriter);
					} catch (Exception) {
						logInfo.SetFileCreateFailed();
					}
				} else {
					logInfo.SetFileCreateFailed();
				}
			}
			if (streamWriter != null) {
				StringBuilder stringBuilder = new StringBuilder();
				switch (logLevel) {
				case LogLevel.Debug:
					stringBuilder.Append("D ");
					break;
				case LogLevel.Info:
					stringBuilder.Append("I ");
					break;
				case LogLevel.Warning:
					stringBuilder.Append("W ");
					break;
				case LogLevel.Error:
					stringBuilder.Append("E ");
					break;
				}
				stringBuilder.Append(DateTime.Now.TimeOfDay.ToString());
				stringBuilder.Append(" ");
				stringBuilder.Append(message);
				streamWriter.WriteLine(stringBuilder.ToString());
				streamWriter.Flush();
			}
		}
		if (!logInfo.IsConsolePrintingEnabled()) {
			return;
		}
		string message2 = string.Format("[{0}] {1}", name, message);
		switch (logLevel) {
		case LogLevel.Debug:
		case LogLevel.Info:
			Debug.Log(message2);
			break;
		case LogLevel.Warning:
			Debug.LogWarning(message2);
			break;
		case LogLevel.Error:
			Debug.LogError(message2);
			break;
		};
	}
	public void ScreenPrint(string name, string message, LogLevel logLevel)
	{
		LogInfo logInfo;
		if (!this.m_logInfos.TryGetValue(name, out logInfo)) {
			return;
		}
		if (!logInfo.IsScreenPrintingEnabled())
		{
			return;
		}
		string message2 = string.Format("[{0}] {1}", name, message);
		this.Print(name, message, logLevel);
		SceneDebugger.Get().AddMessage(message2);
	}

	private void Initialize() {
		this.Load();
	}

	private void OnConfigFileEntryParsed(string baseKey, string subKey, string val, object userData) {
		LogInfo logInfo;
		if (!this.m_logInfos.TryGetValue(baseKey, out logInfo)) {
			logInfo = new LogInfo(baseKey);
			this.m_logInfos.Add(logInfo.GetName(), logInfo);
		}
		if (subKey.Equals("ConsolePrinting")) {
			logInfo.SetConsolePrintingEnabled(GeneralUtils.ForceBool(val));
		} else {
			if (subKey.Equals("ScreenPrinting")) {
				logInfo.SetScreenPrintingEnabled(GeneralUtils.ForceBool(val));
			} else {
				if (subKey.Equals("FilePrinting")) {
					logInfo.SetFilePrintingEnabled(GeneralUtils.ForceBool(val));
				} else {
					if (subKey.Equals("LogLevel")) {
						try {
							LogLevel @enum = EnumUtils.GetEnum<LogLevel>(val);
							logInfo.SetLogLevel(@enum);
						} catch (ArgumentException) {
						}
					}
				}
			}
		}
	}
}
