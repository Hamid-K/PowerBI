using System;
using System.Globalization;

// Token: 0x02001928 RID: 6440
internal static class <d73bbffb-07e5-4834-adda-26f61149df95>ReleaseInfo
{
	// Token: 0x0600A3C3 RID: 41923 RVA: 0x0021E1A0 File Offset: 0x0021C3A0
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x0600A3C4 RID: 41924 RVA: 0x0021E1D0 File Offset: 0x0021C3D0
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
