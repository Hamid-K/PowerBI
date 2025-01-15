using System;
using System.Globalization;

// Token: 0x02000014 RID: 20
internal static class <6caf35ff-d5c3-4ef8-a633-e5d4ef838dd2>ReleaseInfo
{
	// Token: 0x06000061 RID: 97 RVA: 0x00003D50 File Offset: 0x00001F50
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00003D80 File Offset: 0x00001F80
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
