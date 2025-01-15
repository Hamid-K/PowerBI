using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200069F RID: 1695
	internal sealed class PropDefinition : Node
	{
		// Token: 0x06004F8F RID: 20367 RVA: 0x0012099B File Offset: 0x0011EB9B
		internal PropDefinition(Identifier name, Node typeDefExpr)
		{
			this._name = name;
			this._typeDefExpr = typeDefExpr;
		}

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06004F90 RID: 20368 RVA: 0x001209B1 File Offset: 0x0011EBB1
		internal Identifier Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06004F91 RID: 20369 RVA: 0x001209B9 File Offset: 0x0011EBB9
		internal Node Type
		{
			get
			{
				return this._typeDefExpr;
			}
		}

		// Token: 0x04001D2E RID: 7470
		private readonly Identifier _name;

		// Token: 0x04001D2F RID: 7471
		private readonly Node _typeDefExpr;
	}
}
