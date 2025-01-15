using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x0200008B RID: 139
	internal class TimestampComparer<T> : IComparer<T> where T : ITimestamp
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x0000E898 File Offset: 0x0000CA98
		public int Compare(T x, T y)
		{
			return x.Timestamp.CompareTo(y.Timestamp);
		}
	}
}
