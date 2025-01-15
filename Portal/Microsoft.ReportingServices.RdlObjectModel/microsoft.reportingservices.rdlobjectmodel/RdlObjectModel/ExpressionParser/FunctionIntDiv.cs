using System;
using Microsoft.ReportingServices.RdlObjectModel.Expression;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200029A RID: 666
	[Serializable]
	internal sealed class FunctionIntDiv : FunctionBinary
	{
		// Token: 0x060014C9 RID: 5321 RVA: 0x00030B19 File Offset: 0x0002ED19
		public FunctionIntDiv()
		{
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x00030B21 File Offset: 0x0002ED21
		public FunctionIntDiv(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00030B37 File Offset: 0x0002ED37
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Int32;
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00030B3B File Offset: 0x0002ED3B
		public override string BinaryOperator()
		{
			return " \\ ";
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00030B44 File Offset: 0x0002ED44
		public override void Validate(ExpressionValidationContext context)
		{
			base.ArrayCheck();
			base.ValidateIntOperandTypes();
			if (!(base.Rhs is FunctionReportParameter) && !(base.Rhs is FunctionField) && (int)base.Rhs.EvaluateDouble() == 0)
			{
				RDLExceptionHelper.WriteDivisionByZero(base.Lhs.WriteSource() + " \\ " + base.Rhs.WriteSource(), this.StartColumn, this.EndColumn);
			}
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00030BB8 File Offset: 0x0002EDB8
		public override object Evaluate()
		{
			if (Math.Abs(Convert.ToDouble(base.Rhs.EvaluateDouble())) < (double)(1f / (float)FunctionBinary.TwoToThePowerOf16))
			{
				return "Infinity";
			}
			return (int)(base.Lhs.EvaluateDouble() / base.Rhs.EvaluateDouble());
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x00030C0C File Offset: 0x0002EE0C
		public override int PriorityCode
		{
			get
			{
				return 4;
			}
		}
	}
}
