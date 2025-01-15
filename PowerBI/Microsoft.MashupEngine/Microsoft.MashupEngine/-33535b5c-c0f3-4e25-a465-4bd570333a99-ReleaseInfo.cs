using System;
using System.Globalization;

// Token: 0x02002043 RID: 8259
internal static class <33535b5c-c0f3-4e25-a465-4bd570333a99>ReleaseInfo
{
	// Token: 0x0600CA27 RID: 51751 RVA: 0x00286B04 File Offset: 0x00284D04
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x0600CA28 RID: 51752 RVA: 0x00286B34 File Offset: 0x00284D34
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
