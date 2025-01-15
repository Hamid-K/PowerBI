using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005E0 RID: 1504
	public class CompareByIBoundedSizeDescending<TBounded, TUnit> : IComparer<TBounded> where TBounded : IBounded<TUnit> where TUnit : BoundsUnit
	{
		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x0005C76F File Offset: 0x0005A96F
		public static CompareByIBoundedSizeDescending<TBounded, TUnit> Instance { get; } = new CompareByIBoundedSizeDescending<TBounded, TUnit>();

		// Token: 0x06002075 RID: 8309 RVA: 0x0005C778 File Offset: 0x0005A978
		public int Compare(TBounded x, TBounded y)
		{
			int num;
			if (ComparableUtilities.TryHandleNullVariables(x, y, out num))
			{
				return num;
			}
			return x.Bounds.Area() - y.Bounds.Area();
		}
	}
}
