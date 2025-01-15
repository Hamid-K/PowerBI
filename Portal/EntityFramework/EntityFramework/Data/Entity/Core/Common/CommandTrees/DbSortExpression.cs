using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006E1 RID: 1761
	public sealed class DbSortExpression : DbExpression
	{
		// Token: 0x06005186 RID: 20870 RVA: 0x00123B4C File Offset: 0x00121D4C
		internal DbSortExpression(TypeUsage resultType, DbExpressionBinding input, ReadOnlyCollection<DbSortClause> sortOrder)
			: base(DbExpressionKind.Sort, resultType, true)
		{
			this._input = input;
			this._keys = sortOrder;
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06005187 RID: 20871 RVA: 0x00123B66 File Offset: 0x00121D66
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06005188 RID: 20872 RVA: 0x00123B6E File Offset: 0x00121D6E
		public IList<DbSortClause> SortOrder
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x06005189 RID: 20873 RVA: 0x00123B76 File Offset: 0x00121D76
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x00123B8B File Offset: 0x00121D8B
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001DCE RID: 7630
		private readonly DbExpressionBinding _input;

		// Token: 0x04001DCF RID: 7631
		private readonly ReadOnlyCollection<DbSortClause> _keys;
	}
}
