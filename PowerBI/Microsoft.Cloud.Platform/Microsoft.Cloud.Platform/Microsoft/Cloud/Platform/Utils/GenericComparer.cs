using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200021F RID: 543
	public sealed class GenericComparer<T, K> : IEqualityComparer<T>
	{
		// Token: 0x06000E56 RID: 3670 RVA: 0x00032868 File Offset: 0x00030A68
		public GenericComparer(Func<T, K> selector)
			: this(selector, EqualityComparer<K>.Default)
		{
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00032876 File Offset: 0x00030A76
		public GenericComparer(Func<T, K> selector, IEqualityComparer<K> keyComparer)
		{
			this.m_selector = selector;
			this.m_keyComparer = keyComparer;
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0003288C File Offset: 0x00030A8C
		public bool Equals(T x, T y)
		{
			return this.m_keyComparer.Equals(this.m_selector(x), this.m_selector(y));
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x000328B1 File Offset: 0x00030AB1
		public int GetHashCode(T obj)
		{
			return this.m_keyComparer.GetHashCode(this.m_selector(obj));
		}

		// Token: 0x0400058E RID: 1422
		private readonly Func<T, K> m_selector;

		// Token: 0x0400058F RID: 1423
		private readonly IEqualityComparer<K> m_keyComparer;
	}
}
