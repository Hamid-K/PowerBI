using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024DB RID: 9435
	[GeneratedCode("DomGen", "2.0")]
	internal enum PresetCameraTypeValues
	{
		// Token: 0x04007A58 RID: 31320
		[EnumString("legacyObliqueTopLeft")]
		LegacyObliqueTopLeft,
		// Token: 0x04007A59 RID: 31321
		[EnumString("legacyObliqueTop")]
		LegacyObliqueTop,
		// Token: 0x04007A5A RID: 31322
		[EnumString("legacyObliqueTopRight")]
		LegacyObliqueTopRight,
		// Token: 0x04007A5B RID: 31323
		[EnumString("legacyObliqueLeft")]
		LegacyObliqueLeft,
		// Token: 0x04007A5C RID: 31324
		[EnumString("legacyObliqueFront")]
		LegacyObliqueFront,
		// Token: 0x04007A5D RID: 31325
		[EnumString("legacyObliqueRight")]
		LegacyObliqueRight,
		// Token: 0x04007A5E RID: 31326
		[EnumString("legacyObliqueBottomLeft")]
		LegacyObliqueBottomLeft,
		// Token: 0x04007A5F RID: 31327
		[EnumString("legacyObliqueBottom")]
		LegacyObliqueBottom,
		// Token: 0x04007A60 RID: 31328
		[EnumString("legacyObliqueBottomRight")]
		LegacyObliqueBottomRight,
		// Token: 0x04007A61 RID: 31329
		[EnumString("legacyPerspectiveTopLeft")]
		LegacyPerspectiveTopLeft,
		// Token: 0x04007A62 RID: 31330
		[EnumString("legacyPerspectiveTop")]
		LegacyPerspectiveTop,
		// Token: 0x04007A63 RID: 31331
		[EnumString("legacyPerspectiveTopRight")]
		LegacyPerspectiveTopRight,
		// Token: 0x04007A64 RID: 31332
		[EnumString("legacyPerspectiveLeft")]
		LegacyPerspectiveLeft,
		// Token: 0x04007A65 RID: 31333
		[EnumString("legacyPerspectiveFront")]
		LegacyPerspectiveFront,
		// Token: 0x04007A66 RID: 31334
		[EnumString("legacyPerspectiveRight")]
		LegacyPerspectiveRight,
		// Token: 0x04007A67 RID: 31335
		[EnumString("legacyPerspectiveBottomLeft")]
		LegacyPerspectiveBottomLeft,
		// Token: 0x04007A68 RID: 31336
		[EnumString("legacyPerspectiveBottom")]
		LegacyPerspectiveBottom,
		// Token: 0x04007A69 RID: 31337
		[EnumString("legacyPerspectiveBottomRight")]
		LegacyPerspectiveBottomRight,
		// Token: 0x04007A6A RID: 31338
		[EnumString("orthographicFront")]
		OrthographicFront,
		// Token: 0x04007A6B RID: 31339
		[EnumString("isometricTopUp")]
		IsometricTopUp,
		// Token: 0x04007A6C RID: 31340
		[EnumString("isometricTopDown")]
		IsometricTopDown,
		// Token: 0x04007A6D RID: 31341
		[EnumString("isometricBottomUp")]
		IsometricBottomUp,
		// Token: 0x04007A6E RID: 31342
		[EnumString("isometricBottomDown")]
		IsometricBottomDown,
		// Token: 0x04007A6F RID: 31343
		[EnumString("isometricLeftUp")]
		IsometricLeftUp,
		// Token: 0x04007A70 RID: 31344
		[EnumString("isometricLeftDown")]
		IsometricLeftDown,
		// Token: 0x04007A71 RID: 31345
		[EnumString("isometricRightUp")]
		IsometricRightUp,
		// Token: 0x04007A72 RID: 31346
		[EnumString("isometricRightDown")]
		IsometricRightDown,
		// Token: 0x04007A73 RID: 31347
		[EnumString("isometricOffAxis1Left")]
		IsometricOffAxis1Left,
		// Token: 0x04007A74 RID: 31348
		[EnumString("isometricOffAxis1Right")]
		IsometricOffAxis1Right,
		// Token: 0x04007A75 RID: 31349
		[EnumString("isometricOffAxis1Top")]
		IsometricOffAxis1Top,
		// Token: 0x04007A76 RID: 31350
		[EnumString("isometricOffAxis2Left")]
		IsometricOffAxis2Left,
		// Token: 0x04007A77 RID: 31351
		[EnumString("isometricOffAxis2Right")]
		IsometricOffAxis2Right,
		// Token: 0x04007A78 RID: 31352
		[EnumString("isometricOffAxis2Top")]
		IsometricOffAxis2Top,
		// Token: 0x04007A79 RID: 31353
		[EnumString("isometricOffAxis3Left")]
		IsometricOffAxis3Left,
		// Token: 0x04007A7A RID: 31354
		[EnumString("isometricOffAxis3Right")]
		IsometricOffAxis3Right,
		// Token: 0x04007A7B RID: 31355
		[EnumString("isometricOffAxis3Bottom")]
		IsometricOffAxis3Bottom,
		// Token: 0x04007A7C RID: 31356
		[EnumString("isometricOffAxis4Left")]
		IsometricOffAxis4Left,
		// Token: 0x04007A7D RID: 31357
		[EnumString("isometricOffAxis4Right")]
		IsometricOffAxis4Right,
		// Token: 0x04007A7E RID: 31358
		[EnumString("isometricOffAxis4Bottom")]
		IsometricOffAxis4Bottom,
		// Token: 0x04007A7F RID: 31359
		[EnumString("obliqueTopLeft")]
		ObliqueTopLeft,
		// Token: 0x04007A80 RID: 31360
		[EnumString("obliqueTop")]
		ObliqueTop,
		// Token: 0x04007A81 RID: 31361
		[EnumString("obliqueTopRight")]
		ObliqueTopRight,
		// Token: 0x04007A82 RID: 31362
		[EnumString("obliqueLeft")]
		ObliqueLeft,
		// Token: 0x04007A83 RID: 31363
		[EnumString("obliqueRight")]
		ObliqueRight,
		// Token: 0x04007A84 RID: 31364
		[EnumString("obliqueBottomLeft")]
		ObliqueBottomLeft,
		// Token: 0x04007A85 RID: 31365
		[EnumString("obliqueBottom")]
		ObliqueBottom,
		// Token: 0x04007A86 RID: 31366
		[EnumString("obliqueBottomRight")]
		ObliqueBottomRight,
		// Token: 0x04007A87 RID: 31367
		[EnumString("perspectiveFront")]
		PerspectiveFront,
		// Token: 0x04007A88 RID: 31368
		[EnumString("perspectiveLeft")]
		PerspectiveLeft,
		// Token: 0x04007A89 RID: 31369
		[EnumString("perspectiveRight")]
		PerspectiveRight,
		// Token: 0x04007A8A RID: 31370
		[EnumString("perspectiveAbove")]
		PerspectiveAbove,
		// Token: 0x04007A8B RID: 31371
		[EnumString("perspectiveBelow")]
		PerspectiveBelow,
		// Token: 0x04007A8C RID: 31372
		[EnumString("perspectiveAboveLeftFacing")]
		PerspectiveAboveLeftFacing,
		// Token: 0x04007A8D RID: 31373
		[EnumString("perspectiveAboveRightFacing")]
		PerspectiveAboveRightFacing,
		// Token: 0x04007A8E RID: 31374
		[EnumString("perspectiveContrastingLeftFacing")]
		PerspectiveContrastingLeftFacing,
		// Token: 0x04007A8F RID: 31375
		[EnumString("perspectiveContrastingRightFacing")]
		PerspectiveContrastingRightFacing,
		// Token: 0x04007A90 RID: 31376
		[EnumString("perspectiveHeroicLeftFacing")]
		PerspectiveHeroicLeftFacing,
		// Token: 0x04007A91 RID: 31377
		[EnumString("perspectiveHeroicRightFacing")]
		PerspectiveHeroicRightFacing,
		// Token: 0x04007A92 RID: 31378
		[EnumString("perspectiveHeroicExtremeLeftFacing")]
		PerspectiveHeroicExtremeLeftFacing,
		// Token: 0x04007A93 RID: 31379
		[EnumString("perspectiveHeroicExtremeRightFacing")]
		PerspectiveHeroicExtremeRightFacing,
		// Token: 0x04007A94 RID: 31380
		[EnumString("perspectiveRelaxed")]
		PerspectiveRelaxed,
		// Token: 0x04007A95 RID: 31381
		[EnumString("perspectiveRelaxedModerately")]
		PerspectiveRelaxedModerately
	}
}
