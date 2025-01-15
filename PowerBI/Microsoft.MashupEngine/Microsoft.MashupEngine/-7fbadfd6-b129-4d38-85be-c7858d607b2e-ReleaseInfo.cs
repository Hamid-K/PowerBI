using System;
using System.Globalization;

// Token: 0x0200014A RID: 330
internal static class <7fbadfd6-b129-4d38-85be-c7858d607b2e>ReleaseInfo
{
	// Token: 0x060005B5 RID: 1461 RVA: 0x000090CC File Offset: 0x000072CC
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x000090FC File Offset: 0x000072FC
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
