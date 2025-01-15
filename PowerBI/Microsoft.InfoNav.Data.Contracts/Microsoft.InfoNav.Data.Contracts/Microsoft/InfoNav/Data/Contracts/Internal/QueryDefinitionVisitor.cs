using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002A2 RID: 674
	public abstract class QueryDefinitionVisitor
	{
		// Token: 0x06001490 RID: 5264
		protected abstract void VisitParameterDeclaration(QueryExpressionContainer parameterDeclaration);

		// Token: 0x06001491 RID: 5265
		protected abstract void VisitLetBinding(QueryExpressionContainer letBinding);

		// Token: 0x06001492 RID: 5266
		protected abstract void VisitEntitySource(EntitySource source);

		// Token: 0x06001493 RID: 5267
		protected abstract void VisitSelectExpression(QueryExpressionContainer expression);

		// Token: 0x06001494 RID: 5268
		protected abstract void VisitGroupByExpression(QueryExpressionContainer expression);

		// Token: 0x06001495 RID: 5269
		protected abstract void VisitExpression(QueryExpressionContainer expression);

		// Token: 0x06001496 RID: 5270
		protected abstract void VisitFilter(QueryFilter filter);

		// Token: 0x06001497 RID: 5271
		protected abstract void VisitSortClause(QuerySortClause sortClause);

		// Token: 0x06001498 RID: 5272
		protected abstract void VisitTransform(QueryTransform transform);

		// Token: 0x06001499 RID: 5273
		protected abstract void VisitAxis(QueryAxis axis);

		// Token: 0x0600149A RID: 5274
		protected abstract void VisitAxisGroup(QueryAxisGroup axisGroup);

		// Token: 0x0600149B RID: 5275 RVA: 0x00025698 File Offset: 0x00023898
		public void Visit(QueryDefinition queryDefinition)
		{
			if (!queryDefinition.Parameters.IsNullOrEmpty<QueryExpressionContainer>())
			{
				foreach (QueryExpressionContainer queryExpressionContainer in queryDefinition.Parameters)
				{
					this.VisitParameterDeclaration(queryExpressionContainer);
				}
			}
			if (!queryDefinition.Let.IsNullOrEmpty<QueryExpressionContainer>())
			{
				foreach (QueryExpressionContainer queryExpressionContainer2 in queryDefinition.Let)
				{
					this.VisitLetBinding(queryExpressionContainer2);
				}
			}
			if (!queryDefinition.From.IsNullOrEmpty<EntitySource>())
			{
				foreach (EntitySource entitySource in queryDefinition.From)
				{
					this.VisitEntitySource(entitySource);
				}
			}
			if (!queryDefinition.Where.IsNullOrEmpty<QueryFilter>())
			{
				foreach (QueryFilter queryFilter in queryDefinition.Where)
				{
					this.VisitFilter(queryFilter);
				}
			}
			if (!queryDefinition.VisualShape.IsNullOrEmpty<QueryAxis>())
			{
				foreach (QueryAxis queryAxis in queryDefinition.VisualShape)
				{
					this.VisitAxis(queryAxis);
				}
			}
			if (!queryDefinition.OrderBy.IsNullOrEmpty<QuerySortClause>())
			{
				foreach (QuerySortClause querySortClause in queryDefinition.OrderBy)
				{
					this.VisitSortClause(querySortClause);
				}
			}
			if (!queryDefinition.Select.IsNullOrEmpty<QueryExpressionContainer>())
			{
				foreach (QueryExpressionContainer queryExpressionContainer3 in queryDefinition.Select)
				{
					this.VisitSelectExpression(queryExpressionContainer3);
				}
			}
			if (!queryDefinition.GroupBy.IsNullOrEmpty<QueryExpressionContainer>())
			{
				foreach (QueryExpressionContainer queryExpressionContainer4 in queryDefinition.GroupBy)
				{
					this.VisitGroupByExpression(queryExpressionContainer4);
				}
			}
			if (!queryDefinition.Transform.IsNullOrEmpty<QueryTransform>())
			{
				foreach (QueryTransform queryTransform in queryDefinition.Transform)
				{
					this.VisitTransform(queryTransform);
				}
			}
		}
	}
}
