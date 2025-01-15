using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000215 RID: 533
	[ImmutableObject(true)]
	public sealed class ResolvedQueryColumnExpression : ResolvedQueryPropertyExpression
	{
		// Token: 0x06000F6D RID: 3949 RVA: 0x0001D915 File Offset: 0x0001BB15
		internal ResolvedQueryColumnExpression(ResolvedQueryExpression expression, IConceptualColumn column)
			: base(expression)
		{
			this._column = column;
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0001D925 File Offset: 0x0001BB25
		public IConceptualColumn Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0001D92D File Offset: 0x0001BB2D
		public override IConceptualProperty Property
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0001D935 File Offset: 0x0001BB35
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0001D93E File Offset: 0x0001BB3E
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0001D947 File Offset: 0x0001BB47
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryColumnExpression);
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0001D956 File Offset: 0x0001BB56
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x0400072A RID: 1834
		private readonly IConceptualColumn _column;
	}
}
