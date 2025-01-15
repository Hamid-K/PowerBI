using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002AB RID: 683
	[Serializable]
	internal sealed class FunctionRelationalLessThan : FunctionBinary
	{
		// Token: 0x0600152F RID: 5423 RVA: 0x0003157D File Offset: 0x0002F77D
		public FunctionRelationalLessThan(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00031593 File Offset: 0x0002F793
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00031596 File Offset: 0x0002F796
		public override string BinaryOperator()
		{
			return " < ";
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0003159D File Offset: 0x0002F79D
		public override object Evaluate()
		{
			return base.Lhs.EvaluateDouble() < base.Rhs.EvaluateDouble();
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x000315BC File Offset: 0x0002F7BC
		public override int PriorityCode
		{
			get
			{
				return 9;
			}
		}
	}
}
