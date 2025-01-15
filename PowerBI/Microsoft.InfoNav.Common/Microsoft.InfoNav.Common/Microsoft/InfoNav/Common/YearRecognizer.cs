using System;
using System.Globalization;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200007F RID: 127
	internal static class YearRecognizer
	{
		// Token: 0x060004BA RID: 1210 RVA: 0x0000C578 File Offset: 0x0000A778
		internal static bool IsLikelyYearValue(long value, CultureInfo culture)
		{
			int twoDigitYearMax = culture.Calendar.TwoDigitYearMax;
			int yearRangeStart = YearRecognizer.GetYearRangeStart(twoDigitYearMax);
			int yearRangeEnd = YearRecognizer.GetYearRangeEnd(twoDigitYearMax);
			return value >= (long)yearRangeStart && value <= (long)yearRangeEnd;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		internal static int GetDecadeRangeStart(CultureInfo culture)
		{
			return YearRecognizer.GetYearRangeStart(culture.Calendar.TwoDigitYearMax) / 10 * 10;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
		internal static int GetDecadeRangeEnd(CultureInfo culture)
		{
			return YearRecognizer.GetYearRangeEnd(culture.Calendar.TwoDigitYearMax) / 10 * 10;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000C5DC File Offset: 0x0000A7DC
		private static int GetYearRangeEnd(int yearCutoff)
		{
			return yearCutoff;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000C5DF File Offset: 0x0000A7DF
		private static int GetYearRangeStart(int yearCutoff)
		{
			return yearCutoff - 629;
		}

		// Token: 0x04000111 RID: 273
		private const int YearsBeforeCutoff = 629;

		// Token: 0x04000112 RID: 274
		private const int YearsAfterCutoff = 0;
	}
}
