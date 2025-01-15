using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x020026A6 RID: 9894
	[GeneratedCode("DomGen", "2.0")]
	internal enum ParameterIdValues
	{
		// Token: 0x040082CF RID: 33487
		[EnumString("horzAlign")]
		HorizontalAlignment,
		// Token: 0x040082D0 RID: 33488
		[EnumString("vertAlign")]
		VerticalAlignment,
		// Token: 0x040082D1 RID: 33489
		[EnumString("chDir")]
		ChildDirection,
		// Token: 0x040082D2 RID: 33490
		[EnumString("chAlign")]
		ChildAlignment,
		// Token: 0x040082D3 RID: 33491
		[EnumString("secChAlign")]
		SecondaryChildAlignment,
		// Token: 0x040082D4 RID: 33492
		[EnumString("linDir")]
		LinearDirection,
		// Token: 0x040082D5 RID: 33493
		[EnumString("secLinDir")]
		SecondaryLinearDirection,
		// Token: 0x040082D6 RID: 33494
		[EnumString("stElem")]
		StartElement,
		// Token: 0x040082D7 RID: 33495
		[EnumString("bendPt")]
		BendPoint,
		// Token: 0x040082D8 RID: 33496
		[EnumString("connRout")]
		ConnectionRoute,
		// Token: 0x040082D9 RID: 33497
		[EnumString("begSty")]
		BeginningArrowheadStyle,
		// Token: 0x040082DA RID: 33498
		[EnumString("endSty")]
		EndStyle,
		// Token: 0x040082DB RID: 33499
		[EnumString("dim")]
		ConnectorDimension,
		// Token: 0x040082DC RID: 33500
		[EnumString("rotPath")]
		RotationPath,
		// Token: 0x040082DD RID: 33501
		[EnumString("ctrShpMap")]
		CenterShapeMapping,
		// Token: 0x040082DE RID: 33502
		[EnumString("nodeHorzAlign")]
		NodeHorizontalAlignment,
		// Token: 0x040082DF RID: 33503
		[EnumString("nodeVertAlign")]
		NodeVerticalAlignment,
		// Token: 0x040082E0 RID: 33504
		[EnumString("fallback")]
		FallbackScale,
		// Token: 0x040082E1 RID: 33505
		[EnumString("txDir")]
		TextDirection,
		// Token: 0x040082E2 RID: 33506
		[EnumString("pyraAcctPos")]
		PyramidAccentPosition,
		// Token: 0x040082E3 RID: 33507
		[EnumString("pyraAcctTxMar")]
		PyramidAccentTextMargin,
		// Token: 0x040082E4 RID: 33508
		[EnumString("txBlDir")]
		TextBlockDirection,
		// Token: 0x040082E5 RID: 33509
		[EnumString("txAnchorHorz")]
		TextAnchorHorizontal,
		// Token: 0x040082E6 RID: 33510
		[EnumString("txAnchorVert")]
		TextAnchorVertical,
		// Token: 0x040082E7 RID: 33511
		[EnumString("txAnchorHorzCh")]
		TextAnchorHorizontalWithChildren,
		// Token: 0x040082E8 RID: 33512
		[EnumString("txAnchorVertCh")]
		TextAnchorVerticalWithChildren,
		// Token: 0x040082E9 RID: 33513
		[EnumString("parTxLTRAlign")]
		ParentTextLeftToRightAlignment,
		// Token: 0x040082EA RID: 33514
		[EnumString("parTxRTLAlign")]
		ParentTextRightToLeftAlignment,
		// Token: 0x040082EB RID: 33515
		[EnumString("shpTxLTRAlignCh")]
		ShapeTextLeftToRightAlignment,
		// Token: 0x040082EC RID: 33516
		[EnumString("shpTxRTLAlignCh")]
		ShapeTextRightToLeftAlignment,
		// Token: 0x040082ED RID: 33517
		[EnumString("autoTxRot")]
		AutoTextRotation,
		// Token: 0x040082EE RID: 33518
		[EnumString("grDir")]
		GrowDirection,
		// Token: 0x040082EF RID: 33519
		[EnumString("flowDir")]
		FlowDirection,
		// Token: 0x040082F0 RID: 33520
		[EnumString("contDir")]
		ContinueDirection,
		// Token: 0x040082F1 RID: 33521
		[EnumString("bkpt")]
		Breakpoint,
		// Token: 0x040082F2 RID: 33522
		[EnumString("off")]
		Offset,
		// Token: 0x040082F3 RID: 33523
		[EnumString("hierAlign")]
		HierarchyAlignment,
		// Token: 0x040082F4 RID: 33524
		[EnumString("bkPtFixedVal")]
		BreakpointFixedValue,
		// Token: 0x040082F5 RID: 33525
		[EnumString("stBulletLvl")]
		StartBulletsAtLevel,
		// Token: 0x040082F6 RID: 33526
		[EnumString("stAng")]
		StartAngle,
		// Token: 0x040082F7 RID: 33527
		[EnumString("spanAng")]
		SpanAngle,
		// Token: 0x040082F8 RID: 33528
		[EnumString("ar")]
		AspectRatio,
		// Token: 0x040082F9 RID: 33529
		[EnumString("lnSpPar")]
		LineSpacingParent,
		// Token: 0x040082FA RID: 33530
		[EnumString("lnSpAfParP")]
		LineSpacingAfterParentParagraph,
		// Token: 0x040082FB RID: 33531
		[EnumString("lnSpCh")]
		LineSpacingChildren,
		// Token: 0x040082FC RID: 33532
		[EnumString("lnSpAfChP")]
		LineSpacingAfterChildrenParagraph,
		// Token: 0x040082FD RID: 33533
		[EnumString("rtShortDist")]
		RouteShortestDistance,
		// Token: 0x040082FE RID: 33534
		[EnumString("alignTx")]
		TextAlignment,
		// Token: 0x040082FF RID: 33535
		[EnumString("pyraLvlNode")]
		PyramidLevelNode,
		// Token: 0x04008300 RID: 33536
		[EnumString("pyraAcctBkgdNode")]
		PyramidAccentBackgroundNode,
		// Token: 0x04008301 RID: 33537
		[EnumString("pyraAcctTxNode")]
		PyramidAccentTextNode,
		// Token: 0x04008302 RID: 33538
		[EnumString("srcNode")]
		SourceNode,
		// Token: 0x04008303 RID: 33539
		[EnumString("dstNode")]
		DestinationNode,
		// Token: 0x04008304 RID: 33540
		[EnumString("begPts")]
		BeginningPoints,
		// Token: 0x04008305 RID: 33541
		[EnumString("endPts")]
		EndPoints
	}
}
