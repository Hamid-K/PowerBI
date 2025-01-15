using System;
using System.Globalization;

// Token: 0x02000163 RID: 355
internal static class <852722f5-39b4-4b53-9912-190b57c3471e>ReleaseInfo
{
	// Token: 0x060006C4 RID: 1732 RVA: 0x0000B064 File Offset: 0x00009264
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x0000B094 File Offset: 0x00009294
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
