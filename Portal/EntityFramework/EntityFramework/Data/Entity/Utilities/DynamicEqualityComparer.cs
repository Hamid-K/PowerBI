using System;
using System.Collections.Generic;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200007B RID: 123
	internal sealed class DynamicEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x0000FBD4 File Offset: 0x0000DDD4
		public DynamicEqualityComparer(Func<T, T, bool> func)
		{
			this._func = func;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000FBE3 File Offset: 0x0000DDE3
		public bool Equals(T x, T y)
		{
			return this._func(x, y);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000FBF2 File Offset: 0x0000DDF2
		public int GetHashCode(T obj)
		{
			return 0;
		}

		// Token: 0x04000112 RID: 274
		private readonly Func<T, T, bool> _func;
	}
}
