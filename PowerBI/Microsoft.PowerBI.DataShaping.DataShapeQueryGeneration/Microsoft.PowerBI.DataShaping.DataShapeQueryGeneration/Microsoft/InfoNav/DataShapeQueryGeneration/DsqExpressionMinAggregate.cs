using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200007C RID: 124
	internal sealed class DsqExpressionMinAggregate : DsqExpressionAggregateBase
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x00012AAB File Offset: 0x00010CAB
		internal DsqExpressionMinAggregate(DataShapeBindingAggregateContainer bindingAggregate, int? selectIndex, int? groupingIndex)
			: base(bindingAggregate, selectIndex, groupingIndex)
		{
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00012AB6 File Offset: 0x00010CB6
		public override DsqExpressionAggregateKind Kind
		{
			get
			{
				return DsqExpressionAggregateKind.Min;
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00012AB9 File Offset: 0x00010CB9
		public override void Accept(DsqExpressionAggregatesVisitorBase visitor)
		{
			visitor.Visit(this);
		}
	}
}
