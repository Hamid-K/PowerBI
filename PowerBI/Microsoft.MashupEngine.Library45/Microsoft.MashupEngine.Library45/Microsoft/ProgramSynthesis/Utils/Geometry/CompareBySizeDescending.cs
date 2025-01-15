using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005C6 RID: 1478
	public class CompareBySizeDescending<TUnit> : IComparer<Bounds<TUnit>> where TUnit : BoundsUnit
	{
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06002018 RID: 8216 RVA: 0x0005C0FF File Offset: 0x0005A2FF
		public static CompareBySizeDescending<TUnit> Instance { get; } = new CompareBySizeDescending<TUnit>();

		// Token: 0x06002019 RID: 8217 RVA: 0x0005C108 File Offset: 0x0005A308
		public int Compare(Bounds<TUnit> x, Bounds<TUnit> y)
		{
			int num;
			if (ComparableUtilities.TryHandleNullVariables(x, y, out num))
			{
				return num;
			}
			return x.Area() - y.Area();
		}
	}
}
