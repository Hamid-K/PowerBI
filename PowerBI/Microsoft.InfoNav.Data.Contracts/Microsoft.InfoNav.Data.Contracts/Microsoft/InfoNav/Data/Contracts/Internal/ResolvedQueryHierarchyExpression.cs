using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000239 RID: 569
	[ImmutableObject(true)]
	public sealed class ResolvedQueryHierarchyExpression : ResolvedQueryExpression
	{
		// Token: 0x06001142 RID: 4418 RVA: 0x0001F5B1 File Offset: 0x0001D7B1
		internal ResolvedQueryHierarchyExpression(ResolvedQueryExpression expression, IConceptualHierarchy hierarchy)
		{
			this._expression = expression;
			this._hierarchy = hierarchy;
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x0001F5C7 File Offset: 0x0001D7C7
		public ResolvedQueryExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0001F5CF File Offset: 0x0001D7CF
		public IConceptualHierarchy Hierarchy
		{
			get
			{
				return this._hierarchy;
			}
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0001F5D7 File Offset: 0x0001D7D7
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0001F5E0 File Offset: 0x0001D7E0
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0001F5E9 File Offset: 0x0001D7E9
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryHierarchyExpression);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x0400077C RID: 1916
		private readonly ResolvedQueryExpression _expression;

		// Token: 0x0400077D RID: 1917
		private readonly IConceptualHierarchy _hierarchy;
	}
}
