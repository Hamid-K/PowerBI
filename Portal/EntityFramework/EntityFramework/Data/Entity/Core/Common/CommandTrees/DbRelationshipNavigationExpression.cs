using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006DC RID: 1756
	public sealed class DbRelationshipNavigationExpression : DbExpression
	{
		// Token: 0x0600516B RID: 20843 RVA: 0x001238F0 File Offset: 0x00121AF0
		internal DbRelationshipNavigationExpression(TypeUsage resultType, RelationshipType relType, RelationshipEndMember fromEnd, RelationshipEndMember toEnd, DbExpression navigateFrom)
			: base(DbExpressionKind.RelationshipNavigation, resultType, true)
		{
			this._relation = relType;
			this._fromRole = fromEnd;
			this._toRole = toEnd;
			this._from = navigateFrom;
		}

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x0600516C RID: 20844 RVA: 0x0012391A File Offset: 0x00121B1A
		public RelationshipType Relationship
		{
			get
			{
				return this._relation;
			}
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x0600516D RID: 20845 RVA: 0x00123922 File Offset: 0x00121B22
		public RelationshipEndMember NavigateFrom
		{
			get
			{
				return this._fromRole;
			}
		}

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x0600516E RID: 20846 RVA: 0x0012392A File Offset: 0x00121B2A
		public RelationshipEndMember NavigateTo
		{
			get
			{
				return this._toRole;
			}
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x0600516F RID: 20847 RVA: 0x00123932 File Offset: 0x00121B32
		public DbExpression NavigationSource
		{
			get
			{
				return this._from;
			}
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x0012393A File Offset: 0x00121B3A
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x0012394F File Offset: 0x00121B4F
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04001DC1 RID: 7617
		private readonly RelationshipType _relation;

		// Token: 0x04001DC2 RID: 7618
		private readonly RelationshipEndMember _fromRole;

		// Token: 0x04001DC3 RID: 7619
		private readonly RelationshipEndMember _toRole;

		// Token: 0x04001DC4 RID: 7620
		private readonly DbExpression _from;
	}
}
