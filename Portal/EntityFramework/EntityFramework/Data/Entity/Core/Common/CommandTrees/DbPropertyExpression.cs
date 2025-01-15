using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006D6 RID: 1750
	public class DbPropertyExpression : DbExpression
	{
		// Token: 0x06005149 RID: 20809 RVA: 0x0012357E File Offset: 0x0012177E
		internal DbPropertyExpression()
		{
		}

		// Token: 0x0600514A RID: 20810 RVA: 0x00123586 File Offset: 0x00121786
		internal DbPropertyExpression(TypeUsage resultType, EdmMember property, DbExpression instance)
			: base(DbExpressionKind.Property, resultType, true)
		{
			this._property = property;
			this._instance = instance;
		}

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x0600514B RID: 20811 RVA: 0x001235A0 File Offset: 0x001217A0
		public virtual EdmMember Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x0600514C RID: 20812 RVA: 0x001235A8 File Offset: 0x001217A8
		public virtual DbExpression Instance
		{
			get
			{
				return this._instance;
			}
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x001235B0 File Offset: 0x001217B0
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x001235C5 File Offset: 0x001217C5
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0600514F RID: 20815 RVA: 0x001235DA File Offset: 0x001217DA
		public KeyValuePair<string, DbExpression> ToKeyValuePair()
		{
			return new KeyValuePair<string, DbExpression>(this.Property.Name, this);
		}

		// Token: 0x06005150 RID: 20816 RVA: 0x001235ED File Offset: 0x001217ED
		public static implicit operator KeyValuePair<string, DbExpression>(DbPropertyExpression value)
		{
			Check.NotNull<DbPropertyExpression>(value, "value");
			return value.ToKeyValuePair();
		}

		// Token: 0x04001DB7 RID: 7607
		private readonly EdmMember _property;

		// Token: 0x04001DB8 RID: 7608
		private readonly DbExpression _instance;
	}
}
