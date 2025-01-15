using System;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005D9 RID: 1497
	public interface IPixelBounded : IBounded<PixelUnit>
	{
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600205B RID: 8283
		Bounds<PixelUnit> PixelBounds { get; }
	}
}
