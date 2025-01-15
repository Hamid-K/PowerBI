using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis
{
	// Token: 0x020000B1 RID: 177
	internal sealed class FilterReconstructor : FilterVisitor<FilterCondition>
	{
		// Token: 0x060007CC RID: 1996 RVA: 0x0001E2D3 File Offset: 0x0001C4D3
		private FilterReconstructor(FilterCondition filterConditionToRemove)
			: base(null)
		{
			this.m_filterConditionToRemove = filterConditionToRemove;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001E2E3 File Offset: 0x0001C4E3
		public static FilterCondition RemoveFilterCondition(FilterCondition filter, FilterCondition filterToRemove)
		{
			if (filter.Equals(filterToRemove))
			{
				return null;
			}
			return new FilterReconstructor(filterToRemove).Visit(filter);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001E2FC File Offset: 0x0001C4FC
		internal override FilterCondition Visit(UnaryFilterCondition condition)
		{
			return condition;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001E2FF File Offset: 0x0001C4FF
		internal override FilterCondition Visit(BinaryFilterCondition condition)
		{
			return condition;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001E304 File Offset: 0x0001C504
		internal override FilterCondition Visit(CompoundFilterCondition condition)
		{
			if (condition.Conditions.Contains(this.m_filterConditionToRemove))
			{
				Contract.RetailAssert(condition.Operator == CompoundFilterOperator.Any, "Only the Any operator is supported when removing filter conditions.");
				this.m_filterRemoved = true;
				return condition.RemoveCondition(this.m_filterConditionToRemove);
			}
			List<FilterCondition> list = (from c in condition.Conditions
				select this.Visit(c) into c
				where c != null
				select c).ToList<FilterCondition>();
			if (this.m_filterRemoved)
			{
				return condition.Clone(list);
			}
			return condition;
		}

		// Token: 0x040003DE RID: 990
		private readonly FilterCondition m_filterConditionToRemove;

		// Token: 0x040003DF RID: 991
		private bool m_filterRemoved;
	}
}
