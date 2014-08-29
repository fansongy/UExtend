using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
public class EnumUtils
{
	public static string GetString<T>(T enumVal)
	{
		string text = enumVal.ToString();
		FieldInfo field = enumVal.GetType().GetField(text);
		DescriptionAttribute[] array = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
		if (array.Length > 0)
		{
			return array[0].Description;
		}
		return text;
	}
	public static T GetEnum<T>(string str)
	{
		return EnumUtils.GetEnum<T>(str, StringComparison.Ordinal);
	}
	public static T GetEnum<T>(string str, StringComparison comparisonType)
	{
		Type typeFromHandle = typeof(T);
		IEnumerator enumerator = Enum.GetValues(typeFromHandle).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				T t = (T)((object)enumerator.Current);
				string @string = EnumUtils.GetString<T>(t);
				if (@string.Equals(str, comparisonType))
				{
					return t;
				}
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		string text = string.Format("EnumUtils.GetEnum() - {0} has no matching value in enum {1}", str, typeFromHandle);
		throw new ArgumentException(text);
	}
	public static T Parse<T>(string str)
	{
		T result;
		try
		{
			result = (T)((object)Enum.Parse(typeof(T), str));
		}
		catch (Exception)
		{
			result = default(T);
		}
		return result;
	}
}
