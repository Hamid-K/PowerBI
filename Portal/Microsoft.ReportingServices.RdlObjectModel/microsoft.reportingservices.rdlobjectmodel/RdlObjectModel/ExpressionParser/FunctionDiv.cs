using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000297 RID: 663
	[Serializable]
	internal sealed class FunctionDiv : FunctionBinary
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x0003084D File Offset: 0x0002EA4D
		public override int PriorityCode
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00030850 File Offset: 0x0002EA50
		public FunctionDiv()
		{
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x00030858 File Offset: 0x0002EA58
		public FunctionDiv(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0003086E File Offset: 0x0002EA6E
		public override string BinaryOperator()
		{
			return " / ";
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00030878 File Offset: 0x0002EA78
		public override object Evaluate()
		{
			if (Math.Abs(base.Rhs.EvaluateDouble()) < 1.0 / (double)FunctionBinary.TwoToThePowerOf16)
			{
				return "Infinity";
			}
			double num = base.Lhs.EvaluateDouble() / base.Rhs.EvaluateDouble();
			if (Math.Abs(num) < 1.7976931348623157E+308 / (double)FunctionBinary.TwoToThePowerOf16)
			{
				double num2 = Math.Abs((double)((int)num * FunctionBinary.TwoToThePowerOf16) - num * (double)FunctionBinary.TwoToThePowerOf16);
				if (num2 < 1.0 && num2 > 0.0)
				{
					num = (double)((int)num * FunctionBinary.TwoToThePowerOf16 / FunctionBinary.TwoToThePowerOf16);
				}
				if (num == (double)((int)num))
				{
					return (int)num;
				}
			}
			return num;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0003093B File Offset: 0x0002EB3B
		public override void Validate(ExpressionValidationContext context)
		{
			base.ArrayCheck();
			base.ValidateIntOperandTypes();
		}
	}
}
