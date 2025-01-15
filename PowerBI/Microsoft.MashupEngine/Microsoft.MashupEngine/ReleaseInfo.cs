using System;
using System.Globalization;

// Token: 0x020020C3 RID: 8387
internal static class ReleaseInfo
{
	// Token: 0x0600CD60 RID: 52576 RVA: 0x0028D460 File Offset: 0x0028B660
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x0600CD61 RID: 52577 RVA: 0x0028D490 File Offset: 0x0028B690
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
