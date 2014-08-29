/*
 * Loginfo is the setting which the Logger is instance
 * 
 */ 

using System;
using System.IO;
public class LogInfo
{
	private string m_name;
	private bool m_consolePrinting;
	private bool m_screenPrinting;
	private bool m_filePrinting;
	private StreamWriter m_writer;
	private bool m_fileCreateFailed;
	private LogLevel m_logLevel = LogLevel.Debug;
	public LogInfo(string name)
	{
		this.m_name = name;
	}
	public LogInfo(string name, bool consolePrinting, bool screenPrinting, bool filePrinting, LogLevel logLevel)
	{
		this.m_name = name;
		this.m_consolePrinting = consolePrinting;
		this.m_screenPrinting = screenPrinting;
		this.m_filePrinting = filePrinting;
		this.m_logLevel = logLevel;
	}
	public string GetName()
	{
		return this.m_name;
	}
	public bool IsPrintingEnabled()
	{
		return this.IsConsolePrintingEnabled() || this.IsScreenPrintingEnabled();
	}
	public bool IsConsolePrintingEnabled()
	{
		return this.m_consolePrinting;
	}
	public void SetConsolePrintingEnabled(bool enable)
	{
		this.m_consolePrinting = enable;
	}
	public bool IsScreenPrintingEnabled()
	{
		return this.m_screenPrinting;
	}
	public void SetScreenPrintingEnabled(bool enable)
	{
		this.m_screenPrinting = enable;
	}
	public bool IsFilePrintingEnabled()
	{
		return this.m_filePrinting;
	}
	public void SetFilePrintingEnabled(bool enable)
	{
		this.m_filePrinting = enable;
	}
	public StreamWriter GetFileWriter()
	{
		return this.m_writer;
	}
	public void SetFileWriter(StreamWriter writer)
	{
		this.m_writer = writer;
	}
	public bool DidFileCreateFail()
	{
		return this.m_fileCreateFailed;
	}
	public void SetFileCreateFailed()
	{
		this.m_fileCreateFailed = true;
	}
	public LogLevel GetLogLevel()
	{
		return this.m_logLevel;
	}
	public void SetLogLevel(LogLevel logLevel)
	{
		this.m_logLevel = logLevel;
	}
}
