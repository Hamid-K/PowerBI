using System;
using System.Globalization;

// Token: 0x02001EDE RID: 7902
internal static class <0d3e6907-f981-4ad2-9c06-8455fdc008a7>ReleaseInfo
{
	// Token: 0x06010A89 RID: 68233 RVA: 0x00395A08 File Offset: 0x00393C08
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x06010A8A RID: 68234 RVA: 0x00395A38 File Offset: 0x00393C38
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
