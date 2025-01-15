using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200023A RID: 570
	[ImmutableObject(true)]
	public sealed class ResolvedQueryHierarchyLevelExpression : ResolvedQueryExpression
	{
		// Token: 0x06001149 RID: 4425 RVA: 0x0001F601 File Offset: 0x0001D801
		internal ResolvedQueryHierarchyLevelExpression(ResolvedQueryHierarchyExpression hierarchyExpression, IConceptualHierarchyLevel level)
		{
			this._hierarchyExpression = hierarchyExpression;
			this._level = level;
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0001F617 File Offset: 0x0001D817
		public ResolvedQueryHierarchyExpression HierarchyExpression
		{
			get
			{
				return this._hierarchyExpression;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x0001F61F File Offset: 0x0001D81F
		public IConceptualHierarchyLevel Level
		{
			get
			{
				return this._level;
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0001F627 File Offset: 0x0001D827
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0001F630 File Offset: 0x0001D830
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0001F639 File Offset: 0x0001D839
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryHierarchyLevelExpression);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0001F648 File Offset: 0x0001D848
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x0400077E RID: 1918
		private readonly ResolvedQueryHierarchyExpression _hierarchyExpression;

		// Token: 0x0400077F RID: 1919
		private readonly IConceptualHierarchyLevel _level;
	}
}
