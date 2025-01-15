using System;
using System.Collections;

namespace Microsoft.OData.Client
{
	// Token: 0x0200008F RID: 143
	internal class ReferenceEqualityComparer : IEqualityComparer
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x0000347F File Offset: 0x0000167F
		protected ReferenceEqualityComparer()
		{
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000F406 File Offset: 0x0000D606
		bool IEqualityComparer.Equals(object x, object y)
		{
			return x == y;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000F40C File Offset: 0x0000D60C
		int IEqualityComparer.GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}
	}
}
