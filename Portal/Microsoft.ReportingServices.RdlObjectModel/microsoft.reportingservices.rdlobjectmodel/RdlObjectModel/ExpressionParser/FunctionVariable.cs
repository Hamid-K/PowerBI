using System;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200028E RID: 654
	[Serializable]
	internal class FunctionVariable : BaseInternalExpression
	{
		// Token: 0x0600148A RID: 5258 RVA: 0x000302BF File Offset: 0x0002E4BF
		public FunctionVariable(IInternalExpression nameExpr)
		{
			this.m_nameExpr = nameExpr;
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x000302CE File Offset: 0x0002E4CE
		public FunctionVariable(IInternalExpression nameExpr, string propertyName)
		{
			this.m_nameExpr = nameExpr;
			this.m_propertyName = propertyName;
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x000302E4 File Offset: 0x0002E4E4
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x000302E8 File Offset: 0x0002E4E8
		public override string WriteSource(NameChanges nameChanges)
		{
			StringBuilder stringBuilder = new StringBuilder("Variables(");
			stringBuilder.Append(this.m_nameExpr.WriteSource(nameChanges));
			stringBuilder.Append(")");
			if (!string.IsNullOrEmpty(this.m_propertyName))
			{
				stringBuilder.Append(".");
				stringBuilder.Append(this.m_propertyName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0003034B File Offset: 0x0002E54B
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			base.TraverseChildren(callback);
			if (this.m_nameExpr != null)
			{
				this.m_nameExpr.Traverse(callback);
			}
		}

		// Token: 0x040006B2 RID: 1714
		private readonly IInternalExpression m_nameExpr;

		// Token: 0x040006B3 RID: 1715
		private readonly string m_propertyName;
	}
}
