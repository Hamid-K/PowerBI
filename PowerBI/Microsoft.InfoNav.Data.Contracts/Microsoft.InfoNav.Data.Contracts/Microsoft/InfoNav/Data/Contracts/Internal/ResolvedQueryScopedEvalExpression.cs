using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000250 RID: 592
	[ImmutableObject(true)]
	public sealed class ResolvedQueryScopedEvalExpression : ResolvedQueryExpression
	{
		// Token: 0x060011D7 RID: 4567 RVA: 0x0001FC70 File Offset: 0x0001DE70
		internal ResolvedQueryScopedEvalExpression(ResolvedQueryExpression expression, IReadOnlyList<ResolvedQueryExpression> scope)
		{
			this._expression = expression;
			this._scope = scope;
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x0001FC86 File Offset: 0x0001DE86
		public ResolvedQueryExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x0001FC8E File Offset: 0x0001DE8E
		public IReadOnlyList<ResolvedQueryExpression> Scope
		{
			get
			{
				return this._scope;
			}
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0001FC96 File Offset: 0x0001DE96
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0001FC9F File Offset: 0x0001DE9F
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0001FCA8 File Offset: 0x0001DEA8
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryScopedEvalExpression);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0001FCB7 File Offset: 0x0001DEB7
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x0400079D RID: 1949
		private readonly ResolvedQueryExpression _expression;

		// Token: 0x0400079E RID: 1950
		private readonly IReadOnlyList<ResolvedQueryExpression> _scope;
	}
}
