using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200006D RID: 109
	internal sealed class DsqWindowExpansionStateGenerator<TParentBuilder>
	{
		// Token: 0x060004B4 RID: 1204 RVA: 0x00011E1E File Offset: 0x0001001E
		private DsqWindowExpansionStateGenerator(TopNPerLevelLimitBuilder<TParentBuilder> topNPerLevelBuilder, DataShapeGenerationErrorContext errorContext, DsqExpressionGenerator expressionGenerator, string owningQueryName)
		{
			this._topNPerLevelBuilder = topNPerLevelBuilder;
			this._errorContext = errorContext;
			this._expressionGenerator = expressionGenerator;
			this._expressionContext = new ExpressionContext(owningQueryName, SemanticQueryObjectType.ExpansionState, "TopNPerLevel");
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00011E4E File Offset: 0x0001004E
		public static void Generate(TopNPerLevelLimitBuilder<TParentBuilder> topNPerLevelBuilder, DsqExpressionGenerator expressionGenerator, DataShapeGenerationErrorContext errorContext, ResolvedDataReductionWindowExpansionState windowExpansionState, string owningQueryName)
		{
			if (windowExpansionState == null)
			{
				return;
			}
			new DsqWindowExpansionStateGenerator<TParentBuilder>(topNPerLevelBuilder, errorContext, expressionGenerator, owningQueryName).Generate(windowExpansionState);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00011E64 File Offset: 0x00010064
		private void Generate(ResolvedDataReductionWindowExpansionState windowExpansionState)
		{
			if (!windowExpansionState.Levels.IsNullOrEmpty<ResolvedDataShapeBindingAxisExpansionLevel>())
			{
				foreach (ResolvedDataShapeBindingAxisExpansionLevel resolvedDataShapeBindingAxisExpansionLevel in windowExpansionState.Levels)
				{
					this.Visit(resolvedDataShapeBindingAxisExpansionLevel);
				}
			}
			if (windowExpansionState.WindowInstances != null)
			{
				LimitWindowExpansionInstanceBuilder<TopNPerLevelLimitBuilder<TParentBuilder>> limitWindowExpansionInstanceBuilder = this._topNPerLevelBuilder.WithWindowExpansionState();
				this.Visit<TopNPerLevelLimitBuilder<TParentBuilder>>(windowExpansionState.WindowInstances, limitWindowExpansionInstanceBuilder);
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00011EE0 File Offset: 0x000100E0
		private void Visit(ResolvedDataShapeBindingAxisExpansionLevel level)
		{
			List<Expression> list;
			if (this.TryTranslateExpressionList(level.Expressions, out list))
			{
				this._topNPerLevelBuilder.WithLevel(list);
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00011F0C File Offset: 0x0001010C
		private void Visit<TParent>(ResolvedDataReductionWindowExpansionInstance windowInstance, LimitWindowExpansionInstanceBuilder<TParent> builder)
		{
			List<Expression> list;
			if (!windowInstance.Values.IsNullOrEmpty<ResolvedQueryExpression>() && this.TryTranslateExpressionList(windowInstance.Values, out list))
			{
				builder.WithValues(list);
			}
			if (!windowInstance.WindowExpansionInstanceWindowValue.IsNullOrEmpty<ResolvedDataReductionWindowExpansionInstanceValue>())
			{
				foreach (ResolvedDataReductionWindowExpansionInstanceValue resolvedDataReductionWindowExpansionInstanceValue in windowInstance.WindowExpansionInstanceWindowValue)
				{
					List<Expression> list2;
					if (this.TryTranslateExpressionList(resolvedDataReductionWindowExpansionInstanceValue.Values, out list2))
					{
						builder.WithWindowValue(list2, resolvedDataReductionWindowExpansionInstanceValue.WindowStartKind);
					}
				}
			}
			if (!windowInstance.Children.IsNullOrEmpty<ResolvedDataReductionWindowExpansionInstance>())
			{
				foreach (ResolvedDataReductionWindowExpansionInstance resolvedDataReductionWindowExpansionInstance in windowInstance.Children)
				{
					LimitWindowExpansionInstanceBuilder<LimitWindowExpansionInstanceBuilder<TParent>> limitWindowExpansionInstanceBuilder = builder.WithWindowInstance();
					this.Visit<LimitWindowExpansionInstanceBuilder<TParent>>(resolvedDataReductionWindowExpansionInstance, limitWindowExpansionInstanceBuilder);
				}
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00011FFC File Offset: 0x000101FC
		private bool TryTranslateExpression(ResolvedQueryExpression queryExpression, out Expression dsqExpression)
		{
			GeneratedDsqExpression generatedDsqExpression;
			if (!ResolvedQueryExpressionValidator.Validate(queryExpression, this._errorContext, AllowedExpressionContent.ExpansionState, this._expressionContext) || !this._expressionGenerator.TryGenerate(queryExpression, out generatedDsqExpression))
			{
				dsqExpression = null;
				return false;
			}
			dsqExpression = generatedDsqExpression.Expression;
			return true;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00012048 File Offset: 0x00010248
		private bool TryTranslateExpressionList(IReadOnlyList<ResolvedQueryExpression> queryExpressions, out List<Expression> expressions)
		{
			expressions = null;
			if (!queryExpressions.IsEmpty<ResolvedQueryExpression>())
			{
				expressions = new List<Expression>(queryExpressions.Count);
				foreach (ResolvedQueryExpression resolvedQueryExpression in queryExpressions)
				{
					Expression expression;
					if (!this.TryTranslateExpression(resolvedQueryExpression, out expression))
					{
						return false;
					}
					expressions.Add(expression);
				}
				return true;
			}
			return true;
		}

		// Token: 0x0400029A RID: 666
		private readonly TopNPerLevelLimitBuilder<TParentBuilder> _topNPerLevelBuilder;

		// Token: 0x0400029B RID: 667
		private readonly DsqExpressionGenerator _expressionGenerator;

		// Token: 0x0400029C RID: 668
		private readonly DataShapeGenerationErrorContext _errorContext;

		// Token: 0x0400029D RID: 669
		private readonly ExpressionContext _expressionContext;
	}
}
