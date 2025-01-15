using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001B8 RID: 440
	internal sealed class AggregatesTableSingleRowOnlyStrategy : AggregatesTableStrategy
	{
		// Token: 0x06000F6F RID: 3951 RVA: 0x0003E7AF File Offset: 0x0003C9AF
		internal AggregatesTableSingleRowOnlyStrategy(IAggregatesPlanningContext plannerContext, DataShapeContext dsContext, IReadOnlyList<PlanOperation> contextTables, IReadOnlyList<Calculation> aggregateCalculations, IScope containingScope)
			: base(plannerContext, dsContext, contextTables)
		{
			this.m_aggregateCalculations = aggregateCalculations;
			this.m_containingScope = containingScope;
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0003E7CC File Offset: 0x0003C9CC
		internal override PlanOperationContext ToTableContext()
		{
			if (this.m_aggregateCalculations.Count == 0)
			{
				return null;
			}
			Contract.RetailAssert(!this.m_dsContext.HasComplexSlicer, "Aggregate measures are not allowed with complex slicers.");
			return new PlanOperationContext(this.CreateSingleRowWithContextTables(this.m_aggregateCalculations), this.m_containingScope.AsList<IScope>(), this.m_aggregateCalculations);
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0003E824 File Offset: 0x0003CA24
		private PlanOperation CreateSingleRowWithContextTables(IEnumerable<Calculation> aggregateCalcs)
		{
			IEnumerable<PlanOperation> enumerable = null;
			IEnumerable<ExistsFilterItem> enumerable2 = null;
			if (BatchDataSetPlanningUtils.ContainsModelReference(aggregateCalcs, this.m_plannerContext.OutputExpressionTable, this.m_plannerContext.CalculationMap))
			{
				enumerable = this.m_contextTables;
				ExistsFilterCondition existsFilter = this.m_plannerContext.Annotations.GetDataShapeAnnotation(this.m_dsContext.DataShape).ExistsFilter;
				if (existsFilter != null)
				{
					enumerable2 = existsFilter.Items;
				}
			}
			List<SingleRowAdditionalColumn> list = null;
			Filter filter = this.m_plannerContext.Annotations.GetDataShapeAnnotation(this.m_dsContext.DataShape).DataShapeValueFilters.SingleOrDefault<Filter>();
			if (filter != null)
			{
				NamingContext namingContext = new NamingContext(null);
				ICommonPlanningContext plannerContext = this.m_plannerContext;
				NamingContext namingContext2 = namingContext;
				Filter filter2 = filter;
				Func<string, Expression, ExpressionContext, SingleRowAdditionalColumn> func;
				if ((func = AggregatesTableSingleRowOnlyStrategy.<>O.<0>__CreateAdditionalColumn) == null)
				{
					func = (AggregatesTableSingleRowOnlyStrategy.<>O.<0>__CreateAdditionalColumn = new Func<string, Expression, ExpressionContext, SingleRowAdditionalColumn>(AggregatesTableSingleRowOnlyStrategy.CreateAdditionalColumn));
				}
				list = ValueFilterTableBuilder.CreateAdditionalColumns<SingleRowAdditionalColumn>(plannerContext, namingContext2, filter2, func);
			}
			PlanOperation planOperation = PlanOperationBuilder.SingleRow(aggregateCalcs, enumerable, enumerable2, list);
			if (filter != null)
			{
				planOperation = planOperation.FilterBy(filter.Condition);
			}
			return planOperation;
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0003E904 File Offset: 0x0003CB04
		private static SingleRowAdditionalColumn CreateAdditionalColumn(string planName, Expression expr, ExpressionContext exprContext)
		{
			return new SingleRowAdditionalColumn(planName, expr.ExpressionId.Value, exprContext);
		}

		// Token: 0x04000741 RID: 1857
		private readonly IScope m_containingScope;

		// Token: 0x04000742 RID: 1858
		private readonly IReadOnlyList<Calculation> m_aggregateCalculations;

		// Token: 0x02000305 RID: 773
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000B24 RID: 2852
			public static Func<string, Expression, ExpressionContext, SingleRowAdditionalColumn> <0>__CreateAdditionalColumn;
		}
	}
}
