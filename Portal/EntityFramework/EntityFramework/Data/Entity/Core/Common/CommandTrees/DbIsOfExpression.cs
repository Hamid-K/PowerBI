using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006C7 RID: 1735
	public sealed class DbIsOfExpression : DbUnaryExpression
	{
		// Token: 0x060050F0 RID: 20720 RVA: 0x00122214 File Offset: 0x00120414
		internal DbIsOfExpression(DbExpressionKind isOfKind, TypeUsage booleanResultType, DbExpression argument, TypeUsage isOfType)
			: base(isOfKind, booleanResultType, argument)
		{
			this._ofType = isOfType;
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x060050F1 RID: 20721 RVA: 0x00122227 File Offset: 0x00120427
		public TypeUsage OfType
		{
			get
			{
				return this._ofType;
			}
		}

		// Token: 0x060050F2 RID: 20722 RVA: 0x0012222F File Offset: 0x0012042F
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x00122244 File Offset: 0x00120444
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001DA1 RID: 7585
		private readonly TypeUsage _ofType;
	}
}
