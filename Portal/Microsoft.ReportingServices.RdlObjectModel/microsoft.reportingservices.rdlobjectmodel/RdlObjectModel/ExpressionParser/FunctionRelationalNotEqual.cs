using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002AE RID: 686
	[Serializable]
	internal sealed class FunctionRelationalNotEqual : FunctionBinary
	{
		// Token: 0x0600153E RID: 5438 RVA: 0x000316B5 File Offset: 0x0002F8B5
		public FunctionRelationalNotEqual(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x000316CB File Offset: 0x0002F8CB
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x000316CE File Offset: 0x0002F8CE
		public override string BinaryOperator()
		{
			return " <> ";
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x000316D5 File Offset: 0x0002F8D5
		public override object Evaluate()
		{
			return !base.Lhs.Evaluate().Equals(base.Rhs.Evaluate());
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x000316FA File Offset: 0x0002F8FA
		public override int PriorityCode
		{
			get
			{
				return 9;
			}
		}
	}
}
