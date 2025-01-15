using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005C7 RID: 1479
	public class CompareBySizeAscending<TUnit> : IComparer<Bounds<TUnit>> where TUnit : BoundsUnit
	{
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x0005C147 File Offset: 0x0005A347
		public static CompareBySizeAscending<TUnit> Instance { get; } = new CompareBySizeAscending<TUnit>();

		// Token: 0x0600201D RID: 8221 RVA: 0x0005C150 File Offset: 0x0005A350
		public int Compare(Bounds<TUnit> x, Bounds<TUnit> y)
		{
			int num;
			if (ComparableUtilities.TryHandleNullVariables(x, y, out num))
			{
				return num;
			}
			return y.Area() - x.Area();
		}
	}
}
