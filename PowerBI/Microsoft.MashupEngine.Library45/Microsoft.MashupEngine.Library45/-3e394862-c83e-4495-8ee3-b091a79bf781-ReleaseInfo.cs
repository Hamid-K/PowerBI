using System;
using System.Globalization;

// Token: 0x0200003C RID: 60
internal static class <3e394862-c83e-4495-8ee3-b091a79bf781>ReleaseInfo
{
	// Token: 0x06000138 RID: 312 RVA: 0x00006CA0 File Offset: 0x00004EA0
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00006CD0 File Offset: 0x00004ED0
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
