using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200047D RID: 1149
	public static class ComparableUtilities
	{
		// Token: 0x060019FE RID: 6654 RVA: 0x0004E178 File Offset: 0x0004C378
		public static bool TryHandleNullVariables(object x, object y, out int result)
		{
			result = 0;
			if (x == null)
			{
				if (y == null)
				{
					return true;
				}
				result = -1;
				return true;
			}
			else
			{
				if (y == null)
				{
					result = 1;
					return true;
				}
				return false;
			}
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x0004E193 File Offset: 0x0004C393
		public static IComparer<T> ReverseComparer<T>(this IComparer<T> comparer)
		{
			return new ComparableUtilities.ReverseIComparer<T>(comparer);
		}

		// Token: 0x0200047E RID: 1150
		private class ReverseIComparer<T> : IComparer<T>
		{
			// Token: 0x06001A00 RID: 6656 RVA: 0x0004E19B File Offset: 0x0004C39B
			public ReverseIComparer(IComparer<T> comparer)
			{
				this._comparer = comparer;
			}

			// Token: 0x06001A01 RID: 6657 RVA: 0x0004E1AA File Offset: 0x0004C3AA
			public int Compare(T x, T y)
			{
				return -this._comparer.Compare(x, y);
			}

			// Token: 0x04000CDD RID: 3293
			private readonly IComparer<T> _comparer;
		}
	}
}
