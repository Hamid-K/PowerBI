using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.ReportingServices.ReportProcessing.Utils
{
	// Token: 0x020007AB RID: 1963
	internal sealed class StringKeyGenerator
	{
		// Token: 0x06006F01 RID: 28417 RVA: 0x001CFD20 File Offset: 0x001CDF20
		public StringKeyGenerator(CompareInfo compareInfo, CompareOptions compareOptions, bool nullAsBlank, bool useOrdinalStringComparison)
		{
			this._compareInfo = compareInfo;
			this._cultureInfo = new CultureInfo(compareInfo.Name);
			this._compareOptions = compareOptions;
			this._nullAsNumeric = nullAsBlank;
			if (useOrdinalStringComparison)
			{
				this._compareOptions = CompareOptions.Ordinal;
			}
		}

		// Token: 0x06006F02 RID: 28418 RVA: 0x001CFD5D File Offset: 0x001CDF5D
		public StringKeyGenerator(string cultureName, CompareOptions compareOptions, bool nullAsBlank, bool useOrdinalStringComparison)
			: this(CompareInfo.GetCompareInfo(cultureName), compareOptions, nullAsBlank, useOrdinalStringComparison)
		{
		}

		// Token: 0x06006F03 RID: 28419 RVA: 0x001CFD70 File Offset: 0x001CDF70
		public string GetKey(IEnumerable<object> values)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (object obj in values)
			{
				if (flag)
				{
					stringBuilder.Append('\u001d');
				}
				else
				{
					flag = true;
				}
				string key = this.GetKey(obj);
				stringBuilder.Append(key);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006F04 RID: 28420 RVA: 0x001CFDE4 File Offset: 0x001CDFE4
		public string GetKey(object value)
		{
			if (value == null)
			{
				return this.GetNullSortKey();
			}
			return this.GetSortKey((IConvertible)value);
		}

		// Token: 0x06006F05 RID: 28421 RVA: 0x001CFDFC File Offset: 0x001CDFFC
		private string GetSortKey(IConvertible value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			switch (value.GetTypeCode())
			{
			case TypeCode.Empty:
				stringBuilder.Append(this.GetNullSortKey());
				goto IL_0153;
			case TypeCode.Boolean:
				stringBuilder.Append('3');
				stringBuilder.Append(this.GetBooleanSortKey(value));
				goto IL_0153;
			case TypeCode.Char:
			case TypeCode.String:
				stringBuilder.Append('2');
				stringBuilder.Append(this.GetStringSortKey(value));
				goto IL_0153;
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
				stringBuilder.Append('0');
				stringBuilder.Append(this.GetIntSortKey(value));
				stringBuilder.Append('\u001f');
				stringBuilder.Append('1');
				goto IL_0153;
			case TypeCode.Single:
			case TypeCode.Double:
				stringBuilder.Append('0');
				stringBuilder.Append(this.GetDoubleSortKey(value));
				stringBuilder.Append('\u001f');
				stringBuilder.Append('3');
				goto IL_0153;
			case TypeCode.Decimal:
				stringBuilder.Append('0');
				stringBuilder.Append(this.GetDecimalSortKey(value));
				stringBuilder.Append('\u001f');
				stringBuilder.Append('2');
				goto IL_0153;
			case TypeCode.DateTime:
				stringBuilder.Append('1');
				stringBuilder.Append(this.GetDateTimeSortKey(value));
				goto IL_0153;
			}
			throw new NotSupportedException("This data type doesn't have prefix.");
			IL_0153:
			return stringBuilder.ToString();
		}

		// Token: 0x06006F06 RID: 28422 RVA: 0x001CFF62 File Offset: 0x001CE162
		private string GetNullSortKey()
		{
			if (!this._nullAsNumeric)
			{
				return StringKeyGenerator.NullKey;
			}
			return StringKeyGenerator.NullAsNumericKey;
		}

		// Token: 0x06006F07 RID: 28423 RVA: 0x001CFF78 File Offset: 0x001CE178
		private string GetIntSortKey(IConvertible value)
		{
			long num = value.ToInt64(null);
			return StringKeyGenerator.GetNumericSortKey(string.Format(CultureInfo.InvariantCulture, "{0:e20}", value), num == 0L);
		}

		// Token: 0x06006F08 RID: 28424 RVA: 0x001CFFA8 File Offset: 0x001CE1A8
		private string GetDecimalSortKey(IConvertible value)
		{
			decimal num = value.ToDecimal(null);
			return StringKeyGenerator.GetNumericSortKey(string.Format(CultureInfo.InvariantCulture, "{0:e28}", value), num == 0m);
		}

		// Token: 0x06006F09 RID: 28425 RVA: 0x001CFFE0 File Offset: 0x001CE1E0
		private string GetDoubleSortKey(IConvertible value)
		{
			double num = value.ToDouble(null);
			if (double.IsNegativeInfinity(num))
			{
				return "A";
			}
			if (double.IsPositiveInfinity(num))
			{
				return "G";
			}
			if (double.IsNaN(num))
			{
				return "H";
			}
			return StringKeyGenerator.GetNumericSortKey(string.Format(CultureInfo.InvariantCulture, "{0:e28}", value), num == 0.0);
		}

		// Token: 0x06006F0A RID: 28426 RVA: 0x001D0040 File Offset: 0x001CE240
		private string GetDateTimeSortKey(IConvertible value)
		{
			long ticks = value.ToDateTime(null).Ticks;
			return StringKeyGenerator.GetNumericSortKey(string.Format(CultureInfo.InvariantCulture, "{0:e20}", ticks), ticks == 0L);
		}

		// Token: 0x06006F0B RID: 28427 RVA: 0x001D007C File Offset: 0x001CE27C
		private string GetStringSortKey(IConvertible value)
		{
			string text = value.ToString(null);
			if (this._compareOptions.HasFlag(CompareOptions.Ordinal))
			{
				return text;
			}
			if (this._compareOptions.HasFlag(CompareOptions.OrdinalIgnoreCase))
			{
				return text.ToUpperInvariant();
			}
			return StringKeyGenerator.ToComparableBase64String(this._compareInfo.GetSortKey(text, this._compareOptions).KeyData);
		}

		// Token: 0x06006F0C RID: 28428 RVA: 0x001D00EE File Offset: 0x001CE2EE
		private string GetBooleanSortKey(IConvertible value)
		{
			if (!value.ToBoolean(null))
			{
				return "0";
			}
			return "1";
		}

		// Token: 0x06006F0D RID: 28429 RVA: 0x001D0104 File Offset: 0x001CE304
		private static string GetNumericSortKey(string scientific, bool isZero)
		{
			if (isZero)
			{
				return "D";
			}
			char[] array = new char[scientific.Length];
			bool flag = scientific[0] == '-';
			bool flag2 = scientific[scientific.Length - 4] == '-';
			char c;
			if (flag)
			{
				c = ((!flag2) ? 'B' : 'C');
			}
			else
			{
				c = (flag2 ? 'E' : 'F');
			}
			array[0] = c;
			array[1] = StringKeyGenerator.ReverseIfNegative(scientific[scientific.Length - 3], flag2 ^ flag);
			array[2] = StringKeyGenerator.ReverseIfNegative(scientific[scientific.Length - 2], flag2 ^ flag);
			array[3] = StringKeyGenerator.ReverseIfNegative(scientific[scientific.Length - 1], flag2 ^ flag);
			int num = 4;
			int num2 = 0;
			int num3 = 4;
			if (scientific[num2] == '-')
			{
				num2++;
			}
			for (char c2 = scientific[num2++]; c2 != 'e'; c2 = scientific[num2++])
			{
				if (c2 != '.')
				{
					array[num++] = StringKeyGenerator.ReverseIfNegative(c2, flag);
					if (c2 != '0')
					{
						num3 = num;
					}
				}
			}
			if (flag && num3 < array.Length)
			{
				array[num3++] = 'A';
			}
			return new string(array, 0, num3);
		}

		// Token: 0x06006F0E RID: 28430 RVA: 0x001D0230 File Offset: 0x001CE430
		private static char ReverseIfNegative(char digit, bool isNegative)
		{
			if (isNegative)
			{
				int num = (int)(digit - '0');
				num = 9 - num;
				return (char)(48 + num);
			}
			return digit;
		}

		// Token: 0x06006F0F RID: 28431 RVA: 0x001D0254 File Offset: 0x001CE454
		public static string ToComparableBase64String(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			if (bytes.Length == 0)
			{
				return string.Empty;
			}
			char[] array = new char[bytes.Length * 2];
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 63;
			int i = 0;
			int num6 = bytes.Length * 8;
			while (i < num6)
			{
				if (num3 < 8)
				{
					if (num2 < bytes.Length)
					{
						num4 = (num4 << 8) | (int)bytes[num2++];
					}
					else
					{
						num4 <<= 8;
					}
					num3 += 8;
				}
				int num7 = num3 - 6;
				int num8 = (num4 & (num5 << num7)) >> num7;
				array[num++] = StringKeyGenerator.Sorted64Chars[num8];
				num3 -= 6;
				i += 6;
			}
			return new string(array, 0, num);
		}

		// Token: 0x04003989 RID: 14729
		private const char GroupSeparator = '\u001d';

		// Token: 0x0400398A RID: 14730
		private const char UnitSeparator = '\u001f';

		// Token: 0x0400398B RID: 14731
		private static string NullAsNumericKey = "0D\u001f0";

		// Token: 0x0400398C RID: 14732
		private static string NullKey = ".";

		// Token: 0x0400398D RID: 14733
		private readonly CultureInfo _cultureInfo;

		// Token: 0x0400398E RID: 14734
		private readonly CompareInfo _compareInfo;

		// Token: 0x0400398F RID: 14735
		private readonly CompareOptions _compareOptions;

		// Token: 0x04003990 RID: 14736
		private readonly bool _nullAsNumeric;

		// Token: 0x04003991 RID: 14737
		private static char[] Sorted64Chars = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
			'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
			'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
			'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
			'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
			'y', 'z', '{', '}'
		};

		// Token: 0x02000CEF RID: 3311
		private static class TypePrefix
		{
			// Token: 0x04004FAE RID: 20398
			public const char Null = '0';

			// Token: 0x04004FAF RID: 20399
			public const char Int = '0';

			// Token: 0x04004FB0 RID: 20400
			public const char Decimal = '0';

			// Token: 0x04004FB1 RID: 20401
			public const char Double = '0';

			// Token: 0x04004FB2 RID: 20402
			public const char DateTime = '1';

			// Token: 0x04004FB3 RID: 20403
			public const char String = '2';

			// Token: 0x04004FB4 RID: 20404
			public const char Boolean = '3';
		}

		// Token: 0x02000CF0 RID: 3312
		private static class TypeSuffix
		{
			// Token: 0x04004FB5 RID: 20405
			public const char Null = '0';

			// Token: 0x04004FB6 RID: 20406
			public const char Int = '1';

			// Token: 0x04004FB7 RID: 20407
			public const char Decimal = '2';

			// Token: 0x04004FB8 RID: 20408
			public const char Double = '3';
		}
	}
}
