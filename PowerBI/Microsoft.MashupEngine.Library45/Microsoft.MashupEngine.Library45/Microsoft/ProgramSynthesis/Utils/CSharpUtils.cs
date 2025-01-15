using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003D8 RID: 984
	public static class CSharpUtils
	{
		// Token: 0x060015F5 RID: 5621 RVA: 0x0004030B File Offset: 0x0003E50B
		public static object GetUninitializedObject(Type type)
		{
			return FormatterServices.GetUninitializedObject(type);
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00040313 File Offset: 0x0003E513
		public static T GetUninitializedObject<T>()
		{
			return (T)((object)FormatterServices.GetUninitializedObject(typeof(T)));
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x0004032C File Offset: 0x0003E52C
		private static string NonPrimitiveToLiteral(this object obj, Dictionary<object, int> identityCache)
		{
			string text = obj as string;
			if (text != null)
			{
				return CSharpUtils.ObjectDisplay.FormatLiteral(text);
			}
			if (obj is Regex)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("/{0}/", new object[] { obj.ToString().ToLiteral(null).Slice(new int?(1), new int?(-1), 1) }));
			}
			if (obj is DateTime)
			{
				DateTime dateTime = (DateTime)obj;
				return dateTime.ToString((dateTime == dateTime.Date) ? "yyyy-MM-dd" : ((dateTime.Second == 0 && dateTime.Millisecond == 0) ? "yyyy-MM-ddTHH:mm" : ((dateTime.Millisecond == 0) ? "yyyy-MM-ddTHH:mm:ss" : "yyyy-MM-ddTHH:mm:ss.fff")), CultureInfo.InvariantCulture);
			}
			IRenderableLiteral renderableLiteral = obj as IRenderableLiteral;
			if (renderableLiteral != null)
			{
				return renderableLiteral.RenderHumanReadable();
			}
			int? recordArity = obj.GetType().GetRecordArity();
			if (recordArity != null)
			{
				return Enumerable.Range(0, recordArity.Value).Select(new Func<int, object>(obj.GetRecordItem)).ToArray<object>()
					.DumpCollection(ObjectFormatting.Literal, "(", ")", ", ", identityCache);
			}
			if (obj is IDictionary)
			{
				return obj.ToEnumerable<object>().DumpCollection(ObjectFormatting.Literal, "{", "}", ", ", identityCache);
			}
			if (obj is IEnumerable)
			{
				return obj.ToEnumerable<object>().DumpCollection(ObjectFormatting.Literal, "[", "]", ", ", identityCache);
			}
			IOptional optional = obj as IOptional;
			if (optional != null)
			{
				if (!optional.HasValue)
				{
					return "Nothing";
				}
				return FormattableString.Invariant(FormattableStringFactory.Create("Some {0}", new object[] { optional.Value.ToLiteral(null) }));
			}
			else
			{
				if (obj.GetType().GetTypeInfo().IsGenericType && obj.GetType().GetGenericTypeDefinition() == typeof(KeyValuePair<, >))
				{
					string text2 = obj.GetPropertyValue("Key").ToLiteral(identityCache);
					string text3 = obj.GetPropertyValue("Value").ToLiteral(identityCache);
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}: {1}", new object[] { text2, text3 }));
				}
				return obj.InternedFormat(identityCache, ObjectFormatting.ToString);
			}
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00040550 File Offset: 0x0003E750
		public static string ToLiteral(this object obj, Dictionary<object, int> identityCache = null)
		{
			if (obj == null)
			{
				return "null";
			}
			if (obj is bool)
			{
				if (!(bool)obj)
				{
					return "false";
				}
				return "true";
			}
			else
			{
				if (obj is byte)
				{
					byte b = (byte)obj;
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}", new object[] { b }));
				}
				if (obj is sbyte)
				{
					sbyte b2 = (sbyte)obj;
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}", new object[] { b2 }));
				}
				if (obj is short)
				{
					short num = (short)obj;
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}", new object[] { num }));
				}
				if (obj is ushort)
				{
					ushort num2 = (ushort)obj;
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}", new object[] { num2 }));
				}
				if (obj is int)
				{
					int num3 = (int)obj;
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}", new object[] { num3 }));
				}
				if (obj is uint)
				{
					uint num4 = (uint)obj;
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}U", new object[] { num4 }));
				}
				if (obj is long)
				{
					long num5 = (long)obj;
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}L", new object[] { num5 }));
				}
				if (obj is ulong)
				{
					ulong num6 = (ulong)obj;
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}UL", new object[] { num6 }));
				}
				if (obj is double)
				{
					double num7 = (double)obj;
					return FormattableString.Invariant(FormattableStringFactory.Create("{0:R}", new object[] { num7 }));
				}
				if (obj is float)
				{
					float num8 = (float)obj;
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}F", new object[] { num8 }));
				}
				if (obj is decimal)
				{
					decimal num9 = (decimal)obj;
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}M", new object[] { num9 }));
				}
				if (!(obj is char))
				{
					return obj.NonPrimitiveToLiteral(identityCache);
				}
				char c = (char)obj;
				if (c == '\\')
				{
					return "'\\\\'";
				}
				if (c == '\n')
				{
					return "'\\n'";
				}
				if (c != '\'')
				{
					return FormattableString.Invariant(FormattableStringFactory.Create("'{0}'", new object[] { c }));
				}
				return "'\\''";
			}
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x000407E5 File Offset: 0x0003E9E5
		public static object GetPropertyValue(this object obj, string propertyName)
		{
			return obj.GetType().GetProperty(propertyName).GetGetMethod()
				.Invoke(obj, new object[0]);
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00040804 File Offset: 0x0003EA04
		public static int? GetRecordArity(this Type t)
		{
			string name = t.Name;
			if (!name.StartsWith("Record`", StringComparison.Ordinal))
			{
				return null;
			}
			int num;
			if (!int.TryParse(name.Substring("Record`".Length), out num))
			{
				return null;
			}
			return new int?(num);
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x0004085C File Offset: 0x0003EA5C
		public static Type GetDictionaryType(this Type type)
		{
			foreach (Type type2 in CSharpUtils.DictionaryInterfaces)
			{
				Type[] array = type.InheritsGeneric(type2);
				if (array != null)
				{
					return type2.MakeGenericType(array);
				}
			}
			return null;
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00040898 File Offset: 0x0003EA98
		public static bool IsStatic(this Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			return typeInfo.IsSealed && typeInfo.IsAbstract && type.GetConstructor(Type.EmptyTypes) == null;
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x000408D0 File Offset: 0x0003EAD0
		public static object GetMemberValue(this Type type, string member)
		{
			FieldInfo field = type.GetField(member, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (field != null && field.IsStatic)
			{
				return field.GetValue(null);
			}
			PropertyInfo property = type.GetProperty(member, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (property != null)
			{
				return property.GetMethod.Invoke(null, null);
			}
			MethodInfo method = type.GetMethod(member, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (method == null)
			{
				return null;
			}
			return method.Invoke(null, null);
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00040938 File Offset: 0x0003EB38
		public static object GetMemberValue(this MemberInfo member, object instance)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo != null)
			{
				return fieldInfo.GetValue(instance);
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return propertyInfo.GetValue(instance, null);
			}
			MethodInfo methodInfo = member as MethodInfo;
			if (methodInfo != null)
			{
				return methodInfo.Invoke(instance, new object[0]);
			}
			return null;
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00040998 File Offset: 0x0003EB98
		public static bool IsConvertibleTo(this Type fromType, Type toType)
		{
			bool flag;
			try
			{
				Expression.Convert(Expression.Parameter(fromType, null), toType);
				flag = true;
			}
			catch
			{
				int? recordArity = fromType.GetRecordArity();
				int? recordArity2 = toType.GetRecordArity();
				if (recordArity != null && recordArity2 != null)
				{
					int? num = recordArity;
					int? num2 = recordArity2;
					if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
					{
						Type[] genericArguments = fromType.GetGenericArguments();
						Type[] genericArguments2 = toType.GetGenericArguments();
						for (int i = 0; i < genericArguments.Length; i++)
						{
							if (genericArguments[i] != genericArguments2[i] && !genericArguments[i].IsConvertibleTo(genericArguments2[i]))
							{
								return false;
							}
						}
						return true;
					}
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00040A68 File Offset: 0x0003EC68
		public static string CsName(this Type type, bool includeNamespace = true)
		{
			return CSharpCodeProvider.Instance.GetTypeOutput(new CodeTypeReference(type), includeNamespace);
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00040A7C File Offset: 0x0003EC7C
		public static Type[] InheritsGeneric(this Type toCheck, Type generic)
		{
			while (toCheck != null && toCheck != typeof(object))
			{
				TypeInfo typeInfo = toCheck.GetTypeInfo();
				Type type = (typeInfo.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck);
				if (generic == type)
				{
					return toCheck.GetGenericArguments();
				}
				if (type.GetTypeInfo().IsClass)
				{
					Type[] interfaces = toCheck.GetInterfaces();
					for (int i = 0; i < interfaces.Length; i++)
					{
						Type[] array = interfaces[i].InheritsGeneric(generic);
						if (array != null)
						{
							return array;
						}
					}
				}
				toCheck = typeInfo.BaseType;
			}
			return null;
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00040B0C File Offset: 0x0003ED0C
		public static IEnumerable<Type> AncestorsAndSelf(this Type type)
		{
			yield return type;
			if (type.GetTypeInfo().BaseType == null)
			{
				yield break;
			}
			foreach (Type type2 in type.GetTypeInfo().BaseType.AncestorsAndSelf())
			{
				yield return type2;
			}
			IEnumerator<Type> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00040B1C File Offset: 0x0003ED1C
		public static Type ReturnType(this MemberInfo member)
		{
			MemberTypes memberType = member.MemberType;
			if (memberType <= MemberTypes.Field)
			{
				if (memberType == MemberTypes.Event)
				{
					return ((EventInfo)member).EventHandlerType;
				}
				if (memberType == MemberTypes.Field)
				{
					return ((FieldInfo)member).FieldType;
				}
			}
			else
			{
				if (memberType == MemberTypes.Method)
				{
					return ((MethodInfo)member).ReturnType;
				}
				if (memberType == MemberTypes.Property)
				{
					return ((PropertyInfo)member).PropertyType;
				}
			}
			throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("MemberInfo {0} does not have a return type.", new object[] { member.MemberType })));
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00040BA1 File Offset: 0x0003EDA1
		public static bool IsNullable(this Type type)
		{
			return !type.IsValueType || null != Nullable.GetUnderlyingType(type);
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00040BB9 File Offset: 0x0003EDB9
		public static bool IsValidIdentifier(string name)
		{
			return CSharpUtils.UnicodeCharacterUtilities.IsValidIdentifier(name);
		}

		// Token: 0x04000AA5 RID: 2725
		private static readonly Type[] DictionaryInterfaces = new Type[]
		{
			typeof(IDictionary<, >),
			typeof(IReadOnlyDictionary<, >)
		};

		// Token: 0x020003D9 RID: 985
		private static class UnicodeCharacterUtilities
		{
			// Token: 0x06001607 RID: 5639 RVA: 0x00040BE8 File Offset: 0x0003EDE8
			public static bool IsIdentifierStartCharacter(char ch)
			{
				if (ch < 'a')
				{
					return ch >= 'A' && (ch <= 'Z' || ch == '_');
				}
				return ch <= 'z' || (ch > '\u007f' && CSharpUtils.UnicodeCharacterUtilities.IsLetterChar(CharUnicodeInfo.GetUnicodeCategory(ch)));
			}

			// Token: 0x06001608 RID: 5640 RVA: 0x00040C1C File Offset: 0x0003EE1C
			public static bool IsIdentifierPartCharacter(char ch)
			{
				if (ch < 'a')
				{
					if (ch < 'A')
					{
						return ch >= '0' && ch <= '9';
					}
					return ch <= 'Z' || ch == '_';
				}
				else
				{
					if (ch <= 'z')
					{
						return true;
					}
					if (ch <= '\u007f')
					{
						return false;
					}
					UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(ch);
					return CSharpUtils.UnicodeCharacterUtilities.IsLetterChar(unicodeCategory) || CSharpUtils.UnicodeCharacterUtilities.IsDecimalDigitChar(unicodeCategory) || CSharpUtils.UnicodeCharacterUtilities.IsConnectingChar(unicodeCategory) || CSharpUtils.UnicodeCharacterUtilities.IsCombiningChar(unicodeCategory) || CSharpUtils.UnicodeCharacterUtilities.IsFormattingChar(unicodeCategory);
				}
			}

			// Token: 0x06001609 RID: 5641 RVA: 0x00040C90 File Offset: 0x0003EE90
			public static bool IsValidIdentifier(string name)
			{
				if (string.IsNullOrEmpty(name))
				{
					return false;
				}
				if (!CSharpUtils.UnicodeCharacterUtilities.IsIdentifierStartCharacter(name[0]))
				{
					return false;
				}
				int length = name.Length;
				for (int i = 1; i < length; i++)
				{
					if (!CSharpUtils.UnicodeCharacterUtilities.IsIdentifierPartCharacter(name[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600160A RID: 5642 RVA: 0x00040CDB File Offset: 0x0003EEDB
			private static bool IsLetterChar(UnicodeCategory cat)
			{
				return cat <= UnicodeCategory.OtherLetter || cat == UnicodeCategory.LetterNumber;
			}

			// Token: 0x0600160B RID: 5643 RVA: 0x00040CE9 File Offset: 0x0003EEE9
			private static bool IsCombiningChar(UnicodeCategory cat)
			{
				return cat - UnicodeCategory.NonSpacingMark <= 1;
			}

			// Token: 0x0600160C RID: 5644 RVA: 0x00040CF4 File Offset: 0x0003EEF4
			private static bool IsDecimalDigitChar(UnicodeCategory cat)
			{
				return cat == UnicodeCategory.DecimalDigitNumber;
			}

			// Token: 0x0600160D RID: 5645 RVA: 0x00040CFA File Offset: 0x0003EEFA
			private static bool IsConnectingChar(UnicodeCategory cat)
			{
				return cat == UnicodeCategory.ConnectorPunctuation;
			}

			// Token: 0x0600160E RID: 5646 RVA: 0x00040D01 File Offset: 0x0003EF01
			internal static bool IsFormattingChar(char ch)
			{
				return ch > '\u007f' && CSharpUtils.UnicodeCharacterUtilities.IsFormattingChar(CharUnicodeInfo.GetUnicodeCategory(ch));
			}

			// Token: 0x0600160F RID: 5647 RVA: 0x00040D15 File Offset: 0x0003EF15
			private static bool IsFormattingChar(UnicodeCategory cat)
			{
				return cat == UnicodeCategory.Format;
			}
		}

		// Token: 0x020003DA RID: 986
		internal static class ObjectDisplay
		{
			// Token: 0x06001610 RID: 5648 RVA: 0x00040D1C File Offset: 0x0003EF1C
			internal static bool TryReplaceChar(char c, out string replaceWith)
			{
				replaceWith = null;
				switch (c)
				{
				case '\0':
					replaceWith = "\\0";
					break;
				case '\u0001':
				case '\u0002':
				case '\u0003':
				case '\u0004':
				case '\u0005':
				case '\u0006':
					break;
				case '\a':
					replaceWith = "\\a";
					break;
				case '\b':
					replaceWith = "\\b";
					break;
				case '\t':
					replaceWith = "\\t";
					break;
				case '\n':
					replaceWith = "\\n";
					break;
				case '\v':
					replaceWith = "\\v";
					break;
				case '\f':
					replaceWith = "\\f";
					break;
				case '\r':
					replaceWith = "\\r";
					break;
				default:
					if (c == '\\')
					{
						replaceWith = "\\\\";
					}
					break;
				}
				if (replaceWith != null)
				{
					return true;
				}
				if (CSharpUtils.ObjectDisplay.NeedsEscaping(CharUnicodeInfo.GetUnicodeCategory(c)))
				{
					string text = "\\u";
					int num = (int)c;
					replaceWith = text + num.ToString("x4");
					return true;
				}
				return false;
			}

			// Token: 0x06001611 RID: 5649 RVA: 0x00040DEE File Offset: 0x0003EFEE
			private static bool NeedsEscaping(UnicodeCategory category)
			{
				return category - UnicodeCategory.LineSeparator <= 2 || category == UnicodeCategory.Surrogate || category == UnicodeCategory.OtherNotAssigned;
			}

			// Token: 0x06001612 RID: 5650 RVA: 0x00040E04 File Offset: 0x0003F004
			public static string FormatLiteral(string value)
			{
				return CSharpUtils.ObjectDisplay.FormatLiteral(value, true);
			}

			// Token: 0x06001613 RID: 5651 RVA: 0x00040E10 File Offset: 0x0003F010
			internal static string FormatLiteral(string value, bool useQuotes)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				bool flag2 = false;
				if (useQuotes)
				{
					stringBuilder.Append('"');
				}
				for (int i = 0; i < value.Length; i++)
				{
					char c = value[i];
					string text2;
					if (flag && CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.Surrogate)
					{
						UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(value, i);
						if (unicodeCategory == UnicodeCategory.Surrogate)
						{
							StringBuilder stringBuilder2 = stringBuilder;
							string text = "\\u";
							int num = (int)c;
							stringBuilder2.Append(text + num.ToString("x4"));
						}
						else if (CSharpUtils.ObjectDisplay.NeedsEscaping(unicodeCategory))
						{
							stringBuilder.Append("\\U" + char.ConvertToUtf32(value, i).ToString("x8"));
							i++;
						}
						else
						{
							stringBuilder.Append(c);
							stringBuilder.Append(value[++i]);
						}
					}
					else if (flag && CSharpUtils.ObjectDisplay.TryReplaceChar(c, out text2))
					{
						stringBuilder.Append(text2);
					}
					else if (useQuotes && c == '"')
					{
						if (flag2)
						{
							stringBuilder.Append('"');
							stringBuilder.Append('"');
						}
						else
						{
							stringBuilder.Append('\\');
							stringBuilder.Append('"');
						}
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				if (useQuotes)
				{
					stringBuilder.Append('"');
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06001614 RID: 5652 RVA: 0x00040F64 File Offset: 0x0003F164
			private static CultureInfo GetFormatCulture(CultureInfo cultureInfo)
			{
				return cultureInfo ?? CultureInfo.InvariantCulture;
			}
		}
	}
}
