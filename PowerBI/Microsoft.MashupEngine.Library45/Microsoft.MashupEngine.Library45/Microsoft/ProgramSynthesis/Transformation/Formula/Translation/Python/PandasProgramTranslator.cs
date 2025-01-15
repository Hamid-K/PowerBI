using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Formula.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Visitors;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200183C RID: 6204
	internal class PandasProgramTranslator : ProgramTranslatorBase
	{
		// Token: 0x0600CB53 RID: 52051 RVA: 0x002B6B91 File Offset: 0x002B4D91
		private PandasProgramTranslator(Program program, IPandasTranslationOptions options, bool enableMatchUnicode, IEnumerable<Example> examples, IEnumerable<IRow> inputs, ILogger logger)
			: base(program, examples, inputs, logger)
		{
			this._options = options ?? new PandasTranslationConstraint();
			this._enableMatchUnicode = enableMatchUnicode;
		}

		// Token: 0x0600CB54 RID: 52052 RVA: 0x002B6BB7 File Offset: 0x002B4DB7
		public static bool SupportsSeriesFunctions(Program program)
		{
			return !program.Any(delegate(ProgramNodeDetail node)
			{
				DateTimePart dateTimePart;
				return node.Is<If>() || node.Is<RoundNumber>() || node.Is<Find>() || node.Is<SliceBetween>() || node.Is<Match>() || node.Is<MatchFull>() || node.Is<MatchEnd>() || node.Is<ParseDateTime>() || node.Is<FormatDateTime>() || (node.Is(out dateTimePart) && dateTimePart.dateTimePartKind.Value != DateTimePartKind.Second && dateTimePart.dateTimePartKind.Value != DateTimePartKind.Minute && dateTimePart.dateTimePartKind.Value != DateTimePartKind.Hour && dateTimePart.dateTimePartKind.Value != DateTimePartKind.WeekDay && dateTimePart.dateTimePartKind.Value != DateTimePartKind.MonthDay && dateTimePart.dateTimePartKind.Value != DateTimePartKind.MonthDays && dateTimePart.dateTimePartKind.Value != DateTimePartKind.Month && dateTimePart.dateTimePartKind.Value != DateTimePartKind.Year && dateTimePart.dateTimePartKind.Value != DateTimePartKind.YearDay && dateTimePart.dateTimePartKind.Value != DateTimePartKind.Quarter) || node.Is<RoundDateTime>() || node.Is<ParseNumber>() || node.Is<FormatNumber>() || node.IsUpperCase() || node.IsLowerCase() || node.IsProperCase() || node.Is<Length>() || node.Is<FromDateTimePart>() || node.Is<Sum>() || node.Is<Product>() || node.Is<Average>() || node.Is<ToDecimal>() || node.Is<FromNumber>();
			});
		}

		// Token: 0x0600CB55 RID: 52053 RVA: 0x002B6BE1 File Offset: 0x002B4DE1
		public static PythonProgram Translate(Program program, IPandasTranslationOptions options, bool enableMatchUnicode, IEnumerable<Example> examples, IEnumerable<IRow> inputs, ILogger logger = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new PandasProgramTranslator(program, options, enableMatchUnicode, examples, inputs, logger).Translate(cancellationToken) as PythonProgram;
		}

		// Token: 0x0600CB56 RID: 52054 RVA: 0x002B6BFC File Offset: 0x002B4DFC
		protected override FormulaExpression Translate(CancellationToken cancellationToken = default(CancellationToken))
		{
			string definitionName = this._options.TransformationFunctionName ?? PythonProgramTranslator.ResolveIdentifierName(this._options.DerivedColumnName, "transform_column");
			IPandasTranslationOptions options = this._options;
			string definitionName2 = definitionName;
			string text = null;
			int? num = null;
			string text2 = null;
			bool? flag = null;
			uint? num2 = null;
			uint? num3 = null;
			ILocalizedStrings localizedStrings = null;
			int? num4 = null;
			PandasOptimizations? pandasOptimizations = null;
			IPythonTranslationOptions pythonTranslationOptions = options.With(definitionName2, text, num, text2, flag, num2, num3, localizedStrings, num4, pandasOptimizations, null, null, null);
			PythonProgram pythonProgram = PythonProgramTranslator.Translate(base.Program, base.Examples, base.Inputs, pythonTranslationOptions, this._enableMatchUnicode, base.Logger, cancellationToken);
			if (pythonProgram == null || !pythonProgram.Definitions.Any<PythonDefinition>())
			{
				return null;
			}
			PythonDefinition pythonDefinition = pythonProgram.Definitions.SingleOrDefault((PythonDefinition def) => def.Name == definitionName);
			if (pythonDefinition == null)
			{
				return null;
			}
			bool flag2 = !this._options.PandasOptimizations.HasFlag(PandasOptimizations.UseSeriesFunctions) || !PandasProgramTranslator.SupportsSeriesFunctions(base.Program);
			IEnumerable<PythonImport> enumerable;
			if (!this._options.ImportPandas)
			{
				IEnumerable<PythonImport> imports = pythonProgram.Imports;
				enumerable = imports;
			}
			else
			{
				enumerable = pythonProgram.Imports.Concat(PythonExpressionHelper.Import("pandas", "pd").Yield<PythonImport>());
			}
			IEnumerable<PythonImport> enumerable2 = enumerable;
			PandasOptimizations pandasOptimizations2 = this._options.PandasOptimizations;
			Example example = base.Examples.First<Example>();
			object obj;
			if (example == null)
			{
				obj = null;
			}
			else
			{
				IEnumerable<string> columnNames = example.Input.ColumnNames;
				if (columnNames == null)
				{
					obj = null;
				}
				else
				{
					obj = columnNames.OrderBy((string col) => col);
				}
			}
			object obj2 = obj;
			if (obj2 == null)
			{
				throw new Exception("ColumnNames not found");
			}
			IEnumerable<string> enumerable3 = obj2;
			FormulaExpression formulaExpression2;
			if (flag2)
			{
				pandasOptimizations2 &= ~PandasOptimizations.UseSeriesFunctions;
				FormulaExpression[] array = enumerable3.Select((string p) => PythonExpressionHelper.Index<string>(PythonExpressionHelper.Variable<object>("row"), PythonExpressionHelper.StringLiteral(p))).ToArray<FormulaExpression>();
				FormulaExpression formulaExpression = PythonExpressionHelper.Func(definitionName, array);
				formulaExpression2 = PythonExpressionHelper.Dot(PythonExpressionHelper.Variable<object>(this._options.DataFrameName), PythonExpressionHelper.Func("apply", new FormulaExpression[]
				{
					PythonExpressionHelper.Lambda(PythonExpressionHelper.Variable<object>("row").Yield<FormulaExpression>(), formulaExpression),
					PythonExpressionHelper.AssignArg(PythonExpressionHelper.Variable<int>("axis"), PythonExpressionHelper.NumberLiteral(1))
				}));
			}
			else
			{
				FormulaExpression[] array2 = enumerable3.Select((string p) => PythonExpressionHelper.Index<string>(PythonExpressionHelper.Variable<object>(this._options.DataFrameName), PythonExpressionHelper.StringLiteral(p))).ToArray<FormulaExpression>();
				formulaExpression2 = PythonExpressionHelper.Func(definitionName, array2);
			}
			IPandasTranslationOptions options2 = this._options;
			string text3 = null;
			string text4 = null;
			pandasOptimizations = new PandasOptimizations?(pandasOptimizations2);
			IPandasTranslationOptions pandasTranslationOptions = options2.With(text3, text4, null, null, null, null, null, null, null, pandasOptimizations, null, null, null);
			pythonDefinition = PandasExpressionOptimizer.Optimize(pythonDefinition, pandasTranslationOptions) as PythonDefinition;
			if (pythonDefinition == null)
			{
				return null;
			}
			int? derivedColumnIndex = this._options.DerivedColumnIndex;
			FormulaExpression formulaExpression3;
			if (derivedColumnIndex != null && derivedColumnIndex.GetValueOrDefault() >= 0)
			{
				formulaExpression3 = PandasExpressionHelper.Insert(PythonExpressionHelper.Variable<object>(this._options.DataFrameName), new FormulaExpression[]
				{
					PythonExpressionHelper.NumberLiteral(this._options.DerivedColumnIndex.Value),
					PythonExpressionHelper.StringLiteral(this._options.DerivedColumnName),
					formulaExpression2
				});
			}
			else
			{
				formulaExpression3 = PythonExpressionHelper.Assign(PythonExpressionHelper.Index<object>(PythonExpressionHelper.Variable<object>(this._options.DataFrameName), PythonExpressionHelper.StringLiteral(this._options.DerivedColumnName)), formulaExpression2);
			}
			List<PythonDefinition> list = pythonProgram.Definitions.Skip(1).ToList<PythonDefinition>();
			list.Insert(0, pythonDefinition);
			return PandasExpressionOptimizer.Optimize(PythonExpressionHelper.Program(enumerable2, list, formulaExpression3.Yield<FormulaExpression>(), null), pandasTranslationOptions);
		}

		// Token: 0x04004FC6 RID: 20422
		private readonly bool _enableMatchUnicode;

		// Token: 0x04004FC7 RID: 20423
		private readonly IPandasTranslationOptions _options;
	}
}
