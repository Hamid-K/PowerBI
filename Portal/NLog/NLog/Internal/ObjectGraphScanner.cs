using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NLog.Common;
using NLog.Config;

namespace NLog.Internal
{
	// Token: 0x0200012D RID: 301
	internal static class ObjectGraphScanner
	{
		// Token: 0x06000F1D RID: 3869 RVA: 0x00025900 File Offset: 0x00023B00
		public static List<T> FindReachableObjects<T>(bool aggressiveSearch, params object[] rootObjects) where T : class
		{
			if (InternalLogger.IsTraceEnabled)
			{
				InternalLogger.Trace<Type>("FindReachableObject<{0}>:", typeof(T));
			}
			List<T> list = new List<T>();
			HashSet<object> hashSet = new HashSet<object>();
			foreach (object obj in rootObjects)
			{
				ObjectGraphScanner.ScanProperties<T>(aggressiveSearch, list, obj, 0, hashSet);
			}
			return list.ToList<T>();
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0002595C File Offset: 0x00023B5C
		private static void ScanProperties<T>(bool aggressiveSearch, List<T> result, object o, int level, HashSet<object> visitedObjects) where T : class
		{
			if (o == null)
			{
				return;
			}
			Type type = o.GetType();
			try
			{
				if (type == null || !type.IsDefined(typeof(NLogConfigurationItemAttribute), true))
				{
					return;
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Info(ex, "{0}Type reflection not possible for: {1}. Maybe because of .NET Native.", new object[]
				{
					new string(' ', level),
					o.ToString()
				});
				return;
			}
			if (visitedObjects.Contains(o))
			{
				return;
			}
			visitedObjects.Add(o);
			if (InternalLogger.IsTraceEnabled)
			{
				InternalLogger.Trace<string, string, object>("{0}Scanning {1} '{2}'", new string(' ', level), type.Name, o);
			}
			T t;
			if ((t = o as T) != null)
			{
				result.Add(t);
				if (!aggressiveSearch)
				{
					return;
				}
			}
			foreach (PropertyInfo propertyInfo in PropertyHelper.GetAllReadableProperties(type))
			{
				object configurationPropertyValue = ObjectGraphScanner.GetConfigurationPropertyValue(o, propertyInfo, level);
				if (configurationPropertyValue != null)
				{
					ObjectGraphScanner.ScanPropertyForObject<T>(propertyInfo, configurationPropertyValue, aggressiveSearch, result, level, visitedObjects);
				}
			}
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00025A5C File Offset: 0x00023C5C
		private static void ScanPropertyForObject<T>(PropertyInfo prop, object propValue, bool aggressiveSearch, List<T> result, int level, HashSet<object> visitedObjects) where T : class
		{
			if (InternalLogger.IsTraceEnabled)
			{
				InternalLogger.Trace("{0}Scanning Property {1} '{2}' {3}", new object[]
				{
					new string(' ', level + 1),
					prop.Name,
					propValue.ToString(),
					prop.PropertyType.Namespace
				});
			}
			IList list;
			if ((list = propValue as IList) != null)
			{
				object syncRoot = list.SyncRoot;
				List<object> list2;
				lock (syncRoot)
				{
					list2 = new List<object>(list.Count);
					for (int i = 0; i < list.Count; i++)
					{
						object obj = list[i];
						list2.Add(obj);
					}
				}
				ObjectGraphScanner.ScanPropertiesList<T>(aggressiveSearch, result, list2, level + 1, visitedObjects);
				return;
			}
			IEnumerable enumerable;
			if ((enumerable = propValue as IEnumerable) != null)
			{
				IList<object> list3 = (enumerable as IList<object>) ?? enumerable.Cast<object>().ToList<object>();
				ObjectGraphScanner.ScanPropertiesList<T>(aggressiveSearch, result, list3, level + 1, visitedObjects);
				return;
			}
			ObjectGraphScanner.ScanProperties<T>(aggressiveSearch, result, propValue, level + 1, visitedObjects);
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x00025B6C File Offset: 0x00023D6C
		private static object GetConfigurationPropertyValue(object o, PropertyInfo prop, int level)
		{
			if (prop == null || prop.PropertyType == null || prop.PropertyType.IsPrimitive() || prop.PropertyType.IsEnum() || prop.PropertyType == typeof(string))
			{
				return null;
			}
			try
			{
				if (prop.IsDefined(typeof(NLogConfigurationIgnorePropertyAttribute), true))
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Info(ex, "{0}Type reflection not possible for property {1}. Maybe because of .NET Native.", new object[]
				{
					new string(' ', level + 1),
					prop.Name
				});
				return null;
			}
			return prop.GetValue(o, null);
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00025C24 File Offset: 0x00023E24
		private static void ScanPropertiesList<T>(bool aggressiveSearch, List<T> result, IEnumerable<object> elements, int level, HashSet<object> visitedObjects) where T : class
		{
			foreach (object obj in elements)
			{
				ObjectGraphScanner.ScanProperties<T>(aggressiveSearch, result, obj, level, visitedObjects);
			}
		}
	}
}
