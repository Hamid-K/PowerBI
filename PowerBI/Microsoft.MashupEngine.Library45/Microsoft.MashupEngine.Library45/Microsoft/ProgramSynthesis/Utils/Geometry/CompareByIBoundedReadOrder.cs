using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005E1 RID: 1505
	public class CompareByIBoundedReadOrder<TUnit> : IComparer<IBounded<TUnit>> where TUnit : BoundsUnit
	{
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x0005C7D3 File Offset: 0x0005A9D3
		public static CompareByIBoundedReadOrder<TUnit> Instance { get; } = new CompareByIBoundedReadOrder<TUnit>();

		// Token: 0x06002079 RID: 8313 RVA: 0x0005C7DC File Offset: 0x0005A9DC
		public int Compare(IBounded<TUnit> x, IBounded<TUnit> y)
		{
			int num;
			if (ComparableUtilities.TryHandleNullVariables(x, y, out num))
			{
				return num;
			}
			return CompareByReadOrder<TUnit>.Instance.Compare(x.Bounds, y.Bounds);
		}
	}
}
