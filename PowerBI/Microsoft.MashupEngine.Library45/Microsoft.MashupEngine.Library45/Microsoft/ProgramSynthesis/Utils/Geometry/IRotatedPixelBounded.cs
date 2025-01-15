using System;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005DB RID: 1499
	public interface IRotatedPixelBounded : IApparentPixelBounded
	{
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600205D RID: 8285
		Bounds<PixelUnit> ApparentPixelBoundsWithoutRotation { get; }

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600205E RID: 8286
		float? RotationAngle { get; }

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600205F RID: 8287
		bool IsRotated { get; }

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06002060 RID: 8288
		bool IsRotatedByRightAngle { get; }
	}
}
