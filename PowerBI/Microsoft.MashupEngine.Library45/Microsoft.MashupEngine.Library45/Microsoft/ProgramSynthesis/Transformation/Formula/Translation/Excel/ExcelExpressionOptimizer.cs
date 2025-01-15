using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x02001930 RID: 6448
	internal class ExcelExpressionOptimizer
	{
		// Token: 0x0600D2C6 RID: 53958 RVA: 0x002CDD38 File Offset: 0x002CBF38
		private ExcelExpressionOptimizer(IExcelTranslationOptions options = null)
		{
			this._options = options ?? new ExcelTranslationConstraint();
		}

		// Token: 0x0600D2C7 RID: 53959 RVA: 0x002CDD50 File Offset: 0x002CBF50
		public static FormulaExpression Optimize(FormulaExpression expression, IExcelTranslationOptions options = null)
		{
			return new ExcelExpressionOptimizer(options).OptimizeInternal(expression);
		}

		// Token: 0x0600D2C8 RID: 53960 RVA: 0x002CDD60 File Offset: 0x002CBF60
		private FormulaExpression OptimizeInternal(FormulaExpression expression)
		{
			FormulaExpression formulaExpression = expression;
			if (this._options.Optimizations.HasFlag(ExcelOptimizations.UseLet))
			{
				formulaExpression = ExcelExpressionOptimizer.UseLet(formulaExpression);
			}
			if (this._options.Optimizations.HasFlag(ExcelOptimizations.UpperCaseFunctionNames))
			{
				formulaExpression = ExcelExpressionOptimizer.UpperCaseFunctionNames(formulaExpression);
			}
			if (this._options.Optimizations.HasFlag(ExcelOptimizations.FunctionLocaleArgumentSeparator))
			{
				formulaExpression = this.FunctionLocaleArgumentSeparator(formulaExpression);
			}
			if (this._options.Optimizations.HasFlag(ExcelOptimizations.OmitDefaultCulture))
			{
				formulaExpression = this.OmitDefaultCulture(formulaExpression);
			}
			if (this._options.Optimizations.HasFlag(ExcelOptimizations.UpperCaseFunctionNames))
			{
				formulaExpression = ExcelExpressionOptimizer.UpperCaseFunctionNames(formulaExpression);
			}
			return formulaExpression;
		}

		// Token: 0x0600D2C9 RID: 53961 RVA: 0x002CDE28 File Offset: 0x002CC028
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
				ExcelFunc excelFunc = node as ExcelFunc;
				if (excelFunc == null)
				{
					return node;
				}
				return ExcelExpressionHelper.Func(excelFunc.Name, ";", excelFunc.Children);
			});
		}

		// Token: 0x0600D2CA RID: 53962 RVA: 0x002CDE97 File Offset: 0x002CC097
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
				ExcelFormat excelFormat = node as ExcelFormat;
				if (excelFormat == null)
				{
					return node;
				}
				if (!(excelFormat.Locale == this._options.UserInterfaceCulture.Name))
				{
					return node;
				}
				return ExcelExpressionHelper.Format(excelFormat.Format);
			});
		}

		// Token: 0x0600D2CB RID: 53963 RVA: 0x002CDEC5 File Offset: 0x002CC0C5
		private static FormulaExpression UpperCaseFunctionNames(FormulaExpression expression)
		{
			if (expression == null)
			{
				return null;
			}
			return expression.Transform(delegate(FormulaExpression node)
			{
				ExcelFunc excelFunc = node as ExcelFunc;
				if (excelFunc == null)
				{
					return node;
				}
				return ExcelExpressionHelper.Func(excelFunc.Name.ToUpper(), excelFunc.ArgumentSeparator, excelFunc.Children);
			});
		}

		// Token: 0x0600D2CC RID: 53964 RVA: 0x002CDEF8 File Offset: 0x002CC0F8
		private static FormulaExpression UseLet(FormulaExpression expression)
		{
			if (expression == null)
			{
				return null;
			}
			Dictionary<string, int> varNameCount = new Dictionary<string, int>();
			List<FormulaExpressionDetail> list = (from source in expression.NodeDetails.Where(delegate(FormulaExpressionDetail n)
				{
					FormulaExpression node = n.Node;
					return node is ExcelFunc || node is ExcelPlus;
				})
				group source by source.Node into nodeGroup
				where nodeGroup.Count<FormulaExpressionDetail>() > 1
				let first = nodeGroup.First<FormulaExpressionDetail>()
				let maxDepth = nodeGroup.Max((FormulaExpressionDetail i) => i.Depth)
				let maxOrder = nodeGroup.Max((FormulaExpressionDetail i) => i.Order)
				orderby maxDepth descending, maxOrder
				select first).ToList<FormulaExpressionDetail>();
			List<FormulaExpression> sourceNodes = list.Select((FormulaExpressionDetail e) => e.Node).Distinct<FormulaExpression>().ToList<FormulaExpression>();
			var list2 = (from source in list.Where((FormulaExpressionDetail source) => source.Parent != null && !sourceNodes.Contains(source.Parent.Node)).ToList<FormulaExpressionDetail>()
				let variable = base.<UseLet>g__ToVariable|0(source.Node)
				where variable != null
				select new
				{
					Node = source.Node,
					Variable = variable
				}).ToList();
			if (!list2.Any())
			{
				return expression;
			}
			Dictionary<FormulaExpression, FormulaExpression> dictionary = list2.ToDictionary(p => p.Node, p => p.Variable);
			FormulaExpression formulaExpression = expression.Substitute(dictionary);
			if (formulaExpression == null)
			{
				return expression;
			}
			List<FormulaExpression> list3 = new List<FormulaExpression>();
			using (var enumerator = list2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					<>f__AnonymousType30<FormulaExpression, FormulaExpression> item = enumerator.Current;
					FormulaExpression formulaExpression2 = item.Node.Substitute(dictionary.Where((KeyValuePair<FormulaExpression, FormulaExpression> pair) => pair.Key != item.Node).ToDictionary((KeyValuePair<FormulaExpression, FormulaExpression> p) => p.Key, (KeyValuePair<FormulaExpression, FormulaExpression> p) => p.Value));
					list3.Add(item.Variable);
					list3.Add(formulaExpression2);
				}
			}
			return ExcelExpressionHelper.Let(list3, formulaExpression);
		}

		// Token: 0x04005133 RID: 20787
		private readonly IExcelTranslationOptions _options;
	}
}
