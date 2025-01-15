using System;
using System.Globalization;

// Token: 0x020020AE RID: 8366
internal static class <066e9653-7285-491a-9c8c-a5080b4a41b9>ReleaseInfo
{
	// Token: 0x0600CCCF RID: 52431 RVA: 0x0028B4B0 File Offset: 0x002896B0
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x0600CCD0 RID: 52432 RVA: 0x0028B4E0 File Offset: 0x002896E0
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
