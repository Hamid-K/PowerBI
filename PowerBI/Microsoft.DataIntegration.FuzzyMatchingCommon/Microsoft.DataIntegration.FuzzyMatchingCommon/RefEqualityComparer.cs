using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	public struct RefEqualityComparer<T> : IEqualityComparer<T>
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002436 File Offset: 0x00000636
		public int GetHashCode(T i)
		{
			return EqualityComparer<T>.Default.GetHashCode(i);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002443 File Offset: 0x00000643
		public bool Equals(T x, T y)
		{
			return EqualityComparer<T>.Default.Equals(x, y);
		}

		// Token: 0x0400000F RID: 15
		public static readonly RefEqualityComparer<T> Instance;
	}
}
