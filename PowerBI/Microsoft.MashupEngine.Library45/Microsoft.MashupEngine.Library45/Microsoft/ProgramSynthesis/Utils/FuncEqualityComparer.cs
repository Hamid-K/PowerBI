using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200048B RID: 1163
	public class FuncEqualityComparer<T> : IEqualityComparer<T>
	{
		// Token: 0x06001A3A RID: 6714 RVA: 0x0004F3F0 File Offset: 0x0004D5F0
		public FuncEqualityComparer(Func<T, T, bool> comparer)
			: this(comparer, (T t) => 0)
		{
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x0004F418 File Offset: 0x0004D618
		public FuncEqualityComparer(Func<T, T, bool> comparer, Func<T, int> hash)
		{
			this._comparer = comparer;
			this._hash = hash;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x0004F42E File Offset: 0x0004D62E
		public bool Equals(T x, T y)
		{
			return this._comparer(x, y);
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0004F43D File Offset: 0x0004D63D
		public int GetHashCode(T obj)
		{
			return this._hash(obj);
		}

		// Token: 0x04000CEF RID: 3311
		private readonly Func<T, T, bool> _comparer;

		// Token: 0x04000CF0 RID: 3312
		private readonly Func<T, int> _hash;
	}
}
