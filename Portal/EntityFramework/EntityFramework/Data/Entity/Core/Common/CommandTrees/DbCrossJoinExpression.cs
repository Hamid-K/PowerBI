using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006AE RID: 1710
	public sealed class DbCrossJoinExpression : DbExpression
	{
		// Token: 0x06005011 RID: 20497 RVA: 0x00121832 File Offset: 0x0011FA32
		internal DbCrossJoinExpression(TypeUsage collectionOfRowResultType, ReadOnlyCollection<DbExpressionBinding> inputs)
			: base(DbExpressionKind.CrossJoin, collectionOfRowResultType, true)
		{
			this._inputs = inputs;
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06005012 RID: 20498 RVA: 0x00121844 File Offset: 0x0011FA44
		public IList<DbExpressionBinding> Inputs
		{
			get
			{
				return this._inputs;
			}
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x0012184C File Offset: 0x0011FA4C
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x00121861 File Offset: 0x0011FA61
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001D48 RID: 7496
		private readonly ReadOnlyCollection<DbExpressionBinding> _inputs;
	}
}
