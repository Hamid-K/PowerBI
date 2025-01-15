using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006B2 RID: 1714
	public sealed class DbElementExpression : DbUnaryExpression
	{
		// Token: 0x06005022 RID: 20514 RVA: 0x00121935 File Offset: 0x0011FB35
		internal DbElementExpression(TypeUsage resultType, DbExpression argument)
			: base(DbExpressionKind.Element, resultType, argument)
		{
			this._singlePropertyUnwrapped = false;
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x00121948 File Offset: 0x0011FB48
		internal DbElementExpression(TypeUsage resultType, DbExpression argument, bool unwrapSingleProperty)
			: base(DbExpressionKind.Element, resultType, argument)
		{
			this._singlePropertyUnwrapped = unwrapSingleProperty;
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06005024 RID: 20516 RVA: 0x0012195B File Offset: 0x0011FB5B
		internal bool IsSinglePropertyUnwrapped
		{
			get
			{
				return this._singlePropertyUnwrapped;
			}
		}

		// Token: 0x06005025 RID: 20517 RVA: 0x00121963 File Offset: 0x0011FB63
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x00121978 File Offset: 0x0011FB78
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001D4A RID: 7498
		private readonly bool _singlePropertyUnwrapped;
	}
}
