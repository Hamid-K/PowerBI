using System;

namespace Microsoft.InfoNav
{
	// Token: 0x0200000F RID: 15
	internal static class DateTimeExtensions
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00002FDC File Offset: 0x000011DC
		internal static int GetDecade(this DateTime dateTime)
		{
			int year = dateTime.Year;
			return year - year % 10;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00002FEA File Offset: 0x000011EA
		internal static int GetShortDecade(this DateTime dateTime)
		{
			return dateTime.GetDecade() % 100;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00002FF5 File Offset: 0x000011F5
		internal static int GetCentury(this DateTime dateTime)
		{
			int year = dateTime.Year;
			return year - year % 100;
		}
	}
}
