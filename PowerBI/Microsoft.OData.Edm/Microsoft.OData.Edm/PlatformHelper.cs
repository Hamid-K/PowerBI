using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000D3 RID: 211
	internal static class PlatformHelper
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x0000D65B File Offset: 0x0000B85B
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Assembly GetAssembly(this Type type)
		{
			return type.GetTypeInfo().Assembly;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000D668 File Offset: 0x0000B868
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsValueType(this Type type)
		{
			return type.GetTypeInfo().IsValueType;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000D675 File Offset: 0x0000B875
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsAbstract(this Type type)
		{
			return type.GetTypeInfo().IsAbstract;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000D682 File Offset: 0x0000B882
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsGenericType(this Type type)
		{
			return type.GetTypeInfo().IsGenericType;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000D68F File Offset: 0x0000B88F
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsGenericTypeDefinition(this Type type)
		{
			return type.GetTypeInfo().IsGenericTypeDefinition;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000D69C File Offset: 0x0000B89C
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsVisible(this Type type)
		{
			return type.GetTypeInfo().IsVisible;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000D6A9 File Offset: 0x0000B8A9
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsInterface(this Type type)
		{
			return type.GetTypeInfo().IsInterface;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000D6B6 File Offset: 0x0000B8B6
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsClass(this Type type)
		{
			return type.GetTypeInfo().IsClass;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000D6C3 File Offset: 0x0000B8C3
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsEnum(this Type type)
		{
			return type.GetTypeInfo().IsEnum;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000D6D0 File Offset: 0x0000B8D0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Type GetBaseType(this Type type)
		{
			return type.GetTypeInfo().BaseType;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000D6DD File Offset: 0x0000B8DD
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool ContainsGenericParameters(this Type type)
		{
			return type.GetTypeInfo().ContainsGenericParameters;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000D6EA File Offset: 0x0000B8EA
		internal static bool TryConvertStringToDate(string text, out Date date)
		{
			date = default(Date);
			return text != null && PlatformHelper.DateValidator.IsMatch(text) && Date.TryParse(text, CultureInfo.InvariantCulture, out date);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000D714 File Offset: 0x0000B914
		internal static Date ConvertStringToDate(string text)
		{
			Date date;
			if (!PlatformHelper.TryConvertStringToDate(text, out date))
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "String '{0}' was not recognized as a valid Edm.Date.", new object[] { text }));
			}
			return date;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000D74B File Offset: 0x0000B94B
		internal static bool TryConvertStringToTimeOfDay(string text, out TimeOfDay timeOfDay)
		{
			timeOfDay = default(TimeOfDay);
			return text != null && PlatformHelper.TimeOfDayValidator.IsMatch(text) && TimeOfDay.TryParse(text, CultureInfo.InvariantCulture, out timeOfDay);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000D774 File Offset: 0x0000B974
		internal static TimeOfDay ConvertStringToTimeOfDay(string text)
		{
			TimeOfDay timeOfDay;
			if (!PlatformHelper.TryConvertStringToTimeOfDay(text, out timeOfDay))
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "String '{0}' was not recognized as a valid Edm.TimeOfDay.", new object[] { text }));
			}
			return timeOfDay;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		internal static DateTimeOffset ConvertStringToDateTimeOffset(string text)
		{
			text = PlatformHelper.AddSecondsPaddingIfMissing(text);
			DateTimeOffset dateTimeOffset = XmlConvert.ToDateTimeOffset(text);
			PlatformHelper.ValidateTimeZoneInformationInDateTimeOffsetString(text);
			return dateTimeOffset;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000D7D0 File Offset: 0x0000B9D0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		private static void ValidateTimeZoneInformationInDateTimeOffsetString(string text)
		{
			if (text.Length > 1 && (text[text.Length - 1] == 'Z' || text[text.Length - 1] == 'z'))
			{
				return;
			}
			if (text.Length > 6 && (text[text.Length - 6] == '-' || text[text.Length - 6] == '+'))
			{
				return;
			}
			throw new FormatException(Strings.PlatformHelper_DateTimeOffsetMustContainTimeZone(text));
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000D844 File Offset: 0x0000BA44
		internal static string AddSecondsPaddingIfMissing(string text)
		{
			int num = text.IndexOf("T", StringComparison.Ordinal);
			int num2 = num + 6;
			if (num > 0 && (text.Length == num2 || (text.Length > num2 && text[num2] != ':')))
			{
				text = text.Insert(num2, ":00");
			}
			return text;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0000D892 File Offset: 0x0000BA92
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Type GetTypeOrThrow(string typeName)
		{
			return Type.GetType(typeName, true);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0000D89B File Offset: 0x0000BA9B
		internal static UnicodeCategory GetUnicodeCategory(char c)
		{
			return CharUnicodeInfo.GetUnicodeCategory(c);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000D8A3 File Offset: 0x0000BAA3
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsProperty(MemberInfo member)
		{
			return member is PropertyInfo;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0000D8AE File Offset: 0x0000BAAE
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsPrimitive(this Type type)
		{
			return type.GetTypeInfo().IsPrimitive;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000D8BB File Offset: 0x0000BABB
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsSealed(this Type type)
		{
			return type.GetTypeInfo().IsSealed;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsMethod(MemberInfo member)
		{
			return member is MethodInfo;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000022DB File Offset: 0x000004DB
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool AreMembersEqual(MemberInfo member1, MemberInfo member2)
		{
			return member1 == member2;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000D8D3 File Offset: 0x0000BAD3
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly)
		{
			return type.GetPublicProperties(instanceOnly, false);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000D8E0 File Offset: 0x0000BAE0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly, bool declaredOnly)
		{
			IEnumerable<PropertyInfo> enumerable = (declaredOnly ? type.GetTypeInfo().DeclaredProperties : type.GetRuntimeProperties());
			return enumerable.Where((PropertyInfo p) => PlatformHelper.IsPublic(p) && (!instanceOnly || PlatformHelper.IsInstance(p)));
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000D924 File Offset: 0x0000BB24
		internal static IEnumerable<PropertyInfo> GetNonPublicProperties(this Type type, bool instanceOnly, bool declaredOnly)
		{
			IEnumerable<PropertyInfo> enumerable = (declaredOnly ? type.GetTypeInfo().DeclaredProperties : type.GetRuntimeProperties());
			return enumerable.Where((PropertyInfo p) => !PlatformHelper.IsPublic(p) && (!instanceOnly || PlatformHelper.IsInstance(p)));
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000D968 File Offset: 0x0000BB68
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<ConstructorInfo> GetInstanceConstructors(this Type type, bool isPublic)
		{
			return type.GetTypeInfo().DeclaredConstructors.Where((ConstructorInfo c) => !c.IsStatic && isPublic == c.IsPublic);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000D9A0 File Offset: 0x0000BBA0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static ConstructorInfo GetInstanceConstructor(this Type type, bool isPublic, Type[] argTypes)
		{
			return type.GetInstanceConstructors(isPublic).SingleOrDefault((ConstructorInfo c) => PlatformHelper.CheckTypeArgs(c, argTypes));
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
		internal static bool TryGetMethod(this Type type, string name, Type[] parameterTypes, out MethodInfo foundMethod)
		{
			foundMethod = null;
			bool flag;
			try
			{
				foundMethod = type.GetMethod(name, parameterTypes);
				flag = foundMethod != null;
			}
			catch (ArgumentNullException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000DA0C File Offset: 0x0000BC0C
		internal static IEnumerable<MethodInfo> GetMethods(this Type type)
		{
			return type.GetRuntimeMethods();
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000DA14 File Offset: 0x0000BC14
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static MethodInfo GetMethod(this Type type, string name, bool isPublic, bool isStatic)
		{
			return (from m in type.GetRuntimeMethods()
				where m.Name == name && isPublic == m.IsPublic && isStatic == m.IsStatic
				select m).SingleOrDefault<MethodInfo>();
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000DA58 File Offset: 0x0000BC58
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static MethodInfo GetMethod(this Type type, string name, Type[] types, bool isPublic, bool isStatic)
		{
			MethodInfo method = type.GetMethod(name, types);
			if (isPublic == method.IsPublic && isStatic == method.IsStatic)
			{
				return method;
			}
			return null;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000DA84 File Offset: 0x0000BC84
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<MethodInfo> GetPublicStaticMethods(this Type type)
		{
			return from m in type.GetRuntimeMethods()
				where m.IsPublic && m.IsStatic
				select m;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000DAB0 File Offset: 0x0000BCB0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<Type> GetNonPublicNestedTypes(this Type type)
		{
			return from t in type.GetTypeInfo().DeclaredNestedTypes
				where !t.IsNestedPublic
				select t.AsType();
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000DB10 File Offset: 0x0000BD10
		private static bool CheckTypeArgs(ConstructorInfo constructorInfo, Type[] types)
		{
			ParameterInfo[] parameters = constructorInfo.GetParameters();
			if (parameters.Length != types.Length)
			{
				return false;
			}
			for (int i = 0; i < parameters.Length; i++)
			{
				if (parameters[i].ParameterType != types[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000DB4C File Offset: 0x0000BD4C
		internal static bool IsAssignableFrom(this Type thisType, Type otherType)
		{
			return thisType.GetTypeInfo().IsAssignableFrom(otherType.GetTypeInfo());
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000DB60 File Offset: 0x0000BD60
		internal static bool IsSubclassOf(this Type thisType, Type otherType)
		{
			if (thisType == otherType)
			{
				return false;
			}
			for (Type type = thisType.GetTypeInfo().BaseType; type != null; type = type.GetTypeInfo().BaseType)
			{
				if (type == otherType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000DB98 File Offset: 0x0000BD98
		internal static MethodInfo GetMethod(this Type type, string name)
		{
			return (from m in type.GetRuntimeMethods()
				where m.IsPublic && m.Name == name
				select m).SingleOrDefault<MethodInfo>();
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000DBCE File Offset: 0x0000BDCE
		internal static MethodInfo GetMethod(this Type type, string name, Type[] types)
		{
			return type.GetRuntimeMethod(name, types);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
		internal static MethodInfo GetMethodWithGenericArgs(this Type type, string name, bool isPublic, bool isStatic, int genericArgCount)
		{
			return type.GetRuntimeMethods().Single((MethodInfo m) => m.Name == name && m.IsPublic == isPublic && m.IsStatic == isStatic && m.GetGenericArguments().Count<Type>() == genericArgCount);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0000DC20 File Offset: 0x0000BE20
		internal static PropertyInfo GetProperty(this Type type, string name, Type returnType)
		{
			PropertyInfo runtimeProperty = type.GetRuntimeProperty(name);
			if (runtimeProperty != null && runtimeProperty.PropertyType == returnType)
			{
				return runtimeProperty;
			}
			return null;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000DC44 File Offset: 0x0000BE44
		internal static PropertyInfo GetProperty(this Type type, string name)
		{
			return type.GetRuntimeProperty(name);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000DC50 File Offset: 0x0000BE50
		internal static MethodInfo GetGetMethod(this PropertyInfo propertyInfo)
		{
			MethodInfo getMethod = propertyInfo.GetMethod;
			if (getMethod != null && getMethod.IsPublic)
			{
				return getMethod;
			}
			return null;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000DC74 File Offset: 0x0000BE74
		internal static MethodInfo GetSetMethod(this PropertyInfo propertyInfo)
		{
			MethodInfo setMethod = propertyInfo.SetMethod;
			if (setMethod != null && setMethod.IsPublic)
			{
				return setMethod;
			}
			return null;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000DC96 File Offset: 0x0000BE96
		internal static MethodInfo GetBaseDefinition(this MethodInfo methodInfo)
		{
			return methodInfo.GetRuntimeBaseDefinition();
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000DC9E File Offset: 0x0000BE9E
		internal static IEnumerable<PropertyInfo> GetProperties(this Type type)
		{
			return type.GetPublicProperties(false);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000DCA7 File Offset: 0x0000BEA7
		internal static IEnumerable<FieldInfo> GetFields(this Type type)
		{
			return from m in type.GetRuntimeFields()
				where m.IsPublic
				select m;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000DCD3 File Offset: 0x0000BED3
		internal static IEnumerable<object> GetCustomAttributes(this Type type, Type attributeType, bool inherit)
		{
			return type.GetTypeInfo().GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0000DCE2 File Offset: 0x0000BEE2
		internal static IEnumerable<object> GetCustomAttributes(this Type type, bool inherit)
		{
			return type.GetTypeInfo().GetCustomAttributes(inherit);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000DCF0 File Offset: 0x0000BEF0
		internal static Type[] GetGenericArguments(this Type type)
		{
			if (type.GetTypeInfo().IsGenericTypeDefinition)
			{
				return type.GetTypeInfo().GenericTypeParameters;
			}
			return type.GenericTypeArguments;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000DD11 File Offset: 0x0000BF11
		internal static IEnumerable<Type> GetInterfaces(this Type type)
		{
			return type.GetTypeInfo().ImplementedInterfaces;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000DD1E File Offset: 0x0000BF1E
		internal static bool IsInstanceOfType(this Type type, object obj)
		{
			return type.GetTypeInfo().IsAssignableFrom(obj.GetType().GetTypeInfo());
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000DD38 File Offset: 0x0000BF38
		internal static Type GetType(this Assembly assembly, string typeName, bool throwOnError)
		{
			Type type = assembly.GetType(typeName);
			if (type == null && throwOnError)
			{
				throw new TypeLoadException();
			}
			return type;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0000DD5C File Offset: 0x0000BF5C
		internal static IEnumerable<Type> GetTypes(this Assembly assembly)
		{
			return assembly.DefinedTypes.Select((TypeInfo dt) => dt.AsType());
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000DD88 File Offset: 0x0000BF88
		internal static FieldInfo GetField(this Type type, string name)
		{
			return type.GetFields().SingleOrDefault((FieldInfo field) => field.Name == name);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000DDB9 File Offset: 0x0000BFB9
		private static bool IsInstance(PropertyInfo propertyInfo)
		{
			return (propertyInfo.GetMethod != null && !propertyInfo.GetMethod.IsStatic) || (propertyInfo.SetMethod != null && !propertyInfo.SetMethod.IsStatic);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000DDEA File Offset: 0x0000BFEA
		private static bool IsPublic(PropertyInfo propertyInfo)
		{
			return (propertyInfo.GetMethod != null && propertyInfo.GetMethod.IsPublic) || (propertyInfo.SetMethod != null && propertyInfo.SetMethod.IsPublic);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000DE18 File Offset: 0x0000C018
		public static Regex CreateCompiled(string pattern, RegexOptions options)
		{
			options |= RegexOptions.None;
			return new Regex(pattern, options);
		}

		// Token: 0x040001AD RID: 429
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x040001AE RID: 430
		internal static readonly Regex DateValidator = PlatformHelper.CreateCompiled("^(\\d{4})-(0?[1-9]|1[012])-(0?[1-9]|[12]\\d|3[0|1])$", RegexOptions.Singleline);

		// Token: 0x040001AF RID: 431
		internal static readonly Regex TimeOfDayValidator = PlatformHelper.CreateCompiled("^(0?\\d|1\\d|2[0-3]):(0?\\d|[1-5]\\d)(:(0?\\d|[1-5]\\d)(\\.\\d{1,7})?)?$", RegexOptions.Singleline);

		// Token: 0x040001B0 RID: 432
		internal static readonly Regex PotentialDateTimeOffsetValidator = PlatformHelper.CreateCompiled("^(\\d{2,4})-(\\d{1,2})-(\\d{1,2})(T|(\\s+))(\\d{1,2}):(\\d{1,2})", RegexOptions.Singleline);

		// Token: 0x040001B1 RID: 433
		internal static readonly string UriSchemeHttp = "http";

		// Token: 0x040001B2 RID: 434
		internal static readonly string UriSchemeHttps = "https";
	}
}
