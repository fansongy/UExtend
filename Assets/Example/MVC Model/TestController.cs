/*
 * It's the user operate input class.
 * user operate such as clicking ,upArrow ,dragging,should be listen here.
 * After listening,find suit actor(controller) to run the task.
 * 
 * by fansy
 */ 

using UnityEngine;
using System.Collections;

public class TestController :MonoBehaviour {

	OperateController m_curController = null;

	void OnGUI()
	{
		if(GUI.Button(new Rect(100,100,100,50),"boradcast msg"))
		{
			if(m_curController == null || !(m_curController is BroadcastAcotr))
			{
				m_curController = new BroadcastAcotr();
			}
			m_curController.onClick(-1);
		}
		if(GUI.Button(new Rect(100,300,100,50),"notify msg"))
		{
			if(m_curController == null || !(m_curController is NotifyActor))
			{
				m_curController = new NotifyActor();
			}
			m_curController.onClick(-1);
		}
	}
	
}
