using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002A5 RID: 677
	[Serializable]
	internal sealed class FunctionUnaryPlusInteger : FunctionUnary
	{
		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x00031366 File Offset: 0x0002F566
		public override int PriorityCode
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x00031369 File Offset: 0x0002F569
		public FunctionUnaryPlusInteger()
		{
			base.Rhs = null;
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x00031378 File Offset: 0x0002F578
		public FunctionUnaryPlusInteger(IInternalExpression r)
		{
			base.Rhs = r;
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00031387 File Offset: 0x0002F587
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Int32;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0003138B File Offset: 0x0002F58B
		public override string UnaryOperator()
		{
			return "+ ";
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00031392 File Offset: 0x0002F592
		public override object Evaluate()
		{
			return base.Rhs.Evaluate();
		}
	}
}
