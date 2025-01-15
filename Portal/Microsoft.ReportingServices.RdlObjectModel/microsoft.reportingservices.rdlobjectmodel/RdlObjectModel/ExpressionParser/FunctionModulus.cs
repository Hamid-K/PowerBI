using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200029C RID: 668
	[Serializable]
	internal sealed class FunctionModulus : FunctionBinary
	{
		// Token: 0x060014D6 RID: 5334 RVA: 0x00030D5B File Offset: 0x0002EF5B
		public FunctionModulus(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x00030D71 File Offset: 0x0002EF71
		public override TypeCode TypeCode()
		{
			return base.TypeCode();
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00030D79 File Offset: 0x0002EF79
		public override string BinaryOperator()
		{
			return " Mod ";
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x00030D80 File Offset: 0x0002EF80
		public override object Evaluate()
		{
			double num = base.Lhs.EvaluateDouble();
			double num2 = base.Rhs.EvaluateDouble();
			return num % num2;
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x00030DAB File Offset: 0x0002EFAB
		public override int PriorityCode
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00030DAE File Offset: 0x0002EFAE
		public override void Validate(ExpressionValidationContext context)
		{
			base.ArrayCheck();
			base.ValidateIntOperandTypes();
		}
	}
}
