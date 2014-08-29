/*
 * Source output of the log.
 * useage: #define LOG_SOURCE_ENABLED to open it
 * 
 */ 

//#define LOG_SOURCE_ENABLED
using System;
using System.Diagnostics;
using System.Text;
public class LogSource
{
	private string m_sourceName;
	public LogSource(string sourceName)
	{
		this.m_sourceName = sourceName;
	}
	[Conditional("LOG_SOURCE_ENABLED")]
	private void LogMessage(string message, LogLevel logLevel)
	{
		StackTrace stackTrace = new StackTrace(new StackFrame(2, true));
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("[");
		stringBuilder.Append(this.m_sourceName);
		stringBuilder.Append("] ");
		stringBuilder.Append(message);
		if (stackTrace != null)
		{
			StackFrame frame = stackTrace.GetFrame(0);
			if (frame != null)
			{
				stringBuilder.Append(" (");
				stringBuilder.Append(frame.GetFileName());
				stringBuilder.Append(":");
				stringBuilder.Append(frame.GetFileLineNumber());
				stringBuilder.Append(")");
			}
		}
		Log.Asset.LogLevelPrint(stringBuilder.ToString(), logLevel);
	}
	[Conditional("LOG_SOURCE_ENABLED")]
	public void LogDebugStackTrace(int maxFrames, int skipFrames = 0)
	{
		for (int i = 1 + skipFrames; i < maxFrames; i++)
		{
			StackTrace stackTrace = new StackTrace(new StackFrame(i, true));
			if (stackTrace == null)
			{
				break;
			}
			StackFrame frame = stackTrace.GetFrame(0);
			if (frame == null)
			{
				break;
			}
			if (frame.GetMethod() == null || frame.GetMethod().ToString().StartsWith("<"))
			{
				break;
			}
			this.LogDebug("  > {0}", new object[]
			{
				frame.ToString()
			});
		}
	}
	[Conditional("LOG_SOURCE_ENABLED")]
	public void LogDebug(string message)
	{
		this.LogMessage(message, LogLevel.Debug);
	}
	[Conditional("LOG_SOURCE_ENABLED")]
	public void LogDebug(string format, params object[] args)
	{
		string message = string.Format(format, args);
		this.LogMessage(message, LogLevel.Debug);
	}
	[Conditional("LOG_SOURCE_ENABLED")]
	public void LogInfo(string message)
	{
		this.LogMessage(message, LogLevel.Info);
	}
	[Conditional("LOG_SOURCE_ENABLED")]
	public void LogInfo(string format, params object[] args)
	{
		string message = string.Format(format, args);
		this.LogMessage(message, LogLevel.Info);
	}
	[Conditional("LOG_SOURCE_ENABLED")]
	public void LogWarning(string message)
	{
		this.LogMessage(message, LogLevel.Warning);
	}
	[Conditional("LOG_SOURCE_ENABLED")]
	public void LogWarning(string format, params object[] args)
	{
		string message = string.Format(format, args);
		this.LogMessage(message, LogLevel.Warning);
	}
	[Conditional("LOG_SOURCE_ENABLED")]
	public void LogError(string message)
	{
		this.LogMessage(message, LogLevel.Error);
	}
	[Conditional("LOG_SOURCE_ENABLED")]
	public void LogError(string format, params object[] args)
	{
		string message = string.Format(format, args);
		this.LogMessage(message, LogLevel.Error);
	}
}
