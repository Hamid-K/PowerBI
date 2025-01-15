using System;
using System.Globalization;

// Token: 0x02001F11 RID: 7953
internal static class <4ce3c257-7c00-45b1-aef0-90a9b8d93413>ReleaseInfo
{
	// Token: 0x06010BDD RID: 68573 RVA: 0x0039B1DC File Offset: 0x003993DC
	public static string GetMonthYear(CultureInfo culture)
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("Y", culture);
	}

	// Token: 0x06010BDE RID: 68574 RVA: 0x0039B20C File Offset: 0x0039940C
	public static string GetFileMetadataReleaseInfo()
	{
		return DateTime.ParseExact("07/08/2024", "M/d/yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM", CultureInfo.InvariantCulture);
	}
}
