using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000026 RID: 38
	internal sealed class ReferenceEqualityComparer : IEqualityComparer<object>, IEqualityComparer
	{
		// Token: 0x06000140 RID: 320 RVA: 0x00003331 File Offset: 0x00001531
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00003339 File Offset: 0x00001539
		public static ReferenceEqualityComparer Instance { get; } = new ReferenceEqualityComparer();

		// Token: 0x06000142 RID: 322 RVA: 0x00003340 File Offset: 0x00001540
		public bool Equals(object x, object y)
		{
			return x == y;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00003346 File Offset: 0x00001546
		public int GetHashCode(object obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}
	}
}
