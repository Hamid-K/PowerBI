using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200029D RID: 669
	[Serializable]
	internal sealed class FunctionMult : FunctionBinary
	{
		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x00030DBC File Offset: 0x0002EFBC
		public override int PriorityCode
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00030DBF File Offset: 0x0002EFBF
		public FunctionMult()
		{
			base.Lhs = null;
			base.Rhs = null;
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x00030DD5 File Offset: 0x0002EFD5
		public FunctionMult(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00030DEB File Offset: 0x0002EFEB
		public override string BinaryOperator()
		{
			return " * ";
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x00030DF4 File Offset: 0x0002EFF4
		public override object Evaluate()
		{
			object obj = base.Lhs.Evaluate();
			object obj2 = base.Rhs.Evaluate();
			int num = (int)Math.Pow(2.0, 16.0);
			if (obj is int && obj2 is int)
			{
				return (int)obj * (int)obj2;
			}
			double num2 = base.Lhs.EvaluateDouble() * base.Rhs.EvaluateDouble();
			if (Math.Abs(num2) < 1.7976931348623157E+308 / (double)num)
			{
				double num3 = Math.Abs((double)((int)num2 * num) - num2 * (double)num);
				if (num3 < 1.0 && num3 > 0.0)
				{
					num2 = (double)((int)num2 * num / num);
				}
				if (num2 == (double)((int)num2))
				{
					return (int)num2;
				}
			}
			return num2;
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x00030ED2 File Offset: 0x0002F0D2
		public override void Validate(ExpressionValidationContext context)
		{
			base.ArrayCheck();
			base.ValidateIntOperandTypes();
		}
	}
}
