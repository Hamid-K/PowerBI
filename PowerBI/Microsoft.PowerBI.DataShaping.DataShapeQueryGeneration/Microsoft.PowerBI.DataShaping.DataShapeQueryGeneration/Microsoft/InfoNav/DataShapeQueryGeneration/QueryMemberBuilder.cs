using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000B7 RID: 183
	internal sealed class QueryMemberBuilder
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x0001906C File Offset: 0x0001726C
		internal QueryMemberBuilder(DsqExpressionGenerator expressionGenerator, DataShapeGenerationErrorContext errorContext, QuerySortGenerator sort, IList<FilterDefinition> instanceFilters, bool isPrimary, SubtotalType subtotal, QuerySourceExpressionReferenceContext sourceRefContext, QueryGroupBuilderOptions options, bool isContextOnly = false)
		{
			this._expressionGenerator = expressionGenerator;
			this._errorContext = errorContext;
			this._sort = sort;
			this._instanceFilters = ((instanceFilters != null) ? instanceFilters.AsReadOnlyList<FilterDefinition>() : null) ?? null;
			this._isPrimary = isPrimary;
			this._sourceRefContext = sourceRefContext;
			this._measureCalcs = new List<ProjectedDsqExpression>();
			this._projectionExpressions = new List<ResolvedQueryExpression>();
			this._groupBuilder = new QueryGroupBuilder(expressionGenerator, errorContext, subtotal, options, isContextOnly);
			this._isContextOnly = isContextOnly;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x000190EE File Offset: 0x000172EE
		internal QueryGroupBuilder Group
		{
			get
			{
				return this._groupBuilder;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x000190F6 File Offset: 0x000172F6
		internal IReadOnlyList<QueryGroupValueBuilder> ValueBuilders
		{
			get
			{
				return this._groupBuilder.ValueBuilders;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00019103 File Offset: 0x00017303
		internal IEnumerable<ProjectedDsqExpression> MeasureCalculations
		{
			get
			{
				return this._measureCalcs;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001910B File Offset: 0x0001730B
		internal bool IsNonAggregatable
		{
			get
			{
				return this._groupBuilder.IsNonAggregatable;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00019118 File Offset: 0x00017318
		internal bool HasValues
		{
			get
			{
				return this.ValueBuilders.Count > 0;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00019128 File Offset: 0x00017328
		internal bool HasDetailIdentity
		{
			get
			{
				return this.Group.HasDetailIdentity;
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00019135 File Offset: 0x00017335
		internal bool HasInstanceFilters()
		{
			return !this._instanceFilters.IsNullOrEmpty<FilterDefinition>();
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00019145 File Offset: 0x00017345
		internal bool TryAddProjection(ResolvedQuerySelect select, int selectIndex, bool showItemsWithNoData = false)
		{
			return this.TryAddProjection(select.Expression, selectIndex, select.NativeReferenceName, showItemsWithNoData);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001915C File Offset: 0x0001735C
		internal bool TryAddProjection(ResolvedQueryExpression expr, int selectIndex, string nativeReferenceName, bool showItemsWithNoData = false)
		{
			ResolvedQuerySourceRefExpression resolvedQuerySourceRefExpression = expr as ResolvedQuerySourceRefExpression;
			if (resolvedQuerySourceRefExpression != null)
			{
				return this.TryAddEntitySet(expr, resolvedQuerySourceRefExpression.SourceEntity, selectIndex, nativeReferenceName, showItemsWithNoData);
			}
			IConceptualColumn conceptualColumn;
			if (expr.TryGetAsProperty(out conceptualColumn))
			{
				this.AddProjectedColumnToGroup<IConceptualColumn>(expr, selectIndex, nativeReferenceName, conceptualColumn, QueryConceptualColumnAdapter.Instance, showItemsWithNoData);
				return true;
			}
			IntermediateQueryTransformTableColumn intermediateQueryTransformTableColumn;
			if (this._expressionGenerator.TryGetAsTransformColumn(expr, out intermediateQueryTransformTableColumn))
			{
				TransformTableColumnActAs actAs = intermediateQueryTransformTableColumn.ActAs;
				if (actAs == TransformTableColumnActAs.GroupKey)
				{
					this.AddProjectedColumnToGroup<IntermediateQueryTransformTableColumn>(expr, selectIndex, nativeReferenceName, intermediateQueryTransformTableColumn, QueryTransformColumnAdapter.Instance, showItemsWithNoData);
					return true;
				}
				if (actAs - TransformTableColumnActAs.Detail <= 1)
				{
					return false;
				}
				throw new InvalidOperationException(StringUtil.FormatInvariant("Unexpected TransformTableColumnActAs value {0}", intermediateQueryTransformTableColumn.ActAs));
			}
			else
			{
				IntermediateTableSchemaColumn intermediateTableSchemaColumn;
				if (this._sourceRefContext.TryGetColumnInSource(expr, this._errorContext, out intermediateTableSchemaColumn))
				{
					this.AddProjectedColumnToGroup<IIntermediateTableSchemaItem>(expr, selectIndex, nativeReferenceName, intermediateTableSchemaColumn, SourceRefColumnAdapter.Instance, showItemsWithNoData);
					return true;
				}
				return false;
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001921F File Offset: 0x0001741F
		private void AddProjectedColumnToGroup<TColumn>(ResolvedQueryExpression expr, int selectIndex, string nativeReferenceName, TColumn column, QueryColumnAdapter<TColumn> adapter, bool showItemsWithNoData)
		{
			this._groupBuilder.AddProjectedColumn<TColumn>(selectIndex, nativeReferenceName, column, adapter, showItemsWithNoData);
			this._projectionExpressions.Add(expr);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00019240 File Offset: 0x00017440
		internal bool TryAddGroupBy(ResolvedQueryExpression expr, int groupByIndex)
		{
			return this._groupBuilder.TryAddGroupBy(expr, groupByIndex);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001924F File Offset: 0x0001744F
		private bool TryAddEntitySet(ResolvedQueryExpression expr, IConceptualEntity entity, int selectIndex, string nativeReferenceName, bool showItemsWithNoData)
		{
			if (this._groupBuilder.TryAddEntitySet(entity, selectIndex, nativeReferenceName, showItemsWithNoData))
			{
				this._projectionExpressions.Add(expr);
				return true;
			}
			return false;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00019273 File Offset: 0x00017473
		internal void AddMeasureCalculation(ProjectedDsqExpression expr)
		{
			this._measureCalcs.Add(expr);
			this._sort.Rebind(expr);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001928D File Offset: 0x0001748D
		internal void SuppressSubtotals()
		{
			this._groupBuilder.SuppressSubtotals();
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001929A File Offset: 0x0001749A
		internal void ClearInstanceFilters()
		{
			this._instanceFilters = null;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000192A3 File Offset: 0x000174A3
		internal void SetExplicitSubtotals()
		{
			this._hasExplicitSubtotal = true;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000192AC File Offset: 0x000174AC
		internal void ClearExplicitSubtotals()
		{
			this._hasExplicitSubtotal = false;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000192B5 File Offset: 0x000174B5
		internal void SupressSortByMeasureRollup()
		{
			this._groupBuilder.SuppressSortByMeasureRollup();
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000192C2 File Offset: 0x000174C2
		internal bool ProjectsAllExpressions(IReadOnlyList<ResolvedQueryExpression> expressions)
		{
			return this._projectionExpressions.SetEquals(expressions);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x000192D0 File Offset: 0x000174D0
		internal QueryMember ToMember()
		{
			List<QueryGroupValue> list;
			return new QueryMember(this._groupBuilder.ToGroup(this._isPrimary, this._sort, out list), list, this._measureCalcs, this._projectionExpressions, this._instanceFilters, this._hasExplicitSubtotal, this._isContextOnly);
		}

		// Token: 0x04000386 RID: 902
		private readonly DataShapeGenerationErrorContext _errorContext;

		// Token: 0x04000387 RID: 903
		private readonly DsqExpressionGenerator _expressionGenerator;

		// Token: 0x04000388 RID: 904
		private readonly QueryGroupBuilder _groupBuilder;

		// Token: 0x04000389 RID: 905
		private readonly QuerySortGenerator _sort;

		// Token: 0x0400038A RID: 906
		private readonly bool _isPrimary;

		// Token: 0x0400038B RID: 907
		private readonly List<ProjectedDsqExpression> _measureCalcs;

		// Token: 0x0400038C RID: 908
		private readonly List<ResolvedQueryExpression> _projectionExpressions;

		// Token: 0x0400038D RID: 909
		private readonly QuerySourceExpressionReferenceContext _sourceRefContext;

		// Token: 0x0400038E RID: 910
		private readonly bool _isContextOnly;

		// Token: 0x0400038F RID: 911
		private IReadOnlyList<FilterDefinition> _instanceFilters;

		// Token: 0x04000390 RID: 912
		private bool _hasExplicitSubtotal;
	}
}
