using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002A7 RID: 679
	[Serializable]
	internal sealed class FunctionRelationalGreaterThan : FunctionBinary
	{
		// Token: 0x0600151B RID: 5403 RVA: 0x00031458 File Offset: 0x0002F658
		public FunctionRelationalGreaterThan(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0003146E File Offset: 0x0002F66E
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00031471 File Offset: 0x0002F671
		public override string BinaryOperator()
		{
			return " > ";
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00031478 File Offset: 0x0002F678
		public override object Evaluate()
		{
			double num = base.Lhs.EvaluateDouble();
			double num2 = base.Rhs.EvaluateDouble();
			return num > num2;
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x000314A4 File Offset: 0x0002F6A4
		public override int PriorityCode
		{
			get
			{
				return 9;
			}
		}
	}
}
