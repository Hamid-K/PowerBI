using System;
using System.Linq.Expressions;

namespace Microsoft.OData.Client
{
	// Token: 0x020000AC RID: 172
	internal abstract class DataServiceALinqExpressionVisitor : ALinqExpressionVisitor
	{
		// Token: 0x06000586 RID: 1414 RVA: 0x00018230 File Offset: 0x00016430
		internal override Expression Visit(Expression exp)
		{
			if (exp == null)
			{
				return null;
			}
			switch (exp.NodeType)
			{
			case (ExpressionType)10000:
			case (ExpressionType)10001:
			case (ExpressionType)10002:
				return this.VisitQueryableResourceExpression((QueryableResourceExpression)exp);
			case (ExpressionType)10003:
				return this.VisitNavigationPropertySingletonExpression((NavigationPropertySingletonExpression)exp);
			case (ExpressionType)10008:
				return this.VisitInputReferenceExpression((InputReferenceExpression)exp);
			}
			return base.Visit(exp);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x000182AC File Offset: 0x000164AC
		internal virtual Expression VisitQueryableResourceExpression(QueryableResourceExpression rse)
		{
			Expression expression = this.Visit(rse.Source);
			if (expression != rse.Source)
			{
				rse = QueryableResourceExpression.CreateNavigationResourceExpression(rse.NodeType, rse.Type, expression, rse.MemberExpression, rse.ResourceType, rse.ExpandPaths, rse.CountOption, rse.CustomQueryOptions, rse.Projection, rse.ResourceTypeAs, rse.UriVersion, rse.OperationName, rse.OperationParameters);
			}
			return rse;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00018320 File Offset: 0x00016520
		internal virtual Expression VisitNavigationPropertySingletonExpression(NavigationPropertySingletonExpression npse)
		{
			Expression expression = this.Visit(npse.Source);
			if (expression != npse.Source)
			{
				npse = new NavigationPropertySingletonExpression(npse.Type, expression, npse.MemberExpression, npse.MemberExpression.Type, npse.ExpandPaths, npse.CountOption, npse.CustomQueryOptions, npse.Projection, npse.ResourceTypeAs, npse.UriVersion);
			}
			return npse;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00018388 File Offset: 0x00016588
		internal virtual Expression VisitInputReferenceExpression(InputReferenceExpression ire)
		{
			ResourceExpression resourceExpression = (ResourceExpression)this.Visit(ire.Target);
			return resourceExpression.CreateReference();
		}
	}
}
