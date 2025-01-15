using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200007B RID: 123
	internal sealed class DsqExpressionMaxAggregate : DsqExpressionAggregateBase
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x00012A94 File Offset: 0x00010C94
		internal DsqExpressionMaxAggregate(DataShapeBindingAggregateContainer bindingAggregate, int? selectIndex, int? groupingIndex)
			: base(bindingAggregate, selectIndex, groupingIndex)
		{
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00012A9F File Offset: 0x00010C9F
		public override DsqExpressionAggregateKind Kind
		{
			get
			{
				return DsqExpressionAggregateKind.Max;
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00012AA2 File Offset: 0x00010CA2
		public override void Accept(DsqExpressionAggregatesVisitorBase visitor)
		{
			visitor.Visit(this);
		}
	}
}
