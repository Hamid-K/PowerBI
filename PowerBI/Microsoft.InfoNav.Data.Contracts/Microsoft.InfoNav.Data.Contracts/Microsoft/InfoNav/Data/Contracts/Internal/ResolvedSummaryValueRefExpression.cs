using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000264 RID: 612
	[ImmutableObject(true)]
	public class ResolvedSummaryValueRefExpression : ResolvedQueryExpression
	{
		// Token: 0x06001257 RID: 4695 RVA: 0x000202E1 File Offset: 0x0001E4E1
		internal ResolvedSummaryValueRefExpression(string name)
		{
			this.Name = name;
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x000202F0 File Offset: 0x0001E4F0
		public string Name { get; }

		// Token: 0x06001259 RID: 4697 RVA: 0x000202F8 File Offset: 0x0001E4F8
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00020301 File Offset: 0x0001E501
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0002030A File Offset: 0x0001E50A
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedSummaryValueRefExpression);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00020319 File Offset: 0x0001E519
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
