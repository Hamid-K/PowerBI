using System;
using System.ComponentModel;
using System.Globalization;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200008A RID: 138
	[ImmutableObject(true)]
	internal sealed class ResolvedQueryExpressionDsqRewriter : ResolvedQueryExpressionRewriter
	{
		// Token: 0x06000567 RID: 1383 RVA: 0x00013E7C File Offset: 0x0001207C
		internal void SetDefinitionRewriter(ResolvedQueryDefinitionRewriter definitionRewriter)
		{
			this._definitionRewriter = definitionRewriter;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00013E88 File Offset: 0x00012088
		public override ResolvedQueryExpression Visit(ResolvedQueryHierarchyLevelExpression expression)
		{
			ResolvedQueryExpression expression2 = expression.HierarchyExpression.Expression;
			ResolvedQuerySourceRefExpression resolvedQuerySourceRefExpression = expression2 as ResolvedQuerySourceRefExpression;
			if (resolvedQuerySourceRefExpression != null)
			{
				return resolvedQuerySourceRefExpression.Property(expression.Level.Source);
			}
			ResolvedQueryPropertyVariationSourceExpression resolvedQueryPropertyVariationSourceExpression = expression2 as ResolvedQueryPropertyVariationSourceExpression;
			return this.GetSourceEntitySourceRefExpression(resolvedQueryPropertyVariationSourceExpression).Property(expression.Level.Source);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00013EE4 File Offset: 0x000120E4
		public override ResolvedQueryExpression Visit(ResolvedQueryColumnExpression expression)
		{
			ResolvedQueryPropertyVariationSourceExpression resolvedQueryPropertyVariationSourceExpression = expression.Expression as ResolvedQueryPropertyVariationSourceExpression;
			if (resolvedQueryPropertyVariationSourceExpression != null)
			{
				return this.GetSourceEntitySourceRefExpression(resolvedQueryPropertyVariationSourceExpression).Column(expression.Column);
			}
			return base.Visit(expression);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00013F20 File Offset: 0x00012120
		public override ResolvedQueryExpression Visit(ResolvedQueryMeasureExpression expression)
		{
			ResolvedQueryPropertyVariationSourceExpression resolvedQueryPropertyVariationSourceExpression = expression.Expression as ResolvedQueryPropertyVariationSourceExpression;
			if (resolvedQueryPropertyVariationSourceExpression != null)
			{
				return this.GetSourceEntitySourceRefExpression(resolvedQueryPropertyVariationSourceExpression).Measure(expression.Measure);
			}
			return base.Visit(expression);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00013F5C File Offset: 0x0001215C
		private ResolvedQuerySourceRefExpression GetSourceEntitySourceRefExpression(ResolvedQueryPropertyVariationSourceExpression variationExpression)
		{
			IConceptualEntity sourceEntity = variationExpression.SourceEntity;
			if (sourceEntity == variationExpression.SourceRefExpression.SourceEntity)
			{
				return variationExpression.SourceRefExpression;
			}
			string text = string.Format(CultureInfo.InvariantCulture, "__{0}_{1}", variationExpression.VariationSource.Name, sourceEntity.Name);
			return sourceEntity.SourceRef(text);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00013FB0 File Offset: 0x000121B0
		public override ResolvedQueryExpression Visit(ResolvedQuerySubqueryExpression expression)
		{
			if (this._definitionRewriter == null)
			{
				return base.Visit(expression);
			}
			ResolvedQueryDefinition resolvedQueryDefinition = this._definitionRewriter.Rewrite(expression.Subquery);
			if (expression.Subquery == resolvedQueryDefinition)
			{
				return expression;
			}
			return resolvedQueryDefinition.Subquery();
		}

		// Token: 0x040002F5 RID: 757
		private ResolvedQueryDefinitionRewriter _definitionRewriter;
	}
}
