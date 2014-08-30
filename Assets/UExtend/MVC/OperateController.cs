/*
 * This class is an abstract class of Operating element behavior.
 * It's a factory class,OperateType should be setted in it's product.
 * As the operating is complex,the interface maybe change ,but make sure the product shouldn't add operation.
 * 
 * Usage :
 * It's a factory class which should be implemented as a product and init the type in initData()
 * 
 * by fansy
 */

using UnityEngine;
using System.Collections;

public abstract class OperateController  {
	
	protected OperateType ctrlType {
		get;
		set;
	}

	public bool isType(OperateType type)
	{
		return type == ctrlType;
	}

	public ConveyEffect conveyEffect = null;

	public abstract void  initData();
	public abstract void  cleanData();

	//	public abstract void onUp<T>(T from);
	//	public abstract void onDown<T>(T from);
	//	public abstract void onLeft<T>(T from);
	//	public abstract void onRight<T>(T from);
	
	//	public abstract void  onDrag<T>(T target,Vector3 endPos);

	public abstract void  onClick<T>(T target);
}
