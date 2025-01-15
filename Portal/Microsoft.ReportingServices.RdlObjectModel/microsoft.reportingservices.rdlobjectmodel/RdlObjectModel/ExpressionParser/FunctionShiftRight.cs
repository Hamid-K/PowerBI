using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002A0 RID: 672
	[Serializable]
	internal sealed class FunctionShiftRight : FunctionBinary
	{
		// Token: 0x060014F0 RID: 5360 RVA: 0x0003116D File Offset: 0x0002F36D
		public FunctionShiftRight()
		{
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00031175 File Offset: 0x0002F375
		public FunctionShiftRight(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0003118B File Offset: 0x0002F38B
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Double;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0003118F File Offset: 0x0002F38F
		public override string BinaryOperator()
		{
			return " >> ";
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00031198 File Offset: 0x0002F398
		public override object Evaluate()
		{
			int num = (int)base.Rhs.Evaluate();
			return (int)base.Lhs.Evaluate() >> num;
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x000311D0 File Offset: 0x0002F3D0
		public override int PriorityCode
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x000311D3 File Offset: 0x0002F3D3
		public override void Validate(ExpressionValidationContext context)
		{
			base.ArrayCheck();
			base.ValidateIntOperandTypes();
		}
	}
}
