using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers
{
	// Token: 0x020001B1 RID: 433
	internal sealed class AggregateSubqueryTable : IAggregateInputTable
	{
		// Token: 0x06000F4C RID: 3916 RVA: 0x0003E288 File Offset: 0x0003C488
		internal AggregateSubqueryTable(PlanOperation operation, IScope outputRowScope, IScope stopScope, ExpressionId expressionId, string expressionPlanName)
		{
			this.Operation = operation;
			this.OutputRowScope = outputRowScope;
			this.StopScope = stopScope;
			this.ExpressionId = expressionId;
			this.ExpressionPlanName = expressionPlanName;
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x0003E2B5 File Offset: 0x0003C4B5
		public PlanOperation Operation { get; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x0003E2BD File Offset: 0x0003C4BD
		public IScope OutputRowScope { get; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x0003E2C5 File Offset: 0x0003C4C5
		public IScope StopScope { get; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0003E2CD File Offset: 0x0003C4CD
		public ExpressionId ExpressionId { get; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x0003E2D5 File Offset: 0x0003C4D5
		public string ExpressionPlanName { get; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x0003E2DD File Offset: 0x0003C4DD
		public string TableName
		{
			get
			{
				return this.ExpressionPlanName;
			}
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0003E2E5 File Offset: 0x0003C4E5
		public bool HasRequiredShowAll(IReadOnlyList<DataMember> requiredState)
		{
			return requiredState.Count == 0;
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0003E2F0 File Offset: 0x0003C4F0
		public PlanOperation ToPlanOperation(DataShapeAnnotations annotations, ScopeTree scopeTree)
		{
			return this.Operation;
		}
	}
}
