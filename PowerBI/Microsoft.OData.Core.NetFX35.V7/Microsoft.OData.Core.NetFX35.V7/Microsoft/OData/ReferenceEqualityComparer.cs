using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.OData
{
	// Token: 0x020000AD RID: 173
	internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x00002CFE File Offset: 0x00000EFE
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00012B7C File Offset: 0x00010D7C
		internal static ReferenceEqualityComparer<T> Instance
		{
			get
			{
				if (ReferenceEqualityComparer<T>.instance == null)
				{
					ReferenceEqualityComparer<T> referenceEqualityComparer = new ReferenceEqualityComparer<T>();
					Interlocked.CompareExchange<ReferenceEqualityComparer<T>>(ref ReferenceEqualityComparer<T>.instance, referenceEqualityComparer, null);
				}
				return ReferenceEqualityComparer<T>.instance;
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00012BA8 File Offset: 0x00010DA8
		public bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00012BB8 File Offset: 0x00010DB8
		public int GetHashCode(T obj)
		{
			if (obj != null)
			{
				return obj.GetHashCode();
			}
			return 0;
		}

		// Token: 0x040002E8 RID: 744
		private static ReferenceEqualityComparer<T> instance;
	}
}
