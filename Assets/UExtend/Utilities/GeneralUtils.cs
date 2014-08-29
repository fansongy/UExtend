using System;
using System.Collections.Generic;
using System.Reflection;
using System.Globalization;
using UnityEngine;

public static class GeneralUtils
{
	public const float DEVELOPMENT_BUILD_TEXT_WIDTH = 115f;
	public static void Swap<T>(ref T a, ref T b)
	{
		T t = a;
		a = b;
		b = t;
	}
	public static void ListSwap<T>(IList<T> list, int indexA, int indexB)
	{
		T t = list[indexA];
		list.Insert(indexA, list[indexB]);
		list.Insert(indexB, t);
	}
	public static T[] Slice<T>(this T[] arr, int start, int end)
	{
		int num = arr.Length;
		if (start < 0)
		{
			start = num + start;
		}
		if (end < 0)
		{
			end = num + end;
		}
		int num2 = end - start;
		if (num2 <= 0)
		{
			return new T[0];
		}
		int num3 = num - start;
		if (num2 > num3)
		{
			num2 = num3;
		}
		T[] array = new T[num2];
		Array.Copy(arr, start, array, 0, num2);
		return array;
	}
	public static T[] Slice<T>(this T[] arr, int start)
	{
		return arr.Slice(start, arr.Length);
	}
	public static T[] Slice<T>(this T[] arr)
	{
		return arr.Slice(0, arr.Length);
	}
	public static bool IsOverriddenMethod(MethodInfo childMethod, MethodInfo ancestorMethod)
	{
		if (childMethod == null)
		{
			return false;
		}
		if (ancestorMethod == null)
		{
			return false;
		}
		if (childMethod.Equals(ancestorMethod))
		{
			return false;
		}
		MethodInfo baseDefinition = childMethod.GetBaseDefinition();
		while (!baseDefinition.Equals(childMethod) && !baseDefinition.Equals(ancestorMethod))
		{
			MethodInfo methodInfo = baseDefinition;
			baseDefinition = baseDefinition.GetBaseDefinition();
			if (baseDefinition.Equals(methodInfo))
			{
				return false;
			}
		}
		return baseDefinition.Equals(ancestorMethod);
	}
	public static bool IsEditorPlaying()
	{
		return false;
	}
	public static void ExitApplication()
	{
		Application.Quit();
	}
	public static bool IsDevelopmentBuildTextVisible()
	{
		return Debug.isDebugBuild;
	}
	public static bool TryParseBool(string strVal, out bool boolVal)
	{
		string text = strVal.ToLowerInvariant().Trim();
		if (text == "off" || text == "0" || text == "false")
		{
			boolVal = false;
			return true;
		}
		if (text == "on" || text == "1" || text == "true")
		{
			boolVal = true;
			return true;
		}
		boolVal = false;
		return false;
	}
	public static bool ForceBool(string strVal)
	{
		string text = strVal.ToLowerInvariant().Trim();
		return text == "on" || text == "1" || text == "true";
	}
	public static bool TryParseInt(string str, out int val)
	{
		return int.TryParse(str, NumberStyles.Any, null, out val);
	}
	public static int ForceInt(string str)
	{
		int result = 0;
		GeneralUtils.TryParseInt(str, out result);
		return result;
	}
	public static bool TryParseLong(string str, out long val)
	{
		return long.TryParse(str, NumberStyles.Any, null, out val);
	}
	public static long ForceLong(string str)
	{
		long result = 0L;
		GeneralUtils.TryParseLong(str, out result);
		return result;
	}
	public static bool TryParseFloat(string str, out float val)
	{
		return float.TryParse(str, NumberStyles.Any, null, out val);
	}
	public static float ForceFloat(string str)
	{
		float result = 0f;
		GeneralUtils.TryParseFloat(str, out result);
		return result;
	}
	public static bool RandomBool()
	{
		return UnityEngine.Random.Range(0, 2) == 0;
	}
	public static int UnsignedMod(int x, int y)
	{
		int num = x % y;
		if (num < 0)
		{
			num += y;
		}
		return num;
	}
	public static bool AreArraysEqual<T>(T[] arr1, T[] arr2)
	{
		if (arr1 == arr2)
		{
			return true;
		}
		if (arr1 == null)
		{
			return false;
		}
		if (arr2 == null)
		{
			return false;
		}
		if (arr1.Length != arr2.Length)
		{
			return false;
		}
		for (int i = 0; i < arr1.Length; i++)
		{
			if (!arr1[i].Equals(arr2[i]))
			{
				return false;
			}
		}
		return true;
	}
	public static bool AreBytesEqual(byte[] bytes1, byte[] bytes2)
	{
		return GeneralUtils.AreArraysEqual<byte>(bytes1, bytes2);
	}
}
