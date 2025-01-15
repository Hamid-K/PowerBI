using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002858 RID: 10328
	[GeneratedCode("DomGen", "2.0")]
	internal enum ShapeTypeValues
	{
		// Token: 0x04008AEC RID: 35564
		[EnumString("line")]
		Line,
		// Token: 0x04008AED RID: 35565
		[EnumString("lineInv")]
		LineInverse,
		// Token: 0x04008AEE RID: 35566
		[EnumString("triangle")]
		Triangle,
		// Token: 0x04008AEF RID: 35567
		[EnumString("rtTriangle")]
		RightTriangle,
		// Token: 0x04008AF0 RID: 35568
		[EnumString("rect")]
		Rectangle,
		// Token: 0x04008AF1 RID: 35569
		[EnumString("diamond")]
		Diamond,
		// Token: 0x04008AF2 RID: 35570
		[EnumString("parallelogram")]
		Parallelogram,
		// Token: 0x04008AF3 RID: 35571
		[EnumString("trapezoid")]
		Trapezoid,
		// Token: 0x04008AF4 RID: 35572
		[EnumString("nonIsoscelesTrapezoid")]
		NonIsoscelesTrapezoid,
		// Token: 0x04008AF5 RID: 35573
		[EnumString("pentagon")]
		Pentagon,
		// Token: 0x04008AF6 RID: 35574
		[EnumString("hexagon")]
		Hexagon,
		// Token: 0x04008AF7 RID: 35575
		[EnumString("heptagon")]
		Heptagon,
		// Token: 0x04008AF8 RID: 35576
		[EnumString("octagon")]
		Octagon,
		// Token: 0x04008AF9 RID: 35577
		[EnumString("decagon")]
		Decagon,
		// Token: 0x04008AFA RID: 35578
		[EnumString("dodecagon")]
		Dodecagon,
		// Token: 0x04008AFB RID: 35579
		[EnumString("star4")]
		Star4,
		// Token: 0x04008AFC RID: 35580
		[EnumString("star5")]
		Star5,
		// Token: 0x04008AFD RID: 35581
		[EnumString("star6")]
		Star6,
		// Token: 0x04008AFE RID: 35582
		[EnumString("star7")]
		Star7,
		// Token: 0x04008AFF RID: 35583
		[EnumString("star8")]
		Star8,
		// Token: 0x04008B00 RID: 35584
		[EnumString("star10")]
		Star10,
		// Token: 0x04008B01 RID: 35585
		[EnumString("star12")]
		Star12,
		// Token: 0x04008B02 RID: 35586
		[EnumString("star16")]
		Star16,
		// Token: 0x04008B03 RID: 35587
		[EnumString("star24")]
		Star24,
		// Token: 0x04008B04 RID: 35588
		[EnumString("star32")]
		Star32,
		// Token: 0x04008B05 RID: 35589
		[EnumString("roundRect")]
		RoundRectangle,
		// Token: 0x04008B06 RID: 35590
		[EnumString("round1Rect")]
		Round1Rectangle,
		// Token: 0x04008B07 RID: 35591
		[EnumString("round2SameRect")]
		Round2SameRectangle,
		// Token: 0x04008B08 RID: 35592
		[EnumString("round2DiagRect")]
		Round2DiagonalRectangle,
		// Token: 0x04008B09 RID: 35593
		[EnumString("snipRoundRect")]
		SnipRoundRectangle,
		// Token: 0x04008B0A RID: 35594
		[EnumString("snip1Rect")]
		Snip1Rectangle,
		// Token: 0x04008B0B RID: 35595
		[EnumString("snip2SameRect")]
		Snip2SameRectangle,
		// Token: 0x04008B0C RID: 35596
		[EnumString("snip2DiagRect")]
		Snip2DiagonalRectangle,
		// Token: 0x04008B0D RID: 35597
		[EnumString("plaque")]
		Plaque,
		// Token: 0x04008B0E RID: 35598
		[EnumString("ellipse")]
		Ellipse,
		// Token: 0x04008B0F RID: 35599
		[EnumString("teardrop")]
		Teardrop,
		// Token: 0x04008B10 RID: 35600
		[EnumString("homePlate")]
		HomePlate,
		// Token: 0x04008B11 RID: 35601
		[EnumString("chevron")]
		Chevron,
		// Token: 0x04008B12 RID: 35602
		[EnumString("pieWedge")]
		PieWedge,
		// Token: 0x04008B13 RID: 35603
		[EnumString("pie")]
		Pie,
		// Token: 0x04008B14 RID: 35604
		[EnumString("blockArc")]
		BlockArc,
		// Token: 0x04008B15 RID: 35605
		[EnumString("donut")]
		Donut,
		// Token: 0x04008B16 RID: 35606
		[EnumString("noSmoking")]
		NoSmoking,
		// Token: 0x04008B17 RID: 35607
		[EnumString("rightArrow")]
		RightArrow,
		// Token: 0x04008B18 RID: 35608
		[EnumString("leftArrow")]
		LeftArrow,
		// Token: 0x04008B19 RID: 35609
		[EnumString("upArrow")]
		UpArrow,
		// Token: 0x04008B1A RID: 35610
		[EnumString("downArrow")]
		DownArrow,
		// Token: 0x04008B1B RID: 35611
		[EnumString("stripedRightArrow")]
		StripedRightArrow,
		// Token: 0x04008B1C RID: 35612
		[EnumString("notchedRightArrow")]
		NotchedRightArrow,
		// Token: 0x04008B1D RID: 35613
		[EnumString("bentUpArrow")]
		BentUpArrow,
		// Token: 0x04008B1E RID: 35614
		[EnumString("leftRightArrow")]
		LeftRightArrow,
		// Token: 0x04008B1F RID: 35615
		[EnumString("upDownArrow")]
		UpDownArrow,
		// Token: 0x04008B20 RID: 35616
		[EnumString("leftUpArrow")]
		LeftUpArrow,
		// Token: 0x04008B21 RID: 35617
		[EnumString("leftRightUpArrow")]
		LeftRightUpArrow,
		// Token: 0x04008B22 RID: 35618
		[EnumString("quadArrow")]
		QuadArrow,
		// Token: 0x04008B23 RID: 35619
		[EnumString("leftArrowCallout")]
		LeftArrowCallout,
		// Token: 0x04008B24 RID: 35620
		[EnumString("rightArrowCallout")]
		RightArrowCallout,
		// Token: 0x04008B25 RID: 35621
		[EnumString("upArrowCallout")]
		UpArrowCallout,
		// Token: 0x04008B26 RID: 35622
		[EnumString("downArrowCallout")]
		DownArrowCallout,
		// Token: 0x04008B27 RID: 35623
		[EnumString("leftRightArrowCallout")]
		LeftRightArrowCallout,
		// Token: 0x04008B28 RID: 35624
		[EnumString("upDownArrowCallout")]
		UpDownArrowCallout,
		// Token: 0x04008B29 RID: 35625
		[EnumString("quadArrowCallout")]
		QuadArrowCallout,
		// Token: 0x04008B2A RID: 35626
		[EnumString("bentArrow")]
		BentArrow,
		// Token: 0x04008B2B RID: 35627
		[EnumString("uturnArrow")]
		UTurnArrow,
		// Token: 0x04008B2C RID: 35628
		[EnumString("circularArrow")]
		CircularArrow,
		// Token: 0x04008B2D RID: 35629
		[EnumString("leftCircularArrow")]
		LeftCircularArrow,
		// Token: 0x04008B2E RID: 35630
		[EnumString("leftRightCircularArrow")]
		LeftRightCircularArrow,
		// Token: 0x04008B2F RID: 35631
		[EnumString("curvedRightArrow")]
		CurvedRightArrow,
		// Token: 0x04008B30 RID: 35632
		[EnumString("curvedLeftArrow")]
		CurvedLeftArrow,
		// Token: 0x04008B31 RID: 35633
		[EnumString("curvedUpArrow")]
		CurvedUpArrow,
		// Token: 0x04008B32 RID: 35634
		[EnumString("curvedDownArrow")]
		CurvedDownArrow,
		// Token: 0x04008B33 RID: 35635
		[EnumString("swooshArrow")]
		SwooshArrow,
		// Token: 0x04008B34 RID: 35636
		[EnumString("cube")]
		Cube,
		// Token: 0x04008B35 RID: 35637
		[EnumString("can")]
		Can,
		// Token: 0x04008B36 RID: 35638
		[EnumString("lightningBolt")]
		LightningBolt,
		// Token: 0x04008B37 RID: 35639
		[EnumString("heart")]
		Heart,
		// Token: 0x04008B38 RID: 35640
		[EnumString("sun")]
		Sun,
		// Token: 0x04008B39 RID: 35641
		[EnumString("moon")]
		Moon,
		// Token: 0x04008B3A RID: 35642
		[EnumString("smileyFace")]
		SmileyFace,
		// Token: 0x04008B3B RID: 35643
		[EnumString("irregularSeal1")]
		IrregularSeal1,
		// Token: 0x04008B3C RID: 35644
		[EnumString("irregularSeal2")]
		IrregularSeal2,
		// Token: 0x04008B3D RID: 35645
		[EnumString("foldedCorner")]
		FoldedCorner,
		// Token: 0x04008B3E RID: 35646
		[EnumString("bevel")]
		Bevel,
		// Token: 0x04008B3F RID: 35647
		[EnumString("frame")]
		Frame,
		// Token: 0x04008B40 RID: 35648
		[EnumString("halfFrame")]
		HalfFrame,
		// Token: 0x04008B41 RID: 35649
		[EnumString("corner")]
		Corner,
		// Token: 0x04008B42 RID: 35650
		[EnumString("diagStripe")]
		DiagonalStripe,
		// Token: 0x04008B43 RID: 35651
		[EnumString("chord")]
		Chord,
		// Token: 0x04008B44 RID: 35652
		[EnumString("arc")]
		Arc,
		// Token: 0x04008B45 RID: 35653
		[EnumString("leftBracket")]
		LeftBracket,
		// Token: 0x04008B46 RID: 35654
		[EnumString("rightBracket")]
		RightBracket,
		// Token: 0x04008B47 RID: 35655
		[EnumString("leftBrace")]
		LeftBrace,
		// Token: 0x04008B48 RID: 35656
		[EnumString("rightBrace")]
		RightBrace,
		// Token: 0x04008B49 RID: 35657
		[EnumString("bracketPair")]
		BracketPair,
		// Token: 0x04008B4A RID: 35658
		[EnumString("bracePair")]
		BracePair,
		// Token: 0x04008B4B RID: 35659
		[EnumString("straightConnector1")]
		StraightConnector1,
		// Token: 0x04008B4C RID: 35660
		[EnumString("bentConnector2")]
		BentConnector2,
		// Token: 0x04008B4D RID: 35661
		[EnumString("bentConnector3")]
		BentConnector3,
		// Token: 0x04008B4E RID: 35662
		[EnumString("bentConnector4")]
		BentConnector4,
		// Token: 0x04008B4F RID: 35663
		[EnumString("bentConnector5")]
		BentConnector5,
		// Token: 0x04008B50 RID: 35664
		[EnumString("curvedConnector2")]
		CurvedConnector2,
		// Token: 0x04008B51 RID: 35665
		[EnumString("curvedConnector3")]
		CurvedConnector3,
		// Token: 0x04008B52 RID: 35666
		[EnumString("curvedConnector4")]
		CurvedConnector4,
		// Token: 0x04008B53 RID: 35667
		[EnumString("curvedConnector5")]
		CurvedConnector5,
		// Token: 0x04008B54 RID: 35668
		[EnumString("callout1")]
		Callout1,
		// Token: 0x04008B55 RID: 35669
		[EnumString("callout2")]
		Callout2,
		// Token: 0x04008B56 RID: 35670
		[EnumString("callout3")]
		Callout3,
		// Token: 0x04008B57 RID: 35671
		[EnumString("accentCallout1")]
		AccentCallout1,
		// Token: 0x04008B58 RID: 35672
		[EnumString("accentCallout2")]
		AccentCallout2,
		// Token: 0x04008B59 RID: 35673
		[EnumString("accentCallout3")]
		AccentCallout3,
		// Token: 0x04008B5A RID: 35674
		[EnumString("borderCallout1")]
		BorderCallout1,
		// Token: 0x04008B5B RID: 35675
		[EnumString("borderCallout2")]
		BorderCallout2,
		// Token: 0x04008B5C RID: 35676
		[EnumString("borderCallout3")]
		BorderCallout3,
		// Token: 0x04008B5D RID: 35677
		[EnumString("accentBorderCallout1")]
		AccentBorderCallout1,
		// Token: 0x04008B5E RID: 35678
		[EnumString("accentBorderCallout2")]
		AccentBorderCallout2,
		// Token: 0x04008B5F RID: 35679
		[EnumString("accentBorderCallout3")]
		AccentBorderCallout3,
		// Token: 0x04008B60 RID: 35680
		[EnumString("wedgeRectCallout")]
		WedgeRectangleCallout,
		// Token: 0x04008B61 RID: 35681
		[EnumString("wedgeRoundRectCallout")]
		WedgeRoundRectangleCallout,
		// Token: 0x04008B62 RID: 35682
		[EnumString("wedgeEllipseCallout")]
		WedgeEllipseCallout,
		// Token: 0x04008B63 RID: 35683
		[EnumString("cloudCallout")]
		CloudCallout,
		// Token: 0x04008B64 RID: 35684
		[EnumString("cloud")]
		Cloud,
		// Token: 0x04008B65 RID: 35685
		[EnumString("ribbon")]
		Ribbon,
		// Token: 0x04008B66 RID: 35686
		[EnumString("ribbon2")]
		Ribbon2,
		// Token: 0x04008B67 RID: 35687
		[EnumString("ellipseRibbon")]
		EllipseRibbon,
		// Token: 0x04008B68 RID: 35688
		[EnumString("ellipseRibbon2")]
		EllipseRibbon2,
		// Token: 0x04008B69 RID: 35689
		[EnumString("leftRightRibbon")]
		LeftRightRibbon,
		// Token: 0x04008B6A RID: 35690
		[EnumString("verticalScroll")]
		VerticalScroll,
		// Token: 0x04008B6B RID: 35691
		[EnumString("horizontalScroll")]
		HorizontalScroll,
		// Token: 0x04008B6C RID: 35692
		[EnumString("wave")]
		Wave,
		// Token: 0x04008B6D RID: 35693
		[EnumString("doubleWave")]
		DoubleWave,
		// Token: 0x04008B6E RID: 35694
		[EnumString("plus")]
		Plus,
		// Token: 0x04008B6F RID: 35695
		[EnumString("flowChartProcess")]
		FlowChartProcess,
		// Token: 0x04008B70 RID: 35696
		[EnumString("flowChartDecision")]
		FlowChartDecision,
		// Token: 0x04008B71 RID: 35697
		[EnumString("flowChartInputOutput")]
		FlowChartInputOutput,
		// Token: 0x04008B72 RID: 35698
		[EnumString("flowChartPredefinedProcess")]
		FlowChartPredefinedProcess,
		// Token: 0x04008B73 RID: 35699
		[EnumString("flowChartInternalStorage")]
		FlowChartInternalStorage,
		// Token: 0x04008B74 RID: 35700
		[EnumString("flowChartDocument")]
		FlowChartDocument,
		// Token: 0x04008B75 RID: 35701
		[EnumString("flowChartMultidocument")]
		FlowChartMultidocument,
		// Token: 0x04008B76 RID: 35702
		[EnumString("flowChartTerminator")]
		FlowChartTerminator,
		// Token: 0x04008B77 RID: 35703
		[EnumString("flowChartPreparation")]
		FlowChartPreparation,
		// Token: 0x04008B78 RID: 35704
		[EnumString("flowChartManualInput")]
		FlowChartManualInput,
		// Token: 0x04008B79 RID: 35705
		[EnumString("flowChartManualOperation")]
		FlowChartManualOperation,
		// Token: 0x04008B7A RID: 35706
		[EnumString("flowChartConnector")]
		FlowChartConnector,
		// Token: 0x04008B7B RID: 35707
		[EnumString("flowChartPunchedCard")]
		FlowChartPunchedCard,
		// Token: 0x04008B7C RID: 35708
		[EnumString("flowChartPunchedTape")]
		FlowChartPunchedTape,
		// Token: 0x04008B7D RID: 35709
		[EnumString("flowChartSummingJunction")]
		FlowChartSummingJunction,
		// Token: 0x04008B7E RID: 35710
		[EnumString("flowChartOr")]
		FlowChartOr,
		// Token: 0x04008B7F RID: 35711
		[EnumString("flowChartCollate")]
		FlowChartCollate,
		// Token: 0x04008B80 RID: 35712
		[EnumString("flowChartSort")]
		FlowChartSort,
		// Token: 0x04008B81 RID: 35713
		[EnumString("flowChartExtract")]
		FlowChartExtract,
		// Token: 0x04008B82 RID: 35714
		[EnumString("flowChartMerge")]
		FlowChartMerge,
		// Token: 0x04008B83 RID: 35715
		[EnumString("flowChartOfflineStorage")]
		FlowChartOfflineStorage,
		// Token: 0x04008B84 RID: 35716
		[EnumString("flowChartOnlineStorage")]
		FlowChartOnlineStorage,
		// Token: 0x04008B85 RID: 35717
		[EnumString("flowChartMagneticTape")]
		FlowChartMagneticTape,
		// Token: 0x04008B86 RID: 35718
		[EnumString("flowChartMagneticDisk")]
		FlowChartMagneticDisk,
		// Token: 0x04008B87 RID: 35719
		[EnumString("flowChartMagneticDrum")]
		FlowChartMagneticDrum,
		// Token: 0x04008B88 RID: 35720
		[EnumString("flowChartDisplay")]
		FlowChartDisplay,
		// Token: 0x04008B89 RID: 35721
		[EnumString("flowChartDelay")]
		FlowChartDelay,
		// Token: 0x04008B8A RID: 35722
		[EnumString("flowChartAlternateProcess")]
		FlowChartAlternateProcess,
		// Token: 0x04008B8B RID: 35723
		[EnumString("flowChartOffpageConnector")]
		FlowChartOffpageConnector,
		// Token: 0x04008B8C RID: 35724
		[EnumString("actionButtonBlank")]
		ActionButtonBlank,
		// Token: 0x04008B8D RID: 35725
		[EnumString("actionButtonHome")]
		ActionButtonHome,
		// Token: 0x04008B8E RID: 35726
		[EnumString("actionButtonHelp")]
		ActionButtonHelp,
		// Token: 0x04008B8F RID: 35727
		[EnumString("actionButtonInformation")]
		ActionButtonInformation,
		// Token: 0x04008B90 RID: 35728
		[EnumString("actionButtonForwardNext")]
		ActionButtonForwardNext,
		// Token: 0x04008B91 RID: 35729
		[EnumString("actionButtonBackPrevious")]
		ActionButtonBackPrevious,
		// Token: 0x04008B92 RID: 35730
		[EnumString("actionButtonEnd")]
		ActionButtonEnd,
		// Token: 0x04008B93 RID: 35731
		[EnumString("actionButtonBeginning")]
		ActionButtonBeginning,
		// Token: 0x04008B94 RID: 35732
		[EnumString("actionButtonReturn")]
		ActionButtonReturn,
		// Token: 0x04008B95 RID: 35733
		[EnumString("actionButtonDocument")]
		ActionButtonDocument,
		// Token: 0x04008B96 RID: 35734
		[EnumString("actionButtonSound")]
		ActionButtonSound,
		// Token: 0x04008B97 RID: 35735
		[EnumString("actionButtonMovie")]
		ActionButtonMovie,
		// Token: 0x04008B98 RID: 35736
		[EnumString("gear6")]
		Gear6,
		// Token: 0x04008B99 RID: 35737
		[EnumString("gear9")]
		Gear9,
		// Token: 0x04008B9A RID: 35738
		[EnumString("funnel")]
		Funnel,
		// Token: 0x04008B9B RID: 35739
		[EnumString("mathPlus")]
		MathPlus,
		// Token: 0x04008B9C RID: 35740
		[EnumString("mathMinus")]
		MathMinus,
		// Token: 0x04008B9D RID: 35741
		[EnumString("mathMultiply")]
		MathMultiply,
		// Token: 0x04008B9E RID: 35742
		[EnumString("mathDivide")]
		MathDivide,
		// Token: 0x04008B9F RID: 35743
		[EnumString("mathEqual")]
		MathEqual,
		// Token: 0x04008BA0 RID: 35744
		[EnumString("mathNotEqual")]
		MathNotEqual,
		// Token: 0x04008BA1 RID: 35745
		[EnumString("cornerTabs")]
		CornerTabs,
		// Token: 0x04008BA2 RID: 35746
		[EnumString("squareTabs")]
		SquareTabs,
		// Token: 0x04008BA3 RID: 35747
		[EnumString("plaqueTabs")]
		PlaqueTabs,
		// Token: 0x04008BA4 RID: 35748
		[EnumString("chartX")]
		ChartX,
		// Token: 0x04008BA5 RID: 35749
		[EnumString("chartStar")]
		ChartStar,
		// Token: 0x04008BA6 RID: 35750
		[EnumString("chartPlus")]
		ChartPlus
	}
}
