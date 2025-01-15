using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200007D RID: 125
	internal sealed class DsqExpressionMedianAggregate : DsqExpressionAggregateBase
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x00012AC2 File Offset: 0x00010CC2
		internal DsqExpressionMedianAggregate(DataShapeBindingAggregateContainer bindingAggregate, int? selectIndex, int? groupingIndex)
			: base(bindingAggregate, selectIndex, groupingIndex)
		{
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00012ACD File Offset: 0x00010CCD
		public override DsqExpressionAggregateKind Kind
		{
			get
			{
				return DsqExpressionAggregateKind.Median;
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00012AD0 File Offset: 0x00010CD0
		public override void Accept(DsqExpressionAggregatesVisitorBase visitor)
		{
			visitor.Visit(this);
		}
	}
}
