using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000299 RID: 665
	[Serializable]
	internal sealed class FunctionExp : FunctionBinary
	{
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x00030A83 File Offset: 0x0002EC83
		public override int PriorityCode
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00030A86 File Offset: 0x0002EC86
		public FunctionExp()
		{
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00030A90 File Offset: 0x0002EC90
		public FunctionExp(IInternalExpression lhs, IInternalExpression rhs)
		{
			if (rhs is FunctionExp)
			{
				base.Lhs = new FunctionExp(lhs, ((FunctionExp)rhs).Lhs);
				base.Rhs = ((FunctionExp)rhs).Rhs;
				return;
			}
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x00030AE2 File Offset: 0x0002ECE2
		public override string BinaryOperator()
		{
			return "^";
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x00030AE9 File Offset: 0x0002ECE9
		public override object Evaluate()
		{
			return Math.Pow(base.Lhs.EvaluateDouble(), base.Rhs.EvaluateDouble());
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x00030B0B File Offset: 0x0002ED0B
		public override void Validate(ExpressionValidationContext context)
		{
			base.ArrayCheck();
			base.ValidateIntOperandTypes();
		}
	}
}
