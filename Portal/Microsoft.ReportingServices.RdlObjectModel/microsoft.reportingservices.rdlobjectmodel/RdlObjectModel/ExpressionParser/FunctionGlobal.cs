using System;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200027F RID: 639
	[Serializable]
	internal class FunctionGlobal : BaseInternalExpression
	{
		// Token: 0x06001438 RID: 5176 RVA: 0x0002FD67 File Offset: 0x0002DF67
		public FunctionGlobal(IInternalExpression nameExpr)
		{
			this.m_nameExpr = nameExpr;
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0002FD76 File Offset: 0x0002DF76
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0002FD79 File Offset: 0x0002DF79
		public override string WriteSource(NameChanges nameChanges)
		{
			StringBuilder stringBuilder = new StringBuilder("Globals(");
			stringBuilder.Append(this.m_nameExpr.WriteSource(nameChanges));
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0002FDA9 File Offset: 0x0002DFA9
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			base.TraverseChildren(callback);
			if (this.m_nameExpr != null)
			{
				this.m_nameExpr.Traverse(callback);
			}
		}

		// Token: 0x040006A6 RID: 1702
		private readonly IInternalExpression m_nameExpr;
	}
}
