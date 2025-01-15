using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000277 RID: 631
	internal abstract class FilterDefinitionVisitor<TContext>
	{
		// Token: 0x0600132A RID: 4906
		protected abstract void VisitEntitySource(TContext context, EntitySource source);

		// Token: 0x0600132B RID: 4907
		protected abstract void VisitFilter(TContext context, QueryFilter filter);

		// Token: 0x0600132C RID: 4908 RVA: 0x000226E4 File Offset: 0x000208E4
		internal void Visit(TContext context, FilterDefinition filter)
		{
			if (!filter.From.IsNullOrEmpty<EntitySource>())
			{
				foreach (EntitySource entitySource in filter.From)
				{
					this.VisitEntitySource(context, entitySource);
				}
			}
			if (!filter.Where.IsNullOrEmpty<QueryFilter>())
			{
				foreach (QueryFilter queryFilter in filter.Where)
				{
					this.VisitFilter(context, queryFilter);
				}
			}
		}
	}
}
