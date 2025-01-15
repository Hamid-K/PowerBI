using System;
using System.Globalization;

// Token: 0x02001B6C RID: 7020
internal static class <5e073af6-5659-4993-aeb5-f8179006d689>ReleaseInfo
{
	// Token: 0x0600AF98 RID: 44952 RVA: 0x0023EF20 File Offset: 0x0023D120
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x0600AF99 RID: 44953 RVA: 0x0023EF50 File Offset: 0x0023D150
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
