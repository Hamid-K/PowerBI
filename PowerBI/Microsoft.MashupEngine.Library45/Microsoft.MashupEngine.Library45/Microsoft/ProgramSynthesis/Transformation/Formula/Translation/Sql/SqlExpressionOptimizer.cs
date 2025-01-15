using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x0200182B RID: 6187
	internal class SqlExpressionOptimizer
	{
		// Token: 0x0600CAE7 RID: 51943 RVA: 0x002B4FF0 File Offset: 0x002B31F0
		private SqlExpressionOptimizer(ISqlTranslationOptions options = null)
		{
			this._options = options ?? new SqlTranslationConstraint();
		}

		// Token: 0x0600CAE8 RID: 51944 RVA: 0x002B5008 File Offset: 0x002B3208
		public static FormulaExpression Optimize(FormulaExpression expression, ISqlTranslationOptions options = null)
		{
			return new SqlExpressionOptimizer(options).OptimizeInternal(expression);
		}

		// Token: 0x0600CAE9 RID: 51945 RVA: 0x002B5018 File Offset: 0x002B3218
		private FormulaExpression OptimizeInternal(FormulaExpression expression)
		{
			FormulaExpression formulaExpression = expression;
			if (this._options.Optimizations.HasFlag(SqlOptimizations.UpperCaseFunctionNames))
			{
				formulaExpression = SqlExpressionOptimizer.UpperCaseFunctionNames(formulaExpression);
			}
			return formulaExpression;
		}

		// Token: 0x0600CAEA RID: 51946 RVA: 0x002B504C File Offset: 0x002B324C
		private static FormulaExpression UpperCaseFunctionNames(FormulaExpression expression)
		{
			if (expression == null)
			{
				return null;
			}
			return expression.Transform(delegate(FormulaExpression node)
			{
				SqlFunc sqlFunc = node as SqlFunc;
				if (sqlFunc == null)
				{
					return node;
				}
				return SqlExpressionHelper.Func(sqlFunc.Name.ToUpper(), sqlFunc.Children);
			});
		}

		// Token: 0x04004FA5 RID: 20389
		private readonly ISqlTranslationOptions _options;
	}
}
