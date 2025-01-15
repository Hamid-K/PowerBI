using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x0200012C RID: 300
	public sealed class DaxCapabilitiesAnnotation
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x00010278 File Offset: 0x0000E478
		public DaxCapabilitiesAnnotation(bool enforcesGroupByValidation, bool discouragesQueryAggregateUsage, bool alwaysCrossFilteringWithinTable, bool supportsQueryBatching, bool supportsVariables, bool supportsInOperator, bool supportsVirtualColumns, bool supportsTableConstructor, bool limitsMultiColumnFilteringToGroupByColumns, bool supportsDataSourceVariables, bool supportsExecutionMetrics, bool supportsNormalizedFiveStateKpiRange, bool supportsDefaultMembers, bool supportsVisualCalculations, DaxFunctionsAnnotation daxFunctions)
		{
			this.EnforcesGroupByValidation = enforcesGroupByValidation;
			this.DiscouragesQueryAggregateUsage = discouragesQueryAggregateUsage;
			this.AlwaysCrossFilteringWithinTable = alwaysCrossFilteringWithinTable;
			this.SupportsQueryBatching = supportsQueryBatching;
			this.SupportsVariables = supportsVariables;
			this.SupportsInOperator = supportsInOperator;
			this.SupportsVirtualColumns = supportsVirtualColumns;
			this.SupportsTableConstructor = supportsTableConstructor;
			this.LimitsMultiColumnFilteringToGroupByColumns = limitsMultiColumnFilteringToGroupByColumns;
			this.SupportsDataSourceVariables = supportsDataSourceVariables;
			this.SupportsExecutionMetrics = supportsExecutionMetrics;
			this.SupportsNormalizedFiveStateKpiRange = supportsNormalizedFiveStateKpiRange;
			this.SupportsDefaultMembers = supportsDefaultMembers;
			this.SupportsVisualCalculations = supportsVisualCalculations;
			this.DaxFunctions = daxFunctions;
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00010300 File Offset: 0x0000E500
		public bool EnforcesGroupByValidation { get; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00010308 File Offset: 0x0000E508
		public bool DiscouragesQueryAggregateUsage { get; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00010310 File Offset: 0x0000E510
		public bool AlwaysCrossFilteringWithinTable { get; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00010318 File Offset: 0x0000E518
		public bool SupportsQueryBatching { get; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00010320 File Offset: 0x0000E520
		public bool SupportsVariables { get; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00010328 File Offset: 0x0000E528
		public bool SupportsInOperator { get; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00010330 File Offset: 0x0000E530
		public bool SupportsVirtualColumns { get; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00010338 File Offset: 0x0000E538
		public bool SupportsTableConstructor { get; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00010340 File Offset: 0x0000E540
		public bool LimitsMultiColumnFilteringToGroupByColumns { get; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00010348 File Offset: 0x0000E548
		public bool SupportsDataSourceVariables { get; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00010350 File Offset: 0x0000E550
		public bool SupportsExecutionMetrics { get; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00010358 File Offset: 0x0000E558
		public bool SupportsNormalizedFiveStateKpiRange { get; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00010360 File Offset: 0x0000E560
		public bool SupportsDefaultMembers { get; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00010368 File Offset: 0x0000E568
		public bool SupportsVisualCalculations { get; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00010370 File Offset: 0x0000E570
		public DaxFunctionsAnnotation DaxFunctions { get; }

		// Token: 0x060007D8 RID: 2008 RVA: 0x00010378 File Offset: 0x0000E578
		public DaxCapabilitiesAnnotation OverrideCapabilities(bool? inOperator, bool? treatAs, bool? stringMinMax, bool? optimizedNotInOperator, bool? nonVisual, bool? isAfter, bool? formatByLocale, bool? binaryMinMax, bool? executionMetrics, bool? variables, bool? batching, bool? leftOuterJoin, bool? substituteWithIndex, bool? summarizeColumns, bool? tableConstructor)
		{
			DaxFunctionsAnnotation daxFunctionsAnnotation = this.DaxFunctions.OverrideCapabilities(treatAs, stringMinMax, optimizedNotInOperator, nonVisual, isAfter, formatByLocale, binaryMinMax, leftOuterJoin, substituteWithIndex, summarizeColumns);
			return new DaxCapabilitiesAnnotation(this.EnforcesGroupByValidation, this.DiscouragesQueryAggregateUsage, this.AlwaysCrossFilteringWithinTable, batching ?? this.SupportsQueryBatching, variables ?? this.SupportsVariables, inOperator ?? this.SupportsInOperator, this.SupportsVirtualColumns, tableConstructor ?? this.SupportsTableConstructor, this.LimitsMultiColumnFilteringToGroupByColumns, this.SupportsDataSourceVariables, executionMetrics ?? this.SupportsExecutionMetrics, this.SupportsNormalizedFiveStateKpiRange, this.SupportsDefaultMembers, this.SupportsVisualCalculations, daxFunctionsAnnotation);
		}
	}
}
