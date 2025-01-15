using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000114 RID: 276
	internal abstract class LiteralParser
	{
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x000245E3 File Offset: 0x000227E3
		internal static LiteralParser ForETags
		{
			get
			{
				return LiteralParser.DefaultInstance;
			}
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x000245EA File Offset: 0x000227EA
		internal static LiteralParser ForKeys(bool keyAsSegment)
		{
			if (!keyAsSegment)
			{
				return LiteralParser.DefaultInstance;
			}
			return LiteralParser.KeysAsSegmentsInstance;
		}

		// Token: 0x06000CE2 RID: 3298
		internal abstract bool TryParseLiteral(Type targetType, string text, out object result);

		// Token: 0x06000CE4 RID: 3300 RVA: 0x000245FC File Offset: 0x000227FC
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

		// Token: 0x040006FD RID: 1789
		private static readonly LiteralParser DefaultInstance = new LiteralParser.DefaultLiteralParser();

		// Token: 0x040006FE RID: 1790
		private static readonly LiteralParser KeysAsSegmentsInstance = new LiteralParser.KeysAsSegmentsLiteralParser();

		// Token: 0x040006FF RID: 1791
		private static readonly IDictionary<Type, LiteralParser.PrimitiveParser> Parsers;

		// Token: 0x020002C5 RID: 709
		private sealed class DefaultLiteralParser : LiteralParser
		{
			// Token: 0x060018C5 RID: 6341 RVA: 0x00048C4C File Offset: 0x00046E4C
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

			// Token: 0x060018C6 RID: 6342 RVA: 0x00048CC8 File Offset: 0x00046EC8
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

		// Token: 0x020002C6 RID: 710
		private sealed class KeysAsSegmentsLiteralParser : LiteralParser
		{
			// Token: 0x060018C8 RID: 6344 RVA: 0x00048D00 File Offset: 0x00046F00
			internal override bool TryParseLiteral(Type targetType, string text, out object result)
			{
				text = LiteralParser.KeysAsSegmentsLiteralParser.UnescapeLeadingDollarSign(text);
				targetType = Nullable.GetUnderlyingType(targetType) ?? targetType;
				return LiteralParser.Parsers[targetType].TryConvert(text, out result);
			}

			// Token: 0x060018C9 RID: 6345 RVA: 0x00048D29 File Offset: 0x00046F29
			private static string UnescapeLeadingDollarSign(string text)
			{
				if (text.Length > 1 && text.get_Chars(0) == '$')
				{
					text = text.Substring(1);
				}
				return text;
			}
		}

		// Token: 0x020002C7 RID: 711
		private abstract class PrimitiveParser
		{
			// Token: 0x060018CB RID: 6347 RVA: 0x00048D49 File Offset: 0x00046F49
			protected PrimitiveParser(Type expectedType, string suffix, bool suffixRequired)
				: this(expectedType)
			{
				this.prefix = null;
				this.suffix = suffix;
				this.suffixRequired = suffixRequired;
			}

			// Token: 0x060018CC RID: 6348 RVA: 0x00048D67 File Offset: 0x00046F67
			protected PrimitiveParser(Type expectedType, string prefix)
				: this(expectedType)
			{
				this.prefix = prefix;
				this.suffix = null;
				this.suffixRequired = false;
			}

			// Token: 0x060018CD RID: 6349 RVA: 0x00048D85 File Offset: 0x00046F85
			protected PrimitiveParser(Type expectedType)
			{
				this.expectedType = expectedType;
			}

			// Token: 0x060018CE RID: 6350
			internal abstract bool TryConvert(string text, out object targetValue);

			// Token: 0x060018CF RID: 6351 RVA: 0x00048D94 File Offset: 0x00046F94
			internal virtual bool TryRemoveFormatting(ref string text)
			{
				if (this.prefix != null && !UriParserHelper.TryRemovePrefix(this.prefix, ref text))
				{
					return false;
				}
				bool flag = this.prefix != null || LiteralParser.PrimitiveParser.ValueOfTypeCanContainQuotes(this.expectedType);
				return (!flag || UriParserHelper.TryRemoveQuotes(ref text)) && (this.suffix == null || LiteralParser.PrimitiveParser.TryRemoveLiteralSuffix(this.suffix, ref text) || !this.suffixRequired);
			}

			// Token: 0x060018D0 RID: 6352 RVA: 0x00048E00 File Offset: 0x00047000
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

			// Token: 0x060018D1 RID: 6353 RVA: 0x00048E59 File Offset: 0x00047059
			private static bool ValueOfTypeCanContainQuotes(Type type)
			{
				return type == typeof(string);
			}

			// Token: 0x060018D2 RID: 6354 RVA: 0x00048E68 File Offset: 0x00047068
			private static bool IsValidNumericConstant(string text)
			{
				return string.Equals(text, "INF", 5) || string.Equals(text, "-INF", 5) || string.Equals(text, "NaN", 5);
			}

			// Token: 0x04000BFD RID: 3069
			private static readonly char[] XmlWhitespaceChars = new char[] { ' ', '\t', '\n', '\r' };

			// Token: 0x04000BFE RID: 3070
			private readonly string prefix;

			// Token: 0x04000BFF RID: 3071
			private readonly string suffix;

			// Token: 0x04000C00 RID: 3072
			private readonly bool suffixRequired;

			// Token: 0x04000C01 RID: 3073
			private readonly Type expectedType;
		}

		// Token: 0x020002C8 RID: 712
		private class DelegatingPrimitiveParser<T> : LiteralParser.PrimitiveParser
		{
			// Token: 0x060018D4 RID: 6356 RVA: 0x00048EAC File Offset: 0x000470AC
			protected DelegatingPrimitiveParser(Func<string, T> convertMethod, string suffix, bool suffixRequired)
				: base(typeof(T), suffix, suffixRequired)
			{
				this.convertMethod = convertMethod;
			}

			// Token: 0x060018D5 RID: 6357 RVA: 0x00048EC7 File Offset: 0x000470C7
			private DelegatingPrimitiveParser(Func<string, T> convertMethod)
				: base(typeof(T))
			{
				this.convertMethod = convertMethod;
			}

			// Token: 0x060018D6 RID: 6358 RVA: 0x00048EE0 File Offset: 0x000470E0
			private DelegatingPrimitiveParser(Func<string, T> convertMethod, string prefix)
				: base(typeof(T), prefix)
			{
				this.convertMethod = convertMethod;
			}

			// Token: 0x060018D7 RID: 6359 RVA: 0x00048EFA File Offset: 0x000470FA
			internal static LiteralParser.DelegatingPrimitiveParser<T> WithoutMarkup(Func<string, T> convertMethod)
			{
				return new LiteralParser.DelegatingPrimitiveParser<T>(convertMethod);
			}

			// Token: 0x060018D8 RID: 6360 RVA: 0x00048F02 File Offset: 0x00047102
			internal static LiteralParser.DelegatingPrimitiveParser<T> WithPrefix(Func<string, T> convertMethod, string prefix)
			{
				return new LiteralParser.DelegatingPrimitiveParser<T>(convertMethod, prefix);
			}

			// Token: 0x060018D9 RID: 6361 RVA: 0x00048F0B File Offset: 0x0004710B
			internal static LiteralParser.DelegatingPrimitiveParser<T> WithSuffix(Func<string, T> convertMethod, string suffix)
			{
				return LiteralParser.DelegatingPrimitiveParser<T>.WithSuffix(convertMethod, suffix, true);
			}

			// Token: 0x060018DA RID: 6362 RVA: 0x00048F15 File Offset: 0x00047115
			internal static LiteralParser.DelegatingPrimitiveParser<T> WithSuffix(Func<string, T> convertMethod, string suffix, bool required)
			{
				return new LiteralParser.DelegatingPrimitiveParser<T>(convertMethod, suffix, required);
			}

			// Token: 0x060018DB RID: 6363 RVA: 0x00048F20 File Offset: 0x00047120
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

			// Token: 0x04000C02 RID: 3074
			private readonly Func<string, T> convertMethod;
		}

		// Token: 0x020002C9 RID: 713
		private sealed class DecimalPrimitiveParser : LiteralParser.DelegatingPrimitiveParser<decimal>
		{
			// Token: 0x060018DC RID: 6364 RVA: 0x00048F8C File Offset: 0x0004718C
			internal DecimalPrimitiveParser()
				: base(new Func<string, decimal>(LiteralParser.DecimalPrimitiveParser.ConvertDecimal), "M", false)
			{
			}

			// Token: 0x060018DD RID: 6365 RVA: 0x00048FA8 File Offset: 0x000471A8
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

		// Token: 0x020002CA RID: 714
		private sealed class BinaryPrimitiveParser : LiteralParser.PrimitiveParser
		{
			// Token: 0x060018DE RID: 6366 RVA: 0x00048FEC File Offset: 0x000471EC
			internal BinaryPrimitiveParser()
				: base(typeof(byte[]))
			{
			}

			// Token: 0x060018DF RID: 6367 RVA: 0x00049000 File Offset: 0x00047200
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

			// Token: 0x060018E0 RID: 6368 RVA: 0x00049034 File Offset: 0x00047234
			internal override bool TryRemoveFormatting(ref string text)
			{
				return UriParserHelper.TryRemovePrefix("binary", ref text) && UriParserHelper.TryRemoveQuotes(ref text);
			}
		}

		// Token: 0x020002CB RID: 715
		private sealed class StringPrimitiveParser : LiteralParser.PrimitiveParser
		{
			// Token: 0x060018E1 RID: 6369 RVA: 0x00049050 File Offset: 0x00047250
			public StringPrimitiveParser()
				: base(typeof(string))
			{
			}

			// Token: 0x060018E2 RID: 6370 RVA: 0x00049062 File Offset: 0x00047262
			internal override bool TryConvert(string text, out object targetValue)
			{
				targetValue = text;
				return true;
			}

			// Token: 0x060018E3 RID: 6371 RVA: 0x00049068 File Offset: 0x00047268
			internal override bool TryRemoveFormatting(ref string text)
			{
				return UriParserHelper.TryRemoveQuotes(ref text);
			}
		}

		// Token: 0x020002CC RID: 716
		private sealed class DatePrimitiveParser : LiteralParser.PrimitiveParser
		{
			// Token: 0x060018E4 RID: 6372 RVA: 0x00049070 File Offset: 0x00047270
			public DatePrimitiveParser()
				: base(typeof(Date))
			{
			}

			// Token: 0x060018E5 RID: 6373 RVA: 0x00049084 File Offset: 0x00047284
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
