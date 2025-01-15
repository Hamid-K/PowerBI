using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000292 RID: 658
	[Serializable]
	internal sealed class FunctionLogicNot : FunctionUnary
	{
		// Token: 0x0600149D RID: 5277 RVA: 0x0003057C File Offset: 0x0002E77C
		public FunctionLogicNot(IInternalExpression rhs)
		{
			base.Rhs = rhs;
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0003058B File Offset: 0x0002E78B
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0003058E File Offset: 0x0002E78E
		public override string UnaryOperator()
		{
			return "Not ";
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00030595 File Offset: 0x0002E795
		public override object Evaluate()
		{
			return !base.Rhs.EvaluateBoolean();
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x000305AA File Offset: 0x0002E7AA
		public override int PriorityCode
		{
			get
			{
				return 10;
			}
		}
	}
}
