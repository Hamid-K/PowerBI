using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000278 RID: 632
	internal abstract class DefaultFilterDefinitionVisitor : FilterDefinitionVisitor
	{
		// Token: 0x0600132E RID: 4910 RVA: 0x000227A0 File Offset: 0x000209A0
		protected override void VisitFilter(QueryFilter filter)
		{
			if (filter == null)
			{
				return;
			}
			if (!filter.Target.IsNullOrEmpty<QueryExpressionContainer>())
			{
				foreach (QueryExpressionContainer queryExpressionContainer in filter.Target)
				{
					this.VisitExpression(queryExpressionContainer);
				}
			}
			if (filter.Condition != null)
			{
				this.VisitExpression(filter.Condition);
			}
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00022824 File Offset: 0x00020A24
		protected override void VisitEntitySource(EntitySource source)
		{
			if (source != null && source.Expression != null)
			{
				this.VisitExpression(source.Expression);
			}
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00022849 File Offset: 0x00020A49
		protected virtual void VisitExpression(QueryExpressionContainer expression)
		{
		}
	}
}
