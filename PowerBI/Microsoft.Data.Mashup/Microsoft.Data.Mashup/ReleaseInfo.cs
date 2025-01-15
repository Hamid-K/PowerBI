using System;
using System.Globalization;

// Token: 0x02000003 RID: 3
internal static class ReleaseInfo
{
	// Token: 0x06000007 RID: 7 RVA: 0x00002164 File Offset: 0x00000364
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002194 File Offset: 0x00000394
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
