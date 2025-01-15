using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav
{
	// Token: 0x02000027 RID: 39
	internal sealed class ReverseComparer<T> : IComparer<T>
	{
		// Token: 0x06000200 RID: 512 RVA: 0x000063AE File Offset: 0x000045AE
		internal ReverseComparer()
			: this(Comparer<T>.Default)
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000063BB File Offset: 0x000045BB
		internal ReverseComparer(IComparer<T> baseComparer)
		{
			this._baseComparer = baseComparer;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000063CA File Offset: 0x000045CA
		public int Compare(T x, T y)
		{
			return this._baseComparer.Compare(y, x);
		}

		// Token: 0x04000060 RID: 96
		private readonly IComparer<T> _baseComparer;
	}
}
