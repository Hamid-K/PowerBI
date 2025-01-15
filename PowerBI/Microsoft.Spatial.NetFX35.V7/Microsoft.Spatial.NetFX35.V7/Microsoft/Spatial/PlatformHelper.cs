using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace Microsoft.Spatial
{
	// Token: 0x02000003 RID: 3
	internal static class PlatformHelper
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002088 File Offset: 0x00000288
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Assembly GetAssembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002090 File Offset: 0x00000290
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002098 File Offset: 0x00000298
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020A0 File Offset: 0x000002A0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020A8 File Offset: 0x000002A8
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020B0 File Offset: 0x000002B0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsVisible(this Type type)
		{
			return type.IsVisible;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020B8 File Offset: 0x000002B8
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020C0 File Offset: 0x000002C0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000020C8 File Offset: 0x000002C8
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000020D0 File Offset: 0x000002D0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Type GetBaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000020D8 File Offset: 0x000002D8
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool ContainsGenericParameters(this Type type)
		{
			return type.ContainsGenericParameters;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000020E0 File Offset: 0x000002E0
		internal static DateTimeOffset ConvertStringToDateTimeOffset(string text)
		{
			text = PlatformHelper.AddSecondsPaddingIfMissing(text);
			DateTimeOffset dateTimeOffset = XmlConvert.ToDateTimeOffset(text);
			PlatformHelper.ValidateTimeZoneInformationInDateTimeOffsetString(text);
			return dateTimeOffset;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002104 File Offset: 0x00000304
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		private static void ValidateTimeZoneInformationInDateTimeOffsetString(string text)
		{
			if (text.Length > 1 && (text.get_Chars(text.Length - 1) == 'Z' || text.get_Chars(text.Length - 1) == 'z'))
			{
				return;
			}
			if (text.Length > 6 && (text.get_Chars(text.Length - 6) == '-' || text.get_Chars(text.Length - 6) == '+'))
			{
				return;
			}
			throw new FormatException(Strings.PlatformHelper_DateTimeOffsetMustContainTimeZone(text));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002178 File Offset: 0x00000378
		internal static string AddSecondsPaddingIfMissing(string text)
		{
			int num = text.IndexOf("T", 4);
			int num2 = num + 6;
			if (num > 0 && (text.Length == num2 || (text.Length > num2 && text.get_Chars(num2) != ':')))
			{
				text = text.Insert(num2, ":00");
			}
			return text;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021C6 File Offset: 0x000003C6
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Type GetTypeOrThrow(string typeName)
		{
			return Type.GetType(typeName, true);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021CF File Offset: 0x000003CF
		internal static UnicodeCategory GetUnicodeCategory(char c)
		{
			return char.GetUnicodeCategory(c);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021D7 File Offset: 0x000003D7
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsProperty(MemberInfo member)
		{
			return member.MemberType == 16;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021E3 File Offset: 0x000003E3
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000021EB File Offset: 0x000003EB
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000021F3 File Offset: 0x000003F3
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsMethod(MemberInfo member)
		{
			return member.MemberType == 8;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021FE File Offset: 0x000003FE
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool AreMembersEqual(MemberInfo member1, MemberInfo member2)
		{
			return member1.MetadataToken == member2.MetadataToken;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000220E File Offset: 0x0000040E
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly)
		{
			return type.GetPublicProperties(instanceOnly, false);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002218 File Offset: 0x00000418
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
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

		// Token: 0x0600001D RID: 29 RVA: 0x00002240 File Offset: 0x00000440
		internal static IEnumerable<PropertyInfo> GetNonPublicProperties(this Type type, bool instanceOnly, bool declaredOnly)
		{
			BindingFlags bindingFlags = 36;
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

		// Token: 0x0600001E RID: 30 RVA: 0x00002268 File Offset: 0x00000468
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<ConstructorInfo> GetInstanceConstructors(this Type type, bool isPublic)
		{
			BindingFlags bindingFlags = 4;
			bindingFlags |= (isPublic ? 16 : 32);
			return type.GetConstructors(bindingFlags);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000228C File Offset: 0x0000048C
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static ConstructorInfo GetInstanceConstructor(this Type type, bool isPublic, Type[] argTypes)
		{
			BindingFlags bindingFlags = 4;
			bindingFlags |= (isPublic ? 16 : 32);
			return type.GetConstructor(bindingFlags, null, argTypes, null);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022B4 File Offset: 0x000004B4
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

		// Token: 0x06000021 RID: 33 RVA: 0x000022EC File Offset: 0x000004EC
		internal static IEnumerable<MethodInfo> GetMethods(this Type type)
		{
			return type.GetMethods();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000022F4 File Offset: 0x000004F4
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static MethodInfo GetMethod(this Type type, string name, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = 0;
			bindingFlags |= (isPublic ? 16 : 32);
			bindingFlags |= (isStatic ? 8 : 4);
			return type.GetMethod(name, bindingFlags);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002324 File Offset: 0x00000524
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static MethodInfo GetMethod(this Type type, string name, Type[] types, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = 0;
			bindingFlags |= (isPublic ? 16 : 32);
			bindingFlags |= (isStatic ? 8 : 4);
			return type.GetMethod(name, bindingFlags, null, types, null);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002355 File Offset: 0x00000555
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<MethodInfo> GetPublicStaticMethods(this Type type)
		{
			return type.GetMethods(24);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000235F File Offset: 0x0000055F
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<Type> GetNonPublicNestedTypes(this Type type)
		{
			return type.GetNestedTypes(32);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002369 File Offset: 0x00000569
		public static Regex CreateCompiled(string pattern, RegexOptions options)
		{
			options |= 0;
			return new Regex(pattern, options);
		}

		// Token: 0x04000003 RID: 3
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x04000004 RID: 4
		internal static readonly Regex DateValidator = PlatformHelper.CreateCompiled("^(\\d{4})-(0?[1-9]|1[012])-(0?[1-9]|[12]\\d|3[0|1])$", 16);

		// Token: 0x04000005 RID: 5
		internal static readonly Regex TimeOfDayValidator = PlatformHelper.CreateCompiled("^(0?\\d|1\\d|2[0-3]):(0?\\d|[1-5]\\d)(:(0?\\d|[1-5]\\d)(\\.\\d{1,7})?)?$", 16);

		// Token: 0x04000006 RID: 6
		internal static readonly Regex PotentialDateTimeOffsetValidator = PlatformHelper.CreateCompiled("^(\\d{2,4})-(\\d{1,2})-(\\d{1,2})(T|(\\s+))(\\d{1,2}):(\\d{1,2})", 16);

		// Token: 0x04000007 RID: 7
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static readonly string UriSchemeHttp = Uri.UriSchemeHttp;

		// Token: 0x04000008 RID: 8
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static readonly string UriSchemeHttps = Uri.UriSchemeHttps;
	}
}
