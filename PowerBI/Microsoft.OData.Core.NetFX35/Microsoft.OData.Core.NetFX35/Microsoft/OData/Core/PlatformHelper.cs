using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core
{
	// Token: 0x020002A4 RID: 676
	internal static class PlatformHelper
	{
		// Token: 0x06001750 RID: 5968 RVA: 0x00050021 File Offset: 0x0004E221
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Assembly GetAssembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x00050029 File Offset: 0x0004E229
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x00050031 File Offset: 0x0004E231
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsGenericParameter(this Type type)
		{
			return type.IsGenericParameter;
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00050039 File Offset: 0x0004E239
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00050041 File Offset: 0x0004E241
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00050049 File Offset: 0x0004E249
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x00050051 File Offset: 0x0004E251
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsVisible(this Type type)
		{
			return type.IsVisible;
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x00050059 File Offset: 0x0004E259
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00050061 File Offset: 0x0004E261
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00050069 File Offset: 0x0004E269
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x00050071 File Offset: 0x0004E271
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Type GetBaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x00050079 File Offset: 0x0004E279
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool ContainsGenericParameters(this Type type)
		{
			return type.ContainsGenericParameters;
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00050081 File Offset: 0x0004E281
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
		{
			return Array.AsReadOnly<T>(array);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0005008C File Offset: 0x0004E28C
		internal static Date ConvertStringToDate(string text)
		{
			if (text == null || !PlatformHelper.DateValidator.IsMatch(text))
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "String '{0}' was not recognized as a valid Edm.Date.", new object[] { text }));
			}
			return Date.Parse(text, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x000500D8 File Offset: 0x0004E2D8
		internal static TimeOfDay ConvertStringToTimeOfDay(string text)
		{
			if (text == null || !PlatformHelper.TimeOfDayValidator.IsMatch(text))
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "String '{0}' was not recognized as a valid Edm.TimeOfDay.", new object[] { text }));
			}
			return TimeOfDay.Parse(text, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00050124 File Offset: 0x0004E324
		internal static DateTimeOffset ConvertStringToDateTimeOffset(string text)
		{
			text = PlatformHelper.AddSecondsPaddingIfMissing(text);
			DateTimeOffset dateTimeOffset = XmlConvert.ToDateTimeOffset(text);
			PlatformHelper.ValidateTimeZoneInformationInDateTimeOffsetString(text);
			return dateTimeOffset;
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x00050148 File Offset: 0x0004E348
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

		// Token: 0x06001761 RID: 5985 RVA: 0x000501BC File Offset: 0x0004E3BC
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

		// Token: 0x06001762 RID: 5986 RVA: 0x0005020A File Offset: 0x0004E40A
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Type GetTypeOrThrow(string typeName)
		{
			return Type.GetType(typeName, true);
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00050213 File Offset: 0x0004E413
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static TypeCode GetTypeCode(Type type)
		{
			return Type.GetTypeCode(type);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0005021B File Offset: 0x0004E41B
		internal static UnicodeCategory GetUnicodeCategory(char c)
		{
			return char.GetUnicodeCategory(c);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00050223 File Offset: 0x0004E423
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsProperty(MemberInfo member)
		{
			return member.MemberType == 16;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0005022F File Offset: 0x0004E42F
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x00050237 File Offset: 0x0004E437
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0005023F File Offset: 0x0004E43F
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsMethod(MemberInfo member)
		{
			return member.MemberType == 8;
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0005024A File Offset: 0x0004E44A
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool AreMembersEqual(MemberInfo member1, MemberInfo member2)
		{
			return member1.MetadataToken == member2.MetadataToken;
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0005025A File Offset: 0x0004E45A
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly)
		{
			return type.GetPublicProperties(instanceOnly, false);
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x00050264 File Offset: 0x0004E464
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

		// Token: 0x0600176C RID: 5996 RVA: 0x0005028C File Offset: 0x0004E48C
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

		// Token: 0x0600176D RID: 5997 RVA: 0x000502B4 File Offset: 0x0004E4B4
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<ConstructorInfo> GetInstanceConstructors(this Type type, bool isPublic)
		{
			BindingFlags bindingFlags = 4;
			bindingFlags |= (isPublic ? 16 : 32);
			return type.GetConstructors(bindingFlags);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x000502D8 File Offset: 0x0004E4D8
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static ConstructorInfo GetInstanceConstructor(this Type type, bool isPublic, Type[] argTypes)
		{
			BindingFlags bindingFlags = 4;
			bindingFlags |= (isPublic ? 16 : 32);
			return type.GetConstructor(bindingFlags, null, argTypes, null);
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00050300 File Offset: 0x0004E500
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

		// Token: 0x06001770 RID: 6000 RVA: 0x0005033C File Offset: 0x0004E53C
		internal static IEnumerable<MethodInfo> GetMethods(this Type type)
		{
			return type.GetMethods();
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00050344 File Offset: 0x0004E544
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static MethodInfo GetMethod(this Type type, string name, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = 0;
			bindingFlags |= (isPublic ? 16 : 32);
			bindingFlags |= (isStatic ? 8 : 4);
			return type.GetMethod(name, bindingFlags);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00050374 File Offset: 0x0004E574
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static MethodInfo GetMethod(this Type type, string name, Type[] types, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = 0;
			bindingFlags |= (isPublic ? 16 : 32);
			bindingFlags |= (isStatic ? 8 : 4);
			return type.GetMethod(name, bindingFlags, null, types, null);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x000503A5 File Offset: 0x0004E5A5
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<MethodInfo> GetPublicStaticMethods(this Type type)
		{
			return type.GetMethods(24);
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x000503AF File Offset: 0x0004E5AF
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<Type> GetNonPublicNestedTypes(this Type type)
		{
			return type.GetNestedTypes(32);
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x000503B9 File Offset: 0x0004E5B9
		public static Regex CreateCompiled(string pattern, RegexOptions options)
		{
			options = options;
			return new Regex(pattern, options);
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x000503C5 File Offset: 0x0004E5C5
		public static string[] GetSegments(this Uri uri)
		{
			return uri.Segments;
		}

		// Token: 0x04000A0D RID: 2573
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x04000A0E RID: 2574
		internal static readonly Regex DateValidator = PlatformHelper.CreateCompiled("^(\\d{4})-(0?[1-9]|1[012])-(0?[1-9]|[12]\\d|3[0|1])$", 16);

		// Token: 0x04000A0F RID: 2575
		internal static readonly Regex TimeOfDayValidator = PlatformHelper.CreateCompiled("^(0?\\d|1\\d|2[0-3]):(0?\\d|[1-5]\\d)(:(0?\\d|[1-5]\\d)(\\.\\d{1,7})?)?$", 16);

		// Token: 0x04000A10 RID: 2576
		internal static readonly Regex PotentialDateTimeOffsetValidator = PlatformHelper.CreateCompiled("^(\\d{2,4})-(\\d{1,2})-(\\d{1,2})(T|(\\s+))(\\d{1,2}):(\\d{1,2})", 16);

		// Token: 0x04000A11 RID: 2577
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static readonly string UriSchemeHttp = Uri.UriSchemeHttp;

		// Token: 0x04000A12 RID: 2578
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static readonly string UriSchemeHttps = Uri.UriSchemeHttps;
	}
}
