using System;
using System.Globalization;

// Token: 0x02002032 RID: 8242
internal static class <0d0892ab-6cda-4381-8b0e-95ac60f5244c>ReleaseInfo
{
	// Token: 0x060112C5 RID: 70341 RVA: 0x003B2458 File Offset: 0x003B0658
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x060112C6 RID: 70342 RVA: 0x003B2488 File Offset: 0x003B0688
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
