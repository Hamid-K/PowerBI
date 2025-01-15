using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006C2 RID: 1730
	public class DbInExpression : DbExpression
	{
		// Token: 0x060050D9 RID: 20697 RVA: 0x00122042 File Offset: 0x00120242
		internal DbInExpression(TypeUsage booleanResultType, DbExpression item, DbExpressionList list)
			: base(DbExpressionKind.In, booleanResultType, true)
		{
			this._item = item;
			this._list = list;
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x060050DA RID: 20698 RVA: 0x0012205C File Offset: 0x0012025C
		public DbExpression Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x060050DB RID: 20699 RVA: 0x00122064 File Offset: 0x00120264
		public IList<DbExpression> List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x060050DC RID: 20700 RVA: 0x0012206C File Offset: 0x0012026C
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060050DD RID: 20701 RVA: 0x00122081 File Offset: 0x00120281
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001D9D RID: 7581
		private readonly DbExpression _item;

		// Token: 0x04001D9E RID: 7582
		private readonly DbExpressionList _list;
	}
}
