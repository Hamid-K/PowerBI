using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006CF RID: 1743
	public sealed class DbNewInstanceExpression : DbExpression
	{
		// Token: 0x0600512A RID: 20778 RVA: 0x00123378 File Offset: 0x00121578
		internal DbNewInstanceExpression(TypeUsage type, DbExpressionList args)
			: base(DbExpressionKind.NewInstance, type, true)
		{
			this._elements = args;
		}

		// Token: 0x0600512B RID: 20779 RVA: 0x0012338B File Offset: 0x0012158B
		internal DbNewInstanceExpression(TypeUsage resultType, DbExpressionList attributeValues, ReadOnlyCollection<DbRelatedEntityRef> relationships)
			: this(resultType, attributeValues)
		{
			this._relatedEntityRefs = ((relationships.Count > 0) ? relationships : null);
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x0600512C RID: 20780 RVA: 0x001233A8 File Offset: 0x001215A8
		public IList<DbExpression> Arguments
		{
			get
			{
				return this._elements;
			}
		}

		// Token: 0x0600512D RID: 20781 RVA: 0x001233B0 File Offset: 0x001215B0
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600512E RID: 20782 RVA: 0x001233C5 File Offset: 0x001215C5
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x0600512F RID: 20783 RVA: 0x001233DA File Offset: 0x001215DA
		internal bool HasRelatedEntityReferences
		{
			get
			{
				return this._relatedEntityRefs != null;
			}
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06005130 RID: 20784 RVA: 0x001233E5 File Offset: 0x001215E5
		internal ReadOnlyCollection<DbRelatedEntityRef> RelatedEntityReferences
		{
			get
			{
				return this._relatedEntityRefs;
			}
		}

		// Token: 0x04001DB1 RID: 7601
		private readonly DbExpressionList _elements;

		// Token: 0x04001DB2 RID: 7602
		private readonly ReadOnlyCollection<DbRelatedEntityRef> _relatedEntityRefs;
	}
}
