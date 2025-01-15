using System;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005D8 RID: 1496
	public interface IBounded<TUnit> where TUnit : BoundsUnit
	{
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600205A RID: 8282
		Bounds<TUnit> Bounds { get; }
	}
}
