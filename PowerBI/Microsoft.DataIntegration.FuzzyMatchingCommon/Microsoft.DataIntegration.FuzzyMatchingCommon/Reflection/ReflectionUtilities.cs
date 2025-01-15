using System;
using System.Globalization;
using System.Reflection;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Reflection
{
	// Token: 0x02000028 RID: 40
	public static class ReflectionUtilities
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x0000FD3C File Offset: 0x0000DF3C
		public static bool TryInstantiateValue<T>(string value, out T instantiatedValue)
		{
			bool flag;
			try
			{
				instantiatedValue = ReflectionUtilities.InstantiateValue<T>(value);
				flag = true;
			}
			catch (Exception)
			{
				instantiatedValue = default(T);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000FD78 File Offset: 0x0000DF78
		public static T InstantiateValue<T>(string value)
		{
			return (T)((object)ReflectionUtilities.InstantiateValue(typeof(T), value, CultureInfo.GetCultureInfo("en-US")));
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000FD99 File Offset: 0x0000DF99
		public static object InstantiateValue(Type dataType, string value)
		{
			return ReflectionUtilities.InstantiateValue(dataType, value, CultureInfo.GetCultureInfo("en-US"));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		public static object InstantiateValue(Type dataType, string value, IFormatProvider formatProvider)
		{
			if (dataType == typeof(string))
			{
				return value;
			}
			if (dataType == typeof(string[]))
			{
				return value.Split(new char[] { ';' });
			}
			if (dataType.BaseType == typeof(Enum))
			{
				return Enum.Parse(dataType, value);
			}
			if (dataType == typeof(bool))
			{
				if (value.Equals("1"))
				{
					return true;
				}
				if (value.Equals("0"))
				{
					return false;
				}
				return bool.Parse(value.ToLowerInvariant());
			}
			else
			{
				MethodInfo methodInfo;
				if ((methodInfo = dataType.GetMethod("Parse", new Type[]
				{
					typeof(string),
					typeof(IFormatProvider)
				})) != null)
				{
					return methodInfo.Invoke(Activator.CreateInstance(dataType), new object[] { value, formatProvider });
				}
				if ((methodInfo = dataType.GetMethod("Parse", new Type[] { typeof(string) })) != null)
				{
					return methodInfo.Invoke(Activator.CreateInstance(dataType), new object[] { value });
				}
				throw new Exception(string.Format("Unable to convert string '{0}' to data type {1}. No Parse method was found for this data type.", value, dataType));
			}
		}
	}
}
