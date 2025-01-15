using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006DD RID: 1757
	public class DbScanExpression : DbExpression
	{
		// Token: 0x06005172 RID: 20850 RVA: 0x00123964 File Offset: 0x00121B64
		internal DbScanExpression()
		{
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x0012396C File Offset: 0x00121B6C
		internal DbScanExpression(TypeUsage collectionOfEntityType, EntitySetBase entitySet)
			: base(DbExpressionKind.Scan, collectionOfEntityType, true)
		{
			this._targetSet = entitySet;
		}

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06005174 RID: 20852 RVA: 0x0012397F File Offset: 0x00121B7F
		public virtual EntitySetBase Target
		{
			get
			{
				return this._targetSet;
			}
		}

		// Token: 0x06005175 RID: 20853 RVA: 0x00123987 File Offset: 0x00121B87
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005176 RID: 20854 RVA: 0x0012399C File Offset: 0x00121B9C
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001DC5 RID: 7621
		private readonly EntitySetBase _targetSet;
	}
}
