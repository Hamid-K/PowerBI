using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000279 RID: 633
	internal abstract class DefaultFilterDefinitionVisitor<TContext> : FilterDefinitionVisitor<TContext>
	{
		// Token: 0x06001332 RID: 4914 RVA: 0x00022854 File Offset: 0x00020A54
		protected override void VisitFilter(TContext context, QueryFilter filter)
		{
			if (filter == null)
			{
				return;
			}
			if (!filter.Target.IsNullOrEmpty<QueryExpressionContainer>())
			{
				foreach (QueryExpressionContainer queryExpressionContainer in filter.Target)
				{
					this.VisitExpression(context, queryExpressionContainer);
				}
			}
			if (filter.Condition != null)
			{
				this.VisitExpression(context, filter.Condition);
			}
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x000228DC File Offset: 0x00020ADC
		protected override void VisitEntitySource(TContext context, EntitySource source)
		{
			if (source != null && source.Expression != null)
			{
				this.VisitExpression(context, source.Expression);
			}
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00022902 File Offset: 0x00020B02
		protected virtual void VisitExpression(TContext context, QueryExpressionContainer expression)
		{
		}
	}
}
