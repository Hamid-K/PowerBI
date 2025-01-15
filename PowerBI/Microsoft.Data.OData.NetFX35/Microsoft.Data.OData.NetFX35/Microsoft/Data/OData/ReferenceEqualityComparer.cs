using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Data.OData
{
	// Token: 0x02000277 RID: 631
	internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x060013B0 RID: 5040 RVA: 0x00049B80 File Offset: 0x00047D80
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00049B88 File Offset: 0x00047D88
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

		// Token: 0x060013B2 RID: 5042 RVA: 0x00049BB4 File Offset: 0x00047DB4
		public bool Equals(T x, T y)
		{
			return object.ReferenceEquals(x, y);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00049BC7 File Offset: 0x00047DC7
		public int GetHashCode(T obj)
		{
			if (obj != null)
			{
				return obj.GetHashCode();
			}
			return 0;
		}

		// Token: 0x0400078E RID: 1934
		private static ReferenceEqualityComparer<T> instance;
	}
}
