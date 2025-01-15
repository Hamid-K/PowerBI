using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008E1 RID: 2273
	public static class StdLiteralParsing
	{
		// Token: 0x0600310D RID: 12557 RVA: 0x00090461 File Offset: 0x0008E661
		public static Optional<object> TryParse(XElement node, Type expectedType, DeserializationContext context = default(DeserializationContext))
		{
			return StdLiteralParsing.TryParse(node, expectedType, expectedType.GetTypeInfo().GetCustomAttribute<ParseableAttribute>(), context);
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x00090476 File Offset: 0x0008E676
		private static Optional<object> ToOptionalObject(object result)
		{
			IOptional optional = result as IOptional;
			if (optional == null)
			{
				return result.SomeIfNotNull<object>();
			}
			return optional.Cast<object>();
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x00090490 File Offset: 0x0008E690
		private static Optional<object> TryParse(XElement node, Type expectedType, ParseableAttribute attribute, DeserializationContext context)
		{
			if (node.Elements().Count<XElement>() == 1 && node.Elements().First<XElement>().Name != "Item")
			{
				node = node.Elements().First<XElement>();
			}
			if (attribute == null)
			{
				return StdLiteralParsing.TryParseKnownType(node, expectedType, context, null);
			}
			MethodInfo method = (attribute.DeclaringType ?? expectedType).GetMethod(attribute.ParseXML, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (method == null)
			{
				return StdLiteralParsing.TryParseKnownType(node, expectedType, context, attribute);
			}
			MethodBase methodBase = method;
			object obj = null;
			object[] array2;
			if (method.GetParameters().Length != 1)
			{
				object[] array = new object[2];
				array[0] = node;
				array2 = array;
				array[1] = context;
			}
			else
			{
				(array2 = new object[1])[0] = node;
			}
			return StdLiteralParsing.ToOptionalObject(methodBase.Invoke(obj, array2));
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x0009054A File Offset: 0x0008E74A
		public static Optional<object> TryParse(string value, Type expectedType, DeserializationContext context = default(DeserializationContext))
		{
			return StdLiteralParsing.TryParse(value, expectedType, expectedType.GetTypeInfo().GetCustomAttribute<ParseableAttribute>(), context);
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x0009055F File Offset: 0x0008E75F
		public static Optional<T> TryParse<T>(string value, DeserializationContext context = default(DeserializationContext))
		{
			return StdLiteralParsing.TryParse(value, typeof(T), context).Cast<T>();
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x0009057C File Offset: 0x0008E77C
		public static Optional<T> TryParse<T>(XElement value, DeserializationContext context = default(DeserializationContext))
		{
			return StdLiteralParsing.TryParse(value, typeof(T), context).Cast<T>();
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x0009059C File Offset: 0x0008E79C
		private static Optional<object> TryParse(string value, Type expectedType, ParseableAttribute attribute, DeserializationContext context)
		{
			Optional<object> optional = Optional<object>.Nothing;
			if (!string.IsNullOrWhiteSpace((attribute != null) ? attribute.ParseHumanReadableString : null))
			{
				MethodInfo method = (attribute.DeclaringType ?? expectedType).GetMethod(attribute.ParseHumanReadableString, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				if (method != null)
				{
					MethodBase methodBase = method;
					object obj = null;
					object[] array2;
					if (method.GetParameters().Length != 1)
					{
						object[] array = new object[2];
						array[0] = value;
						array2 = array;
						array[1] = context;
					}
					else
					{
						(array2 = new object[1])[0] = value;
					}
					optional = StdLiteralParsing.ToOptionalObject(methodBase.Invoke(obj, array2));
				}
			}
			if (!optional.HasValue)
			{
				return StdLiteralParsing.TryParseKnownType(value, expectedType);
			}
			return optional;
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06003114 RID: 12564 RVA: 0x0009062D File Offset: 0x0008E82D
		internal static IEnumerable<Type> KnownTypes
		{
			get
			{
				return StdLiteralParsing.KnownTypeParsers.Keys;
			}
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x0009063C File Offset: 0x0008E83C
		private static bool IsArrayAssignableType(Type type, out Type elementType)
		{
			elementType = null;
			if (type.IsArray)
			{
				elementType = type.GetElementType();
				return true;
			}
			TypeInfo typeInfo = type.GetTypeInfo();
			if (typeInfo.IsGenericType && (typeInfo.GetGenericTypeDefinition() == typeof(IReadOnlyList<>) || typeInfo.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>)))
			{
				elementType = typeInfo.GetGenericArguments().First<Type>();
				return true;
			}
			return false;
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000906AC File Offset: 0x0008E8AC
		private static Optional<object> TryParseKnownType(XElement node, Type expectedType, DeserializationContext context, ParseableAttribute attribute = null)
		{
			Type type;
			if (StdLiteralParsing.IsArrayAssignableType(expectedType, out type))
			{
				return StdLiteralParsing.TryParseArray(node, type, context);
			}
			if (expectedType.GetRecordArity() != null)
			{
				return StdLiteralParsing.TryParseRecord(node, expectedType, context);
			}
			if (expectedType.GetTypeInfo().IsGenericType && expectedType.GetGenericTypeDefinition() == typeof(KeyValuePair<, >))
			{
				return StdLiteralParsing.TryParseKeyValuePair(node, expectedType, context);
			}
			Type dictionaryType;
			if ((dictionaryType = expectedType.GetDictionaryType()) != null)
			{
				return StdLiteralParsing.TryParseDictionary(node, dictionaryType, context);
			}
			if (expectedType.GetTypeInfo().IsGenericType && expectedType.GetGenericTypeDefinition() == typeof(Optional<>))
			{
				return StdLiteralParsing.TryParseOptional(node, expectedType, context);
			}
			if (expectedType.GetTypeInfo().IsGenericType && expectedType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				return StdLiteralParsing.TryParseNullable(node, expectedType, context);
			}
			return StdLiteralParsing.TryParse(node.Value, expectedType, attribute, context);
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x00090798 File Offset: 0x0008E998
		private static Optional<object> TryParseArray(XElement node, Type elementType, DeserializationContext context)
		{
			Optional<object>[] array = (from n in node.Elements()
				select StdLiteralParsing.TryParse(n, elementType, context)).ToArray<Optional<object>>();
			Array array2 = Array.CreateInstance(elementType, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].HasValue)
				{
					return Optional<object>.Nothing;
				}
				array2.SetValue(array[i].Value, i);
			}
			return array2.Some<object>();
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x00090820 File Offset: 0x0008EA20
		private static Optional<object> TryParseRecord(XElement node, Type expectedType, DeserializationContext context)
		{
			int? recordArity = expectedType.GetRecordArity();
			if (recordArity == null)
			{
				return Optional<object>.Nothing;
			}
			Type[] genericArguments = expectedType.GetGenericArguments();
			XElement[] array = node.Elements().ToArray<XElement>();
			object[] array2 = new object[recordArity.Value];
			for (int i = 0; i < recordArity.Value; i++)
			{
				Optional<object> optional = StdLiteralParsing.TryParse(array[i], genericArguments[i], context);
				if (!optional.HasValue)
				{
					return Optional<object>.Nothing;
				}
				array2[i] = optional.Value;
			}
			return RecordUtils.GetRecordCreator(genericArguments).Invoke(null, array2).Some<object>();
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000908B8 File Offset: 0x0008EAB8
		private static Optional<object> TryParseKeyValuePair(XElement node, Type expectedType, DeserializationContext context)
		{
			if (node.Elements("Key").Count<XElement>() != 1)
			{
				return Optional<object>.Nothing;
			}
			if (node.Elements("Value").Count<XElement>() != 1)
			{
				return Optional<object>.Nothing;
			}
			Optional<object> optional = StdLiteralParsing.TryParse(node.Element("Key"), expectedType.GenericTypeArguments[0], context);
			Optional<object> optional2 = StdLiteralParsing.TryParse(node.Element("Value"), expectedType.GenericTypeArguments[1], context);
			if (!optional.HasValue || !optional2.HasValue)
			{
				return Optional<object>.Nothing;
			}
			return expectedType.GetConstructor(expectedType.GenericTypeArguments).Invoke(new object[] { optional.Value, optional2.Value }).Some<object>();
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x00090987 File Offset: 0x0008EB87
		private static IDictionary<TKey, TValue> BuildDictionary<TKey, TValue>(IEnumerable<object> kvs)
		{
			return kvs.Cast<KeyValuePair<TKey, TValue>>().ToDictionary<TKey, TValue>();
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x00090994 File Offset: 0x0008EB94
		private static Optional<object> TryParseDictionary(XElement node, Type expectedType, DeserializationContext context)
		{
			Type elementType = typeof(KeyValuePair<, >).MakeGenericType(expectedType.GenericTypeArguments);
			IReadOnlyList<Optional<object>> readOnlyList = (from n in node.Elements()
				select StdLiteralParsing.TryParse(n, elementType, context)).ToList<Optional<object>>();
			foreach (Optional<object> optional in readOnlyList)
			{
				if (!optional.HasValue)
				{
					return Optional<object>.Nothing;
				}
			}
			MethodBase methodBase = typeof(StdLiteralParsing).GetMethod("BuildDictionary", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(expectedType.GenericTypeArguments);
			object obj = null;
			object[] array = new object[1];
			array[0] = readOnlyList.Select((Optional<object> el) => el.Value);
			return methodBase.Invoke(obj, array).Some<object>();
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x00090A8C File Offset: 0x0008EC8C
		private static Optional<object> _TryParseOptional<T>(XElement node, DeserializationContext context)
		{
			if (node.Name == "Nothing")
			{
				return Optional<T>.Nothing.Some<object>();
			}
			if (!(node.Name == "Some"))
			{
				return Optional<object>.Nothing;
			}
			Optional<object> optional = StdLiteralParsing.TryParse(node, typeof(T), context);
			if (!optional.HasValue)
			{
				return Optional<object>.Nothing;
			}
			return optional.Cast<T>().Some<object>();
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x00090B14 File Offset: 0x0008ED14
		private static Optional<object> TryParseOptional(XElement node, Type expectedType, DeserializationContext context)
		{
			return (Optional<object>)typeof(StdLiteralParsing).GetMethod("_TryParseOptional", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(expectedType.GenericTypeArguments).Invoke(null, new object[] { node, context });
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x00090B60 File Offset: 0x0008ED60
		private static Optional<object> _TryParseNullable<T>(XElement node, DeserializationContext context)
		{
			if (node.Value == "null")
			{
				return OptionalUtils.Some((T)null);
			}
			return StdLiteralParsing.TryParse(node, typeof(T), context);
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x00090B8C File Offset: 0x0008ED8C
		private static Optional<object> TryParseNullable(XElement node, Type expectedType, DeserializationContext context)
		{
			return (Optional<object>)typeof(StdLiteralParsing).GetMethod("_TryParseNullable", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(expectedType.GenericTypeArguments).Invoke(null, new object[] { node, context });
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x00090BD8 File Offset: 0x0008EDD8
		private static Optional<object> TryParseKnownType(string value, Type expectedType)
		{
			if (!expectedType.GetTypeInfo().IsValueType && value == "null")
			{
				return OptionalUtils.Some((T)null);
			}
			if (expectedType.GetTypeInfo().IsEnum)
			{
				return Enum.Parse(expectedType, value).SomeIfNotNull<object>();
			}
			return StdLiteralParsing.KnownTypeParsers.MaybeGet(expectedType).SelectMany((StdLiteralParsing.Parser parser) => parser(value, expectedType).SomeIfNotNull<object>());
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x00090C70 File Offset: 0x0008EE70
		private static object ParseAsBool(string value, Type expectedType)
		{
			if (value == "True" || value == "true")
			{
				return true;
			}
			if (!(value == "False") && !(value == "false"))
			{
				return null;
			}
			return false;
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x00090CC4 File Offset: 0x0008EEC4
		private static object ParseAsFloat(string value, Type expectedType)
		{
			float num;
			if (!float.TryParse(value.TrimEnd(new char[] { 'F' }), out num))
			{
				return null;
			}
			return num;
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x00090CF4 File Offset: 0x0008EEF4
		private static object ParseAsDouble(string value, Type expectedType)
		{
			double num;
			if (!double.TryParse(value, out num))
			{
				return null;
			}
			return num;
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x00090D14 File Offset: 0x0008EF14
		private static object ParseAsRegex(string value, Type expectedType)
		{
			if (value.Length < 2 || value[0] != '/' || value[value.Length - 1] != '/')
			{
				return null;
			}
			string text = StdLiteralParsing.CSharpLiteralToString(value.Slice(new int?(1), new int?(-1), 1));
			object obj;
			try
			{
				obj = new Regex(text, RegexOptions.ExplicitCapture | RegexOptions.Compiled);
			}
			catch
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x00090D84 File Offset: 0x0008EF84
		private static object ParseAsDateTime(string value, Type expectedType)
		{
			DateTime dateTime;
			if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dateTime))
			{
				return dateTime;
			}
			return null;
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x00090DB0 File Offset: 0x0008EFB0
		private static object ParseAsInt(string value, Type expectedType)
		{
			int num;
			if (!int.TryParse(value, out num))
			{
				return null;
			}
			return num;
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x00090DD0 File Offset: 0x0008EFD0
		private static object ParseAsUInt(string value, Type expectedType)
		{
			uint num;
			if (!uint.TryParse(value.TrimEnd(new char[] { 'U' }), out num))
			{
				return null;
			}
			return num;
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x00090E00 File Offset: 0x0008F000
		private static object ParseAsLong(string value, Type expectedType)
		{
			long num;
			if (!long.TryParse(value.TrimEnd(new char[] { 'L' }), out num))
			{
				return null;
			}
			return num;
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x00090E30 File Offset: 0x0008F030
		private static object ParseAsULong(string value, Type expectedType)
		{
			ulong num;
			if (!ulong.TryParse(value.TrimEnd(new char[] { 'U', 'L' }), out num))
			{
				return null;
			}
			return num;
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x00090E64 File Offset: 0x0008F064
		private static object ParseAsByte(string value, Type expectedType)
		{
			byte b;
			if (!byte.TryParse(value, out b))
			{
				return null;
			}
			return b;
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x00090E84 File Offset: 0x0008F084
		private static object ParseAsSByte(string value, Type expectedType)
		{
			sbyte b;
			if (!sbyte.TryParse(value, out b))
			{
				return null;
			}
			return b;
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x00090EA4 File Offset: 0x0008F0A4
		private static object ParseAsShort(string value, Type expectedType)
		{
			short num;
			if (!short.TryParse(value, out num))
			{
				return null;
			}
			return num;
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x00090EC4 File Offset: 0x0008F0C4
		private static object ParseAsUShort(string value, Type expectedType)
		{
			ushort num;
			if (!ushort.TryParse(value, out num))
			{
				return null;
			}
			return num;
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x00090EE4 File Offset: 0x0008F0E4
		private static object ParseAsDecimal(string value, Type expectedType)
		{
			decimal num;
			if ((value[value.Length - 1] != 'M' || !decimal.TryParse(value.Substring(0, value.Length - 1), out num)) && !decimal.TryParse(value, out num))
			{
				return null;
			}
			return num;
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x00090F30 File Offset: 0x0008F130
		private static object ParseAsString(string value, Type expectedType)
		{
			if (value.Length < 2)
			{
				return null;
			}
			if (value[0] == '@')
			{
				value = value.Substring(1);
			}
			if (value[0] != '"' || value[value.Length - 1] != '"')
			{
				return null;
			}
			return StdLiteralParsing.CSharpLiteralToString(value.Slice(new int?(1), new int?(-1), 1));
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x00090F94 File Offset: 0x0008F194
		private static object ParseAsChar(string value, Type expectedType)
		{
			if (value.Length < 3 || value[0] != '\'' || value[value.Length - 1] != '\'')
			{
				return null;
			}
			if (value.Length == 3)
			{
				return value[1];
			}
			string text = StdLiteralParsing.CSharpLiteralToString(value.Slice(new int?(1), new int?(-1), 1));
			if (text == null || text.Length != 1)
			{
				return null;
			}
			return text[0];
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x00091014 File Offset: 0x0008F214
		private static string CSharpLiteralToString(string literal)
		{
			StringBuilder stringBuilder = new StringBuilder(literal.Length);
			int i = 0;
			while (i < literal.Length)
			{
				char c = literal[i];
				if (c == '\\')
				{
					i++;
					if (i >= literal.Length)
					{
						return null;
					}
					char c2 = literal[i];
					if (c2 <= 'U')
					{
						if (c2 <= '\'')
						{
							if (c2 == '"')
							{
								c = '"';
								goto IL_02C5;
							}
							if (c2 != '\'')
							{
								goto IL_02C3;
							}
							c = '\'';
							goto IL_02C5;
						}
						else
						{
							if (c2 == '0')
							{
								c = '\0';
								goto IL_02C5;
							}
							if (c2 != 'U')
							{
								goto IL_02C3;
							}
						}
					}
					else if (c2 <= 'a')
					{
						if (c2 == '\\')
						{
							c = '\\';
							goto IL_02C5;
						}
						if (c2 != 'a')
						{
							goto IL_02C3;
						}
						c = '\a';
						goto IL_02C5;
					}
					else
					{
						if (c2 == 'b')
						{
							c = '\b';
							goto IL_02C5;
						}
						if (c2 == 'f')
						{
							c = '\f';
							goto IL_02C5;
						}
						switch (c2)
						{
						case 'n':
							c = '\n';
							goto IL_02C5;
						case 'o':
						case 'p':
						case 'q':
						case 's':
						case 'w':
							goto IL_02C3;
						case 'r':
							c = '\r';
							goto IL_02C5;
						case 't':
							c = '\t';
							goto IL_02C5;
						case 'u':
							i++;
							if (i + 3 >= literal.Length)
							{
								return null;
							}
							try
							{
								c = (char)uint.Parse(literal.Substring(i, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
								i += 3;
								goto IL_02C5;
							}
							catch
							{
								return null;
							}
							break;
						case 'v':
							c = '\v';
							goto IL_02C5;
						case 'x':
						{
							StringBuilder stringBuilder2 = new StringBuilder(10);
							i++;
							if (i >= literal.Length)
							{
								return null;
							}
							c = literal[i];
							if (char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
							{
								stringBuilder2.Append(c);
								i++;
								if (i < literal.Length)
								{
									c = literal[i];
									if (char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
									{
										stringBuilder2.Append(c);
										i++;
										if (i < literal.Length)
										{
											c = literal[i];
											if (char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
											{
												stringBuilder2.Append(c);
												i++;
												if (i < literal.Length)
												{
													c = literal[i];
													if (char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
													{
														stringBuilder2.Append(c);
														i++;
													}
												}
											}
										}
									}
								}
							}
							c = (char)int.Parse(stringBuilder2.ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
							i--;
							goto IL_02C5;
						}
						default:
							goto IL_02C3;
						}
					}
					i++;
					if (i + 7 >= literal.Length)
					{
						return null;
					}
					try
					{
						uint num = uint.Parse(literal.Substring(i, 8), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
						if (num > 65535U)
						{
							return null;
						}
						c = (char)num;
						i += 7;
						goto IL_02C5;
					}
					catch
					{
						return null;
					}
					IL_02C3:
					return null;
				}
				IL_02C5:
				i++;
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x00091324 File Offset: 0x0008F524
		// Note: this type is marked as 'beforefieldinit'.
		static StdLiteralParsing()
		{
			Dictionary<Type, StdLiteralParsing.Parser> dictionary = new Dictionary<Type, StdLiteralParsing.Parser>();
			Type typeFromHandle = typeof(int);
			dictionary[typeFromHandle] = new StdLiteralParsing.Parser(StdLiteralParsing.ParseAsInt);
			Type typeFromHandle2 = typeof(uint);
			dictionary[typeFromHandle2] = new StdLiteralParsing.Parser(StdLiteralParsing.ParseAsUInt);
			Type typeFromHandle3 = typeof(long);
			dictionary[typeFromHandle3] = new StdLiteralParsing.Parser(StdLiteralParsing.ParseAsLong);
			Type typeFromHandle4 = typeof(ulong);
			dictionary[typeFromHandle4] = new StdLiteralParsing.Parser(StdLiteralParsing.ParseAsULong);
			Type typeFromHandle5 = typeof(byte);
			dictionary[typeFromHandle5] = new StdLiteralParsing.Parser(StdLiteralParsing.ParseAsByte);
			Type typeFromHandle6 = typeof(sbyte);
			dictionary[typeFromHandle6] = new StdLiteralParsing.Parser(StdLiteralParsing.ParseAsSByte);
			Type typeFromHandle7 = typeof(short);
			dictionary[typeFromHandle7] = new StdLiteralParsing.Parser(StdLiteralParsing.ParseAsShort);
			Type typeFromHandle8 = typeof(ushort);
			dictionary[typeFromHandle8] = new StdLiteralParsing.Parser(StdLiteralParsing.ParseAsUShort);
			Type typeFromHandle9 = typeof(decimal);
			dictionary[typeFromHandle9] = new StdLiteralParsing.Parser(StdLiteralParsing.ParseAsDecimal);
			Type typeFromHandle10 = typeof(bool);
			dictionary[typeFromHandle10] = new StdLiteralParsing.Parser(StdLiteralParsing.ParseAsBool);
			Type typeFromHandle11 = typeof(float);
			dictionary[typeFromHandle11] = new StdLiteralParsing.Parser(StdLiteralParsing.ParseAsFloat);
			Type typeFromHandle12 = typeof(double);
			dictionary[typeFromHandle12] = new StdLiteralParsing.Parser(StdLiteralParsing.ParseAsDouble);
			Type typeFromHandle13 = typeof(string);
			dictionary[typeFromHandle13] = new StdLiteralParsing.Parser(StdLiteralParsing.ParseAsString);
			Type typeFromHandle14 = typeof(char);
			dictionary[typeFromHandle14] = new StdLiteralParsing.Parser(StdLiteralParsing.ParseAsChar);
			Type typeFromHandle15 = typeof(Regex);
			dictionary[typeFromHandle15] = new StdLiteralParsing.Parser(StdLiteralParsing.ParseAsRegex);
			Type typeFromHandle16 = typeof(DateTime);
			dictionary[typeFromHandle16] = new StdLiteralParsing.Parser(StdLiteralParsing.ParseAsDateTime);
			StdLiteralParsing.KnownTypeParsers = dictionary;
		}

		// Token: 0x04001887 RID: 6279
		private static readonly Dictionary<Type, StdLiteralParsing.Parser> KnownTypeParsers;

		// Token: 0x020008E2 RID: 2274
		// (Invoke) Token: 0x06003134 RID: 12596
		private delegate object Parser(string value, Type expectedType);
	}
}
