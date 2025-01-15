using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006BE RID: 1726
	public class DbFunctionExpression : DbExpression
	{
		// Token: 0x060050C3 RID: 20675 RVA: 0x00121EEE File Offset: 0x001200EE
		internal DbFunctionExpression()
		{
		}

		// Token: 0x060050C4 RID: 20676 RVA: 0x00121EF6 File Offset: 0x001200F6
		internal DbFunctionExpression(TypeUsage resultType, EdmFunction function, DbExpressionList arguments)
			: base(DbExpressionKind.Function, resultType, true)
		{
			this._functionInfo = function;
			this._arguments = arguments;
		}

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x060050C5 RID: 20677 RVA: 0x00121F10 File Offset: 0x00120110
		public virtual EdmFunction Function
		{
			get
			{
				return this._functionInfo;
			}
		}

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x060050C6 RID: 20678 RVA: 0x00121F18 File Offset: 0x00120118
		public virtual IList<DbExpression> Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x060050C7 RID: 20679 RVA: 0x00121F20 File Offset: 0x00120120
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060050C8 RID: 20680 RVA: 0x00121F35 File Offset: 0x00120135
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001D94 RID: 7572
		private readonly EdmFunction _functionInfo;

		// Token: 0x04001D95 RID: 7573
		private readonly DbExpressionList _arguments;
	}
}
