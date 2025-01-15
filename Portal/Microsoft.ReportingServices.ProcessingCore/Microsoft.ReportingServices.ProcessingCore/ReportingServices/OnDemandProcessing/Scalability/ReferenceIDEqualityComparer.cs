using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000851 RID: 2129
	internal class ReferenceIDEqualityComparer : IEqualityComparer<ReferenceID>, IComparer<ReferenceID>
	{
		// Token: 0x060076C9 RID: 30409 RVA: 0x001EBB3B File Offset: 0x001E9D3B
		private ReferenceIDEqualityComparer()
		{
		}

		// Token: 0x060076CA RID: 30410 RVA: 0x001EBB43 File Offset: 0x001E9D43
		public bool Equals(ReferenceID x, ReferenceID y)
		{
			return x == y;
		}

		// Token: 0x060076CB RID: 30411 RVA: 0x001EBB4C File Offset: 0x001E9D4C
		public int GetHashCode(ReferenceID obj)
		{
			return (int)obj.Value;
		}

		// Token: 0x060076CC RID: 30412 RVA: 0x001EBB56 File Offset: 0x001E9D56
		public int Compare(ReferenceID x, ReferenceID y)
		{
			if (x < y)
			{
				return -1;
			}
			if (x > y)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04003C1A RID: 15386
		internal static readonly ReferenceIDEqualityComparer Instance = new ReferenceIDEqualityComparer();
	}
}
