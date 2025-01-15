using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200029B RID: 667
	[Serializable]
	internal sealed class FunctionMinus : FunctionBinary
	{
		// Token: 0x060014D0 RID: 5328 RVA: 0x00030C0F File Offset: 0x0002EE0F
		public FunctionMinus()
		{
			base.Lhs = null;
			base.Rhs = null;
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00030C25 File Offset: 0x0002EE25
		public FunctionMinus(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00030C3B File Offset: 0x0002EE3B
		public override string BinaryOperator()
		{
			return " - ";
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00030C44 File Offset: 0x0002EE44
		public override object Evaluate()
		{
			object obj = base.Lhs.Evaluate();
			object obj2 = base.Rhs.Evaluate();
			int num = 65536;
			if (obj is int && obj2 is int)
			{
				return Convert.ToInt32(obj, CultureInfo.CurrentCulture) - Convert.ToInt32(obj2, CultureInfo.CurrentCulture);
			}
			if (obj is DateTime && obj2 is DateTime)
			{
				return Convert.ToDateTime(obj, CultureInfo.CurrentCulture) - Convert.ToDateTime(obj2, CultureInfo.CurrentCulture);
			}
			double num2 = base.Lhs.EvaluateDouble() - base.Rhs.EvaluateDouble();
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

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x00030D4A File Offset: 0x0002EF4A
		public override int PriorityCode
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x00030D4D File Offset: 0x0002EF4D
		public override void Validate(ExpressionValidationContext context)
		{
			base.ArrayCheck();
			base.ValidateIntOperandTypes();
		}
	}
}
