using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006D4 RID: 1748
	public class DbParameterReferenceExpression : DbExpression
	{
		// Token: 0x0600513F RID: 20799 RVA: 0x001234DD File Offset: 0x001216DD
		internal DbParameterReferenceExpression()
		{
		}

		// Token: 0x06005140 RID: 20800 RVA: 0x001234E5 File Offset: 0x001216E5
		internal DbParameterReferenceExpression(TypeUsage type, string name)
			: base(DbExpressionKind.ParameterReference, type, false)
		{
			this._name = name;
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06005141 RID: 20801 RVA: 0x001234F8 File Offset: 0x001216F8
		public virtual string ParameterName
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06005142 RID: 20802 RVA: 0x00123500 File Offset: 0x00121700
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005143 RID: 20803 RVA: 0x00123515 File Offset: 0x00121715
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001DB4 RID: 7604
		private readonly string _name;
	}
}
