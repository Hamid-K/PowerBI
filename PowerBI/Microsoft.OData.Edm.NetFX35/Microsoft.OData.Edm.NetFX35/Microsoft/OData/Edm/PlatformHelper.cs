using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000278 RID: 632
	internal static class PlatformHelper
	{
		// Token: 0x06000E28 RID: 3624 RVA: 0x0002B54D File Offset: 0x0002974D
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Assembly GetAssembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0002B555 File Offset: 0x00029755
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0002B55D File Offset: 0x0002975D
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsGenericParameter(this Type type)
		{
			return type.IsGenericParameter;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0002B565 File Offset: 0x00029765
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0002B56D File Offset: 0x0002976D
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0002B575 File Offset: 0x00029775
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0002B57D File Offset: 0x0002977D
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsVisible(this Type type)
		{
			return type.IsVisible;
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0002B585 File Offset: 0x00029785
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0002B58D File Offset: 0x0002978D
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0002B595 File Offset: 0x00029795
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0002B59D File Offset: 0x0002979D
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Type GetBaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0002B5A5 File Offset: 0x000297A5
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool ContainsGenericParameters(this Type type)
		{
			return type.ContainsGenericParameters;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0002B5AD File Offset: 0x000297AD
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
		{
			return Array.AsReadOnly<T>(array);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0002B5B8 File Offset: 0x000297B8
		internal static Date ConvertStringToDate(string text)
		{
			if (text == null || !PlatformHelper.DateValidator.IsMatch(text))
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "String '{0}' was not recognized as a valid Edm.Date.", new object[] { text }));
			}
			return Date.Parse(text, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0002B604 File Offset: 0x00029804
		internal static TimeOfDay ConvertStringToTimeOfDay(string text)
		{
			if (text == null || !PlatformHelper.TimeOfDayValidator.IsMatch(text))
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "String '{0}' was not recognized as a valid Edm.TimeOfDay.", new object[] { text }));
			}
			return TimeOfDay.Parse(text, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0002B650 File Offset: 0x00029850
		internal static DateTimeOffset ConvertStringToDateTimeOffset(string text)
		{
			text = PlatformHelper.AddSecondsPaddingIfMissing(text);
			DateTimeOffset dateTimeOffset = XmlConvert.ToDateTimeOffset(text);
			PlatformHelper.ValidateTimeZoneInformationInDateTimeOffsetString(text);
			return dateTimeOffset;
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0002B674 File Offset: 0x00029874
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

		// Token: 0x06000E39 RID: 3641 RVA: 0x0002B6E8 File Offset: 0x000298E8
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

		// Token: 0x06000E3A RID: 3642 RVA: 0x0002B736 File Offset: 0x00029936
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static Type GetTypeOrThrow(string typeName)
		{
			return Type.GetType(typeName, true);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0002B73F File Offset: 0x0002993F
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static TypeCode GetTypeCode(Type type)
		{
			return Type.GetTypeCode(type);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0002B747 File Offset: 0x00029947
		internal static UnicodeCategory GetUnicodeCategory(char c)
		{
			return char.GetUnicodeCategory(c);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0002B74F File Offset: 0x0002994F
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsProperty(MemberInfo member)
		{
			return member.MemberType == 16;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0002B75B File Offset: 0x0002995B
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0002B763 File Offset: 0x00029963
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0002B76B File Offset: 0x0002996B
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool IsMethod(MemberInfo member)
		{
			return member.MemberType == 8;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0002B776 File Offset: 0x00029976
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static bool AreMembersEqual(MemberInfo member1, MemberInfo member2)
		{
			return member1.MetadataToken == member2.MetadataToken;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0002B786 File Offset: 0x00029986
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly)
		{
			return type.GetPublicProperties(instanceOnly, false);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0002B790 File Offset: 0x00029990
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

		// Token: 0x06000E44 RID: 3652 RVA: 0x0002B7B8 File Offset: 0x000299B8
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

		// Token: 0x06000E45 RID: 3653 RVA: 0x0002B7E0 File Offset: 0x000299E0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<ConstructorInfo> GetInstanceConstructors(this Type type, bool isPublic)
		{
			BindingFlags bindingFlags = 4;
			bindingFlags |= (isPublic ? 16 : 32);
			return type.GetConstructors(bindingFlags);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0002B804 File Offset: 0x00029A04
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static ConstructorInfo GetInstanceConstructor(this Type type, bool isPublic, Type[] argTypes)
		{
			BindingFlags bindingFlags = 4;
			bindingFlags |= (isPublic ? 16 : 32);
			return type.GetConstructor(bindingFlags, null, argTypes, null);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0002B82C File Offset: 0x00029A2C
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

		// Token: 0x06000E48 RID: 3656 RVA: 0x0002B868 File Offset: 0x00029A68
		internal static IEnumerable<MethodInfo> GetMethods(this Type type)
		{
			return type.GetMethods();
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0002B870 File Offset: 0x00029A70
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static MethodInfo GetMethod(this Type type, string name, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = 0;
			bindingFlags |= (isPublic ? 16 : 32);
			bindingFlags |= (isStatic ? 8 : 4);
			return type.GetMethod(name, bindingFlags);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0002B8A0 File Offset: 0x00029AA0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static MethodInfo GetMethod(this Type type, string name, Type[] types, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = 0;
			bindingFlags |= (isPublic ? 16 : 32);
			bindingFlags |= (isStatic ? 8 : 4);
			return type.GetMethod(name, bindingFlags, null, types, null);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0002B8D1 File Offset: 0x00029AD1
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<MethodInfo> GetPublicStaticMethods(this Type type)
		{
			return type.GetMethods(24);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0002B8DB File Offset: 0x00029ADB
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static IEnumerable<Type> GetNonPublicNestedTypes(this Type type)
		{
			return type.GetNestedTypes(32);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0002B8E5 File Offset: 0x00029AE5
		public static Regex CreateCompiled(string pattern, RegexOptions options)
		{
			options = options;
			return new Regex(pattern, options);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0002B8F1 File Offset: 0x00029AF1
		public static string[] GetSegments(this Uri uri)
		{
			return uri.Segments;
		}

		// Token: 0x04000771 RID: 1905
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x04000772 RID: 1906
		internal static readonly Regex DateValidator = PlatformHelper.CreateCompiled("^(\\d{4})-(0?[1-9]|1[012])-(0?[1-9]|[12]\\d|3[0|1])$", 16);

		// Token: 0x04000773 RID: 1907
		internal static readonly Regex TimeOfDayValidator = PlatformHelper.CreateCompiled("^(0?\\d|1\\d|2[0-3]):(0?\\d|[1-5]\\d)(:(0?\\d|[1-5]\\d)(\\.\\d{1,7})?)?$", 16);

		// Token: 0x04000774 RID: 1908
		internal static readonly Regex PotentialDateTimeOffsetValidator = PlatformHelper.CreateCompiled("^(\\d{2,4})-(\\d{1,2})-(\\d{1,2})(T|(\\s+))(\\d{1,2}):(\\d{1,2})", 16);

		// Token: 0x04000775 RID: 1909
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static readonly string UriSchemeHttp = Uri.UriSchemeHttp;

		// Token: 0x04000776 RID: 1910
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
		internal static readonly string UriSchemeHttps = Uri.UriSchemeHttps;
	}
}
