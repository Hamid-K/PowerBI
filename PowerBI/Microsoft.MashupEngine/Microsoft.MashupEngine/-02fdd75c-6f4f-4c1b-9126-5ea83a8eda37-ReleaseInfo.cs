using System;
using System.Globalization;

// Token: 0x02001E53 RID: 7763
internal static class <02fdd75c-6f4f-4c1b-9126-5ea83a8eda37>ReleaseInfo
{
	// Token: 0x0600BEA6 RID: 48806 RVA: 0x00268EC0 File Offset: 0x002670C0
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x0600BEA7 RID: 48807 RVA: 0x00268EF0 File Offset: 0x002670F0
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
