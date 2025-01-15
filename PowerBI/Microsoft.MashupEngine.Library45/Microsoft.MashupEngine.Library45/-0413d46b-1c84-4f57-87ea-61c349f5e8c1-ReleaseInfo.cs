using System;
using System.Globalization;

// Token: 0x02000003 RID: 3
internal static class <0413d46b-1c84-4f57-87ea-61c349f5e8c1>ReleaseInfo
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002080 File Offset: 0x00000280
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
