using System;
using System.Globalization;
using System.Text;

namespace Microsoft.DataShaping.Processing.Utils
{
	// Token: 0x0200001A RID: 26
	internal sealed class StringKeyGenerator : IKeyGenerator
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00003092 File Offset: 0x00001292
		internal StringKeyGenerator(CompareInfo compareInfo, CompareOptions compareOptions, bool nullAsBlank, bool useOrdinalStringKeyGeneration)
		{
			this._compareInfo = compareInfo;
			this._cultureInfo = new CultureInfo(compareInfo.Name);
			this._compareOptions = compareOptions;
			this._nullAsNumeric = nullAsBlank;
			if (useOrdinalStringKeyGeneration)
			{
				this._compareOptions = CompareOptions.Ordinal;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000030CF File Offset: 0x000012CF
		internal StringKeyGenerator(string cultureName, CompareOptions compareOptions, bool nullAsBlank, bool useOrdinalStringKeyGeneration)
			: this(CompareInfo.GetCompareInfo(cultureName), compareOptions, nullAsBlank, useOrdinalStringKeyGeneration)
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000030E4 File Offset: 0x000012E4
		public string GetKey(object[] values)
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

		// Token: 0x060000B3 RID: 179 RVA: 0x00003137 File Offset: 0x00001337
		public string GetKey(object value)
		{
			if (value == null)
			{
				return this.GetNullKey();
			}
			return this.GetKey((IConvertible)value);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003150 File Offset: 0x00001350
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

		// Token: 0x060000B5 RID: 181 RVA: 0x000031F4 File Offset: 0x000013F4
		private string GetKey(IConvertible value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			switch (value.GetTypeCode())
			{
			case TypeCode.Empty:
				stringBuilder.Append(this.GetNullKey());
				goto IL_0153;
			case TypeCode.Boolean:
				stringBuilder.Append('3');
				stringBuilder.Append(this.GetBooleanKey(value));
				goto IL_0153;
			case TypeCode.Char:
			case TypeCode.String:
				stringBuilder.Append('2');
				stringBuilder.Append(this.GetStringKey(value));
				goto IL_0153;
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
				stringBuilder.Append('0');
				stringBuilder.Append(this.GetIntKey(value));
				stringBuilder.Append('\u001f');
				stringBuilder.Append('1');
				goto IL_0153;
			case TypeCode.Single:
			case TypeCode.Double:
				stringBuilder.Append('0');
				stringBuilder.Append(this.GetDoubleKey(value));
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
				stringBuilder.Append(this.GetDateTimeKey(value));
				goto IL_0153;
			}
			throw new NotSupportedException("This data type doesn't have prefix.");
			IL_0153:
			return stringBuilder.ToString();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000335A File Offset: 0x0000155A
		private string GetNullKey()
		{
			if (!this._nullAsNumeric)
			{
				return StringKeyGenerator.NullKey;
			}
			return StringKeyGenerator.NullAsNumericKey;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003370 File Offset: 0x00001570
		private string GetIntKey(IConvertible value)
		{
			long num = value.ToInt64(null);
			return StringKeyGenerator.GetNumericKey(string.Format(CultureInfo.InvariantCulture, "{0:e20}", value), num == 0L);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000033A0 File Offset: 0x000015A0
		private string GetDecimalSortKey(IConvertible value)
		{
			decimal num = value.ToDecimal(null);
			return StringKeyGenerator.GetNumericKey(string.Format(CultureInfo.InvariantCulture, "{0:e28}", value), num == 0m);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000033D8 File Offset: 0x000015D8
		private string GetDoubleKey(IConvertible value)
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
			return StringKeyGenerator.GetNumericKey(string.Format(CultureInfo.InvariantCulture, "{0:e28}", value), num == 0.0);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003438 File Offset: 0x00001638
		private string GetDateTimeKey(IConvertible value)
		{
			long ticks = value.ToDateTime(null).Ticks;
			return StringKeyGenerator.GetNumericKey(string.Format(CultureInfo.InvariantCulture, "{0:e20}", ticks), ticks == 0L);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003474 File Offset: 0x00001674
		private string GetStringKey(IConvertible value)
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

		// Token: 0x060000BC RID: 188 RVA: 0x000034E6 File Offset: 0x000016E6
		private string GetBooleanKey(IConvertible value)
		{
			if (!value.ToBoolean(null))
			{
				return "0";
			}
			return "1";
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000034FC File Offset: 0x000016FC
		private static string GetNumericKey(string scientific, bool isZero)
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

		// Token: 0x060000BE RID: 190 RVA: 0x00003628 File Offset: 0x00001828
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

		// Token: 0x0400007F RID: 127
		private const char GroupSeparator = '\u001d';

		// Token: 0x04000080 RID: 128
		private const char UnitSeparator = '\u001f';

		// Token: 0x04000081 RID: 129
		private const char NegativeCharacter = '-';

		// Token: 0x04000082 RID: 130
		private const char ZeroCharacter = '0';

		// Token: 0x04000083 RID: 131
		private static readonly string NullAsNumericKey = "0D\u001f0";

		// Token: 0x04000084 RID: 132
		private static readonly string NullKey = ".";

		// Token: 0x04000085 RID: 133
		private readonly CultureInfo _cultureInfo;

		// Token: 0x04000086 RID: 134
		private readonly CompareInfo _compareInfo;

		// Token: 0x04000087 RID: 135
		private readonly CompareOptions _compareOptions;

		// Token: 0x04000088 RID: 136
		private readonly bool _nullAsNumeric;

		// Token: 0x04000089 RID: 137
		private static readonly char[] Sorted64Chars = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
			'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
			'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
			'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
			'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
			'y', 'z', '{', '}'
		};

		// Token: 0x020000C0 RID: 192
		private static class TypePrefix
		{
			// Token: 0x04000287 RID: 647
			internal const char Null = '0';

			// Token: 0x04000288 RID: 648
			internal const char Int = '0';

			// Token: 0x04000289 RID: 649
			internal const char Decimal = '0';

			// Token: 0x0400028A RID: 650
			internal const char Double = '0';

			// Token: 0x0400028B RID: 651
			internal const char DateTime = '1';

			// Token: 0x0400028C RID: 652
			internal const char String = '2';

			// Token: 0x0400028D RID: 653
			internal const char Boolean = '3';
		}

		// Token: 0x020000C1 RID: 193
		private static class TypeSuffix
		{
			// Token: 0x0400028E RID: 654
			internal const char Null = '0';

			// Token: 0x0400028F RID: 655
			internal const char Int = '1';

			// Token: 0x04000290 RID: 656
			internal const char Decimal = '2';

			// Token: 0x04000291 RID: 657
			internal const char Double = '3';
		}
	}
}
