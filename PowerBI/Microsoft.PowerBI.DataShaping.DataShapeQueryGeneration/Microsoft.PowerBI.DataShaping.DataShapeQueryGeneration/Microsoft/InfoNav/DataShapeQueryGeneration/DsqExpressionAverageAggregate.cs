using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200007A RID: 122
	internal sealed class DsqExpressionAverageAggregate : DsqExpressionAggregateBase
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x00012A7D File Offset: 0x00010C7D
		internal DsqExpressionAverageAggregate(DataShapeBindingAggregateContainer bindingAggregate, int? selectIndex, int? groupingIndex)
			: base(bindingAggregate, selectIndex, groupingIndex)
		{
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00012A88 File Offset: 0x00010C88
		public override DsqExpressionAggregateKind Kind
		{
			get
			{
				return DsqExpressionAggregateKind.Average;
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00012A8B File Offset: 0x00010C8B
		public override void Accept(DsqExpressionAggregatesVisitorBase visitor)
		{
			visitor.Visit(this);
		}
	}
}
