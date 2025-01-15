using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x02000091 RID: 145
	internal class ProjectionRewriter : ALinqExpressionVisitor
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x0000F479 File Offset: 0x0000D679
		private ProjectionRewriter(Type proposedParameterType)
		{
			this.newLambdaParameter = Expression.Parameter(proposedParameterType, "it");
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000F494 File Offset: 0x0000D694
		internal static LambdaExpression TryToRewrite(LambdaExpression le, ResourceExpression source)
		{
			Type proposedParameterType = source.ResourceType;
			LambdaExpression lambdaExpression;
			if (!ResourceBinder.PatternRules.MatchSingleArgumentLambda(le, out le) || ClientTypeUtil.TypeOrElementTypeIsEntity(le.Parameters[0].Type) || !le.Parameters[0].Type.GetProperties().Any((PropertyInfo p) => p.PropertyType == proposedParameterType))
			{
				lambdaExpression = le;
			}
			else
			{
				ProjectionRewriter projectionRewriter = new ProjectionRewriter(proposedParameterType);
				lambdaExpression = projectionRewriter.Rebind(le, source);
			}
			return lambdaExpression;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000F518 File Offset: 0x0000D718
		internal LambdaExpression Rebind(LambdaExpression lambda, ResourceExpression source)
		{
			this.successfulRebind = true;
			this.oldLambdaParameter = lambda.Parameters[0];
			this.projectionSource = source;
			Expression expression = this.Visit(lambda.Body);
			if (this.successfulRebind)
			{
				Type type = typeof(Func<, >).MakeGenericType(new Type[]
				{
					this.newLambdaParameter.Type,
					lambda.Body.Type
				});
				return Expression.Lambda(type, expression, new ParameterExpression[] { this.newLambdaParameter });
			}
			throw new NotSupportedException(Strings.ALinq_CanOnlyProjectTheLeaf);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		internal override Expression VisitMemberAccess(MemberExpression m)
		{
			if (m.Expression == this.oldLambdaParameter)
			{
				QueryableResourceExpression queryableResourceExpression = this.projectionSource as QueryableResourceExpression;
				if (queryableResourceExpression != null && queryableResourceExpression.HasTransparentScope && queryableResourceExpression.TransparentScope.Accessor == m.Member.Name)
				{
					return this.newLambdaParameter;
				}
				this.successfulRebind = false;
			}
			return base.VisitMemberAccess(m);
		}

		// Token: 0x0400013B RID: 315
		private readonly ParameterExpression newLambdaParameter;

		// Token: 0x0400013C RID: 316
		private ParameterExpression oldLambdaParameter;

		// Token: 0x0400013D RID: 317
		private ResourceExpression projectionSource;

		// Token: 0x0400013E RID: 318
		private bool successfulRebind;
	}
}
