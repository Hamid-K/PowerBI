using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000140 RID: 320
	internal sealed class BatchQueryGenerationResult
	{
		// Token: 0x06000BCB RID: 3019 RVA: 0x0002F268 File Offset: 0x0002D468
		internal BatchQueryGenerationResult(BatchDataSetPlan dataSetPlan, BatchQueryDefinition queryDefinition, ExpressionTable expressionTable, Dictionary<Identifier, ExpressionId> intersectionCorrelationExpressionId, BatchQueryMemberMatchConditions memberMatchConditions, BatchQueryMemberDiscardConditions memberDiscardConditions, BatchQueryRestartIndicator restartIndicator, GeneratedQueryParameterMap queryParameterMap)
		{
			this.DataSetPlan = dataSetPlan;
			this.QueryDefinition = queryDefinition;
			this.ExpressionTable = expressionTable;
			this.m_intersectionCorrelationExpressionId = intersectionCorrelationExpressionId;
			this.MemberMatchConditions = memberMatchConditions;
			this.MemberDiscardConditions = memberDiscardConditions;
			this.RestartIndicator = restartIndicator;
			this.QueryParameterMap = queryParameterMap;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0002F2B8 File Offset: 0x0002D4B8
		public BatchDataSetPlan DataSetPlan { get; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x0002F2C0 File Offset: 0x0002D4C0
		public BatchQueryDefinition QueryDefinition { get; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0002F2C8 File Offset: 0x0002D4C8
		public ExpressionTable ExpressionTable { get; }

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002F2D0 File Offset: 0x0002D4D0
		public ExpressionId? GetIntersectionCorrelationExpressionId(IContextItem item)
		{
			if (this.m_intersectionCorrelationExpressionId == null)
			{
				return null;
			}
			ExpressionId expressionId;
			this.m_intersectionCorrelationExpressionId.TryGetValue(item.Id, out expressionId);
			return new ExpressionId?(expressionId);
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0002F309 File Offset: 0x0002D509
		public BatchQueryMemberMatchConditions MemberMatchConditions { get; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0002F311 File Offset: 0x0002D511
		public BatchQueryMemberDiscardConditions MemberDiscardConditions { get; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0002F319 File Offset: 0x0002D519
		public BatchQueryRestartIndicator RestartIndicator { get; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0002F321 File Offset: 0x0002D521
		public GeneratedQueryParameterMap QueryParameterMap { get; }

		// Token: 0x040005F3 RID: 1523
		private readonly IReadOnlyDictionary<Identifier, ExpressionId> m_intersectionCorrelationExpressionId;
	}
}
