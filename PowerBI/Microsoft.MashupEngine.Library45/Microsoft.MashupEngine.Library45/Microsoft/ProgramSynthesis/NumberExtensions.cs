using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis
{
	// Token: 0x02000083 RID: 131
	public static class NumberExtensions
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000AF80 File Offset: 0x00009180
		public static bool EqualsWithTruncate(this decimal subject, decimal other)
		{
			int num = subject.Scale();
			int num2 = other.Scale();
			if (num != num2)
			{
				int num3 = Math.Min(num, num2);
				subject = subject.Truncate(num3);
				other = other.Truncate(num3);
			}
			return ValueEquality.Comparer.Equals(subject, other);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000AFCF File Offset: 0x000091CF
		public static bool EqualsWithTruncate(this double subject, double other)
		{
			return Convert.ToDecimal(subject).EqualsWithTruncate(Convert.ToDecimal(other));
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000AFE2 File Offset: 0x000091E2
		public static bool EqualsWithTruncate(this decimal subject, double other)
		{
			return subject.EqualsWithTruncate(Convert.ToDecimal(other));
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000AFF0 File Offset: 0x000091F0
		public static bool EqualsWithTruncate(this double subject, decimal other)
		{
			return Convert.ToDecimal(subject).EqualsWithTruncate(other);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000B000 File Offset: 0x00009200
		public static bool IsNumeric(this object value)
		{
			return value is long || value is int || value is uint || value is byte || value is double || value is float || value is decimal;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000B04C File Offset: 0x0000924C
		public static decimal? AsDecimal(this object value)
		{
			if (!value.IsNumeric())
			{
				return null;
			}
			return new decimal?(Convert.ToDecimal(value));
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000B076 File Offset: 0x00009276
		public static int Scale(this double subject)
		{
			return Convert.ToDecimal(subject).Scale();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000B083 File Offset: 0x00009283
		public static int Scale(this decimal subject)
		{
			return (int)BitConverter.GetBytes(decimal.GetBits(subject)[3])[2];
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000B094 File Offset: 0x00009294
		public static int Digits(this int subject)
		{
			int num = 0;
			while (subject > 0)
			{
				subject /= 10;
				num++;
			}
			return num;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000B0B4 File Offset: 0x000092B4
		public static int IntDigits(this decimal subject)
		{
			return Convert.ToInt32(subject).Digits();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000B0C1 File Offset: 0x000092C1
		public static int IntDigits(this double subject)
		{
			return Convert.ToInt32(subject).Digits();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000B0CE File Offset: 0x000092CE
		public static double Truncate(this double subject, int scale)
		{
			return Convert.ToDouble(Convert.ToDecimal(subject).Truncate(scale));
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000B0E4 File Offset: 0x000092E4
		public static decimal Truncate(this decimal subject, int scale)
		{
			decimal num = Math.Round(subject, scale);
			if (subject > 0m && num > subject)
			{
				return num - new decimal(1, 0, 0, false, (byte)scale);
			}
			if (subject < 0m && num < subject)
			{
				return num + new decimal(1, 0, 0, false, (byte)scale);
			}
			return num;
		}
	}
}
