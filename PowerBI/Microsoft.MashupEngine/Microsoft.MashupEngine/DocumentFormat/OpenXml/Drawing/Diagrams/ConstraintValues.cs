using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x020026A3 RID: 9891
	[GeneratedCode("DomGen", "2.0")]
	internal enum ConstraintValues
	{
		// Token: 0x0400827F RID: 33407
		[EnumString("none")]
		None,
		// Token: 0x04008280 RID: 33408
		[EnumString("alignOff")]
		AlignmentOffset,
		// Token: 0x04008281 RID: 33409
		[EnumString("begMarg")]
		BeginningMargin,
		// Token: 0x04008282 RID: 33410
		[EnumString("bendDist")]
		BendingDistance,
		// Token: 0x04008283 RID: 33411
		[EnumString("begPad")]
		BeginningPadding,
		// Token: 0x04008284 RID: 33412
		[EnumString("b")]
		Bottom,
		// Token: 0x04008285 RID: 33413
		[EnumString("bMarg")]
		BottomMargin,
		// Token: 0x04008286 RID: 33414
		[EnumString("bOff")]
		BottomOffset,
		// Token: 0x04008287 RID: 33415
		[EnumString("ctrX")]
		CenterHeight,
		// Token: 0x04008288 RID: 33416
		[EnumString("ctrXOff")]
		CenterXOffset,
		// Token: 0x04008289 RID: 33417
		[EnumString("ctrY")]
		CenterWidth,
		// Token: 0x0400828A RID: 33418
		[EnumString("ctrYOff")]
		CenterYOffset,
		// Token: 0x0400828B RID: 33419
		[EnumString("connDist")]
		ConnectionDistance,
		// Token: 0x0400828C RID: 33420
		[EnumString("diam")]
		Diameter,
		// Token: 0x0400828D RID: 33421
		[EnumString("endMarg")]
		EndMargin,
		// Token: 0x0400828E RID: 33422
		[EnumString("endPad")]
		EndPadding,
		// Token: 0x0400828F RID: 33423
		[EnumString("h")]
		Height,
		// Token: 0x04008290 RID: 33424
		[EnumString("hArH")]
		ArrowheadHeight,
		// Token: 0x04008291 RID: 33425
		[EnumString("hOff")]
		HeightOffset,
		// Token: 0x04008292 RID: 33426
		[EnumString("l")]
		Left,
		// Token: 0x04008293 RID: 33427
		[EnumString("lMarg")]
		LeftMargin,
		// Token: 0x04008294 RID: 33428
		[EnumString("lOff")]
		LeftOffset,
		// Token: 0x04008295 RID: 33429
		[EnumString("r")]
		Right,
		// Token: 0x04008296 RID: 33430
		[EnumString("rMarg")]
		RightMargin,
		// Token: 0x04008297 RID: 33431
		[EnumString("rOff")]
		RightOffset,
		// Token: 0x04008298 RID: 33432
		[EnumString("primFontSz")]
		PrimaryFontSize,
		// Token: 0x04008299 RID: 33433
		[EnumString("pyraAcctRatio")]
		PyramidAccentRatio,
		// Token: 0x0400829A RID: 33434
		[EnumString("secFontSz")]
		SecondaryFontSize,
		// Token: 0x0400829B RID: 33435
		[EnumString("sibSp")]
		SiblingSpacing,
		// Token: 0x0400829C RID: 33436
		[EnumString("secSibSp")]
		SecondarySiblingSpacing,
		// Token: 0x0400829D RID: 33437
		[EnumString("sp")]
		Spacing,
		// Token: 0x0400829E RID: 33438
		[EnumString("stemThick")]
		StemThickness,
		// Token: 0x0400829F RID: 33439
		[EnumString("t")]
		Top,
		// Token: 0x040082A0 RID: 33440
		[EnumString("tMarg")]
		TopMargin,
		// Token: 0x040082A1 RID: 33441
		[EnumString("tOff")]
		TopOffset,
		// Token: 0x040082A2 RID: 33442
		[EnumString("userA")]
		UserDefinedA,
		// Token: 0x040082A3 RID: 33443
		[EnumString("userB")]
		UserDefinedB,
		// Token: 0x040082A4 RID: 33444
		[EnumString("userC")]
		UserDefinedC,
		// Token: 0x040082A5 RID: 33445
		[EnumString("userD")]
		UserDefinedD,
		// Token: 0x040082A6 RID: 33446
		[EnumString("userE")]
		UserDefinedE,
		// Token: 0x040082A7 RID: 33447
		[EnumString("userF")]
		UserDefinedF,
		// Token: 0x040082A8 RID: 33448
		[EnumString("userG")]
		UserDefinedG,
		// Token: 0x040082A9 RID: 33449
		[EnumString("userH")]
		UserDefinedH,
		// Token: 0x040082AA RID: 33450
		[EnumString("userI")]
		UserDefinedI,
		// Token: 0x040082AB RID: 33451
		[EnumString("userJ")]
		UserDefinedJ,
		// Token: 0x040082AC RID: 33452
		[EnumString("userK")]
		UserDefinedK,
		// Token: 0x040082AD RID: 33453
		[EnumString("userL")]
		UserDefinedL,
		// Token: 0x040082AE RID: 33454
		[EnumString("userM")]
		UserDefinedM,
		// Token: 0x040082AF RID: 33455
		[EnumString("userN")]
		UserDefinedN,
		// Token: 0x040082B0 RID: 33456
		[EnumString("userO")]
		UserDefinedO,
		// Token: 0x040082B1 RID: 33457
		[EnumString("userP")]
		UserDefinedP,
		// Token: 0x040082B2 RID: 33458
		[EnumString("userQ")]
		UserDefinedQ,
		// Token: 0x040082B3 RID: 33459
		[EnumString("userR")]
		UserDefinedR,
		// Token: 0x040082B4 RID: 33460
		[EnumString("userS")]
		UserDefinedS,
		// Token: 0x040082B5 RID: 33461
		[EnumString("userT")]
		UserDefinedT,
		// Token: 0x040082B6 RID: 33462
		[EnumString("userU")]
		UserDefinedU,
		// Token: 0x040082B7 RID: 33463
		[EnumString("userV")]
		UserDefinedV,
		// Token: 0x040082B8 RID: 33464
		[EnumString("userW")]
		UserDefinedW,
		// Token: 0x040082B9 RID: 33465
		[EnumString("userX")]
		UserDefinedX,
		// Token: 0x040082BA RID: 33466
		[EnumString("userY")]
		UserDefinedY,
		// Token: 0x040082BB RID: 33467
		[EnumString("userZ")]
		UserDefinedZ,
		// Token: 0x040082BC RID: 33468
		[EnumString("w")]
		Width,
		// Token: 0x040082BD RID: 33469
		[EnumString("wArH")]
		ArrowheadWidth,
		// Token: 0x040082BE RID: 33470
		[EnumString("wOff")]
		WidthOffset
	}
}
