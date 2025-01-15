using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006DF RID: 1759
	public sealed class DbSkipExpression : DbExpression
	{
		// Token: 0x0600517C RID: 20860 RVA: 0x00123AB3 File Offset: 0x00121CB3
		internal DbSkipExpression(TypeUsage resultType, DbExpressionBinding input, ReadOnlyCollection<DbSortClause> sortOrder, DbExpression count)
			: base(DbExpressionKind.Skip, resultType, true)
		{
			this._input = input;
			this._keys = sortOrder;
			this._count = count;
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x0600517D RID: 20861 RVA: 0x00123AD5 File Offset: 0x00121CD5
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x0600517E RID: 20862 RVA: 0x00123ADD File Offset: 0x00121CDD
		public IList<DbSortClause> SortOrder
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x0600517F RID: 20863 RVA: 0x00123AE5 File Offset: 0x00121CE5
		public DbExpression Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06005180 RID: 20864 RVA: 0x00123AED File Offset: 0x00121CED
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005181 RID: 20865 RVA: 0x00123B02 File Offset: 0x00121D02
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001DC8 RID: 7624
		private readonly DbExpressionBinding _input;

		// Token: 0x04001DC9 RID: 7625
		private readonly ReadOnlyCollection<DbSortClause> _keys;

		// Token: 0x04001DCA RID: 7626
		private readonly DbExpression _count;
	}
}
