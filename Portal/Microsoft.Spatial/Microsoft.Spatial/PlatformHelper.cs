using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace Microsoft.Spatial
{
	// Token: 0x02000003 RID: 3
	internal static class PlatformHelper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Assembly GetAssembly(this Type type)
		{
			return type.GetTypeInfo().Assembly;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205D File Offset: 0x0000025D
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsValueType(this Type type)
		{
			return type.GetTypeInfo().IsValueType;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000206A File Offset: 0x0000026A
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsAbstract(this Type type)
		{
			return type.GetTypeInfo().IsAbstract;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002077 File Offset: 0x00000277
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsGenericType(this Type type)
		{
			return type.GetTypeInfo().IsGenericType;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002084 File Offset: 0x00000284
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsGenericTypeDefinition(this Type type)
		{
			return type.GetTypeInfo().IsGenericTypeDefinition;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002091 File Offset: 0x00000291
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsVisible(this Type type)
		{
			return type.GetTypeInfo().IsVisible;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000209E File Offset: 0x0000029E
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsInterface(this Type type)
		{
			return type.GetTypeInfo().IsInterface;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020AB File Offset: 0x000002AB
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsClass(this Type type)
		{
			return type.GetTypeInfo().IsClass;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020B8 File Offset: 0x000002B8
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsEnum(this Type type)
		{
			return type.GetTypeInfo().IsEnum;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020C5 File Offset: 0x000002C5
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Type GetBaseType(this Type type)
		{
			return type.GetTypeInfo().BaseType;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020D2 File Offset: 0x000002D2
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool ContainsGenericParameters(this Type type)
		{
			return type.GetTypeInfo().ContainsGenericParameters;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020E0 File Offset: 0x000002E0
		internal static DateTimeOffset ConvertStringToDateTimeOffset(string text)
		{
			text = PlatformHelper.AddSecondsPaddingIfMissing(text);
			DateTimeOffset dateTimeOffset = XmlConvert.ToDateTimeOffset(text);
			PlatformHelper.ValidateTimeZoneInformationInDateTimeOffsetString(text);
			return dateTimeOffset;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002104 File Offset: 0x00000304
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

		// Token: 0x0600000E RID: 14 RVA: 0x00002178 File Offset: 0x00000378
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

		// Token: 0x0600000F RID: 15 RVA: 0x000021C6 File Offset: 0x000003C6
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Type GetTypeOrThrow(string typeName)
		{
			return Type.GetType(typeName, true);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021CF File Offset: 0x000003CF
		internal static UnicodeCategory GetUnicodeCategory(char c)
		{
			return CharUnicodeInfo.GetUnicodeCategory(c);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021D7 File Offset: 0x000003D7
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsProperty(MemberInfo member)
		{
			return member is PropertyInfo;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021E2 File Offset: 0x000003E2
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsPrimitive(this Type type)
		{
			return type.GetTypeInfo().IsPrimitive;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021EF File Offset: 0x000003EF
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsSealed(this Type type)
		{
			return type.GetTypeInfo().IsSealed;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021FC File Offset: 0x000003FC
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsMethod(MemberInfo member)
		{
			return member is MethodInfo;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002207 File Offset: 0x00000407
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool AreMembersEqual(MemberInfo member1, MemberInfo member2)
		{
			return member1 == member2;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000220D File Offset: 0x0000040D
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly)
		{
			return type.GetPublicProperties(instanceOnly, false);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002218 File Offset: 0x00000418
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly, bool declaredOnly)
		{
			IEnumerable<PropertyInfo> enumerable = (declaredOnly ? type.GetTypeInfo().DeclaredProperties : type.GetRuntimeProperties());
			return enumerable.Where((PropertyInfo p) => PlatformHelper.IsPublic(p) && (!instanceOnly || PlatformHelper.IsInstance(p)));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000225C File Offset: 0x0000045C
		internal static IEnumerable<PropertyInfo> GetNonPublicProperties(this Type type, bool instanceOnly, bool declaredOnly)
		{
			IEnumerable<PropertyInfo> enumerable = (declaredOnly ? type.GetTypeInfo().DeclaredProperties : type.GetRuntimeProperties());
			return enumerable.Where((PropertyInfo p) => !PlatformHelper.IsPublic(p) && (!instanceOnly || PlatformHelper.IsInstance(p)));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022A0 File Offset: 0x000004A0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<ConstructorInfo> GetInstanceConstructors(this Type type, bool isPublic)
		{
			return type.GetTypeInfo().DeclaredConstructors.Where((ConstructorInfo c) => !c.IsStatic && isPublic == c.IsPublic);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022D8 File Offset: 0x000004D8
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static ConstructorInfo GetInstanceConstructor(this Type type, bool isPublic, Type[] argTypes)
		{
			return type.GetInstanceConstructors(isPublic).SingleOrDefault((ConstructorInfo c) => PlatformHelper.CheckTypeArgs(c, argTypes));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000230C File Offset: 0x0000050C
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

		// Token: 0x0600001C RID: 28 RVA: 0x00002344 File Offset: 0x00000544
		internal static IEnumerable<MethodInfo> GetMethods(this Type type)
		{
			return type.GetRuntimeMethods();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000234C File Offset: 0x0000054C
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static MethodInfo GetMethod(this Type type, string name, bool isPublic, bool isStatic)
		{
			return (from m in type.GetRuntimeMethods()
				where m.Name == name && isPublic == m.IsPublic && isStatic == m.IsStatic
				select m).SingleOrDefault<MethodInfo>();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002390 File Offset: 0x00000590
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

		// Token: 0x0600001F RID: 31 RVA: 0x000023BC File Offset: 0x000005BC
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<MethodInfo> GetPublicStaticMethods(this Type type)
		{
			return from m in type.GetRuntimeMethods()
				where m.IsPublic && m.IsStatic
				select m;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023E8 File Offset: 0x000005E8
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<Type> GetNonPublicNestedTypes(this Type type)
		{
			return from t in type.GetTypeInfo().DeclaredNestedTypes
				where !t.IsNestedPublic
				select t.AsType();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002448 File Offset: 0x00000648
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

		// Token: 0x06000022 RID: 34 RVA: 0x00002484 File Offset: 0x00000684
		internal static bool IsAssignableFrom(this Type thisType, Type otherType)
		{
			return thisType.GetTypeInfo().IsAssignableFrom(otherType.GetTypeInfo());
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002498 File Offset: 0x00000698
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

		// Token: 0x06000024 RID: 36 RVA: 0x000024D0 File Offset: 0x000006D0
		internal static MethodInfo GetMethod(this Type type, string name)
		{
			return (from m in type.GetRuntimeMethods()
				where m.IsPublic && m.Name == name
				select m).SingleOrDefault<MethodInfo>();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002506 File Offset: 0x00000706
		internal static MethodInfo GetMethod(this Type type, string name, Type[] types)
		{
			return type.GetRuntimeMethod(name, types);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002510 File Offset: 0x00000710
		internal static MethodInfo GetMethodWithGenericArgs(this Type type, string name, bool isPublic, bool isStatic, int genericArgCount)
		{
			return type.GetRuntimeMethods().Single((MethodInfo m) => m.Name == name && m.IsPublic == isPublic && m.IsStatic == isStatic && m.GetGenericArguments().Count<Type>() == genericArgCount);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002558 File Offset: 0x00000758
		internal static PropertyInfo GetProperty(this Type type, string name, Type returnType)
		{
			PropertyInfo runtimeProperty = type.GetRuntimeProperty(name);
			if (runtimeProperty != null && runtimeProperty.PropertyType == returnType)
			{
				return runtimeProperty;
			}
			return null;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000257C File Offset: 0x0000077C
		internal static PropertyInfo GetProperty(this Type type, string name)
		{
			return type.GetRuntimeProperty(name);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002588 File Offset: 0x00000788
		internal static MethodInfo GetGetMethod(this PropertyInfo propertyInfo)
		{
			MethodInfo getMethod = propertyInfo.GetMethod;
			if (getMethod != null && getMethod.IsPublic)
			{
				return getMethod;
			}
			return null;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025AC File Offset: 0x000007AC
		internal static MethodInfo GetSetMethod(this PropertyInfo propertyInfo)
		{
			MethodInfo setMethod = propertyInfo.SetMethod;
			if (setMethod != null && setMethod.IsPublic)
			{
				return setMethod;
			}
			return null;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025CE File Offset: 0x000007CE
		internal static MethodInfo GetBaseDefinition(this MethodInfo methodInfo)
		{
			return methodInfo.GetRuntimeBaseDefinition();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025D6 File Offset: 0x000007D6
		internal static IEnumerable<PropertyInfo> GetProperties(this Type type)
		{
			return type.GetPublicProperties(false);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000025DF File Offset: 0x000007DF
		internal static IEnumerable<FieldInfo> GetFields(this Type type)
		{
			return from m in type.GetRuntimeFields()
				where m.IsPublic
				select m;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000260B File Offset: 0x0000080B
		internal static IEnumerable<object> GetCustomAttributes(this Type type, Type attributeType, bool inherit)
		{
			return type.GetTypeInfo().GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000261A File Offset: 0x0000081A
		internal static IEnumerable<object> GetCustomAttributes(this Type type, bool inherit)
		{
			return type.GetTypeInfo().GetCustomAttributes(inherit);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002628 File Offset: 0x00000828
		internal static Type[] GetGenericArguments(this Type type)
		{
			if (type.GetTypeInfo().IsGenericTypeDefinition)
			{
				return type.GetTypeInfo().GenericTypeParameters;
			}
			return type.GenericTypeArguments;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002649 File Offset: 0x00000849
		internal static IEnumerable<Type> GetInterfaces(this Type type)
		{
			return type.GetTypeInfo().ImplementedInterfaces;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002656 File Offset: 0x00000856
		internal static bool IsInstanceOfType(this Type type, object obj)
		{
			return type.GetTypeInfo().IsAssignableFrom(obj.GetType().GetTypeInfo());
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002670 File Offset: 0x00000870
		internal static Type GetType(this Assembly assembly, string typeName, bool throwOnError)
		{
			Type type = assembly.GetType(typeName);
			if (type == null && throwOnError)
			{
				throw new TypeLoadException();
			}
			return type;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002694 File Offset: 0x00000894
		internal static IEnumerable<Type> GetTypes(this Assembly assembly)
		{
			return assembly.DefinedTypes.Select((TypeInfo dt) => dt.AsType());
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000026C0 File Offset: 0x000008C0
		internal static FieldInfo GetField(this Type type, string name)
		{
			return type.GetFields().SingleOrDefault((FieldInfo field) => field.Name == name);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000026F1 File Offset: 0x000008F1
		private static bool IsInstance(PropertyInfo propertyInfo)
		{
			return (propertyInfo.GetMethod != null && !propertyInfo.GetMethod.IsStatic) || (propertyInfo.SetMethod != null && !propertyInfo.SetMethod.IsStatic);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002722 File Offset: 0x00000922
		private static bool IsPublic(PropertyInfo propertyInfo)
		{
			return (propertyInfo.GetMethod != null && propertyInfo.GetMethod.IsPublic) || (propertyInfo.SetMethod != null && propertyInfo.SetMethod.IsPublic);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002750 File Offset: 0x00000950
		public static Regex CreateCompiled(string pattern, RegexOptions options)
		{
			options |= RegexOptions.None;
			return new Regex(pattern, options);
		}

		// Token: 0x04000004 RID: 4
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x04000005 RID: 5
		internal static readonly Regex DateValidator = PlatformHelper.CreateCompiled("^(\\d{4})-(0?[1-9]|1[012])-(0?[1-9]|[12]\\d|3[0|1])$", RegexOptions.Singleline);

		// Token: 0x04000006 RID: 6
		internal static readonly Regex TimeOfDayValidator = PlatformHelper.CreateCompiled("^(0?\\d|1\\d|2[0-3]):(0?\\d|[1-5]\\d)(:(0?\\d|[1-5]\\d)(\\.\\d{1,7})?)?$", RegexOptions.Singleline);

		// Token: 0x04000007 RID: 7
		internal static readonly Regex PotentialDateTimeOffsetValidator = PlatformHelper.CreateCompiled("^(\\d{2,4})-(\\d{1,2})-(\\d{1,2})(T|(\\s+))(\\d{1,2}):(\\d{1,2})", RegexOptions.Singleline);

		// Token: 0x04000008 RID: 8
		internal static readonly string UriSchemeHttp = "http";

		// Token: 0x04000009 RID: 9
		internal static readonly string UriSchemeHttps = "https";
	}
}
