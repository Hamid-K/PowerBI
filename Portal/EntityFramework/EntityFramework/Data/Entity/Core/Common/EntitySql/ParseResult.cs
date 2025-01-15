using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000663 RID: 1635
	public sealed class ParseResult
	{
		// Token: 0x06004E0F RID: 19983 RVA: 0x0011882A File Offset: 0x00116A2A
		internal ParseResult(DbCommandTree commandTree, List<FunctionDefinition> functionDefs)
		{
			this._commandTree = commandTree;
			this._functionDefs = new ReadOnlyCollection<FunctionDefinition>(functionDefs);
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06004E10 RID: 19984 RVA: 0x00118845 File Offset: 0x00116A45
		public DbCommandTree CommandTree
		{
			get
			{
				return this._commandTree;
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06004E11 RID: 19985 RVA: 0x0011884D File Offset: 0x00116A4D
		public ReadOnlyCollection<FunctionDefinition> FunctionDefinitions
		{
			get
			{
				return this._functionDefs;
			}
		}

		// Token: 0x04001C55 RID: 7253
		private readonly DbCommandTree _commandTree;

		// Token: 0x04001C56 RID: 7254
		private readonly ReadOnlyCollection<FunctionDefinition> _functionDefs;
	}
}
