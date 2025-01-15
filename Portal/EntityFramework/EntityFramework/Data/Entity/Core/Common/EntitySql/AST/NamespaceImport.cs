using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200068F RID: 1679
	internal sealed class NamespaceImport : Node
	{
		// Token: 0x06004F5D RID: 20317 RVA: 0x00120649 File Offset: 0x0011E849
		internal NamespaceImport(Identifier identifier)
		{
			this._namespaceName = identifier;
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x00120658 File Offset: 0x0011E858
		internal NamespaceImport(DotExpr dorExpr)
		{
			this._namespaceName = dorExpr;
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x00120668 File Offset: 0x0011E868
		internal NamespaceImport(BuiltInExpr bltInExpr)
		{
			this._namespaceAlias = null;
			Identifier identifier = bltInExpr.Arg1 as Identifier;
			if (identifier == null)
			{
				ErrorContext errCtx = bltInExpr.Arg1.ErrCtx;
				string invalidNamespaceAlias = Strings.InvalidNamespaceAlias;
				throw EntitySqlException.Create(errCtx, invalidNamespaceAlias, null);
			}
			this._namespaceAlias = identifier;
			this._namespaceName = bltInExpr.Arg2;
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06004F60 RID: 20320 RVA: 0x001206BD File Offset: 0x0011E8BD
		internal Identifier Alias
		{
			get
			{
				return this._namespaceAlias;
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06004F61 RID: 20321 RVA: 0x001206C5 File Offset: 0x0011E8C5
		internal Node NamespaceName
		{
			get
			{
				return this._namespaceName;
			}
		}

		// Token: 0x04001D07 RID: 7431
		private readonly Identifier _namespaceAlias;

		// Token: 0x04001D08 RID: 7432
		private readonly Node _namespaceName;
	}
}
