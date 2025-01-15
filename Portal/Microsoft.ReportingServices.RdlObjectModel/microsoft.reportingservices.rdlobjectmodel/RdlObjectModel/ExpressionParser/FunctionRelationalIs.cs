using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002A9 RID: 681
	[Serializable]
	internal sealed class FunctionRelationalIs : FunctionBinary
	{
		// Token: 0x06001525 RID: 5413 RVA: 0x000314EE File Offset: 0x0002F6EE
		public FunctionRelationalIs(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00031504 File Offset: 0x0002F704
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x00031507 File Offset: 0x0002F707
		public override string BinaryOperator()
		{
			return " Is ";
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0003150E File Offset: 0x0002F70E
		public override object Evaluate()
		{
			return base.Lhs.Evaluate().Equals(base.Rhs.Evaluate());
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x00031530 File Offset: 0x0002F730
		public override int PriorityCode
		{
			get
			{
				return 9;
			}
		}
	}
}
