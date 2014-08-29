/*
 * Mutiply Thread Log which should use specially
 * 
 * Usage:
 *     1.define a class member variable
 *        private static LogThreadHealper s_log = new LogThreadHelper("LocalStorage");
 *     2.call send message
 *       s_log.LogDebug("Starting GetFile State={0}", new object[]
 *		 {
 *			gameObject
 *		 });
 *		
 *		 OR
 *		 s_log.LogDebug("Just send msg:"+12345);
 *     3.call Process to run logout
 * 		s_log.Process();
 * 
 * Attaction: the define LOG_SOURCE_ENABLED should be open
 * 
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

public class LogThreadHelper
{
	private class LogEntry
	{
		public string Message;
		public LogLevel Level;
	}

	private LogSource m_logSource;

	private List<LogThreadHelper.LogEntry> m_queuedLogs = new List<LogThreadHelper.LogEntry>();
	public LogThreadHelper(string name)
	{
		this.m_logSource = new LogSource(name);
	}

	[Conditional("LOG_SOURCE_ENABLED")]
	public void Process()
	{
		List<LogThreadHelper.LogEntry> queuedLogs = this.m_queuedLogs;
		Monitor.Enter(queuedLogs);
		try
		{
			foreach(var logItem in m_queuedLogs) {
				switch (logItem.Level) {
				case LogLevel.Info:
					this.m_logSource.LogInfo(logItem.Message);
					continue;
				case LogLevel.Warning:
					this.m_logSource.LogWarning(logItem.Message);
					continue;
				case LogLevel.Error:
					this.m_logSource.LogError(logItem.Message);
					continue;
				}
				this.m_logSource.LogDebug(logItem.Message);
			}

			this.m_queuedLogs.Clear();
		}
		finally
		{
			Monitor.Exit(queuedLogs);
		}
	}
	public void LogDebug(string message)
	{
		this.LogMessage(message, LogLevel.Debug);
	}
	public void LogDebug(string format, params object[] args)
	{
		string message = string.Format(format, args);
		this.LogMessage(message, LogLevel.Debug);
	}
	public void LogInfo(string message)
	{
		this.LogMessage(message, LogLevel.Info);
	}
	public void LogInfo(string format, params object[] args)
	{
		string message = string.Format(format, args);
		this.LogMessage(message, LogLevel.Info);
	}
	public void LogWarning(string message)
	{
		this.LogMessage(message, LogLevel.Warning);
	}
	public void LogWarning(string format, params object[] args)
	{
		string message = string.Format(format, args);
		this.LogMessage(message, LogLevel.Warning);
	}
	public void LogError(string message)
	{
		this.LogMessage(message, LogLevel.Error);
	}
	public void LogError(string format, params object[] args)
	{
		string message = string.Format(format, args);
		this.LogMessage(message, LogLevel.Error);
	}
	private void LogMessage(string message, LogLevel level)
	{
		List<LogThreadHelper.LogEntry> queuedLogs = this.m_queuedLogs;
		Monitor.Enter(queuedLogs);
		try
		{
			this.m_queuedLogs.Add(new LogThreadHelper.LogEntry
			{
				Message = message,
				Level = level
			});
		}
		finally
		{
			Monitor.Exit(queuedLogs);
		}
	}
}
