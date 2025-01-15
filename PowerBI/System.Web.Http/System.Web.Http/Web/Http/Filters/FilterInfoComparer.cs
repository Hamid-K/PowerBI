using System;
using System.Collections.Generic;

namespace System.Web.Http.Filters
{
	// Token: 0x020000CB RID: 203
	internal sealed class FilterInfoComparer : IComparer<FilterInfo>
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0000E077 File Offset: 0x0000C277
		public static FilterInfoComparer Instance
		{
			get
			{
				return FilterInfoComparer._instance;
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000E07E File Offset: 0x0000C27E
		public int Compare(FilterInfo x, FilterInfo y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			return x.Scope - y.Scope;
		}

		// Token: 0x04000137 RID: 311
		private static readonly FilterInfoComparer _instance = new FilterInfoComparer();
	}
}
