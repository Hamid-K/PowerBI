using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003FC RID: 1020
	public sealed class QueryParameter
	{
		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06002048 RID: 8264 RVA: 0x0007F4DC File Offset: 0x0007D6DC
		// (set) Token: 0x06002049 RID: 8265 RVA: 0x0007F4E4 File Offset: 0x0007D6E4
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x0007F4ED File Offset: 0x0007D6ED
		// (set) Token: 0x0600204B RID: 8267 RVA: 0x0007F4F5 File Offset: 0x0007D6F5
		public Expression Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x000025F4 File Offset: 0x000007F4
		public QueryParameter()
		{
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x0007F4FE File Offset: 0x0007D6FE
		internal QueryParameter(string name, string expression)
		{
			this.Name = name;
			if (expression != null && expression != "")
			{
				this.Value = new Expression(expression);
			}
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x0007F529 File Offset: 0x0007D729
		private static string ToIdentifier(string name)
		{
			if (name == null)
			{
				return null;
			}
			if (name[0] == '@')
			{
				return name.Remove(0, 1);
			}
			return name;
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x0007F545 File Offset: 0x0007D745
		internal static Expression MakeExpression(string expr)
		{
			if (expr != null && expr != "")
			{
				return new Expression(Expression.ExpressionString(expr));
			}
			return null;
		}

		// Token: 0x04000E1A RID: 3610
		private string m_name;

		// Token: 0x04000E1B RID: 3611
		private Expression m_value;
	}
}
