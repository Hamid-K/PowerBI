using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200067B RID: 1659
	internal sealed class CreateRefExpr : Node
	{
		// Token: 0x06004F0F RID: 20239 RVA: 0x0011F7C5 File Offset: 0x0011D9C5
		internal CreateRefExpr(Node entitySet, Node keys)
			: this(entitySet, keys, null)
		{
		}

		// Token: 0x06004F10 RID: 20240 RVA: 0x0011F7D0 File Offset: 0x0011D9D0
		internal CreateRefExpr(Node entitySet, Node keys, Node typeIdentifier)
		{
			this._entitySet = entitySet;
			this._keys = keys;
			this._typeIdentifier = typeIdentifier;
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06004F11 RID: 20241 RVA: 0x0011F7ED File Offset: 0x0011D9ED
		internal Node EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06004F12 RID: 20242 RVA: 0x0011F7F5 File Offset: 0x0011D9F5
		internal Node Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06004F13 RID: 20243 RVA: 0x0011F7FD File Offset: 0x0011D9FD
		internal Node TypeIdentifier
		{
			get
			{
				return this._typeIdentifier;
			}
		}

		// Token: 0x04001CC3 RID: 7363
		private readonly Node _entitySet;

		// Token: 0x04001CC4 RID: 7364
		private readonly Node _keys;

		// Token: 0x04001CC5 RID: 7365
		private readonly Node _typeIdentifier;
	}
}
