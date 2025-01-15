using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002AC RID: 684
	[Serializable]
	internal sealed class FunctionRelationalLessThanEqual : FunctionBinary
	{
		// Token: 0x06001534 RID: 5428 RVA: 0x000315C0 File Offset: 0x0002F7C0
		public FunctionRelationalLessThanEqual(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x000315D6 File Offset: 0x0002F7D6
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x000315D9 File Offset: 0x0002F7D9
		public override string BinaryOperator()
		{
			return " <= ";
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x000315E0 File Offset: 0x0002F7E0
		public override object Evaluate()
		{
			return base.Lhs.EvaluateDouble() <= base.Rhs.EvaluateDouble();
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x00031602 File Offset: 0x0002F802
		public override int PriorityCode
		{
			get
			{
				return 9;
			}
		}
	}
}
