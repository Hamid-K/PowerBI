using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200051F RID: 1311
	public class ValueComparison : IComparer, IComparer<object>
	{
		// Token: 0x06001D30 RID: 7472 RVA: 0x00002130 File Offset: 0x00000330
		private ValueComparison()
		{
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x00056C9B File Offset: 0x00054E9B
		public static ValueComparison Instance
		{
			get
			{
				return ValueComparison.Lazy.Value;
			}
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x00056CA7 File Offset: 0x00054EA7
		int IComparer<object>.Compare(object x, object y)
		{
			return this.Compare(x, y);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x00056CB4 File Offset: 0x00054EB4
		public int Compare(object x, object y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null || y == null || x.GetType() != y.GetType())
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Cannot compare arguments of different types: {0} and {1}", new object[] { x, y })));
			}
			return StructuralComparisons.StructuralComparer.Compare(x, y);
		}

		// Token: 0x04000E32 RID: 3634
		private static readonly Lazy<ValueComparison> Lazy = new Lazy<ValueComparison>(() => new ValueComparison());
	}
}
