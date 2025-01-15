using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace Microsoft.Data.OData
{
	// Token: 0x020002AD RID: 685
	internal static class PlatformHelper
	{
		// Token: 0x060015FF RID: 5631 RVA: 0x0004F179 File Offset: 0x0004D379
		internal static Assembly GetAssembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x0004F181 File Offset: 0x0004D381
		internal static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x0004F189 File Offset: 0x0004D389
		internal static bool IsGenericParameter(this Type type)
		{
			return type.IsGenericParameter;
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0004F191 File Offset: 0x0004D391
		internal static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0004F199 File Offset: 0x0004D399
		internal static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x0004F1A1 File Offset: 0x0004D3A1
		internal static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x0004F1A9 File Offset: 0x0004D3A9
		internal static bool IsVisible(this Type type)
		{
			return type.IsVisible;
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x0004F1B1 File Offset: 0x0004D3B1
		internal static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x0004F1B9 File Offset: 0x0004D3B9
		internal static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x0004F1C1 File Offset: 0x0004D3C1
		internal static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x0004F1C9 File Offset: 0x0004D3C9
		internal static Type GetBaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x0004F1D1 File Offset: 0x0004D3D1
		internal static bool ContainsGenericParameters(this Type type)
		{
			return type.ContainsGenericParameters;
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x0004F1D9 File Offset: 0x0004D3D9
		internal static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
		{
			return Array.AsReadOnly<T>(array);
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x0004F1E1 File Offset: 0x0004D3E1
		internal static DateTime ConvertStringToDateTime(string text)
		{
			text = PlatformHelper.AddSecondsPaddingIfMissing(text);
			return XmlConvert.ToDateTime(text, 3);
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x0004F1F2 File Offset: 0x0004D3F2
		internal static DateTimeOffset ConvertStringToDateTimeOffset(string text)
		{
			text = PlatformHelper.AddSecondsPaddingIfMissing(text);
			return XmlConvert.ToDateTimeOffset(text);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x0004F204 File Offset: 0x0004D404
		internal static string AddSecondsPaddingIfMissing(string text)
		{
			int num = text.IndexOf("T", 4);
			int num2 = num + 6;
			if (num > 0 && (text.Length <= num2 || text.get_Chars(num2) != ':'))
			{
				text = text.Insert(num2, ":00");
			}
			return text;
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x0004F24C File Offset: 0x0004D44C
		internal static string ConvertDateTimeToStringInternal(DateTime dateTime)
		{
			if (dateTime.Kind == null)
			{
				DateTimeOffset dateTimeOffset;
				dateTimeOffset..ctor(dateTime, TimeSpan.Zero);
				string text = XmlConvert.ToString(dateTimeOffset);
				return text.TrimEnd(new char[] { 'Z' });
			}
			return XmlConvert.ToString(dateTime);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x0004F295 File Offset: 0x0004D495
		internal static string ConvertDateTimeToString(DateTime dateTime)
		{
			return XmlConvert.ToString(dateTime, 3);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x0004F29E File Offset: 0x0004D49E
		internal static Type GetTypeOrThrow(string typeName)
		{
			return Type.GetType(typeName, true);
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x0004F2A7 File Offset: 0x0004D4A7
		internal static TypeCode GetTypeCode(Type type)
		{
			return Type.GetTypeCode(type);
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0004F2AF File Offset: 0x0004D4AF
		internal static UnicodeCategory GetUnicodeCategory(char c)
		{
			return char.GetUnicodeCategory(c);
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x0004F2B7 File Offset: 0x0004D4B7
		internal static bool IsProperty(MemberInfo member)
		{
			return member.MemberType == 16;
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x0004F2C3 File Offset: 0x0004D4C3
		internal static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x0004F2CB File Offset: 0x0004D4CB
		internal static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x0004F2D3 File Offset: 0x0004D4D3
		internal static bool IsMethod(MemberInfo member)
		{
			return member.MemberType == 8;
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0004F2DE File Offset: 0x0004D4DE
		internal static bool AreMembersEqual(MemberInfo member1, MemberInfo member2)
		{
			return member1.MetadataToken == member2.MetadataToken;
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x0004F2EE File Offset: 0x0004D4EE
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly)
		{
			return type.GetPublicProperties(instanceOnly, false);
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x0004F2F8 File Offset: 0x0004D4F8
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

		// Token: 0x0600161B RID: 5659 RVA: 0x0004F320 File Offset: 0x0004D520
		internal static IEnumerable<ConstructorInfo> GetInstanceConstructors(this Type type, bool isPublic)
		{
			BindingFlags bindingFlags = 4;
			bindingFlags |= (isPublic ? 16 : 32);
			return type.GetConstructors(bindingFlags);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0004F344 File Offset: 0x0004D544
		internal static ConstructorInfo GetInstanceConstructor(this Type type, bool isPublic, Type[] argTypes)
		{
			BindingFlags bindingFlags = 4;
			bindingFlags |= (isPublic ? 16 : 32);
			return type.GetConstructor(bindingFlags, null, argTypes, null);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0004F36C File Offset: 0x0004D56C
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

		// Token: 0x0600161E RID: 5662 RVA: 0x0004F3A8 File Offset: 0x0004D5A8
		internal static MethodInfo GetMethod(this Type type, string name, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = 0;
			bindingFlags |= (isPublic ? 16 : 32);
			bindingFlags |= (isStatic ? 8 : 4);
			return type.GetMethod(name, bindingFlags);
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x0004F3D8 File Offset: 0x0004D5D8
		internal static MethodInfo GetMethod(this Type type, string name, Type[] types, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = 0;
			bindingFlags |= (isPublic ? 16 : 32);
			bindingFlags |= (isStatic ? 8 : 4);
			return type.GetMethod(name, bindingFlags, null, types, null);
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0004F409 File Offset: 0x0004D609
		internal static IEnumerable<MethodInfo> GetPublicStaticMethods(this Type type)
		{
			return type.GetMethods(24);
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0004F413 File Offset: 0x0004D613
		internal static IEnumerable<Type> GetNonPublicNestedTypes(this Type type)
		{
			return type.GetNestedTypes(32);
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0004F41D File Offset: 0x0004D61D
		public static Regex CreateCompiled(string pattern, RegexOptions options)
		{
			options = options;
			return new Regex(pattern, options);
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x0004F429 File Offset: 0x0004D629
		public static string[] GetSegments(this Uri uri)
		{
			return uri.Segments;
		}

		// Token: 0x0400099C RID: 2460
		internal static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x0400099D RID: 2461
		internal static readonly string UriSchemeHttp = Uri.UriSchemeHttp;

		// Token: 0x0400099E RID: 2462
		internal static readonly string UriSchemeHttps = Uri.UriSchemeHttps;
	}
}
