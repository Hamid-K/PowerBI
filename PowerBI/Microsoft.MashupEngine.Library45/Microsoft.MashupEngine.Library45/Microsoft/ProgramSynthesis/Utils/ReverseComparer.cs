using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000504 RID: 1284
	public class ReverseComparer<T> : IComparer<T>
	{
		// Token: 0x06001CAE RID: 7342 RVA: 0x00055943 File Offset: 0x00053B43
		public ReverseComparer(IComparer<T> original)
		{
			this._original = original;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x00055952 File Offset: 0x00053B52
		public int Compare(T left, T right)
		{
			return this._original.Compare(right, left);
		}

		// Token: 0x04000DF6 RID: 3574
		private readonly IComparer<T> _original;
	}
}
