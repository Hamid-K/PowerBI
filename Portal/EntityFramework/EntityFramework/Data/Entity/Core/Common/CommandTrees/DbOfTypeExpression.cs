using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006D2 RID: 1746
	public sealed class DbOfTypeExpression : DbUnaryExpression
	{
		// Token: 0x06005137 RID: 20791 RVA: 0x00123459 File Offset: 0x00121659
		internal DbOfTypeExpression(DbExpressionKind ofTypeKind, TypeUsage collectionResultType, DbExpression argument, TypeUsage type)
			: base(ofTypeKind, collectionResultType, argument)
		{
			this._ofType = type;
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06005138 RID: 20792 RVA: 0x0012346C File Offset: 0x0012166C
		public TypeUsage OfType
		{
			get
			{
				return this._ofType;
			}
		}

		// Token: 0x06005139 RID: 20793 RVA: 0x00123474 File Offset: 0x00121674
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600513A RID: 20794 RVA: 0x00123489 File Offset: 0x00121689
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001DB3 RID: 7603
		private readonly TypeUsage _ofType;
	}
}
