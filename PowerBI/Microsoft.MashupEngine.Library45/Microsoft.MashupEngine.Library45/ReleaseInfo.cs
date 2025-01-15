using System;
using System.Globalization;

// Token: 0x0200203F RID: 8255
internal static class ReleaseInfo
{
	// Token: 0x06011318 RID: 70424 RVA: 0x003B354C File Offset: 0x003B174C
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x06011319 RID: 70425 RVA: 0x003B357C File Offset: 0x003B177C
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
