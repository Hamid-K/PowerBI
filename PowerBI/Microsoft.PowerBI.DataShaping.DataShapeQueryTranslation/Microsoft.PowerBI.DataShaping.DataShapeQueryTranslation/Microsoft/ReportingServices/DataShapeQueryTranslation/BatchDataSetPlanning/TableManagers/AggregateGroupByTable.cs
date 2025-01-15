using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers
{
	// Token: 0x020001B0 RID: 432
	internal sealed class AggregateGroupByTable : IAggregateInputTable
	{
		// Token: 0x06000F3C RID: 3900 RVA: 0x0003DF3C File Offset: 0x0003C13C
		internal AggregateGroupByTable(IAggregateInputTable input, IScope outputRowScope, NamingContext namingContext, bool omitSubtotalIndicatorColumnsInGroups)
		{
			this.Input = input;
			this.OutputRowScope = outputRowScope;
			this.m_referencedDetails = new List<Calculation>();
			this.m_aggregates = new List<PlanAggregateExpressionItem>();
			this.NamingContext = namingContext;
			this.m_omitSubtotalIndicatorColumnsInGroups = omitSubtotalIndicatorColumnsInGroups;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0003DF78 File Offset: 0x0003C178
		internal AggregateGroupByTable(IAggregateInputTable input, IScope outputRowScope, IEnumerable<Calculation> referencedDetails, IEnumerable<PlanAggregateExpressionItem> aggregates, NamingContext namingContext, bool omitSubtotalIndicatorColumnsInGroups = false)
			: this(input, outputRowScope, namingContext, omitSubtotalIndicatorColumnsInGroups)
		{
			foreach (Calculation calculation in referencedDetails)
			{
				this.AddReferencedDetail(calculation);
			}
			foreach (PlanAggregateExpressionItem planAggregateExpressionItem in aggregates)
			{
				this.AddAggregate(planAggregateExpressionItem);
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0003E004 File Offset: 0x0003C204
		public IAggregateInputTable Input { get; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x0003E00C File Offset: 0x0003C20C
		public IScope OutputRowScope { get; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0003E014 File Offset: 0x0003C214
		public bool HasAggregates
		{
			get
			{
				return this.m_aggregates.Count > 0;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x0003E024 File Offset: 0x0003C224
		public IEnumerable<PlanAggregateExpressionItem> Aggregates
		{
			get
			{
				return this.m_aggregates;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0003E02C File Offset: 0x0003C22C
		public IEnumerable<Calculation> ReferencedDetails
		{
			get
			{
				return this.m_referencedDetails;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x0003E034 File Offset: 0x0003C234
		internal NamingContext NamingContext { get; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x0003E03C File Offset: 0x0003C23C
		public string TableName
		{
			get
			{
				return this.Input.TableName;
			}
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0003E04C File Offset: 0x0003C24C
		public PlanAggregateExpressionItem AddOrReuseAggregate(string candidateColumnName, ExpressionNode newAggregate, WritableExpressionTable expressionTable, ExpressionContext context, bool preferPlanName)
		{
			PlanAggregateExpressionItem planAggregateExpressionItem;
			if (this.TryGetExistingAggregate(newAggregate, expressionTable, out planAggregateExpressionItem))
			{
				return planAggregateExpressionItem;
			}
			string text = this.NamingContext.GenerateUniqueName(candidateColumnName);
			SubExpressionNode subExpressionNode = expressionTable.CreateSubExpression(newAggregate);
			planAggregateExpressionItem = new PlanAggregateExpressionItem(text, subExpressionNode.ExpressionId, context, preferPlanName);
			this.m_aggregates.Add(planAggregateExpressionItem);
			return planAggregateExpressionItem;
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0003E098 File Offset: 0x0003C298
		private bool TryGetExistingAggregate(ExpressionNode newAggregate, ExpressionTable expressionTable, out PlanAggregateExpressionItem result)
		{
			foreach (PlanAggregateExpressionItem planAggregateExpressionItem in this.m_aggregates)
			{
				if (expressionTable.GetNode(planAggregateExpressionItem.ExpressionId).Equals(newAggregate))
				{
					result = planAggregateExpressionItem;
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0003E108 File Offset: 0x0003C308
		public void AddAggregate(PlanAggregateExpressionItem aggregate)
		{
			Contract.RetailAssert(this.NamingContext.RegisterUniqueName(aggregate.PlanName), "Aggregate name collision");
			this.m_aggregates.Add(aggregate);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0003E131 File Offset: 0x0003C331
		public void AddReferencedDetail(Calculation calculation)
		{
			if (!this.m_referencedDetails.Contains(calculation))
			{
				this.m_referencedDetails.Add(calculation);
			}
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0003E14D File Offset: 0x0003C34D
		public bool HasRequiredShowAll(IReadOnlyList<DataMember> requiredState)
		{
			return this.Input.HasRequiredShowAll(requiredState);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0003E15C File Offset: 0x0003C35C
		public PlanOperation ToPlanOperation(DataShapeAnnotations annotations, ScopeTree scopeTree)
		{
			PlanOperation planOperation = this.Input.ToPlanOperation(annotations, scopeTree);
			IEnumerable<PlanGroupByItem> enumerable = scopeTree.GetAllParentScopes(this.OutputRowScope).OfType<DataMember>().ToGroupByItems(annotations, SubtotalUsage.None, false, true, null);
			IEnumerable<PlanGroupByItem> enumerable2 = this.m_referencedDetails.ToGroupByItems();
			IEnumerable<PlanGroupByItem> subtotalColumnsGroupByItems = this.GetSubtotalColumnsGroupByItems(annotations);
			return planOperation.GroupBy(enumerable.Concat(enumerable2).Concat(subtotalColumnsGroupByItems), this.m_aggregates.Cast<PlanAggregateItem>());
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0003E1C4 File Offset: 0x0003C3C4
		private IEnumerable<PlanGroupByItem> GetSubtotalColumnsGroupByItems(DataShapeAnnotations annotations)
		{
			if (this.m_omitSubtotalIndicatorColumnsInGroups)
			{
				return Util.EmptyReadOnlyList<PlanGroupByItem>();
			}
			AggregateReferenceTable aggregateReferenceTable = this.Input as AggregateReferenceTable;
			if (aggregateReferenceTable == null)
			{
				return Util.EmptyReadOnlyList<PlanGroupByItem>();
			}
			IReadOnlyList<SubtotalColumnFilteringMetadata> totalsMetadata = aggregateReferenceTable.TotalsMetadata;
			if (totalsMetadata.IsNullOrEmpty<SubtotalColumnFilteringMetadata>())
			{
				return Util.EmptyReadOnlyList<PlanGroupByItem>();
			}
			List<PlanGroupByColumn> list = new List<PlanGroupByColumn>(totalsMetadata.Count);
			foreach (SubtotalColumnFilteringMetadata subtotalColumnFilteringMetadata in totalsMetadata)
			{
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				if (annotations.TryGetBatchSubtotalAnnotation(subtotalColumnFilteringMetadata.Member, out batchSubtotalAnnotation) && subtotalColumnFilteringMetadata.SubtotalColumnFilteringValue != null)
				{
					list.Add(new PlanGroupByColumn(batchSubtotalAnnotation.SubtotalIndicatorColumnName));
				}
			}
			return list.ToReadOnlyList<PlanGroupByColumn>();
		}

		// Token: 0x0400072A RID: 1834
		private readonly List<Calculation> m_referencedDetails;

		// Token: 0x0400072B RID: 1835
		private readonly List<PlanAggregateExpressionItem> m_aggregates;

		// Token: 0x0400072C RID: 1836
		private readonly bool m_omitSubtotalIndicatorColumnsInGroups;
	}
}
