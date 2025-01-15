using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200020F RID: 527
	[ImmutableObject(true)]
	public sealed class ResolvedQueryAnyValueExpression : ResolvedQueryExpression
	{
		// Token: 0x06000F49 RID: 3913 RVA: 0x0001D756 File Offset: 0x0001B956
		private ResolvedQueryAnyValueExpression(bool defaultValueOverridesAncestors)
		{
			this._defaultValueOverridesAncestors = defaultValueOverridesAncestors;
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x0001D765 File Offset: 0x0001B965
		public bool DefaultValueOverridesAncestors
		{
			get
			{
				return this._defaultValueOverridesAncestors;
			}
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0001D76D File Offset: 0x0001B96D
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0001D776 File Offset: 0x0001B976
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0001D77F File Offset: 0x0001B97F
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryAnyValueExpression);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0001D78E File Offset: 0x0001B98E
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x0400071D RID: 1821
		internal static readonly ResolvedQueryAnyValueExpression Instance = new ResolvedQueryAnyValueExpression(false);

		// Token: 0x0400071E RID: 1822
		internal static readonly ResolvedQueryAnyValueExpression DefaultValueOverridesAncestorsInstance = new ResolvedQueryAnyValueExpression(true);

		// Token: 0x0400071F RID: 1823
		private readonly bool _defaultValueOverridesAncestors;
	}
}
