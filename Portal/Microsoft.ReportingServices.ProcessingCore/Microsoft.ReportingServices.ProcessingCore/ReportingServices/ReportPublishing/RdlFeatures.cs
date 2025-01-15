using System;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x0200038E RID: 910
	[Flags]
	internal enum RdlFeatures
	{
		// Token: 0x04001593 RID: 5523
		SharedDataSetReferences = 0,
		// Token: 0x04001594 RID: 5524
		Image_Embedded = 1,
		// Token: 0x04001595 RID: 5525
		Sort_Group_Applied = 2,
		// Token: 0x04001596 RID: 5526
		Sort_DataRegion = 3,
		// Token: 0x04001597 RID: 5527
		Filters = 4,
		// Token: 0x04001598 RID: 5528
		Lookup = 5,
		// Token: 0x04001599 RID: 5529
		RunningValue = 6,
		// Token: 0x0400159A RID: 5530
		Previous = 7,
		// Token: 0x0400159B RID: 5531
		RowNumber = 8,
		// Token: 0x0400159C RID: 5532
		GroupParent = 9,
		// Token: 0x0400159D RID: 5533
		Variables = 10,
		// Token: 0x0400159E RID: 5534
		SubReports = 11,
		// Token: 0x0400159F RID: 5535
		AutomaticSubtotals = 12,
		// Token: 0x040015A0 RID: 5536
		DomainScope = 13,
		// Token: 0x040015A1 RID: 5537
		InScope = 14,
		// Token: 0x040015A2 RID: 5538
		Level = 15,
		// Token: 0x040015A3 RID: 5539
		CreateDrillthroughContext = 16,
		// Token: 0x040015A4 RID: 5540
		UserSort = 17,
		// Token: 0x040015A5 RID: 5541
		AggregatesOfAggregates = 18,
		// Token: 0x040015A6 RID: 5542
		PageHeaderFooter = 19,
		// Token: 0x040015A7 RID: 5543
		SortGroupExpression_OnlySimpleField = 20,
		// Token: 0x040015A8 RID: 5544
		PeerGroups = 21,
		// Token: 0x040015A9 RID: 5545
		ImageTag = 22,
		// Token: 0x040015AA RID: 5546
		ReportSectionName = 23,
		// Token: 0x040015AB RID: 5547
		DeferredSort = 24,
		// Token: 0x040015AC RID: 5548
		EmbeddingMode = 25,
		// Token: 0x040015AD RID: 5549
		EmbeddingMode_Inline = 26,
		// Token: 0x040015AE RID: 5550
		ReportSection_LayoutDirection = 27,
		// Token: 0x040015AF RID: 5551
		ThemeFonts = 28,
		// Token: 0x040015B0 RID: 5552
		TablixHierarchy_EnableDrilldown = 29,
		// Token: 0x040015B1 RID: 5553
		ScopesCollection = 30,
		// Token: 0x040015B2 RID: 5554
		ThemeColors = 31,
		// Token: 0x040015B3 RID: 5555
		ChartHierarchy_EnableDrilldown = 32,
		// Token: 0x040015B4 RID: 5556
		Report_Code = 33,
		// Token: 0x040015B5 RID: 5557
		Report_Classes = 34,
		// Token: 0x040015B6 RID: 5558
		Report_CodeModules = 35,
		// Token: 0x040015B7 RID: 5559
		ComplexExpression = 36,
		// Token: 0x040015B8 RID: 5560
		BackgroundImageFitting = 37,
		// Token: 0x040015B9 RID: 5561
		BackgroundImageTransparency = 38,
		// Token: 0x040015BA RID: 5562
		LabelData_KeyFields = 39,
		// Token: 0x040015BB RID: 5563
		ImageTagsCollection = 40,
		// Token: 0x040015BC RID: 5564
		CellLevelFormatting = 41,
		// Token: 0x040015BD RID: 5565
		ParametersLayout = 42,
		// Token: 0x040015BE RID: 5566
		DefaultFontFamily = 43,
		// Token: 0x040015BF RID: 5567
		AggregateIndicatorField = 44
	}
}
