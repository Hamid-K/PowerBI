using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002A4 RID: 676
	[Serializable]
	internal sealed class FunctionUnaryPlus : FunctionUnary
	{
		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x00031324 File Offset: 0x0002F524
		public override int PriorityCode
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x00031327 File Offset: 0x0002F527
		public FunctionUnaryPlus()
		{
			base.Rhs = null;
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00031336 File Offset: 0x0002F536
		public FunctionUnaryPlus(IInternalExpression r)
		{
			base.Rhs = r;
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x00031345 File Offset: 0x0002F545
		public override TypeCode TypeCode()
		{
			return base.Rhs.TypeCode();
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00031352 File Offset: 0x0002F552
		public override string UnaryOperator()
		{
			return "+ ";
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x00031359 File Offset: 0x0002F559
		public override object Evaluate()
		{
			return base.Rhs.Evaluate();
		}
	}
}
