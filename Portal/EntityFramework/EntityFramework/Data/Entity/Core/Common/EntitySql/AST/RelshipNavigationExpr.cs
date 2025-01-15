using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000690 RID: 1680
	internal sealed class RelshipNavigationExpr : Node
	{
		// Token: 0x06004F62 RID: 20322 RVA: 0x001206CD File Offset: 0x0011E8CD
		internal RelshipNavigationExpr(Node refExpr, Node relshipTypeName, Identifier toEndIdentifier, Identifier fromEndIdentifier)
		{
			this._refExpr = refExpr;
			this._relshipTypeName = relshipTypeName;
			this._toEndIdentifier = toEndIdentifier;
			this._fromEndIdentifier = fromEndIdentifier;
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06004F63 RID: 20323 RVA: 0x001206F2 File Offset: 0x0011E8F2
		internal Node RefExpr
		{
			get
			{
				return this._refExpr;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06004F64 RID: 20324 RVA: 0x001206FA File Offset: 0x0011E8FA
		internal Node TypeName
		{
			get
			{
				return this._relshipTypeName;
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06004F65 RID: 20325 RVA: 0x00120702 File Offset: 0x0011E902
		internal Identifier ToEndIdentifier
		{
			get
			{
				return this._toEndIdentifier;
			}
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06004F66 RID: 20326 RVA: 0x0012070A File Offset: 0x0011E90A
		internal Identifier FromEndIdentifier
		{
			get
			{
				return this._fromEndIdentifier;
			}
		}

		// Token: 0x04001D09 RID: 7433
		private readonly Node _refExpr;

		// Token: 0x04001D0A RID: 7434
		private readonly Node _relshipTypeName;

		// Token: 0x04001D0B RID: 7435
		private readonly Identifier _toEndIdentifier;

		// Token: 0x04001D0C RID: 7436
		private readonly Identifier _fromEndIdentifier;
	}
}
