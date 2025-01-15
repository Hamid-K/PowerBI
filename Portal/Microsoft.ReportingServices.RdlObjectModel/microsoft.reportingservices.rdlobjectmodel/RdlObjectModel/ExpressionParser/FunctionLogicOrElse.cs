using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000294 RID: 660
	[Serializable]
	internal sealed class FunctionLogicOrElse : FunctionBinary
	{
		// Token: 0x060014A7 RID: 5287 RVA: 0x000305F4 File Offset: 0x0002E7F4
		public FunctionLogicOrElse(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0003060A File Offset: 0x0002E80A
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0003060D File Offset: 0x0002E80D
		public override string BinaryOperator()
		{
			return " OrElse ";
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00030614 File Offset: 0x0002E814
		public override object Evaluate()
		{
			try
			{
				bool flag = base.Lhs.EvaluateBoolean();
				if (flag)
				{
					return flag;
				}
			}
			catch
			{
			}
			object obj;
			try
			{
				bool flag = base.Rhs.EvaluateBoolean();
				obj = flag;
			}
			catch
			{
				obj = "#Error";
			}
			return obj;
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x0003067C File Offset: 0x0002E87C
		public override int PriorityCode
		{
			get
			{
				return 12;
			}
		}
	}
}
