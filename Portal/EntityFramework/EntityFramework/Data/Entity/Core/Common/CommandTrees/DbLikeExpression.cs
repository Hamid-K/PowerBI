using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006CB RID: 1739
	public sealed class DbLikeExpression : DbExpression
	{
		// Token: 0x06005115 RID: 20757 RVA: 0x0012322A File Offset: 0x0012142A
		internal DbLikeExpression(TypeUsage booleanResultType, DbExpression input, DbExpression pattern, DbExpression escape)
			: base(DbExpressionKind.Like, booleanResultType, true)
		{
			this._argument = input;
			this._pattern = pattern;
			this._escape = escape;
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06005116 RID: 20758 RVA: 0x0012324C File Offset: 0x0012144C
		public DbExpression Argument
		{
			get
			{
				return this._argument;
			}
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06005117 RID: 20759 RVA: 0x00123254 File Offset: 0x00121454
		public DbExpression Pattern
		{
			get
			{
				return this._pattern;
			}
		}

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06005118 RID: 20760 RVA: 0x0012325C File Offset: 0x0012145C
		public DbExpression Escape
		{
			get
			{
				return this._escape;
			}
		}

		// Token: 0x06005119 RID: 20761 RVA: 0x00123264 File Offset: 0x00121464
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600511A RID: 20762 RVA: 0x00123279 File Offset: 0x00121479
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001DA9 RID: 7593
		private readonly DbExpression _argument;

		// Token: 0x04001DAA RID: 7594
		private readonly DbExpression _pattern;

		// Token: 0x04001DAB RID: 7595
		private readonly DbExpression _escape;
	}
}
