using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000080 RID: 128
	internal sealed class DsqExpressionPercentileAggregate : DsqExpressionAggregateBase
	{
		// Token: 0x06000512 RID: 1298 RVA: 0x00012B46 File Offset: 0x00010D46
		internal DsqExpressionPercentileAggregate(DataShapeBindingAggregateContainer bindingAggregate, int? selectIndex, int? groupingIndex)
			: base(bindingAggregate, selectIndex, groupingIndex)
		{
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00012B51 File Offset: 0x00010D51
		public override DsqExpressionAggregateKind Kind
		{
			get
			{
				return DsqExpressionAggregateKind.Percentile;
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00012B54 File Offset: 0x00010D54
		public override void Accept(DsqExpressionAggregatesVisitorBase visitor)
		{
			visitor.Visit(this);
		}
	}
}
