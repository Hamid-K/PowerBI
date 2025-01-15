using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000291 RID: 657
	[Serializable]
	internal sealed class FunctionLogicAndAlso : FunctionBinary
	{
		// Token: 0x06001498 RID: 5272 RVA: 0x000304E0 File Offset: 0x0002E6E0
		public FunctionLogicAndAlso(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x000304F6 File Offset: 0x0002E6F6
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x000304F9 File Offset: 0x0002E6F9
		public override string BinaryOperator()
		{
			return " AndAlso ";
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x00030500 File Offset: 0x0002E700
		public override object Evaluate()
		{
			try
			{
				if (!base.Lhs.EvaluateBoolean())
				{
					return false;
				}
			}
			catch
			{
				return "#Error";
			}
			object obj;
			try
			{
				obj = base.Lhs.EvaluateBoolean() && base.Rhs.EvaluateBoolean();
			}
			catch
			{
				obj = "#Error";
			}
			return obj;
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x00030578 File Offset: 0x0002E778
		public override int PriorityCode
		{
			get
			{
				return 11;
			}
		}
	}
}
