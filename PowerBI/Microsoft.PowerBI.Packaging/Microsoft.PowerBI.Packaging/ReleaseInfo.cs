using System;
using System.Globalization;

// Token: 0x02000005 RID: 5
internal static class ReleaseInfo
{
	// Token: 0x0600000D RID: 13 RVA: 0x0000228C File Offset: 0x0000048C
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("09/09/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000022BC File Offset: 0x000004BC
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("09/09/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
