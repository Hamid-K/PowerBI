using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001838 RID: 6200
	internal class PandasExpressionHelper
	{
		// Token: 0x0600CB36 RID: 52022 RVA: 0x002B6374 File Offset: 0x002B4574
		public static FormulaExpression Apply(FormulaExpression subject, FormulaExpression argument, FormulaExpression body)
		{
			return PandasExpressionHelper.Apply(subject, argument.Yield<FormulaExpression>(), body);
		}

		// Token: 0x0600CB37 RID: 52023 RVA: 0x002B6383 File Offset: 0x002B4583
		public static FormulaExpression Apply(FormulaExpression subject, IEnumerable<FormulaExpression> arguments, FormulaExpression body)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func("apply", new FormulaExpression[] { PythonExpressionHelper.Lambda(arguments, body) }));
		}

		// Token: 0x0600CB38 RID: 52024 RVA: 0x002B63A5 File Offset: 0x002B45A5
		public static FormulaExpression Column(string dataFrameName, string columnName)
		{
			return PandasExpressionHelper.Column(PythonExpressionHelper.Variable(dataFrameName), PythonExpressionHelper.StringLiteral(columnName));
		}

		// Token: 0x0600CB39 RID: 52025 RVA: 0x002B63B8 File Offset: 0x002B45B8
		public static FormulaExpression Column(FormulaExpression dataFrameName, FormulaExpression columnName)
		{
			return new PandasColumn(dataFrameName, columnName);
		}

		// Token: 0x0600CB3A RID: 52026 RVA: 0x002B63C1 File Offset: 0x002B45C1
		public static FormulaExpression Columns(FormulaExpression dataFrameName, IEnumerable<string> columnNames)
		{
			Func<string, FormulaExpression> func;
			if ((func = PandasExpressionHelper.<>O.<0>__StringLiteral) == null)
			{
				func = (PandasExpressionHelper.<>O.<0>__StringLiteral = new Func<string, FormulaExpression>(PythonExpressionHelper.StringLiteral));
			}
			return new PandasColumn(dataFrameName, PythonExpressionHelper.Array(columnNames.Select(func)));
		}

		// Token: 0x0600CB3B RID: 52027 RVA: 0x002B63EF File Offset: 0x002B45EF
		public static FormulaExpression Columns(string dataFrameName, IEnumerable<string> columnNames)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(dataFrameName);
			Func<string, FormulaExpression> func;
			if ((func = PandasExpressionHelper.<>O.<0>__StringLiteral) == null)
			{
				func = (PandasExpressionHelper.<>O.<0>__StringLiteral = new Func<string, FormulaExpression>(PythonExpressionHelper.StringLiteral));
			}
			return new PandasColumn(formulaExpression, PythonExpressionHelper.Array(columnNames.Select(func)));
		}

		// Token: 0x0600CB3C RID: 52028 RVA: 0x002B6422 File Offset: 0x002B4622
		public static FormulaExpression Iloc(FormulaExpression subject, FormulaExpression rows, FormulaExpression columns)
		{
			return PythonExpressionHelper.Index<object>(PythonExpressionHelper.Dot(subject, "iloc"), PythonExpressionHelper.Tuple(new FormulaExpression[] { rows, columns }));
		}

		// Token: 0x0600CB3D RID: 52029 RVA: 0x002B6447 File Offset: 0x002B4647
		public static FormulaExpression Input(string dataFrameName, string columnName)
		{
			return PythonExpressionHelper.Dot(PythonExpressionHelper.Variable(dataFrameName), columnName);
		}

		// Token: 0x0600CB3E RID: 52030 RVA: 0x002B6455 File Offset: 0x002B4655
		public static FormulaExpression Insert(FormulaExpression subject, params FormulaExpression[] arguments)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func("insert", arguments));
		}

		// Token: 0x0600CB3F RID: 52031 RVA: 0x002B6468 File Offset: 0x002B4668
		public static FormulaExpression Merge(FormulaExpression subject, params FormulaExpression[] arguments)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func("merge", arguments));
		}

		// Token: 0x0600CB40 RID: 52032 RVA: 0x002B647B File Offset: 0x002B467B
		public static FormulaExpression Reindex(FormulaExpression subject, FormulaExpression columns)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func("reindex", new FormulaExpression[] { PythonExpressionHelper.Assign(PythonExpressionHelper.Variable("columns"), columns) }));
		}

		// Token: 0x0600CB41 RID: 52033 RVA: 0x002B64A6 File Offset: 0x002B46A6
		public static FormulaExpression Rename(FormulaExpression subject, string columnName)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func("rename", new FormulaExpression[] { PythonExpressionHelper.StringLiteral(columnName) }));
		}

		// Token: 0x0600CB42 RID: 52034 RVA: 0x002B64C7 File Offset: 0x002B46C7
		public static FormulaExpression Series(FormulaExpression target, params FormulaExpression[] arguments)
		{
			return PythonExpressionHelper.Dot("pd", PythonExpressionHelper.Func("Series", target.Yield<FormulaExpression>().Concat(arguments).ToArray<FormulaExpression>()));
		}

		// Token: 0x0600CB43 RID: 52035 RVA: 0x002B64EE File Offset: 0x002B46EE
		public static FormulaExpression Str(FormulaExpression subject)
		{
			return PythonExpressionHelper.Dot(subject, "str");
		}

		// Token: 0x0600CB44 RID: 52036 RVA: 0x002B64FB File Offset: 0x002B46FB
		public static FormulaExpression StrCat(FormulaExpression subject, params FormulaExpression[] arguments)
		{
			return PythonExpressionHelper.Dot(PythonExpressionHelper.Dot(subject, "str"), PythonExpressionHelper.Func("cat", arguments));
		}

		// Token: 0x0600CB45 RID: 52037 RVA: 0x002B6518 File Offset: 0x002B4718
		public static FormulaExpression StrSplit(FormulaExpression subject, params FormulaExpression[] arguments)
		{
			return PythonExpressionHelper.Dot(PandasExpressionHelper.Str(subject), PythonExpressionHelper.Func("split", arguments));
		}

		// Token: 0x02001839 RID: 6201
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04004FC1 RID: 20417
			public static Func<string, FormulaExpression> <0>__StringLiteral;
		}
	}
}
