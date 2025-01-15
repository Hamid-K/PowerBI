using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200023B RID: 571
	[ImmutableObject(true)]
	public sealed class ResolvedQueryInExpression : ResolvedQueryExpression
	{
		// Token: 0x06001150 RID: 4432 RVA: 0x0001F651 File Offset: 0x0001D851
		internal ResolvedQueryInExpression(IReadOnlyList<ResolvedQueryExpression> expressions, IReadOnlyList<IReadOnlyList<ResolvedQueryExpression>> values, QueryEqualitySemanticsKind? equalityKind)
		{
			this.Expressions = expressions;
			this.Values = values;
			this.EqualityKind = equalityKind;
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0001F66E File Offset: 0x0001D86E
		public ResolvedQueryInExpression(IReadOnlyList<ResolvedQueryExpression> expressions, ResolvedQueryExpression table)
		{
			this.Expressions = expressions;
			this.Table = table;
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x0001F684 File Offset: 0x0001D884
		public IReadOnlyList<ResolvedQueryExpression> Expressions { get; }

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x0001F68C File Offset: 0x0001D88C
		public IReadOnlyList<IReadOnlyList<ResolvedQueryExpression>> Values { get; }

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x0001F694 File Offset: 0x0001D894
		public ResolvedQueryExpression Table { get; }

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x0001F69C File Offset: 0x0001D89C
		public QueryEqualitySemanticsKind? EqualityKind { get; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x0001F6A4 File Offset: 0x0001D8A4
		public bool HasValues
		{
			get
			{
				return !this.Values.IsNullOrEmpty<IReadOnlyList<ResolvedQueryExpression>>();
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0001F6B4 File Offset: 0x0001D8B4
		public bool HasTable
		{
			get
			{
				return this.Table != null;
			}
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0001F6C2 File Offset: 0x0001D8C2
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0001F6CB File Offset: 0x0001D8CB
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0001F6D4 File Offset: 0x0001D8D4
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryInExpression);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0001F6E3 File Offset: 0x0001D8E3
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
