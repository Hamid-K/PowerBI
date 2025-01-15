using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200001D RID: 29
	internal static class PlatformHelper
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x000088DB File Offset: 0x00006ADB
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Assembly GetAssembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000088E3 File Offset: 0x00006AE3
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000088EB File Offset: 0x00006AEB
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000088F3 File Offset: 0x00006AF3
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000088FB File Offset: 0x00006AFB
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008903 File Offset: 0x00006B03
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsVisible(this Type type)
		{
			return type.IsVisible;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000890B File Offset: 0x00006B0B
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008913 File Offset: 0x00006B13
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000891B File Offset: 0x00006B1B
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00008923 File Offset: 0x00006B23
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Type GetBaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000892B File Offset: 0x00006B2B
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool ContainsGenericParameters(this Type type)
		{
			return type.ContainsGenericParameters;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008933 File Offset: 0x00006B33
		internal static Date ConvertStringToDate(string text)
		{
			if (text == null || !PlatformHelper.DateValidator.IsMatch(text))
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "String '{0}' was not recognized as a valid Edm.Date.", new object[] { text }));
			}
			return Date.Parse(text, CultureInfo.InvariantCulture);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000896F File Offset: 0x00006B6F
		internal static TimeOfDay ConvertStringToTimeOfDay(string text)
		{
			if (text == null || !PlatformHelper.TimeOfDayValidator.IsMatch(text))
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "String '{0}' was not recognized as a valid Edm.TimeOfDay.", new object[] { text }));
			}
			return TimeOfDay.Parse(text, CultureInfo.InvariantCulture);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000089AC File Offset: 0x00006BAC
		internal static DateTimeOffset ConvertStringToDateTimeOffset(string text)
		{
			text = PlatformHelper.AddSecondsPaddingIfMissing(text);
			DateTimeOffset dateTimeOffset = XmlConvert.ToDateTimeOffset(text);
			PlatformHelper.ValidateTimeZoneInformationInDateTimeOffsetString(text);
			return dateTimeOffset;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000089D0 File Offset: 0x00006BD0
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

		// Token: 0x060001E0 RID: 480 RVA: 0x00008A44 File Offset: 0x00006C44
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

		// Token: 0x060001E1 RID: 481 RVA: 0x00008A92 File Offset: 0x00006C92
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Type GetTypeOrThrow(string typeName)
		{
			return Type.GetType(typeName, true);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00008A9B File Offset: 0x00006C9B
		internal static UnicodeCategory GetUnicodeCategory(char c)
		{
			return char.GetUnicodeCategory(c);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008AA3 File Offset: 0x00006CA3
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsProperty(MemberInfo member)
		{
			return member.MemberType == 16;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008AAF File Offset: 0x00006CAF
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008AB7 File Offset: 0x00006CB7
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00008ABF File Offset: 0x00006CBF
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsMethod(MemberInfo member)
		{
			return member.MemberType == 8;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00008ACA File Offset: 0x00006CCA
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool AreMembersEqual(MemberInfo member1, MemberInfo member2)
		{
			return member1.MetadataToken == member2.MetadataToken;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00008ADA File Offset: 0x00006CDA
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly)
		{
			return type.GetPublicProperties(instanceOnly, false);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008AE4 File Offset: 0x00006CE4
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

		// Token: 0x060001EA RID: 490 RVA: 0x00008B0C File Offset: 0x00006D0C
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

		// Token: 0x060001EB RID: 491 RVA: 0x00008B34 File Offset: 0x00006D34
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<ConstructorInfo> GetInstanceConstructors(this Type type, bool isPublic)
		{
			BindingFlags bindingFlags = 4;
			bindingFlags |= (isPublic ? 16 : 32);
			return type.GetConstructors(bindingFlags);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00008B58 File Offset: 0x00006D58
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static ConstructorInfo GetInstanceConstructor(this Type type, bool isPublic, Type[] argTypes)
		{
			BindingFlags bindingFlags = 4;
			bindingFlags |= (isPublic ? 16 : 32);
			return type.GetConstructor(bindingFlags, null, argTypes, null);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00008B80 File Offset: 0x00006D80
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

		// Token: 0x060001EE RID: 494 RVA: 0x00008BB8 File Offset: 0x00006DB8
		internal static IEnumerable<MethodInfo> GetMethods(this Type type)
		{
			return type.GetMethods();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008BC0 File Offset: 0x00006DC0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static MethodInfo GetMethod(this Type type, string name, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = 0;
			bindingFlags |= (isPublic ? 16 : 32);
			bindingFlags |= (isStatic ? 8 : 4);
			return type.GetMethod(name, bindingFlags);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008BF0 File Offset: 0x00006DF0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static MethodInfo GetMethod(this Type type, string name, Type[] types, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = 0;
			bindingFlags |= (isPublic ? 16 : 32);
			bindingFlags |= (isStatic ? 8 : 4);
			return type.GetMethod(name, bindingFlags, null, types, null);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00008C21 File Offset: 0x00006E21
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<MethodInfo> GetPublicStaticMethods(this Type type)
		{
			return type.GetMethods(24);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008C2B File Offset: 0x00006E2B
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<Type> GetNonPublicNestedTypes(this Type type)
		{
			return type.GetNestedTypes(32);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00008C35 File Offset: 0x00006E35
		public static Regex CreateCompiled(string pattern, RegexOptions options)
		{
			options |= 0;
			return new Regex(pattern, options);
		}

		// Token: 0x04000031 RID: 49
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x04000032 RID: 50
		internal static readonly Regex DateValidator = PlatformHelper.CreateCompiled("^(\\d{4})-(0?[1-9]|1[012])-(0?[1-9]|[12]\\d|3[0|1])$", 16);

		// Token: 0x04000033 RID: 51
		internal static readonly Regex TimeOfDayValidator = PlatformHelper.CreateCompiled("^(0?\\d|1\\d|2[0-3]):(0?\\d|[1-5]\\d)(:(0?\\d|[1-5]\\d)(\\.\\d{1,7})?)?$", 16);

		// Token: 0x04000034 RID: 52
		internal static readonly Regex PotentialDateTimeOffsetValidator = PlatformHelper.CreateCompiled("^(\\d{2,4})-(\\d{1,2})-(\\d{1,2})(T|(\\s+))(\\d{1,2}):(\\d{1,2})", 16);

		// Token: 0x04000035 RID: 53
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static readonly string UriSchemeHttp = Uri.UriSchemeHttp;

		// Token: 0x04000036 RID: 54
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static readonly string UriSchemeHttps = Uri.UriSchemeHttps;
	}
}
