using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002A3 RID: 675
	[Serializable]
	internal sealed class FunctionUnaryMinusInteger : FunctionUnary
	{
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x000312D7 File Offset: 0x0002F4D7
		public override int PriorityCode
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x000312DA File Offset: 0x0002F4DA
		public FunctionUnaryMinusInteger()
		{
			base.Rhs = null;
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x000312E9 File Offset: 0x0002F4E9
		public FunctionUnaryMinusInteger(IInternalExpression r)
		{
			base.Rhs = r;
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x000312F8 File Offset: 0x0002F4F8
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Int32;
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x000312FC File Offset: 0x0002F4FC
		public override string UnaryOperator()
		{
			return "- ";
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x00031303 File Offset: 0x0002F503
		public override object Evaluate()
		{
			return Convert.ToInt32(0.0 - base.Rhs.EvaluateDouble());
		}
	}
}
