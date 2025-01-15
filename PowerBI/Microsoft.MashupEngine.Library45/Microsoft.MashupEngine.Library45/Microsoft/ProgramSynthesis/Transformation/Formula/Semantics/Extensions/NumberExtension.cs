using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions
{
	// Token: 0x0200178F RID: 6031
	public static class NumberExtension
	{
		// Token: 0x0600C7BE RID: 51134 RVA: 0x002AE0A7 File Offset: 0x002AC2A7
		public static bool BetweenInclusive(this int subject, int lowerLimit, int upperLimit)
		{
			return lowerLimit <= subject && subject <= upperLimit;
		}

		// Token: 0x0600C7BF RID: 51135 RVA: 0x002AE0B6 File Offset: 0x002AC2B6
		public static bool Between(this int subject, int lowerLimit, int upperLimit)
		{
			return lowerLimit < subject && subject < upperLimit;
		}

		// Token: 0x0600C7C0 RID: 51136 RVA: 0x002AE0C2 File Offset: 0x002AC2C2
		public static bool Between(this int? subject, int lowerLimit, int upperLimit)
		{
			return subject != null && subject.GetValueOrDefault().Between(lowerLimit, upperLimit);
		}

		// Token: 0x0600C7C1 RID: 51137 RVA: 0x002AE0DD File Offset: 0x002AC2DD
		public static bool BetweenInclusive(this int? subject, int lowerLimit, int upperLimit)
		{
			return subject != null && subject.GetValueOrDefault().BetweenInclusive(lowerLimit, upperLimit);
		}

		// Token: 0x0600C7C2 RID: 51138 RVA: 0x002AE0F8 File Offset: 0x002AC2F8
		public static bool Between(this double subject, double lowerLimit, double upperLimit, bool inclusive = true)
		{
			if (!inclusive)
			{
				return lowerLimit < subject && subject < upperLimit;
			}
			return lowerLimit <= subject && subject <= upperLimit;
		}

		// Token: 0x0600C7C3 RID: 51139 RVA: 0x002AE118 File Offset: 0x002AC318
		public static bool Between(this double? subject, double lowerLimit, double upperLimit, bool inclusive = true)
		{
			if (!inclusive)
			{
				double? num = subject;
				if ((lowerLimit < num.GetValueOrDefault()) & (num != null))
				{
					num = subject;
					return (num.GetValueOrDefault() < upperLimit) & (num != null);
				}
				return false;
			}
			else
			{
				double? num = subject;
				if ((lowerLimit <= num.GetValueOrDefault()) & (num != null))
				{
					num = subject;
					return (num.GetValueOrDefault() <= upperLimit) & (num != null);
				}
				return false;
			}
		}

		// Token: 0x0600C7C4 RID: 51140 RVA: 0x002AE18B File Offset: 0x002AC38B
		public static bool Between(this decimal subject, decimal lowerLimit, decimal upperLimit, bool inclusive = true)
		{
			if (!inclusive)
			{
				return lowerLimit < subject && subject < upperLimit;
			}
			return lowerLimit <= subject && subject <= upperLimit;
		}

		// Token: 0x0600C7C5 RID: 51141 RVA: 0x002AE1B8 File Offset: 0x002AC3B8
		public static bool Between(this decimal? subject, decimal lowerLimit, decimal upperLimit, bool inclusive = true)
		{
			if (!inclusive)
			{
				decimal? num = subject;
				if ((lowerLimit < num.GetValueOrDefault()) & (num != null))
				{
					num = subject;
					return (num.GetValueOrDefault() < upperLimit) & (num != null);
				}
				return false;
			}
			else
			{
				decimal? num = subject;
				if ((lowerLimit <= num.GetValueOrDefault()) & (num != null))
				{
					num = subject;
					return (num.GetValueOrDefault() <= upperLimit) & (num != null);
				}
				return false;
			}
		}

		// Token: 0x0600C7C6 RID: 51142 RVA: 0x002AE231 File Offset: 0x002AC431
		public static bool IsValidDelimiter(this int index, string subject)
		{
			return index.IsValidIndex(subject) && subject[index].IsDelimiter();
		}

		// Token: 0x0600C7C7 RID: 51143 RVA: 0x002AE24A File Offset: 0x002AC44A
		public static bool IsValidIndex(this int? index, string subject)
		{
			return index != null && index.Value.IsValidIndex(subject);
		}

		// Token: 0x0600C7C8 RID: 51144 RVA: 0x002AE264 File Offset: 0x002AC464
		public static bool IsValidIndex(this int index, string subject)
		{
			return 0 <= index && index < subject.Length;
		}

		// Token: 0x0600C7C9 RID: 51145 RVA: 0x002AE275 File Offset: 0x002AC475
		public static bool IsValidIndex<T>(this int index, ReadOnlySpan<T> subject)
		{
			return 0 <= index && index < subject.Length;
		}

		// Token: 0x0600C7CA RID: 51146 RVA: 0x002AE287 File Offset: 0x002AC487
		public static int ToValidIndex(this int position)
		{
			if (position <= 0)
			{
				return position;
			}
			return position - 1;
		}

		// Token: 0x0600C7CB RID: 51147 RVA: 0x002AE292 File Offset: 0x002AC492
		public static int ToValidPosition(this int index)
		{
			if (index < 0)
			{
				return index;
			}
			return index + 1;
		}
	}
}
