using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200009B RID: 155
	internal abstract class FilterVisitor<TResult>
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x00007174 File Offset: 0x00005374
		protected FilterVisitor(VisitDataShapeDelegate visitDataShape = null)
		{
			this.m_visitDataShape = visitDataShape;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00007183 File Offset: 0x00005383
		protected virtual TResult Visit(Filter filter)
		{
			return this.Visit(filter.Condition);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00007194 File Offset: 0x00005394
		protected virtual void Visit(List<FilterCondition> conditions, ObjectType objectType, string propertyName)
		{
			foreach (FilterCondition filterCondition in conditions)
			{
				this.Visit(filterCondition);
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000071E4 File Offset: 0x000053E4
		protected virtual TResult Visit(FilterCondition condition)
		{
			return condition.Accept<TResult>(this);
		}

		// Token: 0x060003A6 RID: 934
		internal abstract TResult Visit(UnaryFilterCondition condition);

		// Token: 0x060003A7 RID: 935
		internal abstract TResult Visit(BinaryFilterCondition condition);

		// Token: 0x060003A8 RID: 936
		internal abstract TResult Visit(CompoundFilterCondition condition);

		// Token: 0x060003A9 RID: 937 RVA: 0x000071F0 File Offset: 0x000053F0
		internal virtual TResult Visit(InFilterCondition condition)
		{
			return default(TResult);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00007208 File Offset: 0x00005408
		internal virtual TResult Visit(ContextFilterCondition condition)
		{
			this.VisitFilterConditionDataShape(condition.DataShape, condition.ObjectType);
			return default(TResult);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00007230 File Offset: 0x00005430
		internal virtual TResult Visit(ApplyFilterCondition condition)
		{
			return default(TResult);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00007246 File Offset: 0x00005446
		protected virtual void VisitFilterConditionDataShape(DataShape dataShape, ObjectType filterConditionType)
		{
			if (dataShape != null && this.m_visitDataShape != null)
			{
				this.m_visitDataShape(dataShape, filterConditionType);
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00007260 File Offset: 0x00005460
		internal virtual TResult Visit(FilterEmptyGroupsCondition condition)
		{
			return default(TResult);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00007278 File Offset: 0x00005478
		internal virtual TResult Visit(ExistsFilterCondition condition)
		{
			return default(TResult);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00007290 File Offset: 0x00005490
		internal virtual TResult Visit(AnyValueFilterCondition condition)
		{
			return default(TResult);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000072A8 File Offset: 0x000054A8
		internal virtual TResult Visit(DefaultValueFilterCondition condition)
		{
			return default(TResult);
		}

		// Token: 0x04000198 RID: 408
		private readonly VisitDataShapeDelegate m_visitDataShape;
	}
}
