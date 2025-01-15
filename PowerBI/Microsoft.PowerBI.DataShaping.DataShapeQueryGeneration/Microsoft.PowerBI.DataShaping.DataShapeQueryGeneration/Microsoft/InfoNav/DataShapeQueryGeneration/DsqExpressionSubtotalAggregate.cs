using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200007F RID: 127
	internal sealed class DsqExpressionSubtotalAggregate : DsqExpressionAggregateBase
	{
		// Token: 0x0600050E RID: 1294 RVA: 0x00012B08 File Offset: 0x00010D08
		private DsqExpressionSubtotalAggregate()
			: base(null, null, null)
		{
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x00012B2E File Offset: 0x00010D2E
		public override DsqExpressionAggregateKind Kind
		{
			get
			{
				return DsqExpressionAggregateKind.Subtotal;
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00012B31 File Offset: 0x00010D31
		public override void Accept(DsqExpressionAggregatesVisitorBase visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040002D7 RID: 727
		internal static readonly DsqExpressionSubtotalAggregate Instance = new DsqExpressionSubtotalAggregate();
	}
}
