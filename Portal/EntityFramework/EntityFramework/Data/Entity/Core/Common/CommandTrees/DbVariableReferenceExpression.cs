using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006E6 RID: 1766
	public class DbVariableReferenceExpression : DbExpression
	{
		// Token: 0x0600519D RID: 20893 RVA: 0x00123D21 File Offset: 0x00121F21
		internal DbVariableReferenceExpression()
		{
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x00123D29 File Offset: 0x00121F29
		internal DbVariableReferenceExpression(TypeUsage type, string name)
			: base(DbExpressionKind.VariableReference, type, true)
		{
			this._name = name;
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x0600519F RID: 20895 RVA: 0x00123D3C File Offset: 0x00121F3C
		public virtual string VariableName
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x00123D44 File Offset: 0x00121F44
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x00123D59 File Offset: 0x00121F59
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001DD4 RID: 7636
		private readonly string _name;
	}
}
