using System;
using System.Globalization;

// Token: 0x020004A9 RID: 1193
internal static class ReleaseInfo
{
	// Token: 0x06003453 RID: 13395 RVA: 0x00172378 File Offset: 0x00170578
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.Parse("10/1/2016", new CultureInfo("en-US")).ToString("Y", culture);
	}
}
