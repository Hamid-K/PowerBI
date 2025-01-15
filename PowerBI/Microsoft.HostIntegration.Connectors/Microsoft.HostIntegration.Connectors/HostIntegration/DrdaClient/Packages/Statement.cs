using System;
using Microsoft.HostIntegration.StaticSqlUtil;

namespace Microsoft.HostIntegration.DrdaClient.Packages
{
	// Token: 0x02000A61 RID: 2657
	public class Statement
	{
		// Token: 0x060052EE RID: 21230 RVA: 0x0015098C File Offset: 0x0014EB8C
		internal Statement(Statement statement)
		{
			this._statement = statement;
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x0015099B File Offset: 0x0014EB9B
		public Statement()
		{
			this._statement = new Statement();
		}

		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x060052F0 RID: 21232 RVA: 0x001509AE File Offset: 0x0014EBAE
		// (set) Token: 0x060052F1 RID: 21233 RVA: 0x001509BB File Offset: 0x0014EBBB
		public bool Assumptions
		{
			get
			{
				return this._statement.SqlStatementAssumptions;
			}
			set
			{
				this._statement.SqlStatementAssumptions = value;
			}
		}

		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x060052F2 RID: 21234 RVA: 0x001509C9 File Offset: 0x0014EBC9
		// (set) Token: 0x060052F3 RID: 21235 RVA: 0x001509D6 File Offset: 0x0014EBD6
		public int Number
		{
			get
			{
				return this._statement.SqlStatementNumber;
			}
			set
			{
				this._statement.SqlStatementNumber = value;
			}
		}

		// Token: 0x17001413 RID: 5139
		// (get) Token: 0x060052F4 RID: 21236 RVA: 0x001509E4 File Offset: 0x0014EBE4
		// (set) Token: 0x060052F5 RID: 21237 RVA: 0x001509F1 File Offset: 0x0014EBF1
		public string Text
		{
			get
			{
				return this._statement.SqlStatement;
			}
			set
			{
				this._statement.SqlStatement = value;
			}
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x001509FF File Offset: 0x0014EBFF
		internal Statement ToStatement()
		{
			return this._statement;
		}

		// Token: 0x04004159 RID: 16729
		private Statement _statement;
	}
}
