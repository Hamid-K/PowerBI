using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000293 RID: 659
	[Serializable]
	internal sealed class FunctionLogicOr : FunctionBinary
	{
		// Token: 0x060014A2 RID: 5282 RVA: 0x000305AE File Offset: 0x0002E7AE
		public FunctionLogicOr(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x000305C4 File Offset: 0x0002E7C4
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x000305C7 File Offset: 0x0002E7C7
		public override string BinaryOperator()
		{
			return " Or ";
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x000305CE File Offset: 0x0002E7CE
		public override object Evaluate()
		{
			return base.Lhs.EvaluateBoolean() || base.Rhs.EvaluateBoolean();
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x000305F0 File Offset: 0x0002E7F0
		public override int PriorityCode
		{
			get
			{
				return 12;
			}
		}
	}
}
