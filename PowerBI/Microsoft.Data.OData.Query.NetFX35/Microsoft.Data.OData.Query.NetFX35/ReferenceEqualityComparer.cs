using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Data.Experimental.OData
{
	// Token: 0x0200001F RID: 31
	internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003C14 File Offset: 0x00001E14
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003C1C File Offset: 0x00001E1C
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

		// Token: 0x06000082 RID: 130 RVA: 0x00003C48 File Offset: 0x00001E48
		public bool Equals(T x, T y)
		{
			return object.ReferenceEquals(x, y);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003C5B File Offset: 0x00001E5B
		public int GetHashCode(T obj)
		{
			if (obj != null)
			{
				return obj.GetHashCode();
			}
			return 0;
		}

		// Token: 0x040000F9 RID: 249
		private static ReferenceEqualityComparer<T> instance;
	}
}
