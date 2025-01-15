using System;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200028B RID: 651
	[Serializable]
	internal class FunctionUser : BaseInternalExpression
	{
		// Token: 0x0600147E RID: 5246 RVA: 0x00030222 File Offset: 0x0002E422
		public FunctionUser(IInternalExpression nameExpr)
		{
			this.m_nameExpr = nameExpr;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00030231 File Offset: 0x0002E431
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00030234 File Offset: 0x0002E434
		public override string WriteSource(NameChanges nameChanges)
		{
			StringBuilder stringBuilder = new StringBuilder("User(");
			stringBuilder.Append(this.m_nameExpr.WriteSource(nameChanges));
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00030264 File Offset: 0x0002E464
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			base.TraverseChildren(callback);
			if (this.m_nameExpr != null)
			{
				this.m_nameExpr.Traverse(callback);
			}
		}

		// Token: 0x040006B1 RID: 1713
		private readonly IInternalExpression m_nameExpr;
	}
}
