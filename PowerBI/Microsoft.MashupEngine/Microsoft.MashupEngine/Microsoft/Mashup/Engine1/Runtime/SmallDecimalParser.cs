using System;
using System.Globalization;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001619 RID: 5657
	internal class SmallDecimalParser
	{
		// Token: 0x06008E1B RID: 36379 RVA: 0x001DAF70 File Offset: 0x001D9170
		public static bool TryParse(string s, int offset, int length, CultureInfo culture, out SmallDecimal number)
		{
			if (length >= 1)
			{
				char c = s[offset];
				if (c >= '0' && c <= '9')
				{
					return SmallDecimalParser.TryParseUnsigned(s, offset, length, culture, out number);
				}
				string negativeSign = culture.NumberFormat.NegativeSign;
				if (negativeSign.Length == 1 && c == negativeSign[0])
				{
					if (!SmallDecimalParser.TryParseUnsigned(s, offset + 1, length - 1, culture, out number))
					{
						return false;
					}
					number = new SmallDecimal(-number.Numerator, number.Denominator);
					return true;
				}
				else
				{
					string positiveSign = culture.NumberFormat.PositiveSign;
					if (positiveSign.Length == 1 && c == positiveSign[0])
					{
						return SmallDecimalParser.TryParseUnsigned(s, offset + 1, length - 1, culture, out number);
					}
				}
			}
			number = default(SmallDecimal);
			return false;
		}

		// Token: 0x06008E1C RID: 36380 RVA: 0x001DB02C File Offset: 0x001D922C
		public static int ParseDigits(string chars, int offset, int length)
		{
			int num = (int)(chars[offset] - '0');
			if (num > 9)
			{
				return -1;
			}
			while (length > 1)
			{
				offset++;
				length--;
				int num2 = (int)(chars[offset] - '0');
				if (num2 > 9)
				{
					return -1;
				}
				num = num * 10 + num2;
			}
			return num;
		}

		// Token: 0x06008E1D RID: 36381 RVA: 0x001DB073 File Offset: 0x001D9273
		public static int Power10(int value)
		{
			return SmallDecimalParser.power10[value];
		}

		// Token: 0x06008E1E RID: 36382 RVA: 0x001DB07C File Offset: 0x001D927C
		public static bool TryParseUnsigned(string s, int offset, int length, CultureInfo culture, out SmallDecimal number)
		{
			if (length >= 1 && length <= 9 && !culture.IsNeutralCulture)
			{
				int num = SmallDecimalParser.ParseDigits(s, offset, length);
				if (num != -1)
				{
					number = new SmallDecimal(num);
					return true;
				}
				string numberDecimalSeparator = culture.NumberFormat.NumberDecimalSeparator;
				if (length == 1 || numberDecimalSeparator.Length != 1)
				{
					number = default(SmallDecimal);
					return false;
				}
				int num2 = s.IndexOf(numberDecimalSeparator[0], offset, length);
				if (num2 == -1)
				{
					number = default(SmallDecimal);
					return false;
				}
				int num3 = num2 - offset;
				int num4 = length - (num3 + 1);
				int num5 = 0;
				if (num3 > 0)
				{
					num5 = SmallDecimalParser.ParseDigits(s, offset, num3);
					if (num5 == -1)
					{
						number = default(SmallDecimal);
						return false;
					}
				}
				if (num4 == 0)
				{
					number = new SmallDecimal(num5);
					return true;
				}
				if (num4 <= 5)
				{
					int num6 = SmallDecimalParser.ParseDigits(s, num2 + 1, num4);
					if (num6 != -1)
					{
						int num7 = SmallDecimalParser.Power10(num4);
						int num8 = num5 * num7 + num6;
						number = new SmallDecimal(num8, num7);
						return true;
					}
				}
			}
			number = default(SmallDecimal);
			return false;
		}

		// Token: 0x04004D51 RID: 19793
		private static readonly int[] power10 = new int[] { 1, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000 };
	}
}
