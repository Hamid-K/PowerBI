using System;
using System.Collections.Generic;
using System.Reflection;
using AngleSharp.Attributes;

namespace AngleSharp.Extensions
{
	// Token: 0x020000EF RID: 239
	internal static class ObjectExtensions
	{
		// Token: 0x0600076C RID: 1900 RVA: 0x000351F0 File Offset: 0x000333F0
		public static Dictionary<string, string> ToDictionary(this object values)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (values != null)
			{
				foreach (PropertyInfo propertyInfo in values.GetType().GetProperties())
				{
					object obj = propertyInfo.GetValue(values, null) ?? string.Empty;
					dictionary.Add(propertyInfo.Name, obj.ToString());
				}
			}
			return dictionary;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0003524C File Offset: 0x0003344C
		public static T? TryGet<T>(this IDictionary<string, object> values, string key) where T : struct
		{
			object obj = null;
			if (values.TryGetValue(key, out obj) && obj is T)
			{
				return new T?((T)((object)obj));
			}
			return null;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00035284 File Offset: 0x00033484
		public static object TryGet(this IDictionary<string, object> values, string key)
		{
			object obj = null;
			values.TryGetValue(key, out obj);
			return obj;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000352A0 File Offset: 0x000334A0
		public static U GetOrDefault<T, U>(this IDictionary<T, U> values, T key, U defaultValue)
		{
			U u = default(U);
			if (!values.TryGetValue(key, out u))
			{
				return defaultValue;
			}
			return u;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000352C3 File Offset: 0x000334C3
		public static double Constraint(this double value, double min, double max)
		{
			if (value < min)
			{
				return min;
			}
			if (value <= max)
			{
				return value;
			}
			return max;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x000352D2 File Offset: 0x000334D2
		public static string GetMessage<T>(this T code) where T : struct
		{
			DomDescriptionAttribute customAttribute = typeof(T).GetTypeInfo().GetDeclaredField(code.ToString()).GetCustomAttribute<DomDescriptionAttribute>();
			return ((customAttribute != null) ? customAttribute.Description : null) ?? "An unknown error occurred.";
		}
	}
}
