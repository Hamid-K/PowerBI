using System;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005DD RID: 1501
	public struct ApparentPixelBoundsWrapper : IApparentPixelBounded
	{
		// Token: 0x06002063 RID: 8291 RVA: 0x0005C631 File Offset: 0x0005A831
		public ApparentPixelBoundsWrapper(Bounds<PixelUnit> stablePixelBounds, Bounds<PixelUnit> apparentPixelBounds)
		{
			this = default(ApparentPixelBoundsWrapper);
			this.StablePixelBounds = stablePixelBounds;
			this.ApparentPixelBounds = apparentPixelBounds;
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06002064 RID: 8292 RVA: 0x0005C648 File Offset: 0x0005A848
		public readonly Bounds<PixelUnit> StablePixelBounds { get; }

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x0005C650 File Offset: 0x0005A850
		public readonly Bounds<PixelUnit> ApparentPixelBounds { get; }
	}
}
