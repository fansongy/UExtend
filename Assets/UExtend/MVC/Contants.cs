/*
 * This file put the const setting define
 * 
 * Usage example:
 *     1.public enum OperateType
 *       {
 *            testOperate
 *       }
 *     2.public class ClassName
 *		 {
 *			public const string GAME_MODEL = "GameModel";
 *			public const string GAME_CONTROLLER = "TestController";
 *		 } 
 * by fansy
 */

public enum OperateType
{
	BrocastActor,
	NotifyActor
}

public class ClassName
{
	public const string GAME_MODEL = "GameModel";
	public const string GAME_CONTROLLER = "TestController";
	//	public const string GAME_DEBUG_DATA = "ConfigForDebug";
}