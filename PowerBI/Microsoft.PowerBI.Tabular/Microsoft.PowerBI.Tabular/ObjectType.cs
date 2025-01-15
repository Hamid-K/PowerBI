using System;
using System.ComponentModel;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000108 RID: 264
	public enum ObjectType
	{
		// Token: 0x04000281 RID: 641
		Null,
		// Token: 0x04000282 RID: 642
		Model,
		// Token: 0x04000283 RID: 643
		DataSource,
		// Token: 0x04000284 RID: 644
		Table,
		// Token: 0x04000285 RID: 645
		Column,
		// Token: 0x04000286 RID: 646
		AttributeHierarchy,
		// Token: 0x04000287 RID: 647
		Partition,
		// Token: 0x04000288 RID: 648
		Relationship,
		// Token: 0x04000289 RID: 649
		Measure,
		// Token: 0x0400028A RID: 650
		Hierarchy,
		// Token: 0x0400028B RID: 651
		Level,
		// Token: 0x0400028C RID: 652
		Annotation,
		// Token: 0x0400028D RID: 653
		KPI,
		// Token: 0x0400028E RID: 654
		Culture,
		// Token: 0x0400028F RID: 655
		ObjectTranslation,
		// Token: 0x04000290 RID: 656
		LinguisticMetadata,
		// Token: 0x04000291 RID: 657
		Perspective = 29,
		// Token: 0x04000292 RID: 658
		PerspectiveTable,
		// Token: 0x04000293 RID: 659
		PerspectiveColumn,
		// Token: 0x04000294 RID: 660
		PerspectiveHierarchy,
		// Token: 0x04000295 RID: 661
		PerspectiveMeasure,
		// Token: 0x04000296 RID: 662
		Role,
		// Token: 0x04000297 RID: 663
		RoleMembership,
		// Token: 0x04000298 RID: 664
		TablePermission,
		// Token: 0x04000299 RID: 665
		[CompatibilityRequirement(Pbi = "1200", Box = "1400", Excel = "1400")]
		Variation,
		// Token: 0x0400029A RID: 666
		[CompatibilityRequirement(Pbi = "1400")]
		Set,
		// Token: 0x0400029B RID: 667
		[CompatibilityRequirement(Pbi = "1400")]
		PerspectiveSet,
		// Token: 0x0400029C RID: 668
		[CompatibilityRequirement("1400")]
		ExtendedProperty,
		// Token: 0x0400029D RID: 669
		[CompatibilityRequirement("1400")]
		Expression,
		// Token: 0x0400029E RID: 670
		[CompatibilityRequirement("1400")]
		ColumnPermission,
		// Token: 0x0400029F RID: 671
		[CompatibilityRequirement("1400")]
		DetailRowsDefinition,
		// Token: 0x040002A0 RID: 672
		[CompatibilityRequirement(Pbi = "1400")]
		RelatedColumnDetails,
		// Token: 0x040002A1 RID: 673
		[CompatibilityRequirement(Pbi = "1400")]
		GroupByColumn,
		// Token: 0x040002A2 RID: 674
		[CompatibilityRequirement("1470")]
		CalculationGroup,
		// Token: 0x040002A3 RID: 675
		[CompatibilityRequirement("1470")]
		CalculationItem,
		// Token: 0x040002A4 RID: 676
		[CompatibilityRequirement("1460")]
		AlternateOf,
		// Token: 0x040002A5 RID: 677
		[CompatibilityRequirement("1450")]
		RefreshPolicy,
		// Token: 0x040002A6 RID: 678
		[CompatibilityRequirement("1470")]
		FormatStringDefinition,
		// Token: 0x040002A7 RID: 679
		[CompatibilityRequirement("1480")]
		QueryGroup,
		// Token: 0x040002A8 RID: 680
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Preview")]
		AnalyticsAIMetadata,
		// Token: 0x040002A9 RID: 681
		[CompatibilityRequirement("1567")]
		ChangedProperty,
		// Token: 0x040002AA RID: 682
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Preview")]
		ExcludedArtifact,
		// Token: 0x040002AB RID: 683
		[CompatibilityRequirement("1603")]
		DataCoverageDefinition = 58,
		// Token: 0x040002AC RID: 684
		[CompatibilityRequirement("1605")]
		CalculationExpression,
		// Token: 0x040002AD RID: 685
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Preview")]
		Calendar,
		// Token: 0x040002AE RID: 686
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Preview")]
		TimeUnitColumnAssociation,
		// Token: 0x040002AF RID: 687
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Preview")]
		CalendarColumnReference,
		// Token: 0x040002B0 RID: 688
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Internal")]
		Function,
		// Token: 0x040002B1 RID: 689
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Preview")]
		BindingInfo,
		// Token: 0x040002B2 RID: 690
		Database = 1000
	}
}
