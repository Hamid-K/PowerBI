using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000697 RID: 1687
	internal sealed class QueryStatement : Statement
	{
		// Token: 0x06004F7D RID: 20349 RVA: 0x001208B9 File Offset: 0x0011EAB9
		internal QueryStatement(NodeList<FunctionDefinition> functionDefList, Node expr)
		{
			this._functionDefList = functionDefList;
			this._expr = expr;
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06004F7E RID: 20350 RVA: 0x001208CF File Offset: 0x0011EACF
		internal NodeList<FunctionDefinition> FunctionDefList
		{
			get
			{
				return this._functionDefList;
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06004F7F RID: 20351 RVA: 0x001208D7 File Offset: 0x0011EAD7
		internal Node Expr
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x04001D20 RID: 7456
		private readonly NodeList<FunctionDefinition> _functionDefList;

		// Token: 0x04001D21 RID: 7457
		private readonly Node _expr;
	}
}
