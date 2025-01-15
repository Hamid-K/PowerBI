using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002A2 RID: 674
	[Serializable]
	internal sealed class FunctionUnaryMinus : FunctionUnary
	{
		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x00031286 File Offset: 0x0002F486
		public override int PriorityCode
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00031289 File Offset: 0x0002F489
		public FunctionUnaryMinus()
		{
			base.Rhs = null;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00031298 File Offset: 0x0002F498
		public FunctionUnaryMinus(IInternalExpression r)
		{
			base.Rhs = r;
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x000312A7 File Offset: 0x0002F4A7
		public override TypeCode TypeCode()
		{
			return base.Rhs.TypeCode();
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x000312B4 File Offset: 0x0002F4B4
		public override string UnaryOperator()
		{
			return "- ";
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x000312BB File Offset: 0x0002F4BB
		public override object Evaluate()
		{
			return 0.0 - base.Rhs.EvaluateDouble();
		}
	}
}
