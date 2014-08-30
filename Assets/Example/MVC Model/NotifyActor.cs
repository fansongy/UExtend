/*
 * This is product of OperateController.
 * It set the type and implements the abstract function.
 * Sometimes we need to add the conveyEffect operation.
 * 
 * by fansy
 * 
 */

using UnityEngine;
using System.Collections;

public class NotifyActor : OperateController {

	public NotifyActor()
	{
		initData ();
	}

	public override void initData ()
	{
		ctrlType = OperateType.NotifyActor;
	}
	
	public override void cleanData ()
	{
		throw new System.NotImplementedException ();
	}
	
	public override void onClick<T> (T target)
	{
		Messenger.Notify(MsgType.TEST_NOTIFY_MSG_1,"yo");
		Debug.Log("notify test pressed");
	}


}
