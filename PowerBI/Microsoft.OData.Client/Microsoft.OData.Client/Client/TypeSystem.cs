using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.OData.Edm;
using Microsoft.Spatial;

namespace Microsoft.OData.Client
{
	// Token: 0x020000A8 RID: 168
	internal static class TypeSystem
	{
		// Token: 0x0600053E RID: 1342 RVA: 0x000151D0 File Offset: 0x000133D0
		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Cleaner code")]
		static TypeSystem()
		{
			TypeSystem.expressionMethodMap.Add(typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), "contains");
			TypeSystem.expressionMethodMap.Add(typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) }), "endswith");
			TypeSystem.expressionMethodMap.Add(typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }), "startswith");
			TypeSystem.expressionMethodMap.Add(typeof(string).GetMethod("IndexOf", new Type[] { typeof(string) }), "indexof");
			TypeSystem.expressionMethodMap.Add(typeof(string).GetMethod("Replace", new Type[]
			{
				typeof(string),
				typeof(string)
			}), "replace");
			TypeSystem.expressionMethodMap.Add(typeof(string).GetMethod("Substring", new Type[] { typeof(int) }), "substring");
			TypeSystem.expressionMethodMap.Add(typeof(string).GetMethod("Substring", new Type[]
			{
				typeof(int),
				typeof(int)
			}), "substring");
			TypeSystem.expressionMethodMap.Add(typeof(string).GetMethod("ToLower", PlatformHelper.EmptyTypes), "tolower");
			TypeSystem.expressionMethodMap.Add(typeof(string).GetMethod("ToUpper", PlatformHelper.EmptyTypes), "toupper");
			TypeSystem.expressionMethodMap.Add(typeof(string).GetMethod("Trim", PlatformHelper.EmptyTypes), "trim");
			TypeSystem.expressionMethodMap.Add(typeof(string).GetMethod("Concat", new Type[]
			{
				typeof(string),
				typeof(string)
			}), "concat");
			TypeSystem.expressionMethodMap.Add(typeof(string).GetProperty("Length", typeof(int)).GetGetMethod(), "length");
			TypeSystem.expressionMethodMap.Add(typeof(Date).GetProperty("Day", typeof(int)).GetGetMethod(), "day");
			TypeSystem.expressionMethodMap.Add(typeof(Date).GetProperty("Month", typeof(int)).GetGetMethod(), "month");
			TypeSystem.expressionMethodMap.Add(typeof(Date).GetProperty("Year", typeof(int)).GetGetMethod(), "year");
			TypeSystem.expressionMethodMap.Add(typeof(TimeOfDay).GetProperty("Hours", typeof(int)).GetGetMethod(), "hour");
			TypeSystem.expressionMethodMap.Add(typeof(TimeOfDay).GetProperty("Minutes", typeof(int)).GetGetMethod(), "minute");
			TypeSystem.expressionMethodMap.Add(typeof(TimeOfDay).GetProperty("Seconds", typeof(int)).GetGetMethod(), "second");
			TypeSystem.expressionMethodMap.Add(typeof(DateTimeOffset).GetProperty("Date", typeof(DateTime)).GetGetMethod(), "date");
			TypeSystem.expressionMethodMap.Add(typeof(DateTimeOffset).GetProperty("Day", typeof(int)).GetGetMethod(), "day");
			TypeSystem.expressionMethodMap.Add(typeof(DateTimeOffset).GetProperty("Hour", typeof(int)).GetGetMethod(), "hour");
			TypeSystem.expressionMethodMap.Add(typeof(DateTimeOffset).GetProperty("Month", typeof(int)).GetGetMethod(), "month");
			TypeSystem.expressionMethodMap.Add(typeof(DateTimeOffset).GetProperty("Minute", typeof(int)).GetGetMethod(), "minute");
			TypeSystem.expressionMethodMap.Add(typeof(DateTimeOffset).GetProperty("Second", typeof(int)).GetGetMethod(), "second");
			TypeSystem.expressionMethodMap.Add(typeof(DateTimeOffset).GetProperty("Year", typeof(int)).GetGetMethod(), "year");
			TypeSystem.expressionMethodMap.Add(typeof(DateTime).GetProperty("Date", typeof(DateTime)).GetGetMethod(), "date");
			TypeSystem.expressionMethodMap.Add(typeof(DateTime).GetProperty("Day", typeof(int)).GetGetMethod(), "day");
			TypeSystem.expressionMethodMap.Add(typeof(DateTime).GetProperty("Hour", typeof(int)).GetGetMethod(), "hour");
			TypeSystem.expressionMethodMap.Add(typeof(DateTime).GetProperty("Month", typeof(int)).GetGetMethod(), "month");
			TypeSystem.expressionMethodMap.Add(typeof(DateTime).GetProperty("Minute", typeof(int)).GetGetMethod(), "minute");
			TypeSystem.expressionMethodMap.Add(typeof(DateTime).GetProperty("Second", typeof(int)).GetGetMethod(), "second");
			TypeSystem.expressionMethodMap.Add(typeof(DateTime).GetProperty("Year", typeof(int)).GetGetMethod(), "year");
			TypeSystem.expressionMethodMap.Add(typeof(TimeSpan).GetProperty("Hours", typeof(int)).GetGetMethod(), "hour");
			TypeSystem.expressionMethodMap.Add(typeof(TimeSpan).GetProperty("Minutes", typeof(int)).GetGetMethod(), "minute");
			TypeSystem.expressionMethodMap.Add(typeof(TimeSpan).GetProperty("Seconds", typeof(int)).GetGetMethod(), "second");
			TypeSystem.expressionMethodMap.Add(typeof(Math).GetMethod("Round", new Type[] { typeof(double) }), "round");
			TypeSystem.expressionMethodMap.Add(typeof(Math).GetMethod("Round", new Type[] { typeof(decimal) }), "round");
			TypeSystem.expressionMethodMap.Add(typeof(Math).GetMethod("Floor", new Type[] { typeof(double) }), "floor");
			MethodInfo methodInfo = null;
			if (typeof(Math).TryGetMethod("Floor", new Type[] { typeof(decimal) }, out methodInfo))
			{
				TypeSystem.expressionMethodMap.Add(methodInfo, "floor");
			}
			TypeSystem.expressionMethodMap.Add(typeof(Math).GetMethod("Ceiling", new Type[] { typeof(double) }), "ceiling");
			if (typeof(Math).TryGetMethod("Ceiling", new Type[] { typeof(decimal) }, out methodInfo))
			{
				TypeSystem.expressionMethodMap.Add(methodInfo, "ceiling");
			}
			TypeSystem.expressionMethodMap.Add(typeof(GeographyOperationsExtensions).GetMethod("Distance", new Type[]
			{
				typeof(GeographyPoint),
				typeof(GeographyPoint)
			}, true, true), "geo.distance");
			TypeSystem.expressionMethodMap.Add(typeof(GeometryOperationsExtensions).GetMethod("Distance", new Type[]
			{
				typeof(GeometryPoint),
				typeof(GeometryPoint)
			}, true, true), "geo.distance");
			TypeSystem.expressionVBMethodMap = new Dictionary<string, string>(StringComparer.Ordinal);
			TypeSystem.expressionVBMethodMap.Add("Microsoft.VisualBasic.Strings.Trim", "trim");
			TypeSystem.expressionVBMethodMap.Add("Microsoft.VisualBasic.Strings.Len", "length");
			TypeSystem.expressionVBMethodMap.Add("Microsoft.VisualBasic.Strings.Mid", "substring");
			TypeSystem.expressionVBMethodMap.Add("Microsoft.VisualBasic.Strings.UCase", "toupper");
			TypeSystem.expressionVBMethodMap.Add("Microsoft.VisualBasic.Strings.LCase", "tolower");
			TypeSystem.expressionVBMethodMap.Add("Microsoft.VisualBasic.DateAndTime.Year", "year");
			TypeSystem.expressionVBMethodMap.Add("Microsoft.VisualBasic.DateAndTime.Month", "month");
			TypeSystem.expressionVBMethodMap.Add("Microsoft.VisualBasic.DateAndTime.Day", "day");
			TypeSystem.expressionVBMethodMap.Add("Microsoft.VisualBasic.DateAndTime.Hour", "hour");
			TypeSystem.expressionVBMethodMap.Add("Microsoft.VisualBasic.DateAndTime.Minute", "minute");
			TypeSystem.expressionVBMethodMap.Add("Microsoft.VisualBasic.DateAndTime.Second", "second");
			TypeSystem.propertiesAsMethodsMap = new Dictionary<PropertyInfo, MethodInfo>(EqualityComparer<PropertyInfo>.Default);
			TypeSystem.propertiesAsMethodsMap.Add(typeof(string).GetProperty("Length", typeof(int)), typeof(string).GetProperty("Length", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(DateTimeOffset).GetProperty("Day", typeof(int)), typeof(DateTimeOffset).GetProperty("Day", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(DateTimeOffset).GetProperty("Date", typeof(DateTime)), typeof(DateTimeOffset).GetProperty("Date", typeof(DateTime)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(DateTimeOffset).GetProperty("Hour", typeof(int)), typeof(DateTimeOffset).GetProperty("Hour", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(DateTimeOffset).GetProperty("Minute", typeof(int)), typeof(DateTimeOffset).GetProperty("Minute", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(DateTimeOffset).GetProperty("Second", typeof(int)), typeof(DateTimeOffset).GetProperty("Second", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(DateTimeOffset).GetProperty("Month", typeof(int)), typeof(DateTimeOffset).GetProperty("Month", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(DateTimeOffset).GetProperty("Year", typeof(int)), typeof(DateTimeOffset).GetProperty("Year", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(DateTime).GetProperty("Day", typeof(int)), typeof(DateTime).GetProperty("Day", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(DateTime).GetProperty("Date", typeof(DateTime)), typeof(DateTime).GetProperty("Date", typeof(DateTime)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(DateTime).GetProperty("Hour", typeof(int)), typeof(DateTime).GetProperty("Hour", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(DateTime).GetProperty("Minute", typeof(int)), typeof(DateTime).GetProperty("Minute", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(DateTime).GetProperty("Second", typeof(int)), typeof(DateTime).GetProperty("Second", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(DateTime).GetProperty("Month", typeof(int)), typeof(DateTime).GetProperty("Month", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(DateTime).GetProperty("Year", typeof(int)), typeof(DateTime).GetProperty("Year", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(TimeSpan).GetProperty("Hours", typeof(int)), typeof(TimeSpan).GetProperty("Hours", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(TimeSpan).GetProperty("Minutes", typeof(int)), typeof(TimeSpan).GetProperty("Minutes", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(TimeSpan).GetProperty("Seconds", typeof(int)), typeof(TimeSpan).GetProperty("Seconds", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(TimeOfDay).GetProperty("Hours", typeof(int)), typeof(TimeOfDay).GetProperty("Hours", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(TimeOfDay).GetProperty("Minutes", typeof(int)), typeof(TimeOfDay).GetProperty("Minutes", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(TimeOfDay).GetProperty("Seconds", typeof(int)), typeof(TimeOfDay).GetProperty("Seconds", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(Date).GetProperty("Year", typeof(int)), typeof(Date).GetProperty("Year", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(Date).GetProperty("Month", typeof(int)), typeof(Date).GetProperty("Month", typeof(int)).GetGetMethod());
			TypeSystem.propertiesAsMethodsMap.Add(typeof(Date).GetProperty("Day", typeof(int)), typeof(Date).GetProperty("Day", typeof(int)).GetGetMethod());
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000162D8 File Offset: 0x000144D8
		internal static bool TryGetQueryOptionMethod(MethodInfo mi, out string methodName)
		{
			return TypeSystem.expressionMethodMap.TryGetValue(mi, out methodName) || (TypeSystem.IsVisualBasicAssembly(mi.DeclaringType.GetAssembly()) && TypeSystem.expressionVBMethodMap.TryGetValue(mi.DeclaringType.FullName + "." + mi.Name, out methodName));
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001632F File Offset: 0x0001452F
		internal static bool TryGetPropertyAsMethod(PropertyInfo pi, out MethodInfo mi)
		{
			return TypeSystem.propertiesAsMethodsMap.TryGetValue(pi, out mi);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00016340 File Offset: 0x00014540
		internal static Type GetElementType(Type seqType)
		{
			Type type = TypeSystem.FindIEnumerable(seqType);
			if (type == null)
			{
				return seqType;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00016368 File Offset: 0x00014568
		internal static bool IsPrivate(PropertyInfo pi)
		{
			MethodInfo methodInfo = pi.GetGetMethod() ?? pi.GetSetMethod();
			return !(methodInfo != null) || methodInfo.IsPrivate;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00016398 File Offset: 0x00014598
		internal static Type FindIEnumerable(Type seqType)
		{
			if (seqType == null || seqType == typeof(string) || seqType.IsPrimitive() || seqType.IsValueType() || Nullable.GetUnderlyingType(seqType) != null)
			{
				return null;
			}
			Dictionary<Type, Type> dictionary = TypeSystem.ienumerableElementTypeCache;
			Type type;
			lock (dictionary)
			{
				if (!TypeSystem.ienumerableElementTypeCache.TryGetValue(seqType, out type))
				{
					type = TypeSystem.FindIEnumerableForNonPrimitiveType(seqType);
					TypeSystem.ienumerableElementTypeCache.Add(seqType, type);
				}
			}
			return type;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00016430 File Offset: 0x00014630
		private static Type FindIEnumerableForNonPrimitiveType(Type seqType)
		{
			if (seqType.IsArray)
			{
				return typeof(IEnumerable<>).MakeGenericType(new Type[] { seqType.GetElementType() });
			}
			if (seqType.IsGenericType())
			{
				foreach (Type type in seqType.GetGenericArguments())
				{
					Type type2 = typeof(IEnumerable<>).MakeGenericType(new Type[] { type });
					if (type2.IsAssignableFrom(seqType))
					{
						return type2;
					}
				}
			}
			IEnumerable<Type> interfaces = seqType.GetInterfaces();
			if (interfaces != null)
			{
				foreach (Type type3 in interfaces)
				{
					Type type4 = TypeSystem.FindIEnumerable(type3);
					if (type4 != null)
					{
						return type4;
					}
				}
			}
			if (seqType.GetBaseType() != null && seqType.GetBaseType() != typeof(object))
			{
				return TypeSystem.FindIEnumerable(seqType.GetBaseType());
			}
			return null;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00016540 File Offset: 0x00014740
		private static bool IsVisualBasicAssembly(Assembly assembly)
		{
			string fullName = assembly.FullName;
			return fullName.Contains("Microsoft.VisualBasic,") && assembly.FullName.Contains("PublicKeyToken=b03f5f7f11d50a3a");
		}

		// Token: 0x0400023B RID: 571
		private const string OfficialSilverLightPublicKeyToken = "31bf3856ad364e35";

		// Token: 0x0400023C RID: 572
		private const string OfficialDesktopPublicKeyToken = "b03f5f7f11d50a3a";

		// Token: 0x0400023D RID: 573
		private static readonly Dictionary<MethodInfo, string> expressionMethodMap = new Dictionary<MethodInfo, string>(EqualityComparer<MethodInfo>.Default);

		// Token: 0x0400023E RID: 574
		private static readonly Dictionary<string, string> expressionVBMethodMap;

		// Token: 0x0400023F RID: 575
		private static readonly Dictionary<PropertyInfo, MethodInfo> propertiesAsMethodsMap;

		// Token: 0x04000240 RID: 576
		private static readonly Dictionary<Type, Type> ienumerableElementTypeCache = new Dictionary<Type, Type>(EqualityComparer<Type>.Default);

		// Token: 0x04000241 RID: 577
		private const string VisualBasicAssemblyName = "Microsoft.VisualBasic,";

		// Token: 0x04000242 RID: 578
		private const string VisualBasicAssemblyPublicKeyToken = "PublicKeyToken=b03f5f7f11d50a3a";
	}
}
