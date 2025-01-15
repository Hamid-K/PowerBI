using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002A3 RID: 675
	public abstract class QueryDefinitionVisitor<TContext>
	{
		// Token: 0x0600149D RID: 5277
		protected abstract void VisitParameterDeclaration(TContext context, QueryExpressionContainer parameterDeclaration);

		// Token: 0x0600149E RID: 5278
		protected abstract void VisitLetBinding(TContext context, QueryExpressionContainer letBinding);

		// Token: 0x0600149F RID: 5279
		protected abstract void VisitEntitySource(TContext context, EntitySource source);

		// Token: 0x060014A0 RID: 5280
		protected abstract void VisitExpression(TContext context, QueryExpressionContainer expression);

		// Token: 0x060014A1 RID: 5281
		protected abstract void VisitFilter(TContext context, QueryFilter filter);

		// Token: 0x060014A2 RID: 5282
		protected abstract void VisitSortClause(TContext context, QuerySortClause sortClause);

		// Token: 0x060014A3 RID: 5283
		protected abstract void VisitTransform(TContext context, QueryTransform transform);

		// Token: 0x060014A4 RID: 5284
		protected abstract void VisitVisualShape(TContext context, List<QueryAxis> axes);

		// Token: 0x060014A5 RID: 5285
		protected abstract void VisitAxis(TContext context, QueryAxis axis);

		// Token: 0x060014A6 RID: 5286
		protected abstract void VisitAxisGroup(TContext context, QueryAxisGroup axisGroup);

		// Token: 0x060014A7 RID: 5287 RVA: 0x0002598C File Offset: 0x00023B8C
		public virtual void Visit(TContext context, QueryDefinition queryDefinition)
		{
			if (!queryDefinition.Parameters.IsNullOrEmpty<QueryExpressionContainer>())
			{
				foreach (QueryExpressionContainer queryExpressionContainer in queryDefinition.Parameters)
				{
					this.VisitParameterDeclaration(context, queryExpressionContainer);
				}
			}
			if (!queryDefinition.Let.IsNullOrEmpty<QueryExpressionContainer>())
			{
				foreach (QueryExpressionContainer queryExpressionContainer2 in queryDefinition.Let)
				{
					this.VisitLetBinding(context, queryExpressionContainer2);
				}
			}
			if (!queryDefinition.From.IsNullOrEmpty<EntitySource>())
			{
				foreach (EntitySource entitySource in queryDefinition.From)
				{
					this.VisitEntitySource(context, entitySource);
				}
			}
			if (!queryDefinition.Where.IsNullOrEmpty<QueryFilter>())
			{
				foreach (QueryFilter queryFilter in queryDefinition.Where)
				{
					this.VisitFilter(context, queryFilter);
				}
			}
			if (!queryDefinition.VisualShape.IsNullOrEmpty<QueryAxis>())
			{
				this.VisitVisualShape(context, queryDefinition.VisualShape);
			}
			if (!queryDefinition.OrderBy.IsNullOrEmpty<QuerySortClause>())
			{
				foreach (QuerySortClause querySortClause in queryDefinition.OrderBy)
				{
					this.VisitSortClause(context, querySortClause);
				}
			}
			if (!queryDefinition.Select.IsNullOrEmpty<QueryExpressionContainer>())
			{
				foreach (QueryExpressionContainer queryExpressionContainer3 in queryDefinition.Select)
				{
					this.VisitExpression(context, queryExpressionContainer3);
				}
			}
			if (!queryDefinition.GroupBy.IsNullOrEmpty<QueryExpressionContainer>())
			{
				foreach (QueryExpressionContainer queryExpressionContainer4 in queryDefinition.GroupBy)
				{
					this.VisitExpression(context, queryExpressionContainer4);
				}
			}
			if (!queryDefinition.Transform.IsNullOrEmpty<QueryTransform>())
			{
				foreach (QueryTransform queryTransform in queryDefinition.Transform)
				{
					this.VisitTransform(context, queryTransform);
				}
			}
		}
	}
}
