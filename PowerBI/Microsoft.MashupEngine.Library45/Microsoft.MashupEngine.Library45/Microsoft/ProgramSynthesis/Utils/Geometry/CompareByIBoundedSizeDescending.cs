using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005DF RID: 1503
	public class CompareByIBoundedSizeDescending<TUnit> : IComparer<IBounded<TUnit>> where TUnit : BoundsUnit
	{
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600206F RID: 8303 RVA: 0x0005C71C File Offset: 0x0005A91C
		public static CompareByIBoundedSizeDescending<TUnit> Instance { get; } = new CompareByIBoundedSizeDescending<TUnit>();

		// Token: 0x06002070 RID: 8304 RVA: 0x0005C723 File Offset: 0x0005A923
		public static CompareByIBoundedSizeDescending<TBounded, TUnit> TypedInstance<TBounded>() where TBounded : IBounded<TUnit>
		{
			return CompareByIBoundedSizeDescending<TBounded, TUnit>.Instance;
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x0005C72C File Offset: 0x0005A92C
		public int Compare(IBounded<TUnit> x, IBounded<TUnit> y)
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
