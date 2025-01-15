using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000EE RID: 238
	internal static class PlatformHelper
	{
		// Token: 0x06000AE0 RID: 2784 RVA: 0x0001D359 File Offset: 0x0001B559
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Assembly GetAssembly(this Type type)
		{
			return type.GetTypeInfo().Assembly;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0001D366 File Offset: 0x0001B566
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsValueType(this Type type)
		{
			return type.GetTypeInfo().IsValueType;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0001D373 File Offset: 0x0001B573
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsAbstract(this Type type)
		{
			return type.GetTypeInfo().IsAbstract;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001D380 File Offset: 0x0001B580
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsGenericType(this Type type)
		{
			return type.GetTypeInfo().IsGenericType;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0001D38D File Offset: 0x0001B58D
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsGenericTypeDefinition(this Type type)
		{
			return type.GetTypeInfo().IsGenericTypeDefinition;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0001D39A File Offset: 0x0001B59A
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsVisible(this Type type)
		{
			return type.GetTypeInfo().IsVisible;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0001D3A7 File Offset: 0x0001B5A7
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsInterface(this Type type)
		{
			return type.GetTypeInfo().IsInterface;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0001D3B4 File Offset: 0x0001B5B4
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsClass(this Type type)
		{
			return type.GetTypeInfo().IsClass;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001D3C1 File Offset: 0x0001B5C1
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsEnum(this Type type)
		{
			return type.GetTypeInfo().IsEnum;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0001D3CE File Offset: 0x0001B5CE
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Type GetBaseType(this Type type)
		{
			return type.GetTypeInfo().BaseType;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0001D3DB File Offset: 0x0001B5DB
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool ContainsGenericParameters(this Type type)
		{
			return type.GetTypeInfo().ContainsGenericParameters;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0001D3E8 File Offset: 0x0001B5E8
		internal static bool TryConvertStringToDate(string text, out Date date)
		{
			date = default(Date);
			return text != null && PlatformHelper.DateValidator.IsMatch(text) && Date.TryParse(text, CultureInfo.InvariantCulture, out date);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0001D410 File Offset: 0x0001B610
		internal static Date ConvertStringToDate(string text)
		{
			Date date;
			if (!PlatformHelper.TryConvertStringToDate(text, out date))
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "String '{0}' was not recognized as a valid Edm.Date.", new object[] { text }));
			}
			return date;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0001D447 File Offset: 0x0001B647
		internal static bool TryConvertStringToTimeOfDay(string text, out TimeOfDay timeOfDay)
		{
			timeOfDay = default(TimeOfDay);
			return text != null && PlatformHelper.TimeOfDayValidator.IsMatch(text) && TimeOfDay.TryParse(text, CultureInfo.InvariantCulture, out timeOfDay);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0001D470 File Offset: 0x0001B670
		internal static TimeOfDay ConvertStringToTimeOfDay(string text)
		{
			TimeOfDay timeOfDay;
			if (!PlatformHelper.TryConvertStringToTimeOfDay(text, out timeOfDay))
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "String '{0}' was not recognized as a valid Edm.TimeOfDay.", new object[] { text }));
			}
			return timeOfDay;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0001D4A8 File Offset: 0x0001B6A8
		internal static DateTimeOffset ConvertStringToDateTimeOffset(string text)
		{
			text = PlatformHelper.AddSecondsPaddingIfMissing(text);
			DateTimeOffset dateTimeOffset = XmlConvert.ToDateTimeOffset(text);
			PlatformHelper.ValidateTimeZoneInformationInDateTimeOffsetString(text);
			return dateTimeOffset;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0001D4CC File Offset: 0x0001B6CC
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

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0001D540 File Offset: 0x0001B740
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

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0001D58E File Offset: 0x0001B78E
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Type GetTypeOrThrow(string typeName)
		{
			return Type.GetType(typeName, true);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0001D597 File Offset: 0x0001B797
		internal static UnicodeCategory GetUnicodeCategory(char c)
		{
			return CharUnicodeInfo.GetUnicodeCategory(c);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0001D59F File Offset: 0x0001B79F
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsProperty(MemberInfo member)
		{
			return member is PropertyInfo;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0001D5AA File Offset: 0x0001B7AA
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsPrimitive(this Type type)
		{
			return type.GetTypeInfo().IsPrimitive;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0001D5B7 File Offset: 0x0001B7B7
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsSealed(this Type type)
		{
			return type.GetTypeInfo().IsSealed;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0001D5C4 File Offset: 0x0001B7C4
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsMethod(MemberInfo member)
		{
			return member is MethodInfo;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0001D5CF File Offset: 0x0001B7CF
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool AreMembersEqual(MemberInfo member1, MemberInfo member2)
		{
			return member1 == member2;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0001D5D5 File Offset: 0x0001B7D5
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly)
		{
			return type.GetPublicProperties(instanceOnly, false);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0001D5E0 File Offset: 0x0001B7E0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly, bool declaredOnly)
		{
			IEnumerable<PropertyInfo> enumerable = (declaredOnly ? type.GetTypeInfo().DeclaredProperties : type.GetRuntimeProperties());
			return enumerable.Where((PropertyInfo p) => PlatformHelper.IsPublic(p) && (!instanceOnly || PlatformHelper.IsInstance(p)));
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0001D624 File Offset: 0x0001B824
		internal static IEnumerable<PropertyInfo> GetNonPublicProperties(this Type type, bool instanceOnly, bool declaredOnly)
		{
			IEnumerable<PropertyInfo> enumerable = (declaredOnly ? type.GetTypeInfo().DeclaredProperties : type.GetRuntimeProperties());
			return enumerable.Where((PropertyInfo p) => !PlatformHelper.IsPublic(p) && (!instanceOnly || PlatformHelper.IsInstance(p)));
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0001D668 File Offset: 0x0001B868
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<ConstructorInfo> GetInstanceConstructors(this Type type, bool isPublic)
		{
			return type.GetTypeInfo().DeclaredConstructors.Where((ConstructorInfo c) => !c.IsStatic && isPublic == c.IsPublic);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0001D6A0 File Offset: 0x0001B8A0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static ConstructorInfo GetInstanceConstructor(this Type type, bool isPublic, Type[] argTypes)
		{
			return type.GetInstanceConstructors(isPublic).SingleOrDefault((ConstructorInfo c) => PlatformHelper.CheckTypeArgs(c, argTypes));
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0001D6D4 File Offset: 0x0001B8D4
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

		// Token: 0x06000AFF RID: 2815 RVA: 0x0001D70C File Offset: 0x0001B90C
		internal static IEnumerable<MethodInfo> GetMethods(this Type type)
		{
			return type.GetRuntimeMethods();
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0001D714 File Offset: 0x0001B914
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static MethodInfo GetMethod(this Type type, string name, bool isPublic, bool isStatic)
		{
			return (from m in type.GetRuntimeMethods()
				where m.Name == name && isPublic == m.IsPublic && isStatic == m.IsStatic
				select m).SingleOrDefault<MethodInfo>();
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0001D758 File Offset: 0x0001B958
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

		// Token: 0x06000B02 RID: 2818 RVA: 0x0001D784 File Offset: 0x0001B984
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<MethodInfo> GetPublicStaticMethods(this Type type)
		{
			return from m in type.GetRuntimeMethods()
				where m.IsPublic && m.IsStatic
				select m;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0001D7B0 File Offset: 0x0001B9B0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<Type> GetNonPublicNestedTypes(this Type type)
		{
			return from t in type.GetTypeInfo().DeclaredNestedTypes
				where !t.IsNestedPublic
				select t.AsType();
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0001D810 File Offset: 0x0001BA10
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

		// Token: 0x06000B05 RID: 2821 RVA: 0x0001D84C File Offset: 0x0001BA4C
		internal static bool IsAssignableFrom(this Type thisType, Type otherType)
		{
			return thisType.GetTypeInfo().IsAssignableFrom(otherType.GetTypeInfo());
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0001D860 File Offset: 0x0001BA60
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

		// Token: 0x06000B07 RID: 2823 RVA: 0x0001D898 File Offset: 0x0001BA98
		internal static MethodInfo GetMethod(this Type type, string name)
		{
			return (from m in type.GetRuntimeMethods()
				where m.IsPublic && m.Name == name
				select m).SingleOrDefault<MethodInfo>();
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0001D8CE File Offset: 0x0001BACE
		internal static MethodInfo GetMethod(this Type type, string name, Type[] types)
		{
			return type.GetRuntimeMethod(name, types);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0001D8D8 File Offset: 0x0001BAD8
		internal static MethodInfo GetMethodWithGenericArgs(this Type type, string name, bool isPublic, bool isStatic, int genericArgCount)
		{
			return type.GetRuntimeMethods().Single((MethodInfo m) => m.Name == name && m.IsPublic == isPublic && m.IsStatic == isStatic && m.GetGenericArguments().Count<Type>() == genericArgCount);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0001D920 File Offset: 0x0001BB20
		internal static PropertyInfo GetProperty(this Type type, string name, Type returnType)
		{
			PropertyInfo runtimeProperty = type.GetRuntimeProperty(name);
			if (runtimeProperty != null && runtimeProperty.PropertyType == returnType)
			{
				return runtimeProperty;
			}
			return null;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0001D944 File Offset: 0x0001BB44
		internal static PropertyInfo GetProperty(this Type type, string name)
		{
			return type.GetRuntimeProperty(name);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0001D950 File Offset: 0x0001BB50
		internal static MethodInfo GetGetMethod(this PropertyInfo propertyInfo)
		{
			MethodInfo getMethod = propertyInfo.GetMethod;
			if (getMethod != null && getMethod.IsPublic)
			{
				return getMethod;
			}
			return null;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0001D974 File Offset: 0x0001BB74
		internal static MethodInfo GetSetMethod(this PropertyInfo propertyInfo)
		{
			MethodInfo setMethod = propertyInfo.SetMethod;
			if (setMethod != null && setMethod.IsPublic)
			{
				return setMethod;
			}
			return null;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0001D996 File Offset: 0x0001BB96
		internal static MethodInfo GetBaseDefinition(this MethodInfo methodInfo)
		{
			return methodInfo.GetRuntimeBaseDefinition();
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0001D99E File Offset: 0x0001BB9E
		internal static IEnumerable<PropertyInfo> GetProperties(this Type type)
		{
			return type.GetPublicProperties(false);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0001D9A7 File Offset: 0x0001BBA7
		internal static IEnumerable<FieldInfo> GetFields(this Type type)
		{
			return from m in type.GetRuntimeFields()
				where m.IsPublic
				select m;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0001D9D3 File Offset: 0x0001BBD3
		internal static IEnumerable<object> GetCustomAttributes(this Type type, Type attributeType, bool inherit)
		{
			return type.GetTypeInfo().GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0001D9E2 File Offset: 0x0001BBE2
		internal static IEnumerable<object> GetCustomAttributes(this Type type, bool inherit)
		{
			return type.GetTypeInfo().GetCustomAttributes(inherit);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0001D9F0 File Offset: 0x0001BBF0
		internal static Type[] GetGenericArguments(this Type type)
		{
			if (type.GetTypeInfo().IsGenericTypeDefinition)
			{
				return type.GetTypeInfo().GenericTypeParameters;
			}
			return type.GenericTypeArguments;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0001DA11 File Offset: 0x0001BC11
		internal static IEnumerable<Type> GetInterfaces(this Type type)
		{
			return type.GetTypeInfo().ImplementedInterfaces;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0001DA1E File Offset: 0x0001BC1E
		internal static bool IsInstanceOfType(this Type type, object obj)
		{
			return type.GetTypeInfo().IsAssignableFrom(obj.GetType().GetTypeInfo());
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0001DA38 File Offset: 0x0001BC38
		internal static Type GetType(this Assembly assembly, string typeName, bool throwOnError)
		{
			Type type = assembly.GetType(typeName);
			if (type == null && throwOnError)
			{
				throw new TypeLoadException();
			}
			return type;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0001DA5C File Offset: 0x0001BC5C
		internal static IEnumerable<Type> GetTypes(this Assembly assembly)
		{
			return assembly.DefinedTypes.Select((TypeInfo dt) => dt.AsType());
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0001DA88 File Offset: 0x0001BC88
		internal static FieldInfo GetField(this Type type, string name)
		{
			return type.GetFields().SingleOrDefault((FieldInfo field) => field.Name == name);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0001DAB9 File Offset: 0x0001BCB9
		private static bool IsInstance(PropertyInfo propertyInfo)
		{
			return (propertyInfo.GetMethod != null && !propertyInfo.GetMethod.IsStatic) || (propertyInfo.SetMethod != null && !propertyInfo.SetMethod.IsStatic);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0001DAEA File Offset: 0x0001BCEA
		private static bool IsPublic(PropertyInfo propertyInfo)
		{
			return (propertyInfo.GetMethod != null && propertyInfo.GetMethod.IsPublic) || (propertyInfo.SetMethod != null && propertyInfo.SetMethod.IsPublic);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0001DB18 File Offset: 0x0001BD18
		public static Regex CreateCompiled(string pattern, RegexOptions options)
		{
			options |= RegexOptions.None;
			return new Regex(pattern, options);
		}

		// Token: 0x040003DE RID: 990
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x040003DF RID: 991
		internal static readonly Regex DateValidator = PlatformHelper.CreateCompiled("^(\\d{4})-(0?[1-9]|1[012])-(0?[1-9]|[12]\\d|3[0|1])$", RegexOptions.Singleline);

		// Token: 0x040003E0 RID: 992
		internal static readonly Regex TimeOfDayValidator = PlatformHelper.CreateCompiled("^(0?\\d|1\\d|2[0-3]):(0?\\d|[1-5]\\d)(:(0?\\d|[1-5]\\d)(\\.\\d{1,7})?)?$", RegexOptions.Singleline);

		// Token: 0x040003E1 RID: 993
		internal static readonly Regex PotentialDateTimeOffsetValidator = PlatformHelper.CreateCompiled("^(\\d{2,4})-(\\d{1,2})-(\\d{1,2})(T|(\\s+))(\\d{1,2}):(\\d{1,2})", RegexOptions.Singleline);

		// Token: 0x040003E2 RID: 994
		internal static readonly string UriSchemeHttp = "http";

		// Token: 0x040003E3 RID: 995
		internal static readonly string UriSchemeHttps = "https";
	}
}
