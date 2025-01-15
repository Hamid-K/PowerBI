using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006C0 RID: 1728
	public sealed class DbGroupByExpression : DbExpression
	{
		// Token: 0x060050CA RID: 20682 RVA: 0x00121F54 File Offset: 0x00120154
		internal DbGroupByExpression(TypeUsage collectionOfRowResultType, DbGroupExpressionBinding input, DbExpressionList groupKeys, ReadOnlyCollection<DbAggregate> aggregates)
			: base(DbExpressionKind.GroupBy, collectionOfRowResultType, true)
		{
			this._input = input;
			this._keys = groupKeys;
			this._aggregates = aggregates;
		}

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x060050CB RID: 20683 RVA: 0x00121F76 File Offset: 0x00120176
		public DbGroupExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x060050CC RID: 20684 RVA: 0x00121F7E File Offset: 0x0012017E
		public IList<DbExpression> Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x060050CD RID: 20685 RVA: 0x00121F86 File Offset: 0x00120186
		public IList<DbAggregate> Aggregates
		{
			get
			{
				return this._aggregates;
			}
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x00121F8E File Offset: 0x0012018E
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060050CF RID: 20687 RVA: 0x00121FA3 File Offset: 0x001201A3
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001D96 RID: 7574
		private readonly DbGroupExpressionBinding _input;

		// Token: 0x04001D97 RID: 7575
		private readonly DbExpressionList _keys;

		// Token: 0x04001D98 RID: 7576
		private readonly ReadOnlyCollection<DbAggregate> _aggregates;
	}
}
