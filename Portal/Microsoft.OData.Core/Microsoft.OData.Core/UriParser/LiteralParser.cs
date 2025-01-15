using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000157 RID: 343
	internal abstract class LiteralParser
	{
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x0003211B File Offset: 0x0003031B
		internal static LiteralParser ForETags
		{
			get
			{
				return LiteralParser.DefaultInstance;
			}
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00032122 File Offset: 0x00030322
		internal static LiteralParser ForKeys(bool keyAsSegment)
		{
			if (!keyAsSegment)
			{
				return LiteralParser.DefaultInstance;
			}
			return LiteralParser.KeysAsSegmentsInstance;
		}

		// Token: 0x0600118D RID: 4493
		internal abstract bool TryParseLiteral(Type targetType, string text, out object result);

		// Token: 0x04000813 RID: 2067
		private static readonly LiteralParser DefaultInstance = new LiteralParser.DefaultLiteralParser();

		// Token: 0x04000814 RID: 2068
		private static readonly LiteralParser KeysAsSegmentsInstance = new LiteralParser.KeysAsSegmentsLiteralParser();

		// Token: 0x04000815 RID: 2069
		private static readonly IDictionary<Type, LiteralParser.PrimitiveParser> Parsers = new Dictionary<Type, LiteralParser.PrimitiveParser>(ReferenceEqualityComparer<Type>.Instance)
		{
			{
				typeof(byte[]),
				new LiteralParser.BinaryPrimitiveParser()
			},
			{
				typeof(string),
				new LiteralParser.StringPrimitiveParser()
			},
			{
				typeof(decimal),
				new LiteralParser.DecimalPrimitiveParser()
			},
			{
				typeof(Date),
				new LiteralParser.DatePrimitiveParser()
			},
			{
				typeof(bool),
				LiteralParser.DelegatingPrimitiveParser<bool>.WithoutMarkup(new Func<string, bool>(XmlConvert.ToBoolean))
			},
			{
				typeof(byte),
				LiteralParser.DelegatingPrimitiveParser<byte>.WithoutMarkup(new Func<string, byte>(XmlConvert.ToByte))
			},
			{
				typeof(sbyte),
				LiteralParser.DelegatingPrimitiveParser<sbyte>.WithoutMarkup(new Func<string, sbyte>(XmlConvert.ToSByte))
			},
			{
				typeof(short),
				LiteralParser.DelegatingPrimitiveParser<short>.WithoutMarkup(new Func<string, short>(XmlConvert.ToInt16))
			},
			{
				typeof(int),
				LiteralParser.DelegatingPrimitiveParser<int>.WithoutMarkup(new Func<string, int>(XmlConvert.ToInt32))
			},
			{
				typeof(DateTimeOffset),
				LiteralParser.DelegatingPrimitiveParser<DateTimeOffset>.WithoutMarkup(new Func<string, DateTimeOffset>(XmlConvert.ToDateTimeOffset))
			},
			{
				typeof(Guid),
				LiteralParser.DelegatingPrimitiveParser<Guid>.WithoutMarkup(new Func<string, Guid>(XmlConvert.ToGuid))
			},
			{
				typeof(TimeSpan),
				LiteralParser.DelegatingPrimitiveParser<TimeSpan>.WithPrefix(new Func<string, TimeSpan>(EdmValueParser.ParseDuration), "duration")
			},
			{
				typeof(long),
				LiteralParser.DelegatingPrimitiveParser<long>.WithSuffix(new Func<string, long>(XmlConvert.ToInt64), "L", false)
			},
			{
				typeof(float),
				LiteralParser.DelegatingPrimitiveParser<float>.WithSuffix(new Func<string, float>(XmlConvert.ToSingle), "f", false)
			},
			{
				typeof(double),
				LiteralParser.DelegatingPrimitiveParser<double>.WithSuffix(new Func<string, double>(XmlConvert.ToDouble), "D", false)
			}
		};

		// Token: 0x020003AB RID: 939
		private sealed class DefaultLiteralParser : LiteralParser
		{
			// Token: 0x06001FCF RID: 8143 RVA: 0x0005AB98 File Offset: 0x00058D98
			internal override bool TryParseLiteral(Type targetType, string text, out object result)
			{
				targetType = Nullable.GetUnderlyingType(targetType) ?? targetType;
				bool flag = LiteralParser.DefaultLiteralParser.TryRemoveFormattingAndConvert(text, typeof(byte[]), out result);
				if (flag)
				{
					byte[] array = (byte[])result;
					if (targetType == typeof(byte[]))
					{
						result = array;
						return true;
					}
					string @string = Encoding.UTF8.GetString(array, 0, array.Length);
					return LiteralParser.DefaultLiteralParser.TryRemoveFormattingAndConvert(@string, targetType, out result);
				}
				else
				{
					if (targetType == typeof(byte[]))
					{
						result = null;
						return false;
					}
					return LiteralParser.DefaultLiteralParser.TryRemoveFormattingAndConvert(text, targetType, out result);
				}
			}

			// Token: 0x06001FD0 RID: 8144 RVA: 0x0005AC14 File Offset: 0x00058E14
			private static bool TryRemoveFormattingAndConvert(string text, Type targetType, out object targetValue)
			{
				LiteralParser.PrimitiveParser primitiveParser = LiteralParser.Parsers[targetType];
				if (!primitiveParser.TryRemoveFormatting(ref text))
				{
					targetValue = null;
					return false;
				}
				return primitiveParser.TryConvert(text, out targetValue);
			}
		}

		// Token: 0x020003AC RID: 940
		private sealed class KeysAsSegmentsLiteralParser : LiteralParser
		{
			// Token: 0x06001FD2 RID: 8146 RVA: 0x0005AC4C File Offset: 0x00058E4C
			internal override bool TryParseLiteral(Type targetType, string text, out object result)
			{
				text = LiteralParser.KeysAsSegmentsLiteralParser.UnescapeLeadingDollarSign(text);
				targetType = Nullable.GetUnderlyingType(targetType) ?? targetType;
				return LiteralParser.Parsers[targetType].TryConvert(text, out result);
			}

			// Token: 0x06001FD3 RID: 8147 RVA: 0x0005AC75 File Offset: 0x00058E75
			private static string UnescapeLeadingDollarSign(string text)
			{
				if (text.Length > 1 && text[0] == '$')
				{
					text = text.Substring(1);
				}
				return text;
			}
		}

		// Token: 0x020003AD RID: 941
		private abstract class PrimitiveParser
		{
			// Token: 0x06001FD5 RID: 8149 RVA: 0x0005AC95 File Offset: 0x00058E95
			protected PrimitiveParser(Type expectedType, string suffix, bool suffixRequired)
				: this(expectedType)
			{
				this.prefix = null;
				this.suffix = suffix;
				this.suffixRequired = suffixRequired;
			}

			// Token: 0x06001FD6 RID: 8150 RVA: 0x0005ACB3 File Offset: 0x00058EB3
			protected PrimitiveParser(Type expectedType, string prefix)
				: this(expectedType)
			{
				this.prefix = prefix;
				this.suffix = null;
				this.suffixRequired = false;
			}

			// Token: 0x06001FD7 RID: 8151 RVA: 0x0005ACD1 File Offset: 0x00058ED1
			protected PrimitiveParser(Type expectedType)
			{
				this.expectedType = expectedType;
			}

			// Token: 0x06001FD8 RID: 8152
			internal abstract bool TryConvert(string text, out object targetValue);

			// Token: 0x06001FD9 RID: 8153 RVA: 0x0005ACE0 File Offset: 0x00058EE0
			internal virtual bool TryRemoveFormatting(ref string text)
			{
				if (this.prefix != null && !UriParserHelper.TryRemovePrefix(this.prefix, ref text))
				{
					return false;
				}
				bool flag = this.prefix != null || LiteralParser.PrimitiveParser.ValueOfTypeCanContainQuotes(this.expectedType);
				return (!flag || UriParserHelper.TryRemoveQuotes(ref text)) && (this.suffix == null || LiteralParser.PrimitiveParser.TryRemoveLiteralSuffix(this.suffix, ref text) || !this.suffixRequired);
			}

			// Token: 0x06001FDA RID: 8154 RVA: 0x0005AD4C File Offset: 0x00058F4C
			internal static bool TryRemoveLiteralSuffix(string suffix, ref string text)
			{
				text = text.Trim(LiteralParser.PrimitiveParser.XmlWhitespaceChars);
				if (text.Length <= suffix.Length || LiteralParser.PrimitiveParser.IsValidNumericConstant(text) || !text.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				text = text.Substring(0, text.Length - suffix.Length);
				return true;
			}

			// Token: 0x06001FDB RID: 8155 RVA: 0x0005ADA5 File Offset: 0x00058FA5
			private static bool ValueOfTypeCanContainQuotes(Type type)
			{
				return type == typeof(string);
			}

			// Token: 0x06001FDC RID: 8156 RVA: 0x0005ADB4 File Offset: 0x00058FB4
			private static bool IsValidNumericConstant(string text)
			{
				return string.Equals(text, "INF", StringComparison.OrdinalIgnoreCase) || string.Equals(text, "-INF", StringComparison.OrdinalIgnoreCase) || string.Equals(text, "NaN", StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x04000ED0 RID: 3792
			private static readonly char[] XmlWhitespaceChars = new char[] { ' ', '\t', '\n', '\r' };

			// Token: 0x04000ED1 RID: 3793
			private readonly string prefix;

			// Token: 0x04000ED2 RID: 3794
			private readonly string suffix;

			// Token: 0x04000ED3 RID: 3795
			private readonly bool suffixRequired;

			// Token: 0x04000ED4 RID: 3796
			private readonly Type expectedType;
		}

		// Token: 0x020003AE RID: 942
		private class DelegatingPrimitiveParser<T> : LiteralParser.PrimitiveParser
		{
			// Token: 0x06001FDE RID: 8158 RVA: 0x0005ADF8 File Offset: 0x00058FF8
			protected DelegatingPrimitiveParser(Func<string, T> convertMethod, string suffix, bool suffixRequired)
				: base(typeof(T), suffix, suffixRequired)
			{
				this.convertMethod = convertMethod;
			}

			// Token: 0x06001FDF RID: 8159 RVA: 0x0005AE13 File Offset: 0x00059013
			private DelegatingPrimitiveParser(Func<string, T> convertMethod)
				: base(typeof(T))
			{
				this.convertMethod = convertMethod;
			}

			// Token: 0x06001FE0 RID: 8160 RVA: 0x0005AE2C File Offset: 0x0005902C
			private DelegatingPrimitiveParser(Func<string, T> convertMethod, string prefix)
				: base(typeof(T), prefix)
			{
				this.convertMethod = convertMethod;
			}

			// Token: 0x06001FE1 RID: 8161 RVA: 0x0005AE46 File Offset: 0x00059046
			internal static LiteralParser.DelegatingPrimitiveParser<T> WithoutMarkup(Func<string, T> convertMethod)
			{
				return new LiteralParser.DelegatingPrimitiveParser<T>(convertMethod);
			}

			// Token: 0x06001FE2 RID: 8162 RVA: 0x0005AE4E File Offset: 0x0005904E
			internal static LiteralParser.DelegatingPrimitiveParser<T> WithPrefix(Func<string, T> convertMethod, string prefix)
			{
				return new LiteralParser.DelegatingPrimitiveParser<T>(convertMethod, prefix);
			}

			// Token: 0x06001FE3 RID: 8163 RVA: 0x0005AE57 File Offset: 0x00059057
			internal static LiteralParser.DelegatingPrimitiveParser<T> WithSuffix(Func<string, T> convertMethod, string suffix)
			{
				return LiteralParser.DelegatingPrimitiveParser<T>.WithSuffix(convertMethod, suffix, true);
			}

			// Token: 0x06001FE4 RID: 8164 RVA: 0x0005AE61 File Offset: 0x00059061
			internal static LiteralParser.DelegatingPrimitiveParser<T> WithSuffix(Func<string, T> convertMethod, string suffix, bool required)
			{
				return new LiteralParser.DelegatingPrimitiveParser<T>(convertMethod, suffix, required);
			}

			// Token: 0x06001FE5 RID: 8165 RVA: 0x0005AE6C File Offset: 0x0005906C
			internal override bool TryConvert(string text, out object targetValue)
			{
				bool flag;
				try
				{
					targetValue = this.convertMethod(text);
					flag = true;
				}
				catch (FormatException)
				{
					targetValue = default(T);
					flag = false;
				}
				catch (OverflowException)
				{
					targetValue = default(T);
					flag = false;
				}
				return flag;
			}

			// Token: 0x04000ED5 RID: 3797
			private readonly Func<string, T> convertMethod;
		}

		// Token: 0x020003AF RID: 943
		private sealed class DecimalPrimitiveParser : LiteralParser.DelegatingPrimitiveParser<decimal>
		{
			// Token: 0x06001FE6 RID: 8166 RVA: 0x0005AED8 File Offset: 0x000590D8
			internal DecimalPrimitiveParser()
				: base(new Func<string, decimal>(LiteralParser.DecimalPrimitiveParser.ConvertDecimal), "M", false)
			{
			}

			// Token: 0x06001FE7 RID: 8167 RVA: 0x0005AEF4 File Offset: 0x000590F4
			private static decimal ConvertDecimal(string text)
			{
				decimal num;
				try
				{
					num = XmlConvert.ToDecimal(text);
				}
				catch (FormatException)
				{
					decimal num2;
					if (!decimal.TryParse(text, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out num2))
					{
						throw;
					}
					num = num2;
				}
				return num;
			}
		}

		// Token: 0x020003B0 RID: 944
		private sealed class BinaryPrimitiveParser : LiteralParser.PrimitiveParser
		{
			// Token: 0x06001FE8 RID: 8168 RVA: 0x0005AF38 File Offset: 0x00059138
			internal BinaryPrimitiveParser()
				: base(typeof(byte[]))
			{
			}

			// Token: 0x06001FE9 RID: 8169 RVA: 0x0005AF4C File Offset: 0x0005914C
			internal override bool TryConvert(string text, out object targetValue)
			{
				try
				{
					targetValue = Convert.FromBase64String(text);
				}
				catch (FormatException)
				{
					targetValue = null;
					return false;
				}
				return true;
			}

			// Token: 0x06001FEA RID: 8170 RVA: 0x0005AF80 File Offset: 0x00059180
			internal override bool TryRemoveFormatting(ref string text)
			{
				return UriParserHelper.TryRemovePrefix("binary", ref text) && UriParserHelper.TryRemoveQuotes(ref text);
			}
		}

		// Token: 0x020003B1 RID: 945
		private sealed class StringPrimitiveParser : LiteralParser.PrimitiveParser
		{
			// Token: 0x06001FEB RID: 8171 RVA: 0x0005AF9C File Offset: 0x0005919C
			public StringPrimitiveParser()
				: base(typeof(string))
			{
			}

			// Token: 0x06001FEC RID: 8172 RVA: 0x0005AFAE File Offset: 0x000591AE
			internal override bool TryConvert(string text, out object targetValue)
			{
				targetValue = text;
				return true;
			}

			// Token: 0x06001FED RID: 8173 RVA: 0x0005AFB4 File Offset: 0x000591B4
			internal override bool TryRemoveFormatting(ref string text)
			{
				return UriParserHelper.TryRemoveQuotes(ref text);
			}
		}

		// Token: 0x020003B2 RID: 946
		private sealed class DatePrimitiveParser : LiteralParser.PrimitiveParser
		{
			// Token: 0x06001FEE RID: 8174 RVA: 0x0005AFBC File Offset: 0x000591BC
			public DatePrimitiveParser()
				: base(typeof(Date))
			{
			}

			// Token: 0x06001FEF RID: 8175 RVA: 0x0005AFD0 File Offset: 0x000591D0
			internal override bool TryConvert(string text, out object targetValue)
			{
				Date? date;
				bool flag = EdmValueParser.TryParseDate(text, out date);
				targetValue = date;
				return flag;
			}
		}
	}
}
