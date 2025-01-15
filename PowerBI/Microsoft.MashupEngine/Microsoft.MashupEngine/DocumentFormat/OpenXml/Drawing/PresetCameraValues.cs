using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200284C RID: 10316
	[GeneratedCode("DomGen", "2.0")]
	internal enum PresetCameraValues
	{
		// Token: 0x04008A07 RID: 35335
		[EnumString("legacyObliqueTopLeft")]
		LegacyObliqueTopLeft,
		// Token: 0x04008A08 RID: 35336
		[EnumString("legacyObliqueTop")]
		LegacyObliqueTop,
		// Token: 0x04008A09 RID: 35337
		[EnumString("legacyObliqueTopRight")]
		LegacyObliqueTopRight,
		// Token: 0x04008A0A RID: 35338
		[EnumString("legacyObliqueLeft")]
		LegacyObliqueLeft,
		// Token: 0x04008A0B RID: 35339
		[EnumString("legacyObliqueFront")]
		LegacyObliqueFront,
		// Token: 0x04008A0C RID: 35340
		[EnumString("legacyObliqueRight")]
		LegacyObliqueRight,
		// Token: 0x04008A0D RID: 35341
		[EnumString("legacyObliqueBottomLeft")]
		LegacyObliqueBottomLeft,
		// Token: 0x04008A0E RID: 35342
		[EnumString("legacyObliqueBottom")]
		LegacyObliqueBottom,
		// Token: 0x04008A0F RID: 35343
		[EnumString("legacyObliqueBottomRight")]
		LegacyObliqueBottomRight,
		// Token: 0x04008A10 RID: 35344
		[EnumString("legacyPerspectiveTopLeft")]
		LegacyPerspectiveTopLeft,
		// Token: 0x04008A11 RID: 35345
		[EnumString("legacyPerspectiveTop")]
		LegacyPerspectiveTop,
		// Token: 0x04008A12 RID: 35346
		[EnumString("legacyPerspectiveTopRight")]
		LegacyPerspectiveTopRight,
		// Token: 0x04008A13 RID: 35347
		[EnumString("legacyPerspectiveLeft")]
		LegacyPerspectiveLeft,
		// Token: 0x04008A14 RID: 35348
		[EnumString("legacyPerspectiveFront")]
		LegacyPerspectiveFront,
		// Token: 0x04008A15 RID: 35349
		[EnumString("legacyPerspectiveRight")]
		LegacyPerspectiveRight,
		// Token: 0x04008A16 RID: 35350
		[EnumString("legacyPerspectiveBottomLeft")]
		LegacyPerspectiveBottomLeft,
		// Token: 0x04008A17 RID: 35351
		[EnumString("legacyPerspectiveBottom")]
		LegacyPerspectiveBottom,
		// Token: 0x04008A18 RID: 35352
		[EnumString("legacyPerspectiveBottomRight")]
		LegacyPerspectiveBottomRight,
		// Token: 0x04008A19 RID: 35353
		[EnumString("orthographicFront")]
		OrthographicFront,
		// Token: 0x04008A1A RID: 35354
		[EnumString("isometricTopUp")]
		IsometricTopUp,
		// Token: 0x04008A1B RID: 35355
		[EnumString("isometricTopDown")]
		IsometricTopDown,
		// Token: 0x04008A1C RID: 35356
		[EnumString("isometricBottomUp")]
		IsometricBottomUp,
		// Token: 0x04008A1D RID: 35357
		[EnumString("isometricBottomDown")]
		IsometricBottomDown,
		// Token: 0x04008A1E RID: 35358
		[EnumString("isometricLeftUp")]
		IsometricLeftUp,
		// Token: 0x04008A1F RID: 35359
		[EnumString("isometricLeftDown")]
		IsometricLeftDown,
		// Token: 0x04008A20 RID: 35360
		[EnumString("isometricRightUp")]
		IsometricRightUp,
		// Token: 0x04008A21 RID: 35361
		[EnumString("isometricRightDown")]
		IsometricRightDown,
		// Token: 0x04008A22 RID: 35362
		[EnumString("isometricOffAxis1Left")]
		IsometricOffAxis1Left,
		// Token: 0x04008A23 RID: 35363
		[EnumString("isometricOffAxis1Right")]
		IsometricOffAxis1Right,
		// Token: 0x04008A24 RID: 35364
		[EnumString("isometricOffAxis1Top")]
		IsometricOffAxis1Top,
		// Token: 0x04008A25 RID: 35365
		[EnumString("isometricOffAxis2Left")]
		IsometricOffAxis2Left,
		// Token: 0x04008A26 RID: 35366
		[EnumString("isometricOffAxis2Right")]
		IsometricOffAxis2Right,
		// Token: 0x04008A27 RID: 35367
		[EnumString("isometricOffAxis2Top")]
		IsometricOffAxis2Top,
		// Token: 0x04008A28 RID: 35368
		[EnumString("isometricOffAxis3Left")]
		IsometricOffAxis3Left,
		// Token: 0x04008A29 RID: 35369
		[EnumString("isometricOffAxis3Right")]
		IsometricOffAxis3Right,
		// Token: 0x04008A2A RID: 35370
		[EnumString("isometricOffAxis3Bottom")]
		IsometricOffAxis3Bottom,
		// Token: 0x04008A2B RID: 35371
		[EnumString("isometricOffAxis4Left")]
		IsometricOffAxis4Left,
		// Token: 0x04008A2C RID: 35372
		[EnumString("isometricOffAxis4Right")]
		IsometricOffAxis4Right,
		// Token: 0x04008A2D RID: 35373
		[EnumString("isometricOffAxis4Bottom")]
		IsometricOffAxis4Bottom,
		// Token: 0x04008A2E RID: 35374
		[EnumString("obliqueTopLeft")]
		ObliqueTopLeft,
		// Token: 0x04008A2F RID: 35375
		[EnumString("obliqueTop")]
		ObliqueTop,
		// Token: 0x04008A30 RID: 35376
		[EnumString("obliqueTopRight")]
		ObliqueTopRight,
		// Token: 0x04008A31 RID: 35377
		[EnumString("obliqueLeft")]
		ObliqueLeft,
		// Token: 0x04008A32 RID: 35378
		[EnumString("obliqueRight")]
		ObliqueRight,
		// Token: 0x04008A33 RID: 35379
		[EnumString("obliqueBottomLeft")]
		ObliqueBottomLeft,
		// Token: 0x04008A34 RID: 35380
		[EnumString("obliqueBottom")]
		ObliqueBottom,
		// Token: 0x04008A35 RID: 35381
		[EnumString("obliqueBottomRight")]
		ObliqueBottomRight,
		// Token: 0x04008A36 RID: 35382
		[EnumString("perspectiveFront")]
		PerspectiveFront,
		// Token: 0x04008A37 RID: 35383
		[EnumString("perspectiveLeft")]
		PerspectiveLeft,
		// Token: 0x04008A38 RID: 35384
		[EnumString("perspectiveRight")]
		PerspectiveRight,
		// Token: 0x04008A39 RID: 35385
		[EnumString("perspectiveAbove")]
		PerspectiveAbove,
		// Token: 0x04008A3A RID: 35386
		[EnumString("perspectiveBelow")]
		PerspectiveBelow,
		// Token: 0x04008A3B RID: 35387
		[EnumString("perspectiveAboveLeftFacing")]
		PerspectiveAboveLeftFacing,
		// Token: 0x04008A3C RID: 35388
		[EnumString("perspectiveAboveRightFacing")]
		PerspectiveAboveRightFacing,
		// Token: 0x04008A3D RID: 35389
		[EnumString("perspectiveContrastingLeftFacing")]
		PerspectiveContrastingLeftFacing,
		// Token: 0x04008A3E RID: 35390
		[EnumString("perspectiveContrastingRightFacing")]
		PerspectiveContrastingRightFacing,
		// Token: 0x04008A3F RID: 35391
		[EnumString("perspectiveHeroicLeftFacing")]
		PerspectiveHeroicLeftFacing,
		// Token: 0x04008A40 RID: 35392
		[EnumString("perspectiveHeroicRightFacing")]
		PerspectiveHeroicRightFacing,
		// Token: 0x04008A41 RID: 35393
		[EnumString("perspectiveHeroicExtremeLeftFacing")]
		PerspectiveHeroicExtremeLeftFacing,
		// Token: 0x04008A42 RID: 35394
		[EnumString("perspectiveHeroicExtremeRightFacing")]
		PerspectiveHeroicExtremeRightFacing,
		// Token: 0x04008A43 RID: 35395
		[EnumString("perspectiveRelaxed")]
		PerspectiveRelaxed,
		// Token: 0x04008A44 RID: 35396
		[EnumString("perspectiveRelaxedModerately")]
		PerspectiveRelaxedModerately
	}
}
