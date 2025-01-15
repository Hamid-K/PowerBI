using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000699 RID: 1689
	internal sealed class RefTypeDefinition : Node
	{
		// Token: 0x06004F82 RID: 20354 RVA: 0x001208F6 File Offset: 0x0011EAF6
		internal RefTypeDefinition(Node refTypeIdentifier)
		{
			this._refTypeIdentifier = refTypeIdentifier;
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06004F83 RID: 20355 RVA: 0x00120905 File Offset: 0x0011EB05
		internal Node RefTypeIdentifier
		{
			get
			{
				return this._refTypeIdentifier;
			}
		}

		// Token: 0x04001D23 RID: 7459
		private readonly Node _refTypeIdentifier;
	}
}
