using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.OData.Core.UriParser.Parsers.UriParsers;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001FE RID: 510
	internal abstract class LiteralParser
	{
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x00043B3B File Offset: 0x00041D3B
		internal static LiteralParser ForETags
		{
			get
			{
				return LiteralParser.DefaultInstance;
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00043B42 File Offset: 0x00041D42
		internal static LiteralParser ForKeys(bool keyAsSegment)
		{
			if (!keyAsSegment)
			{
				return LiteralParser.DefaultInstance;
			}
			return LiteralParser.KeysAsSegmentsInstance;
		}

		// Token: 0x06001297 RID: 4759
		internal abstract bool TryParseLiteral(Type targetType, string text, out object result);

		// Token: 0x06001299 RID: 4761 RVA: 0x00043B54 File Offset: 0x00041D54
		// Note: this type is marked as 'beforefieldinit'.
		static LiteralParser()
		{
			Dictionary<Type, LiteralParser.PrimitiveParser> dictionary = new Dictionary<Type, LiteralParser.PrimitiveParser>(ReferenceEqualityComparer<Type>.Instance);
			dictionary.Add(typeof(byte[]), new LiteralParser.BinaryPrimitiveParser());
			dictionary.Add(typeof(string), new LiteralParser.StringPrimitiveParser());
			dictionary.Add(typeof(decimal), new LiteralParser.DecimalPrimitiveParser());
			dictionary.Add(typeof(Date), new LiteralParser.DatePrimitiveParser());
			dictionary.Add(typeof(bool), LiteralParser.DelegatingPrimitiveParser<bool>.WithoutMarkup(new Func<string, bool>(XmlConvert.ToBoolean)));
			dictionary.Add(typeof(byte), LiteralParser.DelegatingPrimitiveParser<byte>.WithoutMarkup(new Func<string, byte>(XmlConvert.ToByte)));
			dictionary.Add(typeof(sbyte), LiteralParser.DelegatingPrimitiveParser<sbyte>.WithoutMarkup(new Func<string, sbyte>(XmlConvert.ToSByte)));
			dictionary.Add(typeof(short), LiteralParser.DelegatingPrimitiveParser<short>.WithoutMarkup(new Func<string, short>(XmlConvert.ToInt16)));
			dictionary.Add(typeof(int), LiteralParser.DelegatingPrimitiveParser<int>.WithoutMarkup(new Func<string, int>(XmlConvert.ToInt32)));
			dictionary.Add(typeof(DateTimeOffset), LiteralParser.DelegatingPrimitiveParser<DateTimeOffset>.WithoutMarkup(new Func<string, DateTimeOffset>(XmlConvert.ToDateTimeOffset)));
			dictionary.Add(typeof(Guid), LiteralParser.DelegatingPrimitiveParser<Guid>.WithoutMarkup(new Func<string, Guid>(XmlConvert.ToGuid)));
			dictionary.Add(typeof(TimeSpan), LiteralParser.DelegatingPrimitiveParser<TimeSpan>.WithPrefix(new Func<string, TimeSpan>(EdmValueParser.ParseDuration), "duration"));
			dictionary.Add(typeof(long), LiteralParser.DelegatingPrimitiveParser<long>.WithSuffix(new Func<string, long>(XmlConvert.ToInt64), "L", false));
			dictionary.Add(typeof(float), LiteralParser.DelegatingPrimitiveParser<float>.WithSuffix(new Func<string, float>(XmlConvert.ToSingle), "f", false));
			dictionary.Add(typeof(double), LiteralParser.DelegatingPrimitiveParser<double>.WithSuffix(new Func<string, double>(XmlConvert.ToDouble), "D", false));
			LiteralParser.Parsers = dictionary;
		}

		// Token: 0x04000807 RID: 2055
		private static readonly LiteralParser DefaultInstance = new LiteralParser.DefaultLiteralParser();

		// Token: 0x04000808 RID: 2056
		private static readonly LiteralParser KeysAsSegmentsInstance = new LiteralParser.KeysAsSegmentsLiteralParser();

		// Token: 0x04000809 RID: 2057
		private static readonly IDictionary<Type, LiteralParser.PrimitiveParser> Parsers;

		// Token: 0x020001FF RID: 511
		private sealed class DefaultLiteralParser : LiteralParser
		{
			// Token: 0x0600129A RID: 4762 RVA: 0x00043D64 File Offset: 0x00041F64
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

			// Token: 0x0600129B RID: 4763 RVA: 0x00043DE0 File Offset: 0x00041FE0
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

		// Token: 0x02000200 RID: 512
		private sealed class KeysAsSegmentsLiteralParser : LiteralParser
		{
			// Token: 0x0600129D RID: 4765 RVA: 0x00043E18 File Offset: 0x00042018
			internal override bool TryParseLiteral(Type targetType, string text, out object result)
			{
				text = LiteralParser.KeysAsSegmentsLiteralParser.UnescapeLeadingDollarSign(text);
				targetType = Nullable.GetUnderlyingType(targetType) ?? targetType;
				return LiteralParser.Parsers[targetType].TryConvert(text, out result);
			}

			// Token: 0x0600129E RID: 4766 RVA: 0x00043E41 File Offset: 0x00042041
			private static string UnescapeLeadingDollarSign(string text)
			{
				if (text.Length > 1 && text.get_Chars(0) == '$')
				{
					text = text.Substring(1);
				}
				return text;
			}
		}

		// Token: 0x02000201 RID: 513
		private abstract class PrimitiveParser
		{
			// Token: 0x060012A0 RID: 4768 RVA: 0x00043E69 File Offset: 0x00042069
			protected PrimitiveParser(Type expectedType, string suffix, bool suffixRequired)
				: this(expectedType)
			{
				this.prefix = null;
				this.suffix = suffix;
				this.suffixRequired = suffixRequired;
			}

			// Token: 0x060012A1 RID: 4769 RVA: 0x00043E87 File Offset: 0x00042087
			protected PrimitiveParser(Type expectedType, string prefix)
				: this(expectedType)
			{
				this.prefix = prefix;
				this.suffix = null;
				this.suffixRequired = false;
			}

			// Token: 0x060012A2 RID: 4770 RVA: 0x00043EA5 File Offset: 0x000420A5
			protected PrimitiveParser(Type expectedType)
			{
				this.expectedType = expectedType;
			}

			// Token: 0x060012A3 RID: 4771
			internal abstract bool TryConvert(string text, out object targetValue);

			// Token: 0x060012A4 RID: 4772 RVA: 0x00043EB4 File Offset: 0x000420B4
			internal virtual bool TryRemoveFormatting(ref string text)
			{
				if (this.prefix != null && !UriParserHelper.TryRemovePrefix(this.prefix, ref text))
				{
					return false;
				}
				bool flag = this.prefix != null || LiteralParser.PrimitiveParser.ValueOfTypeCanContainQuotes(this.expectedType);
				return (!flag || UriParserHelper.TryRemoveQuotes(ref text)) && (this.suffix == null || LiteralParser.PrimitiveParser.TryRemoveLiteralSuffix(this.suffix, ref text) || !this.suffixRequired);
			}

			// Token: 0x060012A5 RID: 4773 RVA: 0x00043F20 File Offset: 0x00042120
			internal static bool TryRemoveLiteralSuffix(string suffix, ref string text)
			{
				text = text.Trim(LiteralParser.PrimitiveParser.XmlWhitespaceChars);
				if (text.Length <= suffix.Length || LiteralParser.PrimitiveParser.IsValidNumericConstant(text) || !text.EndsWith(suffix, 5))
				{
					return false;
				}
				text = text.Substring(0, text.Length - suffix.Length);
				return true;
			}

			// Token: 0x060012A6 RID: 4774 RVA: 0x00043F79 File Offset: 0x00042179
			private static bool ValueOfTypeCanContainQuotes(Type type)
			{
				return type == typeof(string);
			}

			// Token: 0x060012A7 RID: 4775 RVA: 0x00043F88 File Offset: 0x00042188
			private static bool IsValidNumericConstant(string text)
			{
				return string.Equals(text, "INF", 5) || string.Equals(text, "-INF", 5) || string.Equals(text, "NaN", 5);
			}

			// Token: 0x0400080A RID: 2058
			private static readonly char[] XmlWhitespaceChars = new char[] { ' ', '\t', '\n', '\r' };

			// Token: 0x0400080B RID: 2059
			private readonly string prefix;

			// Token: 0x0400080C RID: 2060
			private readonly string suffix;

			// Token: 0x0400080D RID: 2061
			private readonly bool suffixRequired;

			// Token: 0x0400080E RID: 2062
			private readonly Type expectedType;
		}

		// Token: 0x02000202 RID: 514
		private class DelegatingPrimitiveParser<T> : LiteralParser.PrimitiveParser
		{
			// Token: 0x060012A9 RID: 4777 RVA: 0x00043FD8 File Offset: 0x000421D8
			protected DelegatingPrimitiveParser(Func<string, T> convertMethod, string suffix, bool suffixRequired)
				: base(typeof(T), suffix, suffixRequired)
			{
				this.convertMethod = convertMethod;
			}

			// Token: 0x060012AA RID: 4778 RVA: 0x00043FF3 File Offset: 0x000421F3
			private DelegatingPrimitiveParser(Func<string, T> convertMethod)
				: base(typeof(T))
			{
				this.convertMethod = convertMethod;
			}

			// Token: 0x060012AB RID: 4779 RVA: 0x0004400C File Offset: 0x0004220C
			private DelegatingPrimitiveParser(Func<string, T> convertMethod, string prefix)
				: base(typeof(T), prefix)
			{
				this.convertMethod = convertMethod;
			}

			// Token: 0x060012AC RID: 4780 RVA: 0x00044026 File Offset: 0x00042226
			internal static LiteralParser.DelegatingPrimitiveParser<T> WithoutMarkup(Func<string, T> convertMethod)
			{
				return new LiteralParser.DelegatingPrimitiveParser<T>(convertMethod);
			}

			// Token: 0x060012AD RID: 4781 RVA: 0x0004402E File Offset: 0x0004222E
			internal static LiteralParser.DelegatingPrimitiveParser<T> WithPrefix(Func<string, T> convertMethod, string prefix)
			{
				return new LiteralParser.DelegatingPrimitiveParser<T>(convertMethod, prefix);
			}

			// Token: 0x060012AE RID: 4782 RVA: 0x00044037 File Offset: 0x00042237
			internal static LiteralParser.DelegatingPrimitiveParser<T> WithSuffix(Func<string, T> convertMethod, string suffix)
			{
				return LiteralParser.DelegatingPrimitiveParser<T>.WithSuffix(convertMethod, suffix, true);
			}

			// Token: 0x060012AF RID: 4783 RVA: 0x00044041 File Offset: 0x00042241
			internal static LiteralParser.DelegatingPrimitiveParser<T> WithSuffix(Func<string, T> convertMethod, string suffix, bool required)
			{
				return new LiteralParser.DelegatingPrimitiveParser<T>(convertMethod, suffix, required);
			}

			// Token: 0x060012B0 RID: 4784 RVA: 0x0004404C File Offset: 0x0004224C
			internal override bool TryConvert(string text, out object targetValue)
			{
				bool flag;
				try
				{
					targetValue = this.convertMethod.Invoke(text);
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

			// Token: 0x0400080F RID: 2063
			private readonly Func<string, T> convertMethod;
		}

		// Token: 0x02000203 RID: 515
		private sealed class DecimalPrimitiveParser : LiteralParser.DelegatingPrimitiveParser<decimal>
		{
			// Token: 0x060012B1 RID: 4785 RVA: 0x000440B8 File Offset: 0x000422B8
			internal DecimalPrimitiveParser()
				: base(new Func<string, decimal>(LiteralParser.DecimalPrimitiveParser.ConvertDecimal), "M", false)
			{
			}

			// Token: 0x060012B2 RID: 4786 RVA: 0x000440D4 File Offset: 0x000422D4
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
					if (!decimal.TryParse(text, 167, NumberFormatInfo.InvariantInfo, ref num2))
					{
						throw;
					}
					num = num2;
				}
				return num;
			}
		}

		// Token: 0x02000204 RID: 516
		private sealed class BinaryPrimitiveParser : LiteralParser.PrimitiveParser
		{
			// Token: 0x060012B3 RID: 4787 RVA: 0x00044118 File Offset: 0x00042318
			internal BinaryPrimitiveParser()
				: base(typeof(byte[]))
			{
			}

			// Token: 0x060012B4 RID: 4788 RVA: 0x0004412C File Offset: 0x0004232C
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

			// Token: 0x060012B5 RID: 4789 RVA: 0x00044160 File Offset: 0x00042360
			internal override bool TryRemoveFormatting(ref string text)
			{
				return UriParserHelper.TryRemovePrefix("binary", ref text) && UriParserHelper.TryRemoveQuotes(ref text);
			}
		}

		// Token: 0x02000205 RID: 517
		private sealed class StringPrimitiveParser : LiteralParser.PrimitiveParser
		{
			// Token: 0x060012B6 RID: 4790 RVA: 0x0004417C File Offset: 0x0004237C
			public StringPrimitiveParser()
				: base(typeof(string))
			{
			}

			// Token: 0x060012B7 RID: 4791 RVA: 0x0004418E File Offset: 0x0004238E
			internal override bool TryConvert(string text, out object targetValue)
			{
				targetValue = text;
				return true;
			}

			// Token: 0x060012B8 RID: 4792 RVA: 0x00044194 File Offset: 0x00042394
			internal override bool TryRemoveFormatting(ref string text)
			{
				return UriParserHelper.TryRemoveQuotes(ref text);
			}
		}

		// Token: 0x02000206 RID: 518
		private sealed class DatePrimitiveParser : LiteralParser.PrimitiveParser
		{
			// Token: 0x060012B9 RID: 4793 RVA: 0x0004419C File Offset: 0x0004239C
			public DatePrimitiveParser()
				: base(typeof(Date))
			{
			}

			// Token: 0x060012BA RID: 4794 RVA: 0x000441B0 File Offset: 0x000423B0
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
