using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Microsoft.InfoNav
{
	// Token: 0x02000026 RID: 38
	[ImmutableObject(true)]
	internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x060001FC RID: 508 RVA: 0x0000637D File Offset: 0x0000457D
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006385 File Offset: 0x00004585
		public bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006395 File Offset: 0x00004595
		public int GetHashCode(T obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x0400005F RID: 95
		internal static readonly IEqualityComparer<T> Instance = new ReferenceEqualityComparer<T>();
	}
}
