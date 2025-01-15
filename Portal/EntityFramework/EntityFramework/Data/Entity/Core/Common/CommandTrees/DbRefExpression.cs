using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006D9 RID: 1753
	public sealed class DbRefExpression : DbUnaryExpression
	{
		// Token: 0x06005160 RID: 20832 RVA: 0x00123780 File Offset: 0x00121980
		internal DbRefExpression(TypeUsage refResultType, EntitySet entitySet, DbExpression refKeys)
			: base(DbExpressionKind.Ref, refResultType, refKeys)
		{
			this._entitySet = entitySet;
		}

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06005161 RID: 20833 RVA: 0x00123793 File Offset: 0x00121993
		public EntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x06005162 RID: 20834 RVA: 0x0012379B File Offset: 0x0012199B
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005163 RID: 20835 RVA: 0x001237B0 File Offset: 0x001219B0
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001DBD RID: 7613
		private readonly EntitySet _entitySet;
	}
}
