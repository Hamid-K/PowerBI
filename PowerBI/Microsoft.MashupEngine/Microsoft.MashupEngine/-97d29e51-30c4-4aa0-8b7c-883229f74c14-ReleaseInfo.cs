using System;
using System.Globalization;

// Token: 0x02002007 RID: 8199
internal static class <97d29e51-30c4-4aa0-8b7c-883229f74c14>ReleaseInfo
{
	// Token: 0x0600C7C6 RID: 51142 RVA: 0x0027BED0 File Offset: 0x0027A0D0
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x0600C7C7 RID: 51143 RVA: 0x0027BF00 File Offset: 0x0027A100
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
