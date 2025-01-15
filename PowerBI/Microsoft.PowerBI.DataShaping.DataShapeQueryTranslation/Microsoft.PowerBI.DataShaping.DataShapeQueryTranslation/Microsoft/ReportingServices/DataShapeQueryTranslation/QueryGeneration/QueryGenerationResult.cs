using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x0200008C RID: 140
	internal sealed class QueryGenerationResult
	{
		// Token: 0x0600069D RID: 1693 RVA: 0x00017F36 File Offset: 0x00016136
		internal QueryGenerationResult(DataSetPlan dataSetPlan, QueryDefinition queryDefinition, ReadOnlyExpressionTable expressionTable, ReadOnlyDictionary<DataMember, string> aggregateIndicatorFieldNames, QueryConstraintStatus? constraintStatus, GeneratedQueryParameterMap queryParameterMap, QueryTrimmer getGroupsToTrimFromQuery = null)
		{
			this.DataSetPlan = dataSetPlan;
			this.QueryDefinition = queryDefinition;
			this.ExpressionTable = expressionTable;
			this.AggregateIndicatorFieldNames = aggregateIndicatorFieldNames;
			this.ConstraintStatus = constraintStatus;
			this.QueryTrimmer = getGroupsToTrimFromQuery;
			this.QueryParameterMap = queryParameterMap;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00017F73 File Offset: 0x00016173
		public DataSetPlan DataSetPlan { get; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00017F7B File Offset: 0x0001617B
		public QueryDefinition QueryDefinition { get; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00017F83 File Offset: 0x00016183
		public ReadOnlyExpressionTable ExpressionTable { get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00017F8B File Offset: 0x0001618B
		public ReadOnlyDictionary<DataMember, string> AggregateIndicatorFieldNames { get; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00017F93 File Offset: 0x00016193
		public QueryConstraintStatus? ConstraintStatus { get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00017F9B File Offset: 0x0001619B
		public QueryTrimmer QueryTrimmer { get; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00017FA3 File Offset: 0x000161A3
		public GeneratedQueryParameterMap QueryParameterMap { get; }
	}
}
