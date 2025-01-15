using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200067A RID: 1658
	internal sealed class Command : Node
	{
		// Token: 0x06004F0C RID: 20236 RVA: 0x0011F79F File Offset: 0x0011D99F
		internal Command(NodeList<NamespaceImport> nsImportList, Statement statement)
		{
			this._namespaceImportList = nsImportList;
			this._statement = statement;
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06004F0D RID: 20237 RVA: 0x0011F7B5 File Offset: 0x0011D9B5
		internal NodeList<NamespaceImport> NamespaceImportList
		{
			get
			{
				return this._namespaceImportList;
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06004F0E RID: 20238 RVA: 0x0011F7BD File Offset: 0x0011D9BD
		internal Statement Statement
		{
			get
			{
				return this._statement;
			}
		}

		// Token: 0x04001CC1 RID: 7361
		private readonly NodeList<NamespaceImport> _namespaceImportList;

		// Token: 0x04001CC2 RID: 7362
		private readonly Statement _statement;
	}
}
