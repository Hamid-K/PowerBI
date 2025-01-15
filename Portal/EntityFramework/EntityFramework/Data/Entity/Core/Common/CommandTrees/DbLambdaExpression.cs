using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006CA RID: 1738
	public sealed class DbLambdaExpression : DbExpression
	{
		// Token: 0x06005110 RID: 20752 RVA: 0x001231D6 File Offset: 0x001213D6
		internal DbLambdaExpression(TypeUsage resultType, DbLambda lambda, DbExpressionList args)
			: base(DbExpressionKind.Lambda, resultType, true)
		{
			this._lambda = lambda;
			this._arguments = args;
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06005111 RID: 20753 RVA: 0x001231F0 File Offset: 0x001213F0
		public DbLambda Lambda
		{
			get
			{
				return this._lambda;
			}
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06005112 RID: 20754 RVA: 0x001231F8 File Offset: 0x001213F8
		public IList<DbExpression> Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x06005113 RID: 20755 RVA: 0x00123200 File Offset: 0x00121400
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x00123215 File Offset: 0x00121415
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001DA7 RID: 7591
		private readonly DbLambda _lambda;

		// Token: 0x04001DA8 RID: 7592
		private readonly DbExpressionList _arguments;
	}
}
