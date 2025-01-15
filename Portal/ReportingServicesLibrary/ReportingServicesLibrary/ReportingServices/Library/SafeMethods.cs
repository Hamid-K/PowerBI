using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000123 RID: 291
	internal static class SafeMethods
	{
		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002B83B File Offset: 0x00029A3B
		public static void AddRange<T>(List<T> target, IEnumerable<T> source)
		{
			if (target != null && source != null)
			{
				target.AddRange(source);
			}
		}
	}
}
