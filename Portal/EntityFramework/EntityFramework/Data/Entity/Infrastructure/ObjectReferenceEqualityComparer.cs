using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000259 RID: 601
	[Serializable]
	public sealed class ObjectReferenceEqualityComparer : IEqualityComparer<object>
	{
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x00055B1B File Offset: 0x00053D1B
		public static ObjectReferenceEqualityComparer Default
		{
			get
			{
				return ObjectReferenceEqualityComparer._default;
			}
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x00055B22 File Offset: 0x00053D22
		bool IEqualityComparer<object>.Equals(object x, object y)
		{
			return x == y;
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x00055B28 File Offset: 0x00053D28
		int IEqualityComparer<object>.GetHashCode(object obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x04000B39 RID: 2873
		private static readonly ObjectReferenceEqualityComparer _default = new ObjectReferenceEqualityComparer();
	}
}
