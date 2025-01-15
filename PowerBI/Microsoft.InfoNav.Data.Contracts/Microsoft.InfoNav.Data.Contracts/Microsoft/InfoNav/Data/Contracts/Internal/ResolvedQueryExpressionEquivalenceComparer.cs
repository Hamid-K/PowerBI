using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000207 RID: 519
	public class ResolvedQueryExpressionEquivalenceComparer : ResolvedQueryExpressionEqualsComparer
	{
		// Token: 0x06000F21 RID: 3873 RVA: 0x0001D166 File Offset: 0x0001B366
		public ResolvedQueryExpressionEquivalenceComparer(ResolvedQueryDefinitionEquivalenceComparer structureComparer, ResolvedQueryEquivalenceComparerContext context)
		{
			this.StructureComparer = structureComparer;
			this._context = context;
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0001D17C File Offset: 0x0001B37C
		public override ResolvedQueryDefinitionEqualityComparer StructureComparer { get; }

		// Token: 0x06000F23 RID: 3875 RVA: 0x0001D184 File Offset: 0x0001B384
		public override bool VisitEquals(ResolvedQuerySourceRefExpression left, ResolvedQuerySourceRefExpression right)
		{
			string text;
			return !(right == null) && this._context.TryGetSourceNameMapping(left.SourceName, out text) && left.SourceEntity == right.SourceEntity && QueryNameComparer.Instance.Equals(text, right.SourceName);
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0001D1D4 File Offset: 0x0001B3D4
		public override int VisitGetHashCode(ResolvedQuerySourceRefExpression obj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0001D1DC File Offset: 0x0001B3DC
		public override bool VisitEquals(ResolvedQueryExpressionSourceRefExpression left, ResolvedQueryExpressionSourceRefExpression right)
		{
			string text;
			return !(right == null) && this._context.TryGetSourceNameMapping(left.SourceName, out text) && QueryNameComparer.Instance.Equals(text, right.SourceName);
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0001D21C File Offset: 0x0001B41C
		public override int VisitGetHashCode(ResolvedQueryExpressionSourceRefExpression obj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0001D224 File Offset: 0x0001B424
		public override bool VisitEquals(ResolvedQueryTransformTableColumnExpression left, ResolvedQueryTransformTableColumnExpression right)
		{
			return right != null && QueryNameComparer.Instance.Equals(left.Table.Name, right.Table.Name) && QueryNameComparer.Instance.Equals(left.Column.Name, right.Column.Name);
		}

		// Token: 0x04000710 RID: 1808
		private readonly ResolvedQueryEquivalenceComparerContext _context;
	}
}
