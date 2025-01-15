using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x0200012D RID: 301
	public class DaxFunctionsAnnotation
	{
		// Token: 0x060007D9 RID: 2009 RVA: 0x00010468 File Offset: 0x0000E668
		public DaxFunctionsAnnotation(bool supportsTreatAsSupportForAllColumns, bool supportsIsEmpty, bool supportsDivide, bool supportsBinaryMinMax, bool supportsSubstituteWithIndex, bool supportsSummarizeColumns, bool supportsGroupBy, bool supportsSelectColumns, bool supportsIsOnOrAfter, bool supportsTreatAs, bool supportsStringMinMax, bool supportsSampleAxisWithLocalMinMax, bool supportsOptimizedNotInOperator, bool supportsNonVisual, bool supportsLeftOuterJoin, bool supportsIsAfter, bool supportsFormatByLocale)
		{
			this.SupportsTreatAsSupportForAllColumns = supportsTreatAsSupportForAllColumns;
			this.SupportsIsEmpty = supportsIsEmpty;
			this.SupportsDivide = supportsDivide;
			this.SupportsBinaryMinMax = supportsBinaryMinMax;
			this.SupportsSubstituteWithIndex = supportsSubstituteWithIndex;
			this.SupportsSummarizeColumns = supportsSummarizeColumns;
			this.SupportsGroupBy = supportsGroupBy;
			this.SupportsSelectColumns = supportsSelectColumns;
			this.SupportsIsOnOrAfter = supportsIsOnOrAfter;
			this.SupportsTreatAs = supportsTreatAs;
			this.SupportsStringMinMax = supportsStringMinMax;
			this.SupportsSampleAxisWithLocalMinMax = supportsSampleAxisWithLocalMinMax;
			this.SupportsOptimizedNotInOperator = supportsOptimizedNotInOperator;
			this.SupportsNonVisual = supportsNonVisual;
			this.SupportsLeftOuterJoin = supportsLeftOuterJoin;
			this.SupportsIsAfter = supportsIsAfter;
			this.SupportsFormatByLocale = supportsFormatByLocale;
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00010500 File Offset: 0x0000E700
		public bool SupportsTreatAsSupportForAllColumns { get; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x00010508 File Offset: 0x0000E708
		public bool SupportsIsEmpty { get; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x00010510 File Offset: 0x0000E710
		public bool SupportsDivide { get; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00010518 File Offset: 0x0000E718
		public bool SupportsBinaryMinMax { get; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x00010520 File Offset: 0x0000E720
		public bool SupportsSubstituteWithIndex { get; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x00010528 File Offset: 0x0000E728
		public bool SupportsSummarizeColumns { get; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00010530 File Offset: 0x0000E730
		public bool SupportsGroupBy { get; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00010538 File Offset: 0x0000E738
		public bool SupportsSelectColumns { get; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x00010540 File Offset: 0x0000E740
		public bool SupportsIsOnOrAfter { get; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00010548 File Offset: 0x0000E748
		public bool SupportsTreatAs { get; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00010550 File Offset: 0x0000E750
		public bool SupportsStringMinMax { get; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00010558 File Offset: 0x0000E758
		public bool SupportsSampleAxisWithLocalMinMax { get; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00010560 File Offset: 0x0000E760
		public bool SupportsOptimizedNotInOperator { get; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00010568 File Offset: 0x0000E768
		public bool SupportsNonVisual { get; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00010570 File Offset: 0x0000E770
		public bool SupportsLeftOuterJoin { get; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00010578 File Offset: 0x0000E778
		public bool SupportsIsAfter { get; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00010580 File Offset: 0x0000E780
		public bool SupportsFormatByLocale { get; }

		// Token: 0x060007EB RID: 2027 RVA: 0x00010588 File Offset: 0x0000E788
		public DaxFunctionsAnnotation OverrideCapabilities(bool? treatAs, bool? stringMinMax, bool? optimizedNotInOperator, bool? nonVisual, bool? isAfter, bool? formatByLocale, bool? binaryMinMax, bool? leftOuterJoin, bool? substituteWithIndex, bool? summarizeColumns)
		{
			return new DaxFunctionsAnnotation(this.SupportsTreatAsSupportForAllColumns, this.SupportsIsEmpty, this.SupportsDivide, binaryMinMax ?? this.SupportsBinaryMinMax, substituteWithIndex ?? this.SupportsSubstituteWithIndex, summarizeColumns ?? this.SupportsSummarizeColumns, this.SupportsGroupBy, this.SupportsSelectColumns, this.SupportsIsOnOrAfter, treatAs ?? this.SupportsTreatAs, stringMinMax ?? this.SupportsStringMinMax, this.SupportsSampleAxisWithLocalMinMax, optimizedNotInOperator ?? this.SupportsOptimizedNotInOperator, nonVisual ?? this.SupportsNonVisual, leftOuterJoin ?? this.SupportsLeftOuterJoin, isAfter ?? this.SupportsIsAfter, formatByLocale ?? this.SupportsFormatByLocale);
		}
	}
}
