using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x02001910 RID: 6416
	internal class PowerAutomateExpressionOptimizer
	{
		// Token: 0x0600D184 RID: 53636 RVA: 0x002CA23E File Offset: 0x002C843E
		private PowerAutomateExpressionOptimizer(IPowerAutomateTranslationOptions options = null)
		{
			this._options = options ?? new PowerAutomateTranslationConstraint();
		}

		// Token: 0x0600D185 RID: 53637 RVA: 0x002CA256 File Offset: 0x002C8456
		public static FormulaExpression Optimize(FormulaExpression expression, IPowerAutomateTranslationOptions options = null)
		{
			return new PowerAutomateExpressionOptimizer(options).OptimizeInternal(expression);
		}

		// Token: 0x0600D186 RID: 53638 RVA: 0x002CA264 File Offset: 0x002C8464
		private FormulaExpression OptimizeInternal(FormulaExpression expression)
		{
			FormulaExpression formulaExpression = expression;
			if (this._options.Optimizations.HasFlag(PowerAutomateOptimizations.FlattenConcat))
			{
				formulaExpression = PowerAutomateExpressionOptimizer.FlattenConcat(formulaExpression);
			}
			formulaExpression = PowerAutomateExpressionOptimizer.UseSubstringForConstantSlice(formulaExpression);
			if (this._options.Optimizations.HasFlag(PowerAutomateOptimizations.CamelCaseFunctionNames))
			{
				formulaExpression = PowerAutomateExpressionOptimizer.CamelCaseFunctionNames(formulaExpression);
			}
			return formulaExpression;
		}

		// Token: 0x0600D187 RID: 53639 RVA: 0x002CA2C3 File Offset: 0x002C84C3
		private static FormulaExpression CamelCaseFunctionNames(FormulaExpression expression)
		{
			if (expression == null)
			{
				return null;
			}
			return expression.Transform(delegate(FormulaExpression node)
			{
				PowerAutomateFunc powerAutomateFunc = node as PowerAutomateFunc;
				if (powerAutomateFunc == null)
				{
					return node;
				}
				return new PowerAutomateFunc(powerAutomateFunc.Name.Substring(0, 1).ToLowerInvariant() + powerAutomateFunc.Name.Substring(1), powerAutomateFunc.Children);
			});
		}

		// Token: 0x0600D188 RID: 53640 RVA: 0x002CA2F5 File Offset: 0x002C84F5
		private static FormulaExpression FlattenConcat(FormulaExpression expression)
		{
			if (expression == null)
			{
				return null;
			}
			return expression.Transform(delegate(FormulaExpression node)
			{
				bool flag = !(node is PowerAutomateConcat) || ((PowerAutomateFunc)node).Name == "concat";
				if (flag)
				{
					return node;
				}
				List<FormulaExpression> list = node.Children.ToList<FormulaExpression>();
				List<int> list2 = list.FindAllIndexes((FormulaExpression c) => c is PowerAutomateConcat, 0).Reverse<int>().ToList<int>();
				if (!list2.Any<int>())
				{
					return node;
				}
				foreach (int num in list2)
				{
					FormulaExpression formulaExpression = list.ElementAt(num);
					list.RemoveAt(num);
					int num2 = num;
					foreach (FormulaExpression formulaExpression2 in formulaExpression.Children)
					{
						list.Insert(num2++, formulaExpression2);
					}
				}
				return PowerAutomateExpressionHelper.Concat(list);
			});
		}

		// Token: 0x0600D189 RID: 53641 RVA: 0x002CA327 File Offset: 0x002C8527
		private static FormulaExpression UseSubstringForConstantSlice(FormulaExpression expression)
		{
			if (expression == null)
			{
				return null;
			}
			return expression.Transform(delegate(FormulaExpression node)
			{
				PowerAutomateSlice powerAutomateSlice = node as PowerAutomateSlice;
				if (powerAutomateSlice == null)
				{
					return node;
				}
				PowerAutomateAdd powerAutomateAdd = powerAutomateSlice.StartIndex as PowerAutomateAdd;
				PowerAutomateNumberLiteral powerAutomateNumberLiteral;
				PowerAutomateNumberLiteral powerAutomateNumberLiteral2;
				bool flag;
				if (powerAutomateAdd != null)
				{
					PowerAutomateAdd powerAutomateAdd2 = powerAutomateSlice.EndIndex as PowerAutomateAdd;
					if (powerAutomateAdd2 != null && powerAutomateAdd.Left == powerAutomateAdd2.Left)
					{
						powerAutomateNumberLiteral = powerAutomateAdd.Right as PowerAutomateNumberLiteral;
						if (powerAutomateNumberLiteral != null)
						{
							powerAutomateNumberLiteral2 = powerAutomateAdd2.Right as PowerAutomateNumberLiteral;
							if (powerAutomateNumberLiteral2 != null)
							{
								flag = powerAutomateNumberLiteral2.Value > powerAutomateNumberLiteral.Value;
								goto IL_006F;
							}
						}
					}
				}
				flag = false;
				IL_006F:
				bool flag2 = flag;
				FormulaFunc formulaFunc;
				if (flag2)
				{
					formulaFunc = powerAutomateAdd.Left as FormulaFunc;
					bool flag3;
					if (formulaFunc != null)
					{
						string name = formulaFunc.Name;
						if (name == "IndexOf" || name == "LastIndexOf" || name == "NthIndexOf")
						{
							flag3 = true;
							goto IL_00C1;
						}
					}
					flag3 = false;
					IL_00C1:
					flag2 = flag3;
				}
				if (flag2)
				{
					FormulaStringLiteral formulaStringLiteral = formulaFunc.Children[1] as FormulaStringLiteral;
					if (formulaStringLiteral != null)
					{
						int length = formulaStringLiteral.Value.Length;
						double value = powerAutomateNumberLiteral.Value;
						double value2 = powerAutomateNumberLiteral2.Value;
						return PowerAutomateExpressionHelper.Substring(powerAutomateSlice.Subject, PowerAutomateExpressionHelper.Add(formulaFunc, (double)length + value - (double)length), value2 - value);
					}
				}
				return node;
			});
		}

		// Token: 0x0400511D RID: 20765
		private readonly IPowerAutomateTranslationOptions _options;
	}
}
