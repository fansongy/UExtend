/*
 * Logger class 
 * up level the interface of Log.cs
 * 
 * 
 */

using System;
public class Logger
{
	private string m_name;
	public Logger(string name)
	{
		this.m_name = name;
	}
	public bool IsPrintingEnabled()
	{
		return Log.Get().IsPrintingEnabled(this.m_name);
	}
	public bool IsConsolePrintingEnabled()
	{
		return Log.Get().IsConsolePrintingEnabled(this.m_name);
	}
	public bool IsScreenPrintingEnabled()
	{
		return Log.Get().IsScreenPrintingEnabled(this.m_name);
	}
	public bool IsFilePrintingEnabled()
	{
		return Log.Get().IsFilePrintingEnabled(this.m_name);
	}
	public void LogLevelPrint(string message, LogLevel logLevel)
	{
		Log.Get().Print(this.m_name, message, logLevel);
	}
	public void Print(string message)
	{
		Log.Get().Print(this.m_name, message, LogLevel.Debug);
	}
	public void Print(string format, params object[] args)
	{
		string message = string.Format(format, args);
		Log.Get().Print(this.m_name, message, LogLevel.Debug);
	}
	public void ScreenPrint(string format, params object[] args)
	{
		string message = string.Format(format, args);
		Log.Get().ScreenPrint(this.m_name, message, LogLevel.Debug);
	}
}
