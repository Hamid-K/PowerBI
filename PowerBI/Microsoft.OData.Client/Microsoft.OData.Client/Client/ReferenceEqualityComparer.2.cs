using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.OData.Client
{
	// Token: 0x02000090 RID: 144
	internal sealed class ReferenceEqualityComparer<T> : ReferenceEqualityComparer, IEqualityComparer<T>
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x0000F419 File Offset: 0x0000D619
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000F424 File Offset: 0x0000D624
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

		// Token: 0x06000459 RID: 1113 RVA: 0x0000F450 File Offset: 0x0000D650
		public bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000F460 File Offset: 0x0000D660
		public int GetHashCode(T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x0400013A RID: 314
		private static ReferenceEqualityComparer<T> instance;
	}
}
