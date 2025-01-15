using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200017C RID: 380
	internal sealed class BatchSortByMeasureExpressionMappings
	{
		// Token: 0x06000D79 RID: 3449 RVA: 0x00037630 File Offset: 0x00035830
		internal BatchSortByMeasureExpressionMappings()
		{
			this.m_sortByMeasureMappings = new Dictionary<SortKey, BatchSortByMeasureExpressionMappings.SortByMeasureExpressionInfo>();
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00037643 File Offset: 0x00035843
		internal int Count
		{
			get
			{
				return this.m_sortByMeasureMappings.Count;
			}
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00037650 File Offset: 0x00035850
		internal void Add(SortKey sortKey, ExpressionId expressionId, string suggestedName)
		{
			this.m_sortByMeasureMappings.Add(sortKey, new BatchSortByMeasureExpressionMappings.SortByMeasureExpressionInfo(new Expression(ExprNodes.Literal(suggestedName), new ExpressionId?(expressionId)), suggestedName));
		}

		// Token: 0x17000220 RID: 544
		internal BatchSortByMeasureExpressionMappings.SortByMeasureExpressionInfo this[SortKey sortKey]
		{
			get
			{
				return this.m_sortByMeasureMappings[sortKey];
			}
		}

		// Token: 0x0400069C RID: 1692
		private readonly Dictionary<SortKey, BatchSortByMeasureExpressionMappings.SortByMeasureExpressionInfo> m_sortByMeasureMappings;

		// Token: 0x020002F3 RID: 755
		internal sealed class SortByMeasureExpressionInfo
		{
			// Token: 0x060016DA RID: 5850 RVA: 0x00052108 File Offset: 0x00050308
			internal SortByMeasureExpressionInfo(Expression expression, string suggestedName)
			{
				this.m_expression = expression;
				this.m_suggestedName = suggestedName;
			}

			// Token: 0x170003FF RID: 1023
			// (get) Token: 0x060016DB RID: 5851 RVA: 0x0005211E File Offset: 0x0005031E
			internal Expression NewColumnExpression
			{
				get
				{
					return this.m_expression;
				}
			}

			// Token: 0x17000400 RID: 1024
			// (get) Token: 0x060016DC RID: 5852 RVA: 0x00052126 File Offset: 0x00050326
			internal string SuggestedName
			{
				get
				{
					return this.m_suggestedName;
				}
			}

			// Token: 0x04000AF2 RID: 2802
			private readonly Expression m_expression;

			// Token: 0x04000AF3 RID: 2803
			private readonly string m_suggestedName;
		}
	}
}
