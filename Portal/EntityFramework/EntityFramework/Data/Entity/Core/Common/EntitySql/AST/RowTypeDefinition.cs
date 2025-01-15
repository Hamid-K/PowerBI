using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200069B RID: 1691
	internal sealed class RowTypeDefinition : Node
	{
		// Token: 0x06004F86 RID: 20358 RVA: 0x00120924 File Offset: 0x0011EB24
		internal RowTypeDefinition(NodeList<PropDefinition> propDefList)
		{
			this._propDefList = propDefList;
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06004F87 RID: 20359 RVA: 0x00120933 File Offset: 0x0011EB33
		internal NodeList<PropDefinition> Properties
		{
			get
			{
				return this._propDefList;
			}
		}

		// Token: 0x04001D25 RID: 7461
		private readonly NodeList<PropDefinition> _propDefList;
	}
}
