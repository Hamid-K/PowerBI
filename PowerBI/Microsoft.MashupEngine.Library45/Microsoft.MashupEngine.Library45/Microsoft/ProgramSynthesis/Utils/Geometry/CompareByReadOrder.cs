using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005C5 RID: 1477
	public class CompareByReadOrder<TUnit> : IComparer<Bounds<TUnit>> where TUnit : BoundsUnit
	{
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06002014 RID: 8212 RVA: 0x0005C057 File Offset: 0x0005A257
		public static CompareByReadOrder<TUnit> Instance { get; } = new CompareByReadOrder<TUnit>();

		// Token: 0x06002015 RID: 8213 RVA: 0x0005C060 File Offset: 0x0005A260
		public int Compare(Bounds<TUnit> x, Bounds<TUnit> y)
		{
			int num;
			if (ComparableUtilities.TryHandleNullVariables(x, y, out num))
			{
				return num;
			}
			if (x.Top != y.Top)
			{
				return x.Top - y.Top;
			}
			if (x.Left != y.Left)
			{
				return x.Left - y.Left;
			}
			if (x.Bottom != y.Bottom)
			{
				return x.Bottom - y.Bottom;
			}
			return x.Right - y.Right;
		}
	}
}
