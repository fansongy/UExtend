/*
 * This class is an abstract class of element effect which is Operated  
 * 
 * Usage :
 * It's a factory class which should be implemented as a product and used in OperateController's product
 * 
 * by fansy
 */

using UnityEngine;
using System.Collections;

public abstract class ConveyEffect : MonoBehaviour {
	public abstract void  onFoucs<T>(T target);
	public abstract void  onBlur<T>(T target);
}
