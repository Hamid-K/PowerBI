using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200007E RID: 126
	internal sealed class DsqExpressionCountAggregate : DsqExpressionAggregateBase
	{
		// Token: 0x0600050B RID: 1291 RVA: 0x00012ADC File Offset: 0x00010CDC
		internal DsqExpressionCountAggregate(int? selectIndex)
			: base(null, selectIndex, null)
		{
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00012AFA File Offset: 0x00010CFA
		public override DsqExpressionAggregateKind Kind
		{
			get
			{
				return DsqExpressionAggregateKind.Count;
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00012AFD File Offset: 0x00010CFD
		public override void Accept(DsqExpressionAggregatesVisitorBase visitor)
		{
			visitor.Visit(this);
		}
	}
}
