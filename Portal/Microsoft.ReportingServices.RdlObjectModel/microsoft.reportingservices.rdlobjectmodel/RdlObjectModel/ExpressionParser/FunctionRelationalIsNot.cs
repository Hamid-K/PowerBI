using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002AA RID: 682
	[Serializable]
	internal sealed class FunctionRelationalIsNot : FunctionBinary
	{
		// Token: 0x0600152A RID: 5418 RVA: 0x00031534 File Offset: 0x0002F734
		public FunctionRelationalIsNot(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0003154A File Offset: 0x0002F74A
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0003154D File Offset: 0x0002F74D
		public override string BinaryOperator()
		{
			return " IsNot ";
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x00031554 File Offset: 0x0002F754
		public override object Evaluate()
		{
			return !base.Lhs.Evaluate().Equals(base.Rhs.Evaluate());
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00031579 File Offset: 0x0002F779
		public override int PriorityCode
		{
			get
			{
				return 9;
			}
		}
	}
}
