using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Xml;

namespace Microsoft.ReportingServices.OData
{
	// Token: 0x0200000A RID: 10
	internal static class PlatformHelper
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002327 File Offset: 0x00000527
		internal static Assembly GetAssembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000232F File Offset: 0x0000052F
		internal static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002337 File Offset: 0x00000537
		internal static bool IsGenericParameter(this Type type)
		{
			return type.IsGenericParameter;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000233F File Offset: 0x0000053F
		internal static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002347 File Offset: 0x00000547
		internal static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000234F File Offset: 0x0000054F
		internal static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002357 File Offset: 0x00000557
		internal static bool IsVisible(this Type type)
		{
			return type.IsVisible;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000235F File Offset: 0x0000055F
		internal static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002367 File Offset: 0x00000567
		internal static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000236F File Offset: 0x0000056F
		internal static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002377 File Offset: 0x00000577
		internal static Type GetBaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000237F File Offset: 0x0000057F
		internal static bool ContainsGenericParameters(this Type type)
		{
			return type.ContainsGenericParameters;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002387 File Offset: 0x00000587
		internal static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
		{
			return Array.AsReadOnly<T>(array);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000238F File Offset: 0x0000058F
		internal static DateTime ConvertStringToDateTime(string text)
		{
			return XmlConvert.ToDateTime(text, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002398 File Offset: 0x00000598
		internal static string ConvertDateTimeToString(DateTime dateTime)
		{
			return XmlConvert.ToString(dateTime, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000023A1 File Offset: 0x000005A1
		internal static Type GetTypeOrThrow(string typeName)
		{
			return Type.GetType(typeName, true);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000023AA File Offset: 0x000005AA
		internal static TypeCode GetTypeCode(Type type)
		{
			return Type.GetTypeCode(type);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000023B2 File Offset: 0x000005B2
		internal static bool IsProperty(MemberInfo member)
		{
			return member.MemberType == MemberTypes.Property;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000023BE File Offset: 0x000005BE
		internal static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000023C6 File Offset: 0x000005C6
		internal static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000023CE File Offset: 0x000005CE
		internal static bool IsMethod(MemberInfo member)
		{
			return member.MemberType == MemberTypes.Method;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000023D9 File Offset: 0x000005D9
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly)
		{
			return type.GetPublicProperties(instanceOnly, false);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000023E4 File Offset: 0x000005E4
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly, bool declaredOnly)
		{
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
			if (!instanceOnly)
			{
				bindingFlags |= BindingFlags.Static;
			}
			if (declaredOnly)
			{
				bindingFlags |= BindingFlags.DeclaredOnly;
			}
			return type.GetProperties(bindingFlags);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000240C File Offset: 0x0000060C
		internal static IEnumerable<ConstructorInfo> GetInstanceConstructors(this Type type, bool isPublic)
		{
			BindingFlags bindingFlags = BindingFlags.Instance;
			bindingFlags |= (isPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			return type.GetConstructors(bindingFlags);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002430 File Offset: 0x00000630
		internal static ConstructorInfo GetInstanceConstructor(this Type type, bool isPublic, Type[] argTypes)
		{
			BindingFlags bindingFlags = BindingFlags.Instance;
			bindingFlags |= (isPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			return type.GetConstructor(bindingFlags, null, argTypes, null);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002458 File Offset: 0x00000658
		internal static MethodInfo GetMethod(this Type type, string name, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = BindingFlags.Default;
			bindingFlags |= (isPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			bindingFlags |= (isStatic ? BindingFlags.Static : BindingFlags.Instance);
			return type.GetMethod(name, bindingFlags);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002488 File Offset: 0x00000688
		internal static MethodInfo GetMethod(this Type type, string name, Type[] types, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = BindingFlags.Default;
			bindingFlags |= (isPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			bindingFlags |= (isStatic ? BindingFlags.Static : BindingFlags.Instance);
			return type.GetMethod(name, bindingFlags, null, types, null);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000024B9 File Offset: 0x000006B9
		internal static IEnumerable<MethodInfo> GetPublicStaticMethods(this Type type)
		{
			return type.GetMethods(BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000024C3 File Offset: 0x000006C3
		internal static IEnumerable<Type> GetNonPublicNestedTypes(this Type type)
		{
			return type.GetNestedTypes(BindingFlags.NonPublic);
		}

		// Token: 0x0400001C RID: 28
		internal static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x0400001D RID: 29
		internal static readonly string UriSchemeHttp = Uri.UriSchemeHttp;

		// Token: 0x0400001E RID: 30
		internal static readonly string UriSchemeHttps = Uri.UriSchemeHttps;
	}
}
