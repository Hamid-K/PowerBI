using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace Microsoft.Data.Edm
{
	// Token: 0x0200023F RID: 575
	internal static class PlatformHelper
	{
		// Token: 0x06000D20 RID: 3360 RVA: 0x00029FB1 File Offset: 0x000281B1
		internal static Assembly GetAssembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00029FB9 File Offset: 0x000281B9
		internal static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00029FC1 File Offset: 0x000281C1
		internal static bool IsGenericParameter(this Type type)
		{
			return type.IsGenericParameter;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00029FC9 File Offset: 0x000281C9
		internal static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00029FD1 File Offset: 0x000281D1
		internal static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00029FD9 File Offset: 0x000281D9
		internal static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00029FE1 File Offset: 0x000281E1
		internal static bool IsVisible(this Type type)
		{
			return type.IsVisible;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00029FE9 File Offset: 0x000281E9
		internal static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00029FF1 File Offset: 0x000281F1
		internal static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00029FF9 File Offset: 0x000281F9
		internal static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0002A001 File Offset: 0x00028201
		internal static Type GetBaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0002A009 File Offset: 0x00028209
		internal static bool ContainsGenericParameters(this Type type)
		{
			return type.ContainsGenericParameters;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0002A011 File Offset: 0x00028211
		internal static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
		{
			return Array.AsReadOnly<T>(array);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0002A019 File Offset: 0x00028219
		internal static DateTime ConvertStringToDateTime(string text)
		{
			text = PlatformHelper.AddSecondsPaddingIfMissing(text);
			return XmlConvert.ToDateTime(text, 3);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0002A02A File Offset: 0x0002822A
		internal static DateTimeOffset ConvertStringToDateTimeOffset(string text)
		{
			text = PlatformHelper.AddSecondsPaddingIfMissing(text);
			return XmlConvert.ToDateTimeOffset(text);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0002A03C File Offset: 0x0002823C
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

		// Token: 0x06000D30 RID: 3376 RVA: 0x0002A084 File Offset: 0x00028284
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

		// Token: 0x06000D31 RID: 3377 RVA: 0x0002A0CD File Offset: 0x000282CD
		internal static string ConvertDateTimeToString(DateTime dateTime)
		{
			return XmlConvert.ToString(dateTime, 3);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0002A0D6 File Offset: 0x000282D6
		internal static Type GetTypeOrThrow(string typeName)
		{
			return Type.GetType(typeName, true);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0002A0DF File Offset: 0x000282DF
		internal static TypeCode GetTypeCode(Type type)
		{
			return Type.GetTypeCode(type);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0002A0E7 File Offset: 0x000282E7
		internal static UnicodeCategory GetUnicodeCategory(char c)
		{
			return char.GetUnicodeCategory(c);
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0002A0EF File Offset: 0x000282EF
		internal static bool IsProperty(MemberInfo member)
		{
			return member.MemberType == 16;
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0002A0FB File Offset: 0x000282FB
		internal static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0002A103 File Offset: 0x00028303
		internal static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0002A10B File Offset: 0x0002830B
		internal static bool IsMethod(MemberInfo member)
		{
			return member.MemberType == 8;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0002A116 File Offset: 0x00028316
		internal static bool AreMembersEqual(MemberInfo member1, MemberInfo member2)
		{
			return member1.MetadataToken == member2.MetadataToken;
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0002A126 File Offset: 0x00028326
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly)
		{
			return type.GetPublicProperties(instanceOnly, false);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0002A130 File Offset: 0x00028330
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

		// Token: 0x06000D3C RID: 3388 RVA: 0x0002A158 File Offset: 0x00028358
		internal static IEnumerable<ConstructorInfo> GetInstanceConstructors(this Type type, bool isPublic)
		{
			BindingFlags bindingFlags = 4;
			bindingFlags |= (isPublic ? 16 : 32);
			return type.GetConstructors(bindingFlags);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0002A17C File Offset: 0x0002837C
		internal static ConstructorInfo GetInstanceConstructor(this Type type, bool isPublic, Type[] argTypes)
		{
			BindingFlags bindingFlags = 4;
			bindingFlags |= (isPublic ? 16 : 32);
			return type.GetConstructor(bindingFlags, null, argTypes, null);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0002A1A4 File Offset: 0x000283A4
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

		// Token: 0x06000D3F RID: 3391 RVA: 0x0002A1E0 File Offset: 0x000283E0
		internal static MethodInfo GetMethod(this Type type, string name, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = 0;
			bindingFlags |= (isPublic ? 16 : 32);
			bindingFlags |= (isStatic ? 8 : 4);
			return type.GetMethod(name, bindingFlags);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0002A210 File Offset: 0x00028410
		internal static MethodInfo GetMethod(this Type type, string name, Type[] types, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = 0;
			bindingFlags |= (isPublic ? 16 : 32);
			bindingFlags |= (isStatic ? 8 : 4);
			return type.GetMethod(name, bindingFlags, null, types, null);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0002A241 File Offset: 0x00028441
		internal static IEnumerable<MethodInfo> GetPublicStaticMethods(this Type type)
		{
			return type.GetMethods(24);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0002A24B File Offset: 0x0002844B
		internal static IEnumerable<Type> GetNonPublicNestedTypes(this Type type)
		{
			return type.GetNestedTypes(32);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0002A255 File Offset: 0x00028455
		public static Regex CreateCompiled(string pattern, RegexOptions options)
		{
			options = options;
			return new Regex(pattern, options);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0002A261 File Offset: 0x00028461
		public static string[] GetSegments(this Uri uri)
		{
			return uri.Segments;
		}

		// Token: 0x0400074B RID: 1867
		internal static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x0400074C RID: 1868
		internal static readonly string UriSchemeHttp = Uri.UriSchemeHttp;

		// Token: 0x0400074D RID: 1869
		internal static readonly string UriSchemeHttps = Uri.UriSchemeHttps;
	}
}
