using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000036 RID: 54
	internal class StringWithQualityHeaderValueComparer : IComparer<StringWithQualityHeaderValue>
	{
		// Token: 0x06000218 RID: 536 RVA: 0x00004BD2 File Offset: 0x00002DD2
		private StringWithQualityHeaderValueComparer()
		{
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000072C9 File Offset: 0x000054C9
		public static StringWithQualityHeaderValueComparer QualityComparer
		{
			get
			{
				return StringWithQualityHeaderValueComparer._qualityComparer;
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000072D0 File Offset: 0x000054D0
		public int Compare(StringWithQualityHeaderValue stringWithQuality1, StringWithQualityHeaderValue stringWithQuality2)
		{
			double num = stringWithQuality1.Quality ?? 1.0;
			double num2 = stringWithQuality2.Quality ?? 1.0;
			double num3 = num - num2;
			if (num3 < 0.0)
			{
				return -1;
			}
			if (num3 > 0.0)
			{
				return 1;
			}
			if (!string.Equals(stringWithQuality1.Value, stringWithQuality2.Value, StringComparison.OrdinalIgnoreCase))
			{
				if (string.Equals(stringWithQuality1.Value, "*", StringComparison.OrdinalIgnoreCase))
				{
					return -1;
				}
				if (string.Equals(stringWithQuality2.Value, "*", StringComparison.OrdinalIgnoreCase))
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x0400009E RID: 158
		private static readonly StringWithQualityHeaderValueComparer _qualityComparer = new StringWithQualityHeaderValueComparer();
	}
}
