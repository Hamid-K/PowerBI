using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000245 RID: 581
	[ImmutableObject(true)]
	public sealed class ResolvedQueryNativeVisualCalculationExpression : ResolvedQueryExpression
	{
		// Token: 0x06001194 RID: 4500 RVA: 0x0001F95F File Offset: 0x0001DB5F
		internal ResolvedQueryNativeVisualCalculationExpression(string language, string expression)
		{
			this.Language = language;
			this.Expression = expression;
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x0001F975 File Offset: 0x0001DB75
		public string Language { get; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x0001F97D File Offset: 0x0001DB7D
		public string Expression { get; }

		// Token: 0x06001197 RID: 4503 RVA: 0x0001F985 File Offset: 0x0001DB85
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0001F98E File Offset: 0x0001DB8E
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0001F997 File Offset: 0x0001DB97
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryNativeVisualCalculationExpression);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0001F9A6 File Offset: 0x0001DBA6
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
