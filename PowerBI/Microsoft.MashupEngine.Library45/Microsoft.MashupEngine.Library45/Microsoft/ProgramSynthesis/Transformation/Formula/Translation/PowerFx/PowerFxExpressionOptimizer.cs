using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018F4 RID: 6388
	internal class PowerFxExpressionOptimizer
	{
		// Token: 0x0600D05D RID: 53341 RVA: 0x002C596C File Offset: 0x002C3B6C
		private PowerFxExpressionOptimizer(IPowerFxTranslationOptions options = null)
		{
			this._options = options ?? new PowerFxTranslationConstraint();
		}

		// Token: 0x0600D05E RID: 53342 RVA: 0x002C5984 File Offset: 0x002C3B84
		public static FormulaExpression Optimize(FormulaExpression expression, IPowerFxTranslationOptions options = null)
		{
			return new PowerFxExpressionOptimizer(options).OptimizeInternal(expression);
		}

		// Token: 0x0600D05F RID: 53343 RVA: 0x002C5994 File Offset: 0x002C3B94
		private FormulaExpression OptimizeInternal(FormulaExpression expression)
		{
			FormulaExpression formulaExpression = expression;
			if (this._options.Optimizations.HasFlag(PowerFxOptimizations.FunctionLocaleArgumentSeparator))
			{
				formulaExpression = this.FunctionLocaleArgumentSeparator(formulaExpression);
			}
			if (this._options.Optimizations.HasFlag(PowerFxOptimizations.OmitDefaultCulture))
			{
				formulaExpression = this.OmitDefaultCulture(formulaExpression);
			}
			if (this._options.Optimizations.HasFlag(PowerFxOptimizations.UseWith))
			{
				formulaExpression = PowerFxExpressionOptimizer.UseWith(formulaExpression);
			}
			return PowerFxExpressionOptimizer.AvoidReturnVariable(formulaExpression);
		}

		// Token: 0x0600D060 RID: 53344 RVA: 0x002C5A19 File Offset: 0x002C3C19
		private static FormulaExpression AvoidReturnVariable(FormulaExpression expression)
		{
			if (expression == null)
			{
				return null;
			}
			return expression.Transform(delegate(FormulaExpression node)
			{
				PowerFxWith powerFxWith = node as PowerFxWith;
				if (powerFxWith == null)
				{
					return node;
				}
				PowerFxVariable powerFxVariable = powerFxWith.Body as PowerFxVariable;
				FormulaExpression formulaExpression;
				if (powerFxVariable != null && powerFxWith.Record.TryGetValue(powerFxVariable.Name, out formulaExpression))
				{
					return formulaExpression;
				}
				return node;
			});
		}

		// Token: 0x0600D061 RID: 53345 RVA: 0x002C5A4C File Offset: 0x002C3C4C
		private FormulaExpression FunctionLocaleArgumentSeparator(FormulaExpression expression)
		{
			if (expression == null)
			{
				return null;
			}
			if (this._options.UserInterfaceCulture == null)
			{
				return expression;
			}
			if (this._options.UserInterfaceCulture.NumberFormat.NumberDecimalSeparator != ",")
			{
				return expression;
			}
			return expression.Transform(delegate(FormulaExpression node)
			{
				PowerFxFunc powerFxFunc = node as PowerFxFunc;
				if (powerFxFunc == null)
				{
					return node;
				}
				return PowerFxExpressionHelper.Func(powerFxFunc.Name, ";", powerFxFunc.Children);
			});
		}

		// Token: 0x0600D062 RID: 53346 RVA: 0x002C5ABB File Offset: 0x002C3CBB
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
				PowerFxFunc powerFxFunc = node as PowerFxFunc;
				if (powerFxFunc == null)
				{
					return node;
				}
				IEnumerable<FormulaExpression> enumerable = powerFxFunc.Arguments.Where(delegate(FormulaExpression arg)
				{
					PowerFxLocale powerFxLocale = arg as PowerFxLocale;
					return powerFxLocale == null || powerFxLocale.Value != this._options.UserInterfaceCulture.Name;
				});
				return PowerFxExpressionHelper.Func(powerFxFunc.Name, enumerable);
			});
		}

		// Token: 0x0600D063 RID: 53347 RVA: 0x002C5AEC File Offset: 0x002C3CEC
		private static FormulaExpression UseWith(FormulaExpression expression)
		{
			if (expression == null)
			{
				return null;
			}
			Dictionary<string, int> varNameCount = new Dictionary<string, int>();
			List<FormulaExpressionDetail> list = (from source in expression.NodeDetails.Where(delegate(FormulaExpressionDetail n)
				{
					if (!(n.Node is PowerFxPlus))
					{
						PowerFxFunc powerFxFunc = n.Node as PowerFxFunc;
						return powerFxFunc != null && PowerFxExpressionOptimizer._useWithAllowFunctions.Contains(powerFxFunc.Name);
					}
					return true;
				})
				group source by source.Node into nodeGroup
				where nodeGroup.Count<FormulaExpressionDetail>() > 1
				let first = nodeGroup.First<FormulaExpressionDetail>()
				let maxDepth = nodeGroup.Max((FormulaExpressionDetail i) => i.Depth)
				let maxOrder = nodeGroup.Max((FormulaExpressionDetail i) => i.Order)
				orderby maxDepth descending, maxOrder
				select first).ToList<FormulaExpressionDetail>();
			List<FormulaExpression> sourceNodes = list.Select((FormulaExpressionDetail e) => e.Node).Distinct<FormulaExpression>().ToList<FormulaExpression>();
			Dictionary<FormulaExpression, FormulaExpression> dictionary = (from source in list.Where((FormulaExpressionDetail source) => source.Parent != null && !sourceNodes.Contains(source.Parent.Node)).ToList<FormulaExpressionDetail>()
				let variable = base.<UseWith>g__ToVariable|0(source.Node)
				where variable != null
				select new
				{
					Node = source.Node,
					Variable = variable
				}).ToDictionary(p => p.Node, p => p.Variable);
			if (!dictionary.Any<KeyValuePair<FormulaExpression, FormulaExpression>>())
			{
				return expression;
			}
			FormulaExpression formulaExpression = expression.Substitute(dictionary);
			if (formulaExpression == null)
			{
				return expression;
			}
			return PowerFxExpressionHelper.With(dictionary.ToDictionary((KeyValuePair<FormulaExpression, FormulaExpression> item) => item.Value.ToString(), (KeyValuePair<FormulaExpression, FormulaExpression> item) => item.Key), formulaExpression);
		}

		// Token: 0x040050E8 RID: 20712
		private readonly IPowerFxTranslationOptions _options;

		// Token: 0x040050E9 RID: 20713
		private static readonly HashSet<string> _useWithAllowFunctions = new string[] { "Split", "Find", "Match", "MatchAll", "Substitute" }.ConvertToHashSet<string>();
	}
}
