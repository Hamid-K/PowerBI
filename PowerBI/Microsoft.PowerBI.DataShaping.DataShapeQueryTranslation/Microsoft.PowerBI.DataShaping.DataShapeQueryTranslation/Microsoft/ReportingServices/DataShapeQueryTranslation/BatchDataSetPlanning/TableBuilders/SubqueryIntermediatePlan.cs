using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001E3 RID: 483
	internal sealed class SubqueryIntermediatePlan
	{
		// Token: 0x060010AB RID: 4267 RVA: 0x00045972 File Offset: 0x00043B72
		internal SubqueryIntermediatePlan(IValueFilterPlanningContext plannerContext, BatchGroupAndJoinBuilder builder, PlanDeclarationCollection declarations, IScope outerScope, IScope innerScope)
		{
			this.m_plannerContext = plannerContext;
			this.m_joinBuilder = builder;
			this.m_declarations = declarations;
			this.m_outerScope = outerScope;
			this.m_innerScope = innerScope;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0004599F File Offset: 0x00043B9F
		internal void AddExpression(string expressionPlanName, Expression expression, bool suppressJoinPredicate, ExpressionContext expressionContext)
		{
			this.m_joinBuilder.AddAdditionalColumn(expressionPlanName, expression, suppressJoinPredicate, expressionContext);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x000459B1 File Offset: 0x00043BB1
		internal void AddValueFilter(Filter valueFilter)
		{
			if (this.m_valueFilters == null)
			{
				this.m_valueFilters = new List<Filter>();
			}
			this.m_valueFilters.Add(valueFilter);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x000459D2 File Offset: 0x00043BD2
		internal void AddValueFilters(IEnumerable<Filter> valueFilters)
		{
			if (this.m_valueFilters == null)
			{
				this.m_valueFilters = new List<Filter>();
			}
			this.m_valueFilters.AddRange(valueFilters);
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x000459F3 File Offset: 0x00043BF3
		internal void AddSubqueryFilter(Filter subqueryFilter)
		{
			Util.AddToLazyList<Filter>(ref this.m_subqueryFilters, subqueryFilter);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00045A01 File Offset: 0x00043C01
		internal void AddSubqueryFilters(IReadOnlyList<Filter> subqueryFilters)
		{
			Util.AddToLazyList<Filter>(ref this.m_subqueryFilters, subqueryFilters);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00045A10 File Offset: 0x00043C10
		public PlanOperation ToPlanOperation()
		{
			bool flag = false;
			if (this.m_valueFilters != null && this.m_valueFilters.Count > 0)
			{
				flag = this.m_valueFilters.Any((Filter filter) => this.ShouldApplyAsContextTable(filter));
				if (flag)
				{
					this.ApplyValueFiltersAsContextTables(this.m_valueFilters);
				}
				else
				{
					this.AddValueFiltersAdditionalColumns(this.m_valueFilters);
				}
			}
			PlanOperation planOperation = this.m_joinBuilder.ToPlanOperation(null);
			if (this.m_valueFilters != null && !flag)
			{
				foreach (Filter filter2 in this.m_valueFilters)
				{
					planOperation = planOperation.FilterBy(filter2.Condition);
				}
			}
			planOperation = this.ApplySubqueryFilters(planOperation);
			return planOperation;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00045AD8 File Offset: 0x00043CD8
		private bool ShouldApplyAsContextTable(Filter valueFilter)
		{
			IScope resolvedScope = valueFilter.Target.GetResolvedScope(this.m_plannerContext.OutputExpressionTable);
			return BatchDataSetPlanningFilterUtils.ShouldApplyValueFilterAsContextTable(this.m_plannerContext, this.m_joinBuilder, resolvedScope);
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00045B10 File Offset: 0x00043D10
		private void ApplyValueFiltersAsContextTables(IEnumerable<Filter> valueFilters)
		{
			if (valueFilters != null)
			{
				foreach (Filter filter in valueFilters)
				{
					PlanOperation planOperation = BatchDataSetPlanningFilterUtils.CreateValueFilterTable(this.m_plannerContext, this.m_joinBuilder, this.m_declarations, filter, this.m_innerScope, false);
					this.m_joinBuilder.AddContextTable(planOperation);
				}
			}
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00045B80 File Offset: 0x00043D80
		private void AddValueFiltersAdditionalColumns(IEnumerable<Filter> valueFilters)
		{
			if (valueFilters != null)
			{
				foreach (Filter filter in valueFilters)
				{
					ICommonPlanningContext plannerContext = this.m_plannerContext;
					NamingContext namingContext = this.m_joinBuilder.NamingContext;
					Filter filter2 = filter;
					Func<string, Expression, ExpressionContext, PlanGroupAndJoinAdditionalColumn> func;
					if ((func = SubqueryIntermediatePlan.<>O.<0>__CreateValueFilterGroupAndJoinColumn) == null)
					{
						func = (SubqueryIntermediatePlan.<>O.<0>__CreateValueFilterGroupAndJoinColumn = new Func<string, Expression, ExpressionContext, PlanGroupAndJoinAdditionalColumn>(ValueFilterTableBuilder.CreateValueFilterGroupAndJoinColumn));
					}
					List<PlanGroupAndJoinAdditionalColumn> list = ValueFilterTableBuilder.CreateAdditionalColumns<PlanGroupAndJoinAdditionalColumn>(plannerContext, namingContext, filter2, func);
					this.m_joinBuilder.AddAdditionalColumns(list);
				}
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00045C04 File Offset: 0x00043E04
		private PlanOperation ApplySubqueryFilters(PlanOperation table)
		{
			if (this.m_subqueryFilters.IsNullOrEmpty<Filter>())
			{
				return table;
			}
			foreach (Filter filter in this.m_subqueryFilters)
			{
				DataShape containingDataShape = this.m_plannerContext.Annotations.GetContainingDataShape(filter);
				Filter filter2 = containingDataShape.Filters.SingleOrDefault((Filter f) => f.UsageKind == FilterUsageKind.SubqueryOutput);
				table = SubqueryFilterTableBuilder.ProjectAndApplySubqueryFilter(this.m_plannerContext, containingDataShape, filter2.Condition, table);
			}
			return table;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00045CB4 File Offset: 0x00043EB4
		public SubqueryIntermediatePlan Clone()
		{
			SubqueryIntermediatePlan subqueryIntermediatePlan = new SubqueryIntermediatePlan(this.m_plannerContext, this.m_joinBuilder.Clone(null), this.m_declarations, this.m_outerScope, this.m_innerScope);
			if (this.m_valueFilters != null)
			{
				subqueryIntermediatePlan.AddValueFilters(this.m_valueFilters);
			}
			if (this.m_subqueryFilters != null)
			{
				subqueryIntermediatePlan.AddSubqueryFilters(this.m_subqueryFilters);
			}
			return subqueryIntermediatePlan;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x00045D14 File Offset: 0x00043F14
		internal IScope OuterScope
		{
			get
			{
				return this.m_outerScope;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x00045D1C File Offset: 0x00043F1C
		internal IScope InnerScope
		{
			get
			{
				return this.m_innerScope;
			}
		}

		// Token: 0x040007D3 RID: 2003
		private readonly IValueFilterPlanningContext m_plannerContext;

		// Token: 0x040007D4 RID: 2004
		private readonly BatchGroupAndJoinBuilder m_joinBuilder;

		// Token: 0x040007D5 RID: 2005
		private readonly PlanDeclarationCollection m_declarations;

		// Token: 0x040007D6 RID: 2006
		private readonly IScope m_outerScope;

		// Token: 0x040007D7 RID: 2007
		private readonly IScope m_innerScope;

		// Token: 0x040007D8 RID: 2008
		private List<Filter> m_valueFilters;

		// Token: 0x040007D9 RID: 2009
		private List<Filter> m_subqueryFilters;

		// Token: 0x02000318 RID: 792
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000B58 RID: 2904
			public static Func<string, Expression, ExpressionContext, PlanGroupAndJoinAdditionalColumn> <0>__CreateValueFilterGroupAndJoinColumn;
		}
	}
}
