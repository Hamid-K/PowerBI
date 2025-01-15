using System;
using System.Globalization;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005D7 RID: 1495
	internal static class DateTimeUtil
	{
		// Token: 0x060053E3 RID: 21475 RVA: 0x001615CC File Offset: 0x0015F7CC
		internal static bool TryParseDateTime(string strDateTime, CultureInfo formatProvider, out DateTimeOffset dateTimeOffset, out bool hasTimeOffset)
		{
			hasTimeOffset = false;
			if (!DateTimeOffset.TryParse(strDateTime, formatProvider, DateTimeStyles.None, out dateTimeOffset))
			{
				DateTimeFormatInfo dateTimeFormatInfo = ((formatProvider != null) ? formatProvider.DateTimeFormat : CultureInfo.CurrentCulture.DateTimeFormat);
				string[] allDateTimePatterns = dateTimeFormatInfo.GetAllDateTimePatterns('d');
				if (!DateTimeOffset.TryParseExact(strDateTime, allDateTimePatterns, formatProvider, DateTimeStyles.None, out dateTimeOffset))
				{
					string[] allDateTimePatterns2 = dateTimeFormatInfo.GetAllDateTimePatterns('G');
					if (!DateTimeOffset.TryParseExact(strDateTime, allDateTimePatterns2, formatProvider, DateTimeStyles.None, out dateTimeOffset))
					{
						for (int i = 0; i < allDateTimePatterns2.Length; i++)
						{
							string[] array = allDateTimePatterns2;
							int num = i;
							array[num] += " zzz";
						}
						if (!DateTimeOffset.TryParseExact(strDateTime, allDateTimePatterns2, formatProvider, DateTimeStyles.None, out dateTimeOffset))
						{
							return false;
						}
						hasTimeOffset = true;
					}
				}
				return true;
			}
			TimeSpan timeSpan;
			if (TimeSpan.TryParse(strDateTime, out timeSpan))
			{
				return false;
			}
			DateTimeOffset dateTimeOffset2;
			if (!DateTimeOffset.TryParse(strDateTime + " +0", formatProvider, DateTimeStyles.None, out dateTimeOffset2))
			{
				hasTimeOffset = true;
			}
			return true;
		}
	}
}
