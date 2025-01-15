using System;
using System.Globalization;

// Token: 0x02000004 RID: 4
internal static class ReleaseInfo
{
	// Token: 0x06000006 RID: 6 RVA: 0x000020FC File Offset: 0x000002FC
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000212C File Offset: 0x0000032C
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
