using System;
using System.Globalization;

// Token: 0x0200002A RID: 42
internal static class <9d499489-7606-41ac-b77b-5f31a68d089b>ReleaseInfo
{
	// Token: 0x060000E7 RID: 231 RVA: 0x00005EE4 File Offset: 0x000040E4
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00005F14 File Offset: 0x00004114
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
