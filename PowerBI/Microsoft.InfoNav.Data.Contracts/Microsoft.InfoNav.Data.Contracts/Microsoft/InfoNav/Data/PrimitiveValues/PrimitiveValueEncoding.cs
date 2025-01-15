using System;
using System.Xml;

namespace Microsoft.InfoNav.Data.PrimitiveValues
{
	// Token: 0x0200006F RID: 111
	public static class PrimitiveValueEncoding
	{
		// Token: 0x06000229 RID: 553 RVA: 0x000067B8 File Offset: 0x000049B8
		public static string ToTypeEncodedString(object value)
		{
			return PrimitiveValueEncoding.ToString(value, true);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000067C1 File Offset: 0x000049C1
		public static string ToTypeEncodedString(PrimitiveValue value)
		{
			return PrimitiveValueEncoding.ToString(value, true);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000067CA File Offset: 0x000049CA
		internal static string ToSimpleEncodedString(object value)
		{
			return PrimitiveValueEncoding.ToString(value, false);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000067D3 File Offset: 0x000049D3
		internal static string ToSimpleEncodedString(PrimitiveValue value)
		{
			return PrimitiveValueEncoding.ToString(value, false);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000067DC File Offset: 0x000049DC
		private static string ToString(object value, bool isTypeEncodedString)
		{
			if (value == null)
			{
				return "null";
			}
			TypeCode typeCode = Type.GetTypeCode(value.GetType());
			if (typeCode != TypeCode.Object)
			{
				if (typeCode != TypeCode.Boolean)
				{
					switch (typeCode)
					{
					case TypeCode.Int64:
						if (!isTypeEncodedString)
						{
							return PrimitiveValueEncoding.InternalToSimpleEncodedString((long)value);
						}
						return PrimitiveValueEncoding.InternalToTypeEncodedString((long)value);
					case TypeCode.Double:
						if (!isTypeEncodedString)
						{
							return PrimitiveValueEncoding.InternalToSimpleEncodedString((double)value);
						}
						return PrimitiveValueEncoding.InternalToTypeEncodedString((double)value);
					case TypeCode.Decimal:
						if (!isTypeEncodedString)
						{
							return PrimitiveValueEncoding.InternalToSimpleEncodedString((decimal)value);
						}
						return PrimitiveValueEncoding.InternalToTypeEncodedString((decimal)value);
					case TypeCode.DateTime:
						if (!isTypeEncodedString)
						{
							return PrimitiveValueEncoding.InternalToSimpleEncodedString((DateTime)value);
						}
						return PrimitiveValueEncoding.InternalToTypeEncodedString((DateTime)value);
					case TypeCode.String:
						if (!isTypeEncodedString)
						{
							return PrimitiveValueEncoding.InternalToSimpleEncodedString((string)value);
						}
						return PrimitiveValueEncoding.InternalToTypeEncodedString((string)value);
					}
				}
				else
				{
					if (!isTypeEncodedString)
					{
						return PrimitiveValueEncoding.InternalToSimpleEncodedString((bool)value);
					}
					return PrimitiveValueEncoding.InternalToTypeEncodedString((bool)value);
				}
			}
			else
			{
				byte[] array = value as byte[];
				if (array != null)
				{
					if (!isTypeEncodedString)
					{
						return PrimitiveValueEncoding.InternalToSimpleEncodedString(array);
					}
					return PrimitiveValueEncoding.InternalToTypeEncodedString(array);
				}
			}
			throw Contract.ExceptNotSupported(StringUtil.FormatInvariant("Primitive value encoding for {0} is not currently supported.", value));
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00006904 File Offset: 0x00004B04
		private static string ToString(PrimitiveValue value, bool isTypeEncodedString)
		{
			switch (value.Type)
			{
			case ConceptualPrimitiveType.Null:
				return "null";
			case ConceptualPrimitiveType.Text:
				if (!isTypeEncodedString)
				{
					return PrimitiveValueEncoding.InternalToSimpleEncodedString(((TextPrimitiveValue)value).Value);
				}
				return PrimitiveValueEncoding.InternalToTypeEncodedString(((TextPrimitiveValue)value).Value);
			case ConceptualPrimitiveType.Decimal:
				if (!isTypeEncodedString)
				{
					return PrimitiveValueEncoding.InternalToSimpleEncodedString(((DecimalPrimitiveValue)value).Value);
				}
				return PrimitiveValueEncoding.InternalToTypeEncodedString(((DecimalPrimitiveValue)value).Value);
			case ConceptualPrimitiveType.Double:
				if (!isTypeEncodedString)
				{
					return PrimitiveValueEncoding.InternalToSimpleEncodedString(((DoublePrimitiveValue)value).Value);
				}
				return PrimitiveValueEncoding.InternalToTypeEncodedString(((DoublePrimitiveValue)value).Value);
			case ConceptualPrimitiveType.Integer:
				if (!isTypeEncodedString)
				{
					return PrimitiveValueEncoding.InternalToSimpleEncodedString(((IntegerPrimitiveValue)value).Value);
				}
				return PrimitiveValueEncoding.InternalToTypeEncodedString(((IntegerPrimitiveValue)value).Value);
			case ConceptualPrimitiveType.Boolean:
				if (!isTypeEncodedString)
				{
					return PrimitiveValueEncoding.InternalToSimpleEncodedString(((BooleanPrimitiveValue)value).Value);
				}
				return PrimitiveValueEncoding.InternalToTypeEncodedString(((BooleanPrimitiveValue)value).Value);
			case ConceptualPrimitiveType.DateTime:
				if (!isTypeEncodedString)
				{
					return PrimitiveValueEncoding.InternalToSimpleEncodedString(((DateTimePrimitiveValue)value).Value);
				}
				return PrimitiveValueEncoding.InternalToTypeEncodedString(((DateTimePrimitiveValue)value).Value);
			case ConceptualPrimitiveType.Binary:
				if (!isTypeEncodedString)
				{
					return PrimitiveValueEncoding.InternalToSimpleEncodedString(((BinaryPrimitiveValue)value).Value);
				}
				return PrimitiveValueEncoding.InternalToTypeEncodedString(((BinaryPrimitiveValue)value).Value);
			}
			throw Contract.ExceptNotSupported(StringUtil.FormatInvariant("Primitive value encoding for {0} is not currently supported.", value.Type));
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00006A76 File Offset: 0x00004C76
		private static string InternalToTypeEncodedString(decimal value)
		{
			return XmlConvert.ToString(value) + "M";
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00006A88 File Offset: 0x00004C88
		private static string InternalToSimpleEncodedString(decimal value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00006A90 File Offset: 0x00004C90
		private static string InternalToTypeEncodedString(string value)
		{
			return "'" + value.Replace("'", "''") + "'";
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006AB1 File Offset: 0x00004CB1
		private static string InternalToSimpleEncodedString(string value)
		{
			return value;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00006AB4 File Offset: 0x00004CB4
		private static string InternalToTypeEncodedString(double value)
		{
			return XmlConvert.ToString(value) + "D";
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00006AC6 File Offset: 0x00004CC6
		private static string InternalToSimpleEncodedString(double value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006ACE File Offset: 0x00004CCE
		private static string InternalToTypeEncodedString(long value)
		{
			return XmlConvert.ToString(value) + "L";
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00006AE0 File Offset: 0x00004CE0
		private static string InternalToSimpleEncodedString(long value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006AE8 File Offset: 0x00004CE8
		private static string InternalToTypeEncodedString(bool value)
		{
			if (!value)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00006AF8 File Offset: 0x00004CF8
		private static string InternalToSimpleEncodedString(bool value)
		{
			if (!value)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00006B08 File Offset: 0x00004D08
		private static string InternalToTypeEncodedString(DateTime value)
		{
			return "datetime'" + XmlConvert.ToString(value, XmlDateTimeSerializationMode.Unspecified) + "'";
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00006B20 File Offset: 0x00004D20
		private static string InternalToSimpleEncodedString(DateTime value)
		{
			return XmlConvert.ToString(value, XmlDateTimeSerializationMode.Unspecified);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00006B29 File Offset: 0x00004D29
		private static string InternalToTypeEncodedString(byte[] value)
		{
			return "base64'" + Convert.ToBase64String(value) + "'";
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00006B40 File Offset: 0x00004D40
		private static string InternalToSimpleEncodedString(byte[] value)
		{
			return Convert.ToBase64String(value);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00006B48 File Offset: 0x00004D48
		public static bool TryParseTypeEncodedString(string text, out PrimitiveValue primitiveValue)
		{
			try
			{
				if (text == null || text.Equals("null", StringComparison.OrdinalIgnoreCase))
				{
					primitiveValue = PrimitiveValue.Null;
					return true;
				}
				if (PrimitiveValueEncoding.TryRemoveQuotes(ref text))
				{
					primitiveValue = text;
					return true;
				}
				if (PrimitiveValueEncoding.TryRemoveLiteralSuffix("M", ref text))
				{
					primitiveValue = XmlConvert.ToDecimal(text);
					return true;
				}
				if (PrimitiveValueEncoding.TryRemoveLiteralSuffix("D", ref text))
				{
					primitiveValue = XmlConvert.ToDouble(text);
					return true;
				}
				if (PrimitiveValueEncoding.TryRemoveLiteralSuffix("L", ref text))
				{
					primitiveValue = XmlConvert.ToInt64(text);
					return true;
				}
				if (text.Equals("true", StringComparison.OrdinalIgnoreCase))
				{
					primitiveValue = BooleanPrimitiveValue.True;
					return true;
				}
				if (text.Equals("false", StringComparison.OrdinalIgnoreCase))
				{
					primitiveValue = BooleanPrimitiveValue.False;
					return true;
				}
				if (PrimitiveValueEncoding.TryRemoveLiteralPrefix("datetime", ref text) && PrimitiveValueEncoding.TryRemoveQuotes(ref text))
				{
					primitiveValue = XmlConvert.ToDateTime(text, XmlDateTimeSerializationMode.RoundtripKind);
					return true;
				}
				if (PrimitiveValueEncoding.TryRemoveLiteralPrefix("base64", ref text) && PrimitiveValueEncoding.TryRemoveQuotes(ref text))
				{
					primitiveValue = Convert.FromBase64String(text);
					return true;
				}
			}
			catch (Exception ex) when (!ex.IsStoppingException())
			{
			}
			primitiveValue = null;
			return false;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00006CB8 File Offset: 0x00004EB8
		public static bool TryParseSimpleEncodedString(string text, ConceptualPrimitiveType type, out PrimitiveValue primitiveValue)
		{
			try
			{
				if (text == null)
				{
					primitiveValue = PrimitiveValue.Null;
					return true;
				}
				switch (type)
				{
				case ConceptualPrimitiveType.Text:
					primitiveValue = text;
					return true;
				case ConceptualPrimitiveType.Decimal:
					primitiveValue = XmlConvert.ToDecimal(text);
					return true;
				case ConceptualPrimitiveType.Double:
					primitiveValue = XmlConvert.ToDouble(text);
					return true;
				case ConceptualPrimitiveType.Integer:
					primitiveValue = XmlConvert.ToInt64(text);
					return true;
				case ConceptualPrimitiveType.Boolean:
					if (text.Equals("true", StringComparison.OrdinalIgnoreCase))
					{
						primitiveValue = BooleanPrimitiveValue.True;
						return true;
					}
					if (text.Equals("false", StringComparison.OrdinalIgnoreCase))
					{
						primitiveValue = BooleanPrimitiveValue.False;
						return true;
					}
					break;
				case ConceptualPrimitiveType.DateTime:
					primitiveValue = XmlConvert.ToDateTime(text, XmlDateTimeSerializationMode.RoundtripKind);
					return true;
				case ConceptualPrimitiveType.Binary:
					primitiveValue = Convert.FromBase64String(text);
					return true;
				case ConceptualPrimitiveType.Variant:
					return PrimitiveValueEncoding.TryParseTypeEncodedString(text, out primitiveValue);
				}
			}
			catch (Exception ex) when (!ex.IsStoppingException())
			{
			}
			primitiveValue = null;
			return false;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00006DF8 File Offset: 0x00004FF8
		private static bool TryRemoveLiteralPrefix(string prefix, ref string text)
		{
			if (text.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
			{
				text = text.Remove(0, prefix.Length);
				return true;
			}
			return false;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00006E18 File Offset: 0x00005018
		private static bool TryRemoveLiteralSuffix(string suffix, ref string text)
		{
			text = text.Trim(PrimitiveValueEncoding._whitespaceChars);
			if (text.Length <= suffix.Length || !text.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			text = text.Substring(0, text.Length - suffix.Length);
			return true;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00006E68 File Offset: 0x00005068
		private static bool TryRemoveQuotes(ref string text)
		{
			if (text.Length < 2)
			{
				return false;
			}
			char c = text[0];
			if (c != '\'' || text[text.Length - 1] != c)
			{
				return false;
			}
			string text2 = text.Substring(1, text.Length - 2);
			int num = 0;
			for (;;)
			{
				int num2 = text2.IndexOf(c, num);
				if (num2 < 0)
				{
					goto IL_0076;
				}
				text2 = text2.Remove(num2, 1);
				if (text2.Length < num2 + 1 || text2[num2] != c)
				{
					break;
				}
				num = num2 + 1;
			}
			return false;
			IL_0076:
			text = text2;
			return true;
		}

		// Token: 0x04000170 RID: 368
		private const string NullLiteral = "null";

		// Token: 0x04000171 RID: 369
		private const string Int64Suffix = "L";

		// Token: 0x04000172 RID: 370
		private const string DoubleSuffix = "D";

		// Token: 0x04000173 RID: 371
		private const string DecimalSuffix = "M";

		// Token: 0x04000174 RID: 372
		private const string DateTimePrefix = "datetime";

		// Token: 0x04000175 RID: 373
		private const string TrueLiteral = "true";

		// Token: 0x04000176 RID: 374
		private const string FalseLiteral = "false";

		// Token: 0x04000177 RID: 375
		private const char Quote = '\'';

		// Token: 0x04000178 RID: 376
		internal const string BinaryDataPrefix = "base64";

		// Token: 0x04000179 RID: 377
		private static readonly char[] _whitespaceChars = new char[] { ' ', '\t', '\n', '\r' };
	}
}
