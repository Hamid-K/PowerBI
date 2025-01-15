using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000203 RID: 515
	public class ResolvedQueryDefinitionEquivalenceComparer : ResolvedQueryDefinitionEqualsComparer
	{
		// Token: 0x06000E47 RID: 3655 RVA: 0x0001C0BE File Offset: 0x0001A2BE
		public ResolvedQueryDefinitionEquivalenceComparer()
		{
			this._context = new ResolvedQueryEquivalenceComparerContext();
			this.ExpressionComparer = new ResolvedQueryExpressionEquivalenceComparer(this, this._context);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0001C0E3 File Offset: 0x0001A2E3
		public ResolvedQueryDefinitionEquivalenceComparer(Func<ResolvedQueryDefinitionEquivalenceComparer, ResolvedQueryEquivalenceComparerContext, ResolvedQueryExpressionEquivalenceComparer> createExpressionComparer)
		{
			this._context = new ResolvedQueryEquivalenceComparerContext();
			this.ExpressionComparer = createExpressionComparer(this, this._context);
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0001C109 File Offset: 0x0001A309
		protected override ResolvedQueryExpressionEqualityComparer ExpressionComparer { get; }

		// Token: 0x06000E4A RID: 3658 RVA: 0x0001C111 File Offset: 0x0001A311
		public override bool Equals(ResolvedQueryDefinition left, ResolvedQueryDefinition right)
		{
			this._context.BeginQuery();
			bool flag = base.Equals(left, right);
			this._context.EndQuery();
			return flag;
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0001C131 File Offset: 0x0001A331
		protected override bool EqualsQueryDefinitionName(string leftName, string rightName)
		{
			return true;
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0001C134 File Offset: 0x0001A334
		protected override bool EqualsQuerySource(ResolvedQuerySource left, ResolvedQuerySource right)
		{
			if (right == null)
			{
				return false;
			}
			this._context.AddSourceNameMapping(left.Name, right.Name);
			return true;
		}

		// Token: 0x0400070D RID: 1805
		private readonly ResolvedQueryEquivalenceComparerContext _context;
	}
}
