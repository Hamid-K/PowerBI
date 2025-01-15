using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018C7 RID: 6343
	internal class PowerQueryExpressionOptimizer
	{
		// Token: 0x0600CF1D RID: 53021 RVA: 0x002C194B File Offset: 0x002BFB4B
		private PowerQueryExpressionOptimizer(IPowerQueryTranslationOptions options = null)
		{
			this._options = options ?? new PowerQueryTranslationConstraint();
		}

		// Token: 0x0600CF1E RID: 53022 RVA: 0x002C1963 File Offset: 0x002BFB63
		public static FormulaExpression Optimize(FormulaExpression expression, IPowerQueryTranslationOptions options = null)
		{
			return new PowerQueryExpressionOptimizer(options).OptimizeInternal(expression);
		}

		// Token: 0x0600CF1F RID: 53023 RVA: 0x002C1974 File Offset: 0x002BFB74
		private FormulaExpression OptimizeInternal(FormulaExpression expression)
		{
			FormulaExpression formulaExpression = expression;
			if (this._options.OutputType == OutputType.LambdaExpression)
			{
				formulaExpression = new PowerQueryLambdaFunction(null, formulaExpression);
			}
			if (this._options.Optimizations.HasFlag(PowerQueryOptimizations.OmitDefaultCulture))
			{
				formulaExpression = this.OmitDefaultCulture(formulaExpression);
			}
			return formulaExpression;
		}

		// Token: 0x0600CF20 RID: 53024 RVA: 0x002C19BE File Offset: 0x002BFBBE
		private FormulaExpression OmitDefaultCulture(FormulaExpression expression)
		{
			if (expression == null)
			{
				return null;
			}
			if (this._options.UserInterfaceCulture == null)
			{
				return expression;
			}
			return expression.Transform(delegate(FormulaExpression node)
			{
				PowerQueryFunc powerQueryFunc = node as PowerQueryFunc;
				if (powerQueryFunc == null)
				{
					PowerQueryRecord powerQueryRecord = node as PowerQueryRecord;
					if (powerQueryRecord != null)
					{
						PowerQueryRecord powerQueryRecord2 = powerQueryRecord;
						FormulaExpression formulaExpression;
						if (powerQueryRecord2.Values.TryGetValue("Culture", out formulaExpression))
						{
							PowerQueryLocale powerQueryLocale = formulaExpression as PowerQueryLocale;
							if (powerQueryLocale != null && powerQueryLocale.Value == this._options.UserInterfaceCulture.Name)
							{
								return new PowerQueryRecord(powerQueryRecord2.Values.Where((KeyValuePair<string, FormulaExpression> kv) => kv.Key != "Culture").ToDictionary<string, FormulaExpression>());
							}
						}
					}
				}
				else
				{
					PowerQueryFunc powerQueryFunc2 = powerQueryFunc;
					PowerQueryLocale powerQueryLocale2 = powerQueryFunc2.Arguments.LastOrDefault<FormulaExpression>() as PowerQueryLocale;
					if (powerQueryLocale2 != null && powerQueryLocale2.Value == this._options.UserInterfaceCulture.Name)
					{
						return PowerQueryExpressionHelper.Func(powerQueryFunc2.Name, powerQueryFunc2.Arguments.SkipLast(1));
					}
				}
				return node;
			});
		}

		// Token: 0x040050C1 RID: 20673
		private readonly IPowerQueryTranslationOptions _options;
	}
}
