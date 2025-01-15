using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200020D RID: 525
	internal class StringEqualityComparer : IEqualityComparer<string>
	{
		// Token: 0x060013E6 RID: 5094 RVA: 0x00051779 File Offset: 0x0004F979
		private StringEqualityComparer()
		{
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x00051781 File Offset: 0x0004F981
		public bool Equals(string str1, string str2)
		{
			return string.Equals(str1, str2, StringComparison.Ordinal);
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0005178B File Offset: 0x0004F98B
		public int GetHashCode(string str)
		{
			return str.GetHashCode();
		}

		// Token: 0x0400096D RID: 2413
		internal static readonly IEqualityComparer<string> Instance = new StringEqualityComparer();
	}
}
