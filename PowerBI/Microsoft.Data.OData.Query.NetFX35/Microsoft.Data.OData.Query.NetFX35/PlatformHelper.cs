using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Xml;

namespace Microsoft.Data.Experimental.OData
{
	// Token: 0x0200005C RID: 92
	internal static class PlatformHelper
	{
		// Token: 0x06000252 RID: 594 RVA: 0x0000CB53 File Offset: 0x0000AD53
		internal static Assembly GetAssembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000CB5B File Offset: 0x0000AD5B
		internal static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000CB63 File Offset: 0x0000AD63
		internal static bool IsGenericParameter(this Type type)
		{
			return type.IsGenericParameter;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000CB6B File Offset: 0x0000AD6B
		internal static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000CB73 File Offset: 0x0000AD73
		internal static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000CB7B File Offset: 0x0000AD7B
		internal static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000CB83 File Offset: 0x0000AD83
		internal static bool IsVisible(this Type type)
		{
			return type.IsVisible;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000CB8B File Offset: 0x0000AD8B
		internal static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000CB93 File Offset: 0x0000AD93
		internal static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000CB9B File Offset: 0x0000AD9B
		internal static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000CBA3 File Offset: 0x0000ADA3
		internal static Type GetBaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000CBAB File Offset: 0x0000ADAB
		internal static bool ContainsGenericParameters(this Type type)
		{
			return type.ContainsGenericParameters;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000CBB3 File Offset: 0x0000ADB3
		internal static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
		{
			return Array.AsReadOnly<T>(array);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000CBBB File Offset: 0x0000ADBB
		internal static DateTime ConvertStringToDateTime(string text)
		{
			return XmlConvert.ToDateTime(text, 3);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
		internal static string ConvertDateTimeToString(DateTime dateTime)
		{
			return XmlConvert.ToString(dateTime, 3);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000CBCD File Offset: 0x0000ADCD
		internal static Type GetTypeOrThrow(string typeName)
		{
			return Type.GetType(typeName, true);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000CBD6 File Offset: 0x0000ADD6
		internal static TypeCode GetTypeCode(Type type)
		{
			return Type.GetTypeCode(type);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000CBDE File Offset: 0x0000ADDE
		internal static bool IsProperty(MemberInfo member)
		{
			return member.MemberType == 16;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000CBEA File Offset: 0x0000ADEA
		internal static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000CBF2 File Offset: 0x0000ADF2
		internal static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000CBFA File Offset: 0x0000ADFA
		internal static bool IsMethod(MemberInfo member)
		{
			return member.MemberType == 8;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000CC05 File Offset: 0x0000AE05
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly)
		{
			return type.GetPublicProperties(instanceOnly, false);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000CC10 File Offset: 0x0000AE10
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly, bool declaredOnly)
		{
			BindingFlags bindingFlags = 20;
			if (!instanceOnly)
			{
				bindingFlags |= 8;
			}
			if (declaredOnly)
			{
				bindingFlags |= 2;
			}
			return type.GetProperties(bindingFlags);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000CC38 File Offset: 0x0000AE38
		internal static IEnumerable<ConstructorInfo> GetInstanceConstructors(this Type type, bool isPublic)
		{
			BindingFlags bindingFlags = 4;
			bindingFlags |= (isPublic ? 16 : 32);
			return type.GetConstructors(bindingFlags);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000CC5C File Offset: 0x0000AE5C
		internal static ConstructorInfo GetInstanceConstructor(this Type type, bool isPublic, Type[] argTypes)
		{
			BindingFlags bindingFlags = 4;
			bindingFlags |= (isPublic ? 16 : 32);
			return type.GetConstructor(bindingFlags, null, argTypes, null);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000CC84 File Offset: 0x0000AE84
		internal static MethodInfo GetMethod(this Type type, string name, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = 0;
			bindingFlags |= (isPublic ? 16 : 32);
			bindingFlags |= (isStatic ? 8 : 4);
			return type.GetMethod(name, bindingFlags);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000CCB4 File Offset: 0x0000AEB4
		internal static MethodInfo GetMethod(this Type type, string name, Type[] types, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = 0;
			bindingFlags |= (isPublic ? 16 : 32);
			bindingFlags |= (isStatic ? 8 : 4);
			return type.GetMethod(name, bindingFlags, null, types, null);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000CCE5 File Offset: 0x0000AEE5
		internal static IEnumerable<MethodInfo> GetPublicStaticMethods(this Type type)
		{
			return type.GetMethods(24);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000CCEF File Offset: 0x0000AEEF
		internal static IEnumerable<Type> GetNonPublicNestedTypes(this Type type)
		{
			return type.GetNestedTypes(32);
		}

		// Token: 0x04000224 RID: 548
		internal static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x04000225 RID: 549
		internal static readonly string UriSchemeHttp = Uri.UriSchemeHttp;

		// Token: 0x04000226 RID: 550
		internal static readonly string UriSchemeHttps = Uri.UriSchemeHttps;
	}
}
