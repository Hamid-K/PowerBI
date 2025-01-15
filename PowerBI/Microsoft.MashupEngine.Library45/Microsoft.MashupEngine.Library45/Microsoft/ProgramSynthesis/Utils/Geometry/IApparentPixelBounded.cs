using System;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005DC RID: 1500
	public interface IApparentPixelBounded
	{
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06002061 RID: 8289
		Bounds<PixelUnit> StablePixelBounds { get; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06002062 RID: 8290
		Bounds<PixelUnit> ApparentPixelBounds { get; }
	}
}
