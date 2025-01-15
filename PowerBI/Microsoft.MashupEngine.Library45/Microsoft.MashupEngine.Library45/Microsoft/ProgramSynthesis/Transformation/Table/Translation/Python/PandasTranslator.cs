using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.Detection.RichDataTypes;
using Microsoft.ProgramSynthesis.Detection.Translation.Python;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Split.Text.Build;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Split.Translation;
using Microsoft.ProgramSynthesis.Split.Translation.Python;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Util;
using Microsoft.ProgramSynthesis.Transformation.Table.Translation.Pandas;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.Python
{
	// Token: 0x02001B3A RID: 6970
	internal class PandasTranslator
	{
		// Token: 0x0600E4DE RID: 58590 RVA: 0x0030877C File Offset: 0x0030697C
		internal static Microsoft.ProgramSynthesis.Transformation.Table.Translation.Pandas.PandasTranslation Translate(Program program, ITable<object> inputTable, Microsoft.ProgramSynthesis.Transformation.Table.Constraints.PandasTranslationConstraint constraint)
		{
			FormulaExpression formulaExpression = null;
			IEnumerable<string> enumerable = null;
			TTableProgram ttableProgram;
			if (!Language.Build.Node.IsRule.TTableProgram(program.ProgramNode, out ttableProgram))
			{
				return null;
			}
			LabelEncode labelEncode;
			AddSplitColumns addSplitColumns;
			DropColumn dropColumn;
			DropRows dropRows;
			CastColumn castColumn;
			FillMissingValues fillMissingValues;
			OneHotEncode oneHotEncode;
			if (Language.Build.Node.IsRule.LabelEncode(ttableProgram.table.Node, out labelEncode))
			{
				formulaExpression = Microsoft.ProgramSynthesis.Transformation.Table.Translation.Python.PandasTranslator.TranslateLabelEncode(labelEncode, inputTable, constraint, out enumerable);
			}
			else if (Language.Build.Node.IsRule.AddSplitColumns(ttableProgram.table.Node, out addSplitColumns))
			{
				formulaExpression = Microsoft.ProgramSynthesis.Transformation.Table.Translation.Python.PandasTranslator.TranslateAddSplitColumns(addSplitColumns, inputTable, constraint, out enumerable);
			}
			else if (Language.Build.Node.IsRule.DropColumn(ttableProgram.table.Node, out dropColumn))
			{
				formulaExpression = Microsoft.ProgramSynthesis.Transformation.Table.Translation.Python.PandasTranslator.TranslateDropColumn(dropColumn, constraint);
			}
			else if (Language.Build.Node.IsRule.DropRows(ttableProgram.table.Node, out dropRows))
			{
				formulaExpression = Microsoft.ProgramSynthesis.Transformation.Table.Translation.Python.PandasTranslator.TranslateDropRows(dropRows, inputTable, constraint);
			}
			else if (Language.Build.Node.IsRule.CastColumn(ttableProgram.table.Node, out castColumn))
			{
				formulaExpression = Microsoft.ProgramSynthesis.Transformation.Table.Translation.Python.PandasTranslator.TranslateCastColumn(castColumn, constraint);
			}
			else if (Language.Build.Node.IsRule.FillMissingValues(ttableProgram.table.Node, out fillMissingValues))
			{
				formulaExpression = Microsoft.ProgramSynthesis.Transformation.Table.Translation.Python.PandasTranslator.TranslateFillMissingValues(fillMissingValues, constraint);
			}
			else if (Language.Build.Node.IsRule.OneHotEncode(ttableProgram.table.Node, out oneHotEncode))
			{
				formulaExpression = Microsoft.ProgramSynthesis.Transformation.Table.Translation.Python.PandasTranslator.TranslateOneHotEncode(oneHotEncode, constraint);
			}
			if (formulaExpression == null)
			{
				return null;
			}
			return new Microsoft.ProgramSynthesis.Transformation.Table.Translation.Pandas.PandasTranslation(program, formulaExpression, constraint, new Metadata(enumerable));
		}

		// Token: 0x0600E4DF RID: 58591 RVA: 0x00308938 File Offset: 0x00306B38
		private static FormulaExpression TranslateLabelEncode(LabelEncode labelEncode, ITable<object> inputTable, Microsoft.ProgramSynthesis.Transformation.Table.Constraints.PandasTranslationConstraint constraint, out IEnumerable<string> outputColumnNames)
		{
			string dataFrameName = constraint.DataFrameName;
			string text = labelEncode.sourceColumnName.Value + "_label_encoded";
			outputColumnNames = new string[] { text };
			FormulaExpression formulaExpression = PythonExpressionHelper.Dot(PythonExpressionHelper.Dot(PythonExpressionHelper.DotFunc<object>(PandasExpressionHelper.Column(PythonExpressionHelper.Variable(dataFrameName), PythonExpressionHelper.StringLiteral(labelEncode.sourceColumnName.Value)), "astype", new FormulaExpression[] { PythonExpressionHelper.StringLiteral("category") }), "cat"), "codes");
			if (formulaExpression == null)
			{
				return null;
			}
			return PythonExpressionHelper.Assign(PandasExpressionHelper.Column(PythonExpressionHelper.Variable(dataFrameName), PythonExpressionHelper.StringLiteral(text)), formulaExpression);
		}

		// Token: 0x0600E4E0 RID: 58592 RVA: 0x003089E0 File Offset: 0x00306BE0
		private static FormulaExpression TranslateAddSplitColumns(AddSplitColumns AddSplitColumns, ITable<object> inputs, Microsoft.ProgramSynthesis.Transformation.Table.Constraints.PandasTranslationConstraint constraint, out IEnumerable<string> outputColumnNames)
		{
			outputColumnNames = null;
			SplitColumn splitColumn;
			if (Language.Build.Node.IsRule.SplitColumn(AddSplitColumns.newColumns.Node, out splitColumn))
			{
				return Microsoft.ProgramSynthesis.Transformation.Table.Translation.Python.PandasTranslator.TranslateSplitColumn(splitColumn, inputs, constraint, out outputColumnNames);
			}
			return null;
		}

		// Token: 0x0600E4E1 RID: 58593 RVA: 0x00308A24 File Offset: 0x00306C24
		private static FormulaExpression TranslateDropColumn(DropColumn dropColumn, Microsoft.ProgramSynthesis.Transformation.Table.Constraints.PandasTranslationConstraint constraint)
		{
			string dataFrameName = constraint.DataFrameName;
			string value = dropColumn.sourceColumnName.Value;
			return PythonExpressionHelper.DotFunc<object>(PythonExpressionHelper.Variable(dataFrameName), "drop", new FormulaExpression[]
			{
				PythonExpressionHelper.AssignArg("columns", PythonExpressionHelper.StringLiteral(value)),
				PythonExpressionHelper.AssignArg("inplace", PythonExpressionHelper.True())
			});
		}

		// Token: 0x0600E4E2 RID: 58594 RVA: 0x00308A84 File Offset: 0x00306C84
		private static FormulaExpression TranslateSplitColumn(SplitColumn splitColumn, ITable<object> inputs, Microsoft.ProgramSynthesis.Transformation.Table.Constraints.PandasTranslationConstraint constraint, out IEnumerable<string> outputColumnNames)
		{
			outputColumnNames = null;
			Microsoft.ProgramSynthesis.Split.Text.Build.GrammarBuilders grammarBuilders = Microsoft.ProgramSynthesis.Split.Text.Build.GrammarBuilders.Instance(Language.Grammar);
			Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes.Split split;
			SelectColumnToSplit selectColumnToSplit;
			SplitRegion splitRegion;
			if (Language.Build.Node.IsRule.Split(splitColumn.splitCell.Node, out split) && Language.Build.Node.IsRule.SelectColumnToSplit(splitColumn.columnToSplit.Node, out selectColumnToSplit) && grammarBuilders.Node.IsRule.SplitRegion(split.regionSplit.Node, out splitRegion))
			{
				SplitProgram splitProgram = new SplitProgram(splitRegion);
				Session session = new Session(null, null, null);
				Optional<IEnumerable<string>> optional = (from cell in inputs.Column(selectColumnToSplit.sourceColumnName.Value)
					select (cell == null || cell is string).Then((string)cell)).WholeSequenceOfValues<string>();
				Microsoft.ProgramSynthesis.Split.Translation.Python.PandasTranslationConstraint pandasTranslationConstraint = new Microsoft.ProgramSynthesis.Split.Translation.Python.PandasTranslationConstraint
				{
					DataFrameName = constraint.DataFrameName,
					InputColumnName = selectColumnToSplit.sourceColumnName.Value,
					OutputColumnPrefix = selectColumnToSplit.sourceColumnName.Value + "_",
					MaximumExamplesInComments = 0
				};
				session.Constraints.Add(pandasTranslationConstraint);
				NotifyingCollection<StringRegion> inputs2 = session.Inputs;
				IEnumerable<string> value = optional.Value;
				Func<string, StringRegion> func;
				if ((func = Microsoft.ProgramSynthesis.Transformation.Table.Translation.Python.PandasTranslator.<>O.<0>__CreateStringRegion) == null)
				{
					func = (Microsoft.ProgramSynthesis.Transformation.Table.Translation.Python.PandasTranslator.<>O.<0>__CreateStringRegion = new Func<string, StringRegion>(SplitSession.CreateStringRegion));
				}
				inputs2.Add(value.Select(func));
				SplitTranslation splitTranslation = session.Translate(TargetLanguage.Pandas, splitProgram, null, default(CancellationToken)) as SplitTranslation;
				outputColumnNames = splitTranslation.Metadata.OutputColumnNames;
				return splitTranslation.TranslatedExpression;
			}
			return null;
		}

		// Token: 0x0600E4E3 RID: 58595 RVA: 0x00308C3C File Offset: 0x00306E3C
		private static FormulaExpression TranslateDropRows(DropRows dropRows, ITable<object> inputs, Microsoft.ProgramSynthesis.Transformation.Table.Constraints.PandasTranslationConstraint constraint)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(constraint.DataFrameName);
			FormulaExpression formulaExpression2 = PythonExpressionHelper.Dot(constraint.PandasAlias, "NA");
			if (dropRows.dropCondition.Value is DuplicateCondition)
			{
				return PythonExpressionHelper.DotFunc<object>(formulaExpression, "drop_duplicates", new FormulaExpression[] { PythonExpressionHelper.AssignArg("inplace", PythonExpressionHelper.True()) });
			}
			MissingCondition missingCondition = dropRows.dropCondition.Value as MissingCondition;
			if (missingCondition != null)
			{
				FormulaExpression formulaExpression3 = formulaExpression;
				if (missingCondition.MissingValueTypes.HasFlag(MissingValueType.EmptyString))
				{
					formulaExpression3 = PythonExpressionHelper.DotFunc<object>(formulaExpression3, "replace", new FormulaExpression[]
					{
						PythonExpressionHelper.StringLiteral(""),
						formulaExpression2
					});
				}
				if (missingCondition.MissingValueTypes.HasFlag(MissingValueType.WhiteSpace))
				{
					formulaExpression3 = PythonExpressionHelper.DotFunc<object>(formulaExpression3, "replace", new FormulaExpression[]
					{
						PythonExpressionHelper.StringLiteral("^\\s+$"),
						formulaExpression2,
						PythonExpressionHelper.AssignArg("regex", PythonExpressionHelper.True())
					});
				}
				if (missingCondition.MissingValueTypes.HasFlag(MissingValueType.NanString))
				{
					formulaExpression3 = PythonExpressionHelper.DotFunc<object>(formulaExpression3, "replace", new FormulaExpression[]
					{
						PythonExpressionHelper.StringLiteral("NaN"),
						formulaExpression2
					});
				}
				if (inputs.ColumnNames.Count<string>() <= 1)
				{
					return PythonExpressionHelper.Assign(formulaExpression, PythonExpressionHelper.DotFunc<object>(formulaExpression3, "dropna"));
				}
				if (missingCondition.MissingValueFraction == 1.0)
				{
					return PythonExpressionHelper.Assign(formulaExpression, PythonExpressionHelper.DotFunc<object>(formulaExpression3, "dropna", new FormulaExpression[] { PythonExpressionHelper.AssignArg("how", PythonExpressionHelper.StringLiteral("all")) }));
				}
				if (missingCondition.MissingValueFraction == 0.0)
				{
					return PythonExpressionHelper.Assign(formulaExpression, PythonExpressionHelper.DotFunc<object>(formulaExpression3, "dropna", new FormulaExpression[] { PythonExpressionHelper.AssignArg("how", PythonExpressionHelper.StringLiteral("any")) }));
				}
				if (missingCondition.MissingValueFraction > 0.0 && missingCondition.MissingValueFraction < 1.0)
				{
					FormulaExpression formulaExpression4 = PythonExpressionHelper.Func("int", new FormulaExpression[] { PythonExpressionHelper.Multiply(PythonExpressionHelper.NumberLiteral(missingCondition.MissingValueFraction), PythonExpressionHelper.Func("len", new FormulaExpression[] { PythonExpressionHelper.Dot(formulaExpression, "columns") })) });
					return PythonExpressionHelper.Assign(formulaExpression, PythonExpressionHelper.DotFunc<object>(formulaExpression3, "dropna", new FormulaExpression[] { PythonExpressionHelper.AssignArg("thresh", formulaExpression4) }));
				}
			}
			return null;
		}

		// Token: 0x0600E4E4 RID: 58596 RVA: 0x00308EC0 File Offset: 0x003070C0
		private static FormulaExpression TranslateCastColumn(CastColumn castColumn, Microsoft.ProgramSynthesis.Transformation.Table.Constraints.PandasTranslationConstraint constraint)
		{
			string value = castColumn.sourceColumnName.Value;
			string dataFrameName = constraint.DataFrameName;
			IRichDataType value2 = castColumn.richDataType.Value;
			PythonSnippet pythonSnippet = null;
			if (castColumn.isMixedColumn.Value)
			{
				FormulaExpression formulaExpression = PandasExpressionHelper.Column(dataFrameName, value);
				FormulaExpression formulaExpression2 = PythonExpressionHelper.Dot(formulaExpression, PythonExpressionHelper.Func("astype", new FormulaExpression[] { PythonExpressionHelper.Variable("str") }));
				pythonSnippet = new PythonSnippet(new PythonImports(), PythonExpressionHelper.Assign(formulaExpression, formulaExpression2).ToString(), dataFrameName, null);
			}
			PythonSnippet pythonSnippet2 = PythonTranslator.TranslateToFormulaExpression(value2, dataFrameName, value, constraint.PandasAlias);
			pythonSnippet2 = ((pythonSnippet != null) ? pythonSnippet.Merge(pythonSnippet2) : null) ?? pythonSnippet2;
			return PythonExpressionHelper.Raw(pythonSnippet2.GetCode(true, 2U, 1U));
		}

		// Token: 0x0600E4E5 RID: 58597 RVA: 0x00308F8C File Offset: 0x0030718C
		private static FormulaExpression TranslateFillMissingValues(FillMissingValues fillMissingValues, Microsoft.ProgramSynthesis.Transformation.Table.Constraints.PandasTranslationConstraint constraint)
		{
			Microsoft.ProgramSynthesis.Transformation.Table.Build.GrammarBuilders.Instance(Language.Grammar);
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(constraint.PandasAlias);
			FormulaExpression formulaExpression2 = PythonExpressionHelper.Variable(constraint.DataFrameName);
			string value3 = fillMissingValues.sourceColumnName.Value;
			FormulaExpression formulaExpression3 = PandasExpressionHelper.Column(formulaExpression2, PythonExpressionHelper.StringLiteral(value3));
			IEnumerable<object> value2 = fillMissingValues.missingValueMarkers.Value;
			IEnumerable<object> enumerable = value2.Where((object value) => !Utilities.IsNumeric(value));
			FormulaExpression formulaExpression5;
			if (fillMissingValues.fillMethod.Value == FillMethod.Mean || fillMissingValues.fillMethod.Value == FillMethod.RoundedMean)
			{
				FormulaExpression formulaExpression4 = (enumerable.Any<object>() ? PythonExpressionHelper.DotFunc<object>(PythonExpressionHelper.DotFunc<object>(formulaExpression, "to_numeric", new FormulaExpression[]
				{
					formulaExpression3,
					PythonExpressionHelper.AssignArg("errors", "coerce")
				}), "mean") : PythonExpressionHelper.DotFunc<object>(formulaExpression3, "mean"));
				formulaExpression5 = ((fillMissingValues.fillMethod.Value == FillMethod.RoundedMean) ? PythonExpressionHelper.Func<object>("round", new FormulaExpression[] { formulaExpression4 }) : formulaExpression4);
			}
			else
			{
				if (fillMissingValues.fillMethod.Value != FillMethod.Mode)
				{
					return null;
				}
				FormulaExpression formulaExpression6 = formulaExpression3;
				FormulaExpression formulaExpression7 = formulaExpression3;
				string text = "isin";
				FormulaExpression[] array = new FormulaExpression[1];
				array[0] = PythonExpressionHelper.Array(value2.Select((object value) => PythonExpressionHelper.Raw(value.ToPythonLiteral())));
				formulaExpression5 = PythonExpressionHelper.Index<object>(PythonExpressionHelper.DotFunc<object>(PythonExpressionHelper.Index<object>(formulaExpression6, PythonExpressionHelper.Negate(PythonExpressionHelper.DotFunc<object>(formulaExpression7, text, array))), "mode"), 0);
			}
			FormulaExpression formulaExpression8;
			if (!enumerable.Any<object>())
			{
				formulaExpression8 = formulaExpression3;
			}
			else
			{
				FormulaExpression formulaExpression9 = formulaExpression3;
				string text2 = "replace";
				FormulaExpression[] array2 = new FormulaExpression[2];
				array2[0] = PythonExpressionHelper.Array(enumerable.Select((object value) => PythonExpressionHelper.Raw(value.ToPythonLiteral())));
				array2[1] = PythonExpressionHelper.Dot(PythonExpressionHelper.Variable(constraint.PandasAlias), "NA");
				formulaExpression8 = PythonExpressionHelper.DotFunc<object>(formulaExpression9, text2, array2);
			}
			FormulaExpression formulaExpression10 = formulaExpression8;
			FormulaExpression formulaExpression11 = PythonExpressionHelper.Variable("fillValue");
			FormulaExpression formulaExpression12 = PythonExpressionHelper.Assign(formulaExpression11, formulaExpression5);
			FormulaExpression formulaExpression13 = PythonExpressionHelper.Assign(formulaExpression3, PythonExpressionHelper.DotFunc<object>(formulaExpression10, "fillna", new FormulaExpression[] { formulaExpression11 }));
			return PythonExpressionHelper.Block(new FormulaExpression[] { formulaExpression12, formulaExpression13 });
		}

		// Token: 0x0600E4E6 RID: 58598 RVA: 0x003091D4 File Offset: 0x003073D4
		private static FormulaExpression TranslateOneHotEncode(OneHotEncode oneHotEncode, Microsoft.ProgramSynthesis.Transformation.Table.Constraints.PandasTranslationConstraint constraint)
		{
			string value = oneHotEncode.sourceColumnName.Value;
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(constraint.DataFrameName);
			FormulaExpression formulaExpression2 = PythonExpressionHelper.Variable(constraint.PandasAlias);
			return PythonExpressionHelper.Assign(formulaExpression, PythonExpressionHelper.DotFunc<object>(formulaExpression2, "get_dummies", new FormulaExpression[]
			{
				formulaExpression,
				PythonExpressionHelper.AssignArg(PythonExpressionHelper.Variable("columns"), PythonExpressionHelper.Array(new string[] { value }))
			}));
		}

		// Token: 0x02001B3B RID: 6971
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040056DE RID: 22238
			public static Func<string, StringRegion> <0>__CreateStringRegion;
		}
	}
}
