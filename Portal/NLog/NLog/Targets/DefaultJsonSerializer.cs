using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NLog.Internal;

namespace NLog.Targets
{
	// Token: 0x02000037 RID: 55
	public class DefaultJsonSerializer : IJsonConverter, IJsonSerializer
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		public static DefaultJsonSerializer Instance
		{
			get
			{
				return DefaultJsonSerializer.instance;
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		private DefaultJsonSerializer()
		{
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000CAC2 File Offset: 0x0000ACC2
		public string SerializeObject(object value)
		{
			return this.SerializeObject(value, this._serializeOptions);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0000CAD4 File Offset: 0x0000ACD4
		public string SerializeObject(object value, JsonSerializeOptions options)
		{
			if (value == null)
			{
				return "null";
			}
			string text;
			if ((text = value as string) != null)
			{
				for (int i = 0; i < text.Length; i++)
				{
					if (DefaultJsonSerializer.RequiresJsonEscape(text[i], options.EscapeUnicode))
					{
						StringBuilder stringBuilder = new StringBuilder(text.Length + 4);
						stringBuilder.Append('"');
						DefaultJsonSerializer.AppendStringEscape(stringBuilder, text, options.EscapeUnicode);
						stringBuilder.Append('"');
						return stringBuilder.ToString();
					}
				}
				return DefaultJsonSerializer.QuoteValue(text);
			}
			IConvertible convertible = value as IConvertible;
			TypeCode typeCode = ((convertible != null) ? convertible.GetTypeCode() : TypeCode.Object);
			if (typeCode != TypeCode.Object && typeCode != TypeCode.Char && StringHelpers.IsNullOrWhiteSpace(options.Format) && options.FormatProvider == null)
			{
				Enum @enum;
				if (!options.EnumAsInteger && DefaultJsonSerializer.IsNumericTypeCode(typeCode, false) && (@enum = value as Enum) != null)
				{
					return DefaultJsonSerializer.QuoteValue(this.EnumAsString(@enum));
				}
				string text2 = XmlHelper.XmlConvertToString(convertible, typeCode, false);
				if (DefaultJsonSerializer.SkipQuotes(convertible, typeCode))
				{
					return text2;
				}
				return DefaultJsonSerializer.QuoteValue(text2);
			}
			else
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				if (!this.SerializeObject(value, stringBuilder2, options))
				{
					return null;
				}
				return stringBuilder2.ToString();
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000CBE7 File Offset: 0x0000ADE7
		public bool SerializeObject(object value, StringBuilder destination)
		{
			return this.SerializeObject(value, destination, this._serializeOptions);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0000CBF8 File Offset: 0x0000ADF8
		public bool SerializeObject(object value, StringBuilder destination, JsonSerializeOptions options)
		{
			return this.SerializeObject(value, destination, options, default(SingleItemOptimizedHashSet<object>), 0);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000CC18 File Offset: 0x0000AE18
		private bool SerializeObject(object value, StringBuilder destination, JsonSerializeOptions options, SingleItemOptimizedHashSet<object> objectsInPath, int depth)
		{
			int length = destination.Length;
			bool flag;
			try
			{
				if (this.SerializeSimpleObjectValue(value, destination, options, false))
				{
					flag = true;
				}
				else
				{
					flag = this.SerializeObjectWithReflection(value, destination, options, ref objectsInPath, depth);
				}
			}
			catch
			{
				destination.Length = length;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000CC6C File Offset: 0x0000AE6C
		private bool SerializeObjectWithReflection(object value, StringBuilder destination, JsonSerializeOptions options, ref SingleItemOptimizedHashSet<object> objectsInPath, int depth)
		{
			if (destination.Length > 524288)
			{
				return false;
			}
			if (objectsInPath.Contains(value))
			{
				return false;
			}
			IDictionary dictionary;
			if ((dictionary = value as IDictionary) != null)
			{
				using (DefaultJsonSerializer.StartCollectionScope(ref objectsInPath, dictionary))
				{
					this.SerializeDictionaryObject(dictionary, destination, options, objectsInPath, depth);
					return true;
				}
			}
			IEnumerable enumerable;
			if ((enumerable = value as IEnumerable) != null)
			{
				ObjectReflectionCache.ObjectPropertyList objectPropertyList;
				if (this._objectReflectionCache.TryLookupExpandoObject(value, out objectPropertyList))
				{
					if (objectPropertyList.ConvertToString || depth >= options.MaxRecursionLimit)
					{
						return this.SerializeObjectAsString(value, destination, options);
					}
					using (new SingleItemOptimizedHashSet<object>.SingleItemScopedInsert(value, ref objectsInPath, false, DefaultJsonSerializer._referenceEqualsComparer))
					{
						return this.SerializeObjectProperties(objectPropertyList, destination, options, objectsInPath, depth);
					}
				}
				using (DefaultJsonSerializer.StartCollectionScope(ref objectsInPath, value))
				{
					this.SerializeCollectionObject(enumerable, destination, options, objectsInPath, depth);
					return true;
				}
			}
			return this.SerializeObjectWithProperties(value, destination, options, ref objectsInPath, depth);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000CDA0 File Offset: 0x0000AFA0
		private bool SerializeSimpleObjectValue(object value, StringBuilder destination, JsonSerializeOptions options, bool forceToString = false)
		{
			IConvertible convertible = value as IConvertible;
			TypeCode typeCode = ((value == null) ? TypeCode.Empty : ((convertible != null) ? convertible.GetTypeCode() : TypeCode.Object));
			if (typeCode != TypeCode.Object)
			{
				this.SerializeSimpleTypeCodeValue(convertible, typeCode, destination, options, forceToString);
				return true;
			}
			if (value is DateTimeOffset)
			{
				DefaultJsonSerializer.QuoteValue(destination, string.Format("{0:yyyy-MM-dd HH:mm:ss zzz}", value));
				return true;
			}
			return false;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000CDF5 File Offset: 0x0000AFF5
		private static SingleItemOptimizedHashSet<object>.SingleItemScopedInsert StartCollectionScope(ref SingleItemOptimizedHashSet<object> objectsInPath, object value)
		{
			return new SingleItemOptimizedHashSet<object>.SingleItemScopedInsert(value, ref objectsInPath, true, DefaultJsonSerializer._referenceEqualsComparer);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000CE04 File Offset: 0x0000B004
		private void SerializeWithFormatProvider(IFormattable formattable, bool includeQuotes, StringBuilder destination, JsonSerializeOptions options, bool hasFormat)
		{
			if (includeQuotes)
			{
				destination.Append('"');
			}
			IFormatProvider formatProvider = options.FormatProvider ?? (hasFormat ? this._defaultFormatProvider : null);
			string text = formattable.ToString(hasFormat ? options.Format : "", formatProvider);
			if (includeQuotes)
			{
				DefaultJsonSerializer.AppendStringEscape(destination, text, options.EscapeUnicode);
			}
			else
			{
				destination.Append(text);
			}
			if (includeQuotes)
			{
				destination.Append('"');
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000CE78 File Offset: 0x0000B078
		private void SerializeDictionaryObject(IDictionary dictionary, StringBuilder destination, JsonSerializeOptions options, SingleItemOptimizedHashSet<object> objectsInPath, int depth)
		{
			bool flag = true;
			int num = ((objectsInPath.Count <= 1) ? depth : (depth + 1));
			if (num > options.MaxRecursionLimit)
			{
				destination.Append("{}");
				return;
			}
			destination.Append('{');
			foreach (DictionaryEntry dictionaryEntry in new DictionaryEntryEnumerable(dictionary))
			{
				int length = destination.Length;
				if (length > 524288)
				{
					break;
				}
				if (!flag)
				{
					destination.Append(',');
				}
				object key = dictionaryEntry.Key;
				if (options.QuoteKeys)
				{
					if (!this.SerializeObjectAsString(key, destination, options))
					{
						destination.Length = length;
						continue;
					}
				}
				else if (!this.SerializeObject(key, destination, options, objectsInPath, num))
				{
					destination.Length = length;
					continue;
				}
				if (options.SanitizeDictionaryKeys)
				{
					int num2 = (options.QuoteKeys ? 1 : 0);
					int num3 = destination.Length - num2;
					int num4 = length + (flag ? 0 : 1) + num2;
					if (!DefaultJsonSerializer.SanitizeDictionaryKey(destination, num4, num3 - num4))
					{
						destination.Length = length;
						continue;
					}
				}
				destination.Append(':');
				object value = dictionaryEntry.Value;
				if (!this.SerializeObject(value, destination, options, objectsInPath, num))
				{
					destination.Length = length;
				}
				else
				{
					flag = false;
				}
			}
			destination.Append('}');
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000CFE8 File Offset: 0x0000B1E8
		private static bool SanitizeDictionaryKey(StringBuilder destination, int keyStartIndex, int keyLength)
		{
			if (keyLength == 0)
			{
				return false;
			}
			int num = keyStartIndex + keyLength;
			for (int i = keyStartIndex; i < num; i++)
			{
				char c = destination[i];
				if (c != '_' && !char.IsLetterOrDigit(c))
				{
					destination[i] = '_';
				}
			}
			return true;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000D02C File Offset: 0x0000B22C
		private void SerializeCollectionObject(IEnumerable value, StringBuilder destination, JsonSerializeOptions options, SingleItemOptimizedHashSet<object> objectsInPath, int depth)
		{
			bool flag = true;
			int num = ((objectsInPath.Count <= 1) ? depth : (depth + 1));
			if (num > options.MaxRecursionLimit)
			{
				destination.Append("[]");
				return;
			}
			destination.Append('[');
			foreach (object obj in value)
			{
				int length = destination.Length;
				if (length > 524288)
				{
					break;
				}
				if (!flag)
				{
					destination.Append(',');
				}
				if (!this.SerializeObject(obj, destination, options, objectsInPath, num))
				{
					destination.Length = length;
				}
				else
				{
					flag = false;
				}
			}
			destination.Append(']');
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000D0EC File Offset: 0x0000B2EC
		private bool SerializeObjectWithProperties(object value, StringBuilder destination, JsonSerializeOptions options, ref SingleItemOptimizedHashSet<object> objectsInPath, int depth)
		{
			if (depth < options.MaxRecursionLimit)
			{
				ObjectReflectionCache.ObjectPropertyList objectPropertyList = this._objectReflectionCache.LookupObjectProperties(value);
				if (!objectPropertyList.ConvertToString)
				{
					if (options == DefaultJsonSerializer.instance._serializeOptions && value is Exception)
					{
						options = DefaultJsonSerializer.instance._exceptionSerializeOptions;
					}
					using (new SingleItemOptimizedHashSet<object>.SingleItemScopedInsert(value, ref objectsInPath, false, DefaultJsonSerializer._referenceEqualsComparer))
					{
						return this.SerializeObjectProperties(objectPropertyList, destination, options, objectsInPath, depth);
					}
				}
			}
			return this.SerializeObjectAsString(value, destination, options);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000D188 File Offset: 0x0000B388
		private void SerializeSimpleTypeCodeValue(IConvertible value, TypeCode objTypeCode, StringBuilder destination, JsonSerializeOptions options, bool forceToString = false)
		{
			if (value == null)
			{
				destination.Append(forceToString ? "\"\"" : "null");
				return;
			}
			if (objTypeCode == TypeCode.String || objTypeCode == TypeCode.Char)
			{
				destination.Append('"');
				DefaultJsonSerializer.AppendStringEscape(destination, value.ToString(), options.EscapeUnicode);
				destination.Append('"');
				return;
			}
			bool flag = !StringHelpers.IsNullOrWhiteSpace(options.Format);
			IFormattable formattable;
			if ((options.FormatProvider != null || flag) && (formattable = value as IFormattable) != null)
			{
				bool flag2 = forceToString || objTypeCode == TypeCode.Object || !DefaultJsonSerializer.SkipQuotes(value, objTypeCode);
				this.SerializeWithFormatProvider(formattable, flag2, destination, options, flag);
				return;
			}
			if (DefaultJsonSerializer.IsNumericTypeCode(objTypeCode, false))
			{
				this.SerializeSimpleNumericValue(value, objTypeCode, destination, options, forceToString);
				return;
			}
			string text = XmlHelper.XmlConvertToString(value, objTypeCode, false);
			if (!forceToString && text != null && DefaultJsonSerializer.SkipQuotes(value, objTypeCode))
			{
				destination.Append(text);
				return;
			}
			DefaultJsonSerializer.QuoteValue(destination, text);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0000D26C File Offset: 0x0000B46C
		private void SerializeSimpleNumericValue(IConvertible value, TypeCode objTypeCode, StringBuilder destination, JsonSerializeOptions options, bool forceToString)
		{
			Enum @enum;
			if (!options.EnumAsInteger && (@enum = value as Enum) != null)
			{
				DefaultJsonSerializer.QuoteValue(destination, this.EnumAsString(@enum));
				return;
			}
			if (forceToString)
			{
				destination.Append('"');
			}
			destination.AppendIntegerAsString(value, objTypeCode);
			if (forceToString)
			{
				destination.Append('"');
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0000D2BC File Offset: 0x0000B4BC
		private static CultureInfo CreateFormatProvider()
		{
			CultureInfo cultureInfo = new CultureInfo("en-US", false);
			NumberFormatInfo numberFormat = cultureInfo.NumberFormat;
			numberFormat.NumberGroupSeparator = string.Empty;
			numberFormat.NumberDecimalSeparator = ".";
			numberFormat.NumberGroupSizes = new int[1];
			return cultureInfo;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0000D2FD File Offset: 0x0000B4FD
		private static string QuoteValue(string value)
		{
			return "\"" + value + "\"";
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0000D30F File Offset: 0x0000B50F
		private static void QuoteValue(StringBuilder destination, string value)
		{
			destination.Append('"');
			destination.Append(value);
			destination.Append('"');
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000D32C File Offset: 0x0000B52C
		private string EnumAsString(Enum value)
		{
			string text;
			if (!this._enumCache.TryGetValue(value, out text))
			{
				text = Convert.ToString(value, CultureInfo.InvariantCulture);
				this._enumCache.TryAddValue(value, text);
			}
			return text;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0000D364 File Offset: 0x0000B564
		private static bool SkipQuotes(IConvertible value, TypeCode objTypeCode)
		{
			switch (objTypeCode)
			{
			case TypeCode.Empty:
				return true;
			case TypeCode.Object:
			case TypeCode.DBNull:
				break;
			case TypeCode.Boolean:
				return true;
			case TypeCode.Char:
				return false;
			default:
				switch (objTypeCode)
				{
				case TypeCode.Single:
				{
					float num = value.ToSingle(CultureInfo.InvariantCulture);
					return !float.IsNaN(num) && !float.IsInfinity(num);
				}
				case TypeCode.Double:
				{
					double num2 = value.ToDouble(CultureInfo.InvariantCulture);
					return !double.IsNaN(num2) && !double.IsInfinity(num2);
				}
				case TypeCode.Decimal:
					return true;
				case TypeCode.DateTime:
					return false;
				case TypeCode.String:
					return false;
				}
				break;
			}
			return DefaultJsonSerializer.IsNumericTypeCode(objTypeCode, false);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000D401 File Offset: 0x0000B601
		private static bool IsNumericTypeCode(TypeCode objTypeCode, bool includeDecimals)
		{
			return objTypeCode - TypeCode.SByte <= 7 || (objTypeCode - TypeCode.Single <= 2 && includeDecimals);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000D418 File Offset: 0x0000B618
		internal static void AppendStringEscape(StringBuilder destination, string text, bool escapeUnicode)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			StringBuilder stringBuilder = null;
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				if (!DefaultJsonSerializer.RequiresJsonEscape(c, escapeUnicode))
				{
					if (stringBuilder != null)
					{
						stringBuilder.Append(c);
					}
				}
				else
				{
					if (stringBuilder == null)
					{
						stringBuilder = destination;
						stringBuilder.Append(text, 0, i);
					}
					if (c <= '"')
					{
						switch (c)
						{
						case '\b':
							stringBuilder.Append("\\b");
							goto IL_0129;
						case '\t':
							stringBuilder.Append("\\t");
							goto IL_0129;
						case '\n':
							stringBuilder.Append("\\n");
							goto IL_0129;
						case '\v':
							break;
						case '\f':
							stringBuilder.Append("\\f");
							goto IL_0129;
						case '\r':
							stringBuilder.Append("\\r");
							goto IL_0129;
						default:
							if (c == '"')
							{
								stringBuilder.Append("\\\"");
								goto IL_0129;
							}
							break;
						}
					}
					else
					{
						if (c == '/')
						{
							stringBuilder.Append("\\/");
							goto IL_0129;
						}
						if (c == '\\')
						{
							stringBuilder.Append("\\\\");
							goto IL_0129;
						}
					}
					if (DefaultJsonSerializer.EscapeChar(c, escapeUnicode))
					{
						stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "\\u{0:x4}", new object[] { (int)c });
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				IL_0129:;
			}
			if (stringBuilder == null)
			{
				destination.Append(text);
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000D569 File Offset: 0x0000B769
		internal static bool RequiresJsonEscape(char ch, bool escapeUnicode)
		{
			return DefaultJsonSerializer.EscapeChar(ch, escapeUnicode) || (ch == '"' || ch == '/' || ch == '\\');
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000D588 File Offset: 0x0000B788
		private static bool EscapeChar(char ch, bool escapeUnicode)
		{
			return ch < ' ' || (escapeUnicode && ch > '\u007f');
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0000D59C File Offset: 0x0000B79C
		private bool SerializeObjectProperties(ObjectReflectionCache.ObjectPropertyList objectPropertyList, StringBuilder destination, JsonSerializeOptions options, SingleItemOptimizedHashSet<object> objectsInPath, int depth)
		{
			destination.Append('{');
			bool flag = true;
			foreach (ObjectReflectionCache.ObjectPropertyList.PropertyValue propertyValue in objectPropertyList)
			{
				int length = destination.Length;
				try
				{
					if (DefaultJsonSerializer.HasNameAndValue(propertyValue))
					{
						if (!flag)
						{
							destination.Append(", ");
						}
						if (options.QuoteKeys)
						{
							DefaultJsonSerializer.QuoteValue(destination, propertyValue.Name);
						}
						else
						{
							destination.Append(propertyValue.Name);
						}
						destination.Append(':');
						TypeCode typeCode = propertyValue.TypeCode;
						if (typeCode != TypeCode.Object)
						{
							this.SerializeSimpleTypeCodeValue((IConvertible)propertyValue.Value, typeCode, destination, options, false);
							flag = false;
						}
						else if (!this.SerializeObject(propertyValue.Value, destination, options, objectsInPath, depth + 1))
						{
							destination.Length = length;
						}
						else
						{
							flag = false;
						}
					}
				}
				catch
				{
					destination.Length = length;
				}
			}
			destination.Append('}');
			return true;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000D6B0 File Offset: 0x0000B8B0
		private static bool HasNameAndValue(ObjectReflectionCache.ObjectPropertyList.PropertyValue propertyValue)
		{
			return propertyValue.Name != null && propertyValue.Value != null;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0000D6C8 File Offset: 0x0000B8C8
		private bool SerializeObjectAsString(object value, StringBuilder destination, JsonSerializeOptions options)
		{
			int length = destination.Length;
			bool flag;
			try
			{
				if (this.SerializeSimpleObjectValue(value, destination, options, true))
				{
					flag = true;
				}
				else
				{
					bool flag2 = !StringHelpers.IsNullOrWhiteSpace(options.Format);
					IFormattable formattable;
					if ((options.FormatProvider != null || flag2) && (formattable = value as IFormattable) != null)
					{
						this.SerializeWithFormatProvider(formattable, true, destination, options, flag2);
						flag = true;
					}
					else
					{
						string text = Convert.ToString(value, CultureInfo.InvariantCulture);
						destination.Append('"');
						DefaultJsonSerializer.AppendStringEscape(destination, text, options.EscapeUnicode);
						destination.Append('"');
						flag = true;
					}
				}
			}
			catch
			{
				destination.Length = length;
				flag = false;
			}
			return flag;
		}

		// Token: 0x040000C0 RID: 192
		private readonly ObjectReflectionCache _objectReflectionCache = new ObjectReflectionCache();

		// Token: 0x040000C1 RID: 193
		private readonly MruCache<Enum, string> _enumCache = new MruCache<Enum, string>(1500);

		// Token: 0x040000C2 RID: 194
		private readonly JsonSerializeOptions _serializeOptions = new JsonSerializeOptions();

		// Token: 0x040000C3 RID: 195
		private readonly JsonSerializeOptions _exceptionSerializeOptions = new JsonSerializeOptions
		{
			SanitizeDictionaryKeys = true
		};

		// Token: 0x040000C4 RID: 196
		private readonly IFormatProvider _defaultFormatProvider = DefaultJsonSerializer.CreateFormatProvider();

		// Token: 0x040000C5 RID: 197
		private const int MaxJsonLength = 524288;

		// Token: 0x040000C6 RID: 198
		private static readonly DefaultJsonSerializer instance = new DefaultJsonSerializer();

		// Token: 0x040000C7 RID: 199
		private static readonly IEqualityComparer<object> _referenceEqualsComparer = SingleItemOptimizedHashSet<object>.ReferenceEqualityComparer.Default;
	}
}
