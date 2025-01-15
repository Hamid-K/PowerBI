using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000276 RID: 630
	internal abstract class FilterDefinitionVisitor
	{
		// Token: 0x06001326 RID: 4902
		protected abstract void VisitEntitySource(EntitySource source);

		// Token: 0x06001327 RID: 4903
		protected abstract void VisitFilter(QueryFilter filter);

		// Token: 0x06001328 RID: 4904 RVA: 0x0002262C File Offset: 0x0002082C
		internal void Visit(FilterDefinition filter)
		{
			if (!filter.From.IsNullOrEmpty<EntitySource>())
			{
				foreach (EntitySource entitySource in filter.From)
				{
					this.VisitEntitySource(entitySource);
				}
			}
			if (!filter.Where.IsNullOrEmpty<QueryFilter>())
			{
				foreach (QueryFilter queryFilter in filter.Where)
				{
					this.VisitFilter(queryFilter);
				}
			}
		}
	}
}
