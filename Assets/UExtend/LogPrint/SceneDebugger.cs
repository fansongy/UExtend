/*
 * Debug System Logout on screen
 * 
 */ 

using System;
using System.Diagnostics;
using System.Text;
using UnityEngine;
public class SceneDebugger : MonoBehaviour
{
	private const int MAX_MESSAGES = 60;
	public float m_DefaultTimeScale = 1f;
	public float m_MinTimeScale = 0.01f;
	public float m_MaxTimeScale = 4f;
	public Vector2 m_GUIPosition = new Vector2(0.01f, 0.065f);
	public Vector2 m_GUISize = new Vector2(175f, 30f);
	private static SceneDebugger s_instance;
	private QueueList<string> m_messages;
	private StringBuilder m_messageBuilder;
	private GUIStyle m_messageStyle;
	private bool m_hideMessages;
	private void Awake()
	{
		SceneDebugger.s_instance = this;
	}
	private void OnDestroy()
	{
		SceneDebugger.s_instance = null;
	}
	private void OnGUI()
	{
		this.LayoutLeftScreenControls();
	}
	public static SceneDebugger Get()
	{
		if(SceneDebugger.s_instance)
		{
			return SceneDebugger.s_instance;
		}
		else
		{
			SceneDebugger.s_instance = Camera.main.gameObject.AddComponent<SceneDebugger>();
			return SceneDebugger.s_instance;
		}
	}
	public void AddMessage(string message)
	{
		this.InitMessagesIfNecessary();
		if (this.m_messages.Count >= 60)
		{
			this.m_messages.Dequeue();
		}
		this.m_messages.Enqueue(message);
	}
	private void LayoutLeftScreenControls()
	{
		Vector2 gUISize = this.m_GUISize;
		Vector2 vector = new Vector2((float)Screen.width * this.m_GUIPosition.x, (float)Screen.height * this.m_GUIPosition.y);
		Vector2 vector2 = new Vector2(vector.x, vector.y);
		Vector2 vector3 = default(Vector2);
		vector3 = vector2;
		this.LayoutTimeControls(ref vector3, gUISize);
		this.LayoutQualityControls(ref vector3, gUISize);
		this.LayoutMessages(ref vector3, gUISize);
	}
	private void LayoutTimeControls(ref Vector2 offset, Vector2 size)
	{
		GUI.Box(new Rect(offset.x, offset.y, size.x, size.y), string.Format("Time Scale: {0}", Time.timeScale));
		offset.y += 1f * size.y;
		Time.timeScale = GUI.HorizontalSlider(new Rect(offset.x, offset.y, size.x, size.y), Time.timeScale, this.m_MinTimeScale, this.m_MaxTimeScale);
		offset.y += 1f * size.y;
		if (GUI.Button(new Rect(offset.x, offset.y, size.x, size.y), "Reset Time Scale"))
		{
			Time.timeScale = 1f;
		}
		offset.y += 1.5f * size.y;
	}
	private void LayoutQualityControls(ref Vector2 offset, Vector2 size)
	{
		float num = size.x / 3f;
//		if (GUI.Button(new Rect(offset.x, offset.y, num, size.y), "Low"))
//		{
//			GraphicsManager.Get().RenderQualityLevel = GraphicsQuality.Low;
//		}
//		if (GUI.Button(new Rect(offset.x + num, offset.y, num, size.y), "Medium"))
//		{
//			GraphicsManager.Get().RenderQualityLevel = GraphicsQuality.Medium;
//		}
//		if (GUI.Button(new Rect(offset.x + num * 2f, offset.y, num, size.y), "High"))
//		{
//			GraphicsManager.Get().RenderQualityLevel = GraphicsQuality.High;
//		}
		offset.y += 1.5f * size.y;
	}
	[Conditional("UNITY_EDITOR")]
	private void LayoutCursorControls(ref Vector2 offset, Vector2 size)
	{
		if (Screen.showCursor)
		{
			if (GUI.Button(new Rect(offset.x, offset.y, size.x, size.y), "Force Hardware Cursor Off"))
			{
				Screen.showCursor = false;
			}
		}
		else
		{
			if (GUI.Button(new Rect(offset.x, offset.y, size.x, size.y), "Force Hardware Cursor On"))
			{
				Screen.showCursor = true;
			}
		}
		offset.y += 1.5f * size.y;
	}
	private void InitMessagesIfNecessary()
	{
		if (this.m_messages != null)
		{
			return;
		}
		this.m_messages = new QueueList<string>();
	}
	private void InitMessageStyleIfNecessary()
	{
		if (this.m_messageStyle != null)
		{
			return;
		}
		this.m_messageStyle = new GUIStyle("box")
		{
			alignment = TextAnchor.UpperLeft,
			wordWrap = true,
			clipping = TextClipping.Overflow,
			stretchWidth = true
		};
	}
	private void LayoutMessages(ref Vector2 offset, Vector2 size)
	{
		if (this.m_messages == null)
		{
			return;
		}
		if (this.m_messages.Count == 0)
		{
			return;
		}
		this.InitMessageStyleIfNecessary();
		if (this.m_hideMessages)
		{
			if (!GUI.Button(new Rect(offset.x, offset.y, size.x, size.y), "Show Messages"))
			{
				return;
			}
			this.m_hideMessages = false;
		}
		else
		{
			if (GUI.Button(new Rect(offset.x, offset.y, size.x, size.y), "Hide Messages"))
			{
				this.m_hideMessages = true;
				return;
			}
		}
		if (GUI.Button(new Rect(size.x + offset.x, offset.y, size.x, size.y), "Clear Messages"))
		{
			this.m_messages.Clear();
			return;
		}
		offset.y += size.y;
		string messageText = this.GetMessageText();
		float num = (float)Screen.height - offset.y;
		GUI.Box(new Rect(offset.x, offset.y, (float)Screen.width, num), messageText, this.m_messageStyle);
		offset.y += num;
	}
	private string GetMessageText()
	{
		this.m_messageBuilder = new StringBuilder();
		for (int i = 0; i < this.m_messages.Count; i++)
		{
			this.m_messageBuilder.AppendLine(this.m_messages[i]);
		}
		return this.m_messageBuilder.ToString();
	}
}
