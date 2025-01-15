using System;
using System.Globalization;

// Token: 0x02001BD2 RID: 7122
internal static class <56679c40-65e8-425c-b862-5448311d8618>ReleaseInfo
{
	// Token: 0x0600B1B4 RID: 45492 RVA: 0x00243F40 File Offset: 0x00242140
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x0600B1B5 RID: 45493 RVA: 0x00243F70 File Offset: 0x00242170
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
