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

public class BroadcastAcotr : OperateController {

	public BroadcastAcotr()
	{
		initData ();
	}

	public override void initData ()
	{

	}

	public override void cleanData ()
	{
		throw new System.NotImplementedException ();
	}

	public override void onClick<T> (T target)
	{
		Messenger.Broadcast<string>(MsgType.TEST_CLICK_MSG_1,"I'm from controller");
		GameModel gm = Singleton.getInstance(ClassName.GAME_MODEL) as GameModel;
		gm.clickNum++;
	}
}
