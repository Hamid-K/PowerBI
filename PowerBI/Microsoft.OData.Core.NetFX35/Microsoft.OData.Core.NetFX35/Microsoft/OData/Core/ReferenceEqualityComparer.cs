using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.OData.Core
{
	// Token: 0x020001B5 RID: 437
	internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x0600103D RID: 4157 RVA: 0x00038548 File Offset: 0x00036748
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x00038550 File Offset: 0x00036750
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

		// Token: 0x0600103F RID: 4159 RVA: 0x0003857C File Offset: 0x0003677C
		public bool Equals(T x, T y)
		{
			return object.ReferenceEquals(x, y);
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0003858F File Offset: 0x0003678F
		public int GetHashCode(T obj)
		{
			if (obj != null)
			{
				return obj.GetHashCode();
			}
			return 0;
		}

		// Token: 0x04000751 RID: 1873
		private static ReferenceEqualityComparer<T> instance;
	}
}
