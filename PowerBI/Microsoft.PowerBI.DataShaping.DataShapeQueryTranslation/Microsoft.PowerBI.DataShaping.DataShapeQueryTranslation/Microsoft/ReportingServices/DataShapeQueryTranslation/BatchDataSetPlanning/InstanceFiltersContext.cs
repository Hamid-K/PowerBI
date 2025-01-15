using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200018E RID: 398
	internal sealed class InstanceFiltersContext
	{
		// Token: 0x06000DBE RID: 3518 RVA: 0x000384E2 File Offset: 0x000366E2
		internal InstanceFiltersContext(IFilterDeclarationCollection instanceFilterDeclarations, QueryStageForInstanceFilters queryStageForInstanceFilters, HashSet<FilterCondition> instanceFiltersRequiringPostFiltering)
		{
			this.InstanceFilterDeclarations = instanceFilterDeclarations;
			this.QueryStageForInstanceFilters = queryStageForInstanceFilters;
			this.InstanceFiltersRequiringPostFiltering = instanceFiltersRequiringPostFiltering;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00038500 File Offset: 0x00036700
		internal static InstanceFiltersContext Create(DataShapeContext dsContext, IFederatedConceptualSchema schema, PlanDeclarationCollection declarations, ExpressionTable expressionTable, IFeatureSwitchProvider featureSwitches, out InstanceFilterTelemetry instanceFilterTelemetry)
		{
			QueryStageForInstanceFilters queryStageForInstanceFilters = BatchDataSetPlanningFilterUtils.DetermineQueryStageForInstanceFilters(dsContext, schema, featureSwitches);
			bool flag = queryStageForInstanceFilters == QueryStageForInstanceFilters.CoreTableAndShowAllRollupContextTables || queryStageForInstanceFilters == QueryStageForInstanceFilters.PostCoreTableAndInShowAllRollupContextTables;
			HashSet<FilterCondition> hashSet = null;
			IFilterDeclarationCollection filterDeclarationCollection;
			if (!flag)
			{
				IFilterDeclarationCollection instance = EmptyFilterDeclarationCollection.Instance;
				filterDeclarationCollection = instance;
			}
			else
			{
				filterDeclarationCollection = InstanceFilterCollector.Analyze(declarations, dsContext, expressionTable, schema, out hashSet);
			}
			InstanceFilterTelemetry instanceFilterTelemetry2;
			if (queryStageForInstanceFilters == QueryStageForInstanceFilters.None)
			{
				instanceFilterTelemetry2 = null;
			}
			else
			{
				InstanceFilterTelemetry instanceFilterTelemetry3 = new InstanceFilterTelemetry();
				instanceFilterTelemetry3.QueryStage = queryStageForInstanceFilters.ToString();
				instanceFilterTelemetry2 = instanceFilterTelemetry3;
				instanceFilterTelemetry3.HasNegatedTuples = hashSet != null && hashSet.Count > 0;
			}
			instanceFilterTelemetry = instanceFilterTelemetry2;
			return new InstanceFiltersContext(filterDeclarationCollection, queryStageForInstanceFilters, hashSet);
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x00038577 File Offset: 0x00036777
		internal IFilterDeclarationCollection InstanceFilterDeclarations { get; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0003857F File Offset: 0x0003677F
		internal QueryStageForInstanceFilters QueryStageForInstanceFilters { get; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x00038587 File Offset: 0x00036787
		internal bool ShouldApplyInstanceFiltersPostFilterToCoreTable
		{
			get
			{
				return this.QueryStageForInstanceFilters == QueryStageForInstanceFilters.PostCoreTableAndInShowAllPostFilter || this.QueryStageForInstanceFilters == QueryStageForInstanceFilters.PostCoreTableAndInShowAllRollupContextTables;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x0003859D File Offset: 0x0003679D
		internal HashSet<FilterCondition> InstanceFiltersRequiringPostFiltering { get; }

		// Token: 0x040006B7 RID: 1719
		internal static readonly InstanceFiltersContext Empty = new InstanceFiltersContext(EmptyFilterDeclarationCollection.Instance, QueryStageForInstanceFilters.None, null);
	}
}
