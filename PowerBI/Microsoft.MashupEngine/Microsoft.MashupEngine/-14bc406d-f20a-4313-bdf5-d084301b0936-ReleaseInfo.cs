using System;
using System.Globalization;

// Token: 0x02001C3B RID: 7227
internal static class <14bc406d-f20a-4313-bdf5-d084301b0936>ReleaseInfo
{
	// Token: 0x0600B46F RID: 46191 RVA: 0x002497F4 File Offset: 0x002479F4
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x0600B470 RID: 46192 RVA: 0x00249824 File Offset: 0x00247A24
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
