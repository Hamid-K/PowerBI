using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000298 RID: 664
	[Serializable]
	internal sealed class FunctionDivDecimal : FunctionBinary
	{
		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x00030949 File Offset: 0x0002EB49
		public override int PriorityCode
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0003094C File Offset: 0x0002EB4C
		public FunctionDivDecimal()
		{
			base.Lhs = null;
			base.Rhs = null;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x00030962 File Offset: 0x0002EB62
		public FunctionDivDecimal(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00030978 File Offset: 0x0002EB78
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Decimal;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0003097C File Offset: 0x0002EB7C
		public override string BinaryOperator()
		{
			return " / ";
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00030984 File Offset: 0x0002EB84
		public override object Evaluate()
		{
			decimal num = 0m;
			if (Math.Abs(base.Rhs.EvaluateDouble()) < (double)(1f / (float)FunctionBinary.TwoToThePowerOf16))
			{
				return "Infinity";
			}
			num = base.Lhs.EvaluateDecimal() / base.Rhs.EvaluateDecimal();
			if (Math.Abs(num) < 79228162514264337593543950335m / FunctionBinary.TwoToThePowerOf16)
			{
				decimal num2 = Math.Abs((int)num * FunctionBinary.TwoToThePowerOf16 - num * FunctionBinary.TwoToThePowerOf16);
				if (num2 < 1m && num2 > 0m)
				{
					num = (int)num * FunctionBinary.TwoToThePowerOf16 / FunctionBinary.TwoToThePowerOf16;
				}
				if (num == (int)num)
				{
					return (int)num;
				}
			}
			return num;
		}
	}
}
