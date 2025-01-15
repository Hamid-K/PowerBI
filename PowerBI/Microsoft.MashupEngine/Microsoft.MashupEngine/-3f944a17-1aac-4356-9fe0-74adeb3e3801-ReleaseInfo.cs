using System;
using System.Globalization;

// Token: 0x0200194C RID: 6476
internal static class <3f944a17-1aac-4356-9fe0-74adeb3e3801>ReleaseInfo
{
	// Token: 0x0600A442 RID: 42050 RVA: 0x002202D4 File Offset: 0x0021E4D4
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x0600A443 RID: 42051 RVA: 0x00220304 File Offset: 0x0021E504
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
