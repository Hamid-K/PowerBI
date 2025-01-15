using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002A8 RID: 680
	[Serializable]
	internal sealed class FunctionRelationalGreaterThanEqual : FunctionBinary
	{
		// Token: 0x06001520 RID: 5408 RVA: 0x000314A8 File Offset: 0x0002F6A8
		public FunctionRelationalGreaterThanEqual(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x000314BE File Offset: 0x0002F6BE
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x000314C1 File Offset: 0x0002F6C1
		public override string BinaryOperator()
		{
			return " >= ";
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x000314C8 File Offset: 0x0002F6C8
		public override object Evaluate()
		{
			return base.Lhs.EvaluateDouble() >= base.Rhs.EvaluateDouble();
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x000314EA File Offset: 0x0002F6EA
		public override int PriorityCode
		{
			get
			{
				return 9;
			}
		}
	}
}
