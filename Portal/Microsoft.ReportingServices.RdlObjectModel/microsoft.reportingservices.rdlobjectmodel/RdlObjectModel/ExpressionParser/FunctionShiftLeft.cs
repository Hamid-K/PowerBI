using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200029F RID: 671
	[Serializable]
	internal sealed class FunctionShiftLeft : FunctionBinary
	{
		// Token: 0x060014E9 RID: 5353 RVA: 0x00031108 File Offset: 0x0002F308
		public FunctionShiftLeft()
		{
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00031110 File Offset: 0x0002F310
		public FunctionShiftLeft(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x00031126 File Offset: 0x0002F326
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Int32;
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0003112A File Offset: 0x0002F32A
		public override string BinaryOperator()
		{
			return " << ";
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x00031131 File Offset: 0x0002F331
		public override object Evaluate()
		{
			return (int)base.Lhs.Evaluate() << (int)base.Rhs.Evaluate();
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x0003115C File Offset: 0x0002F35C
		public override int PriorityCode
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0003115F File Offset: 0x0002F35F
		public override void Validate(ExpressionValidationContext context)
		{
			base.ArrayCheck();
			base.ValidateIntOperandTypes();
		}
	}
}
