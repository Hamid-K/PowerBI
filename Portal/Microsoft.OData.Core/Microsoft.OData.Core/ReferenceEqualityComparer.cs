using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.OData
{
	// Token: 0x020000CB RID: 203
	internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x06000968 RID: 2408 RVA: 0x000036A9 File Offset: 0x000018A9
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00017498 File Offset: 0x00015698
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

		// Token: 0x0600096A RID: 2410 RVA: 0x000174C4 File Offset: 0x000156C4
		public bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x000174D4 File Offset: 0x000156D4
		public int GetHashCode(T obj)
		{
			if (obj != null)
			{
				return obj.GetHashCode();
			}
			return 0;
		}

		// Token: 0x04000348 RID: 840
		private static ReferenceEqualityComparer<T> instance;
	}
}
