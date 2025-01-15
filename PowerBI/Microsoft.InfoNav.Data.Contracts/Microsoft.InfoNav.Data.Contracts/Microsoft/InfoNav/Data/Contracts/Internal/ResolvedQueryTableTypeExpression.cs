using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000258 RID: 600
	[ImmutableObject(true)]
	public sealed class ResolvedQueryTableTypeExpression : ResolvedQueryExpression
	{
		// Token: 0x0600120F RID: 4623 RVA: 0x0001FEFB File Offset: 0x0001E0FB
		internal ResolvedQueryTableTypeExpression(IReadOnlyList<ResolvedQueryTableTypeColumn> columns)
		{
			this.Columns = columns;
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x0001FF0A File Offset: 0x0001E10A
		public IReadOnlyList<ResolvedQueryTableTypeColumn> Columns { get; }

		// Token: 0x06001211 RID: 4625 RVA: 0x0001FF12 File Offset: 0x0001E112
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0001FF1B File Offset: 0x0001E11B
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0001FF24 File Offset: 0x0001E124
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryTableTypeExpression);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0001FF33 File Offset: 0x0001E133
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
