using System;
using System.Globalization;

// Token: 0x02001FD8 RID: 8152
internal static class <a43d2438-c7b5-4d25-b599-d5828fabc521>ReleaseInfo
{
	// Token: 0x0600C71D RID: 50973 RVA: 0x0027A6E4 File Offset: 0x002788E4
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x0600C71E RID: 50974 RVA: 0x0027A714 File Offset: 0x00278914
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
