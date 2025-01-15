using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Split.Translation;
using Microsoft.ProgramSynthesis.Split.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Wrangling.Logging;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.PowerQuery
{
	// Token: 0x02001B35 RID: 6965
	internal class PowerQueryTranslator
	{
		// Token: 0x0600E4CC RID: 58572 RVA: 0x00307CFC File Offset: 0x00305EFC
		private PowerQueryTranslator(Program program, Session session, OptimizeFor? optimizeFor, CancellationToken cancellationToken)
		{
			this._program = program;
			this._session = session;
			this._optimizeFor = optimizeFor;
			this._cancellationToken = cancellationToken;
			this._translationConstraint = this._session.Constraints.OfType<Microsoft.ProgramSynthesis.Transformation.Table.Constraints.PowerQueryTranslationConstraint>().OnlyOrDefault<Microsoft.ProgramSynthesis.Transformation.Table.Constraints.PowerQueryTranslationConstraint>();
			if (this._translationConstraint == null)
			{
				throw new Exception("Cannot translate to PowerQuery unless session has a PowerQueryTranslationConstraint.");
			}
		}

		// Token: 0x0600E4CD RID: 58573 RVA: 0x00307D60 File Offset: 0x00305F60
		internal static TransformationTableTranslation Translate(Program program, Session session, OptimizeFor? optimizeFor, CancellationToken cancellationToken)
		{
			return new PowerQueryTranslator(program, session, optimizeFor, cancellationToken).Translate();
		}

		// Token: 0x0600E4CE RID: 58574 RVA: 0x00307D70 File Offset: 0x00305F70
		private TransformationTableTranslation Translate()
		{
			GrammarBuilders grammarBuilders = GrammarBuilders.Instance(Language.Grammar);
			PowerQueryLetExpressionBuilder powerQueryLetExpressionBuilder = new PowerQueryLetExpressionBuilder(this._translationConstraint.InputStepName, this._translationConstraint._forbiddenStepNames);
			IEnumerable<string> enumerable = null;
			foreach (table table in this._program.Operations)
			{
				DropColumn dropColumn;
				AddSplitColumns addSplitColumns;
				FillMissingValues fillMissingValues;
				if (table.Is_DropColumn(grammarBuilders, out dropColumn))
				{
					if (!this.TryTranslateDropColumn(dropColumn, powerQueryLetExpressionBuilder))
					{
						return null;
					}
				}
				else if (table.Is_AddSplitColumns(grammarBuilders, out addSplitColumns))
				{
					if (!this.TryTranslateAddSplitColumns(addSplitColumns, powerQueryLetExpressionBuilder.AllForbiddenStepNames, powerQueryLetExpressionBuilder, out enumerable))
					{
						return null;
					}
				}
				else if (table.Is_FillMissingValues(grammarBuilders, out fillMissingValues))
				{
					if (!this.TryTranslateFillMissingValues(fillMissingValues, powerQueryLetExpressionBuilder))
					{
						return null;
					}
				}
				else
				{
					DropRows dropRows;
					if (!table.Is_DropRows(grammarBuilders, out dropRows))
					{
						return null;
					}
					if (!this.TryTranslateDropRows(dropRows, powerQueryLetExpressionBuilder))
					{
						return null;
					}
				}
			}
			PowerQueryLet powerQueryLet = powerQueryLetExpressionBuilder.CombineSteps();
			return new Microsoft.ProgramSynthesis.Transformation.Table.Translation.PowerQuery.PowerQueryTranslation(this._program, powerQueryLet, this._translationConstraint, new Metadata(enumerable));
		}

		// Token: 0x0600E4CF RID: 58575 RVA: 0x00307E94 File Offset: 0x00306094
		private bool TryTranslateDropColumn(DropColumn dropColumn, PowerQueryLetExpressionBuilder builder)
		{
			string value = dropColumn.sourceColumnName.Value;
			builder.AddStep(this._translationConstraint.LocalizedStrings.RemovedColumn, new PowerQueryFunc(MTableFunctionName.RemoveColumns.QualifiedName, new FormulaExpression[]
			{
				new PowerQueryVariable(builder.LastStepName),
				new PowerQueryList(new FormulaExpression[]
				{
					new PowerQueryStringLiteral(value)
				})
			}), false);
			return true;
		}

		// Token: 0x0600E4D0 RID: 58576 RVA: 0x00307F04 File Offset: 0x00306104
		private bool TryTranslateAddSplitColumns(AddSplitColumns AddSplitColumns, IEnumerable<string> forbiddenStepNames, PowerQueryLetExpressionBuilder builder, out IEnumerable<string> outputColumnNames)
		{
			GrammarBuilders grammarBuilders = GrammarBuilders.Instance(Language.Grammar);
			outputColumnNames = null;
			SplitColumn splitColumn;
			if (!AddSplitColumns.newColumns.Is_SplitColumn(grammarBuilders, out splitColumn))
			{
				return false;
			}
			SplitProgram splitProgram = new SplitProgram(splitColumn.splitCell.Cast_Split().regionSplit);
			ILogger logger = this._session.Logger;
			Session session = new Session(this._session.JournalStorage, this._session.Culture, logger);
			string inputColumnName = splitColumn.columnToSplit.Cast_SelectColumnToSplit().sourceColumnName.Value;
			session.Constraints.Add(new Microsoft.ProgramSynthesis.Split.Translation.PowerQuery.PowerQueryTranslationConstraint(builder.LastStepName, inputColumnName, this._translationConstraint.LocalizedStrings, this._translationConstraint.Escape, forbiddenStepNames, this._session.InputTable.ColumnNames.Union(this._translationConstraint.ForbiddenColumnNames ?? Enumerable.Empty<string>()), this._translationConstraint.RemoveOriginalColumn_Split));
			session.Inputs.Add(this._session.Inputs.SelectMany(delegate(ITable<object> table)
			{
				IEnumerable<string> enumerable = from cell in table.Column(inputColumnName)
					select cell as string;
				Func<string, StringRegion> func;
				if ((func = PowerQueryTranslator.<>O.<0>__CreateStringRegion) == null)
				{
					func = (PowerQueryTranslator.<>O.<0>__CreateStringRegion = new Func<string, StringRegion>(SplitSession.CreateStringRegion));
				}
				return enumerable.Select(func);
			}));
			Microsoft.ProgramSynthesis.Split.Translation.PowerQuery.PowerQueryTranslation powerQueryTranslation = session.TranslatePowerQuery(splitProgram, this._optimizeFor, this._cancellationToken);
			if (((powerQueryTranslation != null) ? powerQueryTranslation.PowerQueryProgram : null) == null)
			{
				return false;
			}
			outputColumnNames = powerQueryTranslation.Metadata.OutputColumnNames;
			builder.AddSteps(powerQueryTranslation.PowerQueryProgram);
			return true;
		}

		// Token: 0x0600E4D1 RID: 58577 RVA: 0x00308088 File Offset: 0x00306288
		private bool TryTranslateDropRows(DropRows dropRows, PowerQueryLetExpressionBuilder builder)
		{
			if (dropRows.dropCondition.Value is DuplicateCondition)
			{
				builder.AddStep(this._translationConstraint.LocalizedStrings.RemovedDuplicateRows, PowerQueryExpressionHelper.Func(MTableFunctionName.Distinct.QualifiedName, new FormulaExpression[]
				{
					new PowerQueryVariable(builder.LastStepName)
				}), false);
				return true;
			}
			MissingCondition missingCondition = dropRows.dropCondition.Value as MissingCondition;
			if (missingCondition != null)
			{
				List<string> list = new List<string>();
				if (missingCondition.MissingValueTypes.HasFlag(MissingValueType.NAValue))
				{
					list.Add(null);
				}
				if (missingCondition.MissingValueTypes.HasFlag(MissingValueType.EmptyString))
				{
					list.Add(string.Empty);
				}
				if (missingCondition.MissingValueTypes.HasFlag(MissingValueType.NanString))
				{
					list.Add("NaN");
				}
				if (missingCondition.MissingValueTypes.HasFlag(MissingValueType.WhiteSpace))
				{
					list.Add(" ");
				}
				string removedBlankRows = this._translationConstraint.LocalizedStrings.RemovedBlankRows;
				string qualifiedName = MTableFunctionName.SelectRows.QualifiedName;
				FormulaExpression[] array = new FormulaExpression[2];
				array[0] = PowerQueryExpressionHelper.Variable(builder.LastStepName);
				int num = 1;
				IReadOnlyList<FormulaExpression> readOnlyList = null;
				string text = "List.Count";
				FormulaExpression[] array2 = new FormulaExpression[1];
				int num2 = 0;
				string text2 = "List.RemoveMatchingItems";
				FormulaExpression[] array3 = new FormulaExpression[2];
				array3[0] = PowerQueryExpressionHelper.Func("Record.FieldValues", new FormulaExpression[] { PowerQueryExpressionHelper.Variable("_") });
				array3[1] = new PowerQueryList(list.Select((string v) => v.MaybeLiteral().Value));
				array2[num2] = PowerQueryExpressionHelper.Func(text2, array3);
				array[num] = new PowerQueryLambdaFunction(readOnlyList, PowerQueryExpressionHelper.GreaterThan(PowerQueryExpressionHelper.Divide(PowerQueryExpressionHelper.Func(text, array2), PowerQueryExpressionHelper.Func("Record.FieldCount", new FormulaExpression[] { PowerQueryExpressionHelper.Variable("_") })), PowerQueryExpressionHelper.NumberLiteral(missingCondition.MissingValueFraction)));
				builder.AddStep(removedBlankRows, PowerQueryExpressionHelper.Func(qualifiedName, array), false);
				return true;
			}
			return false;
		}

		// Token: 0x0600E4D2 RID: 58578 RVA: 0x00308278 File Offset: 0x00306478
		private bool TryTranslateFillMissingValues(FillMissingValues fillMissingValues, PowerQueryLetExpressionBuilder builder)
		{
			GrammarBuilders.Instance(Language.Grammar);
			FormulaExpression formulaExpression = PowerQueryExpressionHelper.Variable("table");
			FormulaExpression columnVariable = PowerQueryExpressionHelper.Variable("column");
			PowerQueryLetExpressionBuilder innerLet = new PowerQueryLetExpressionBuilder("table", null);
			Optional<IReadOnlyList<FormulaExpression>> optional = fillMissingValues.missingValueMarkers.Value.Select((object val) => val.MaybeLiteral()).WholeReadOnlyListOfValues<FormulaExpression>();
			if (!optional.HasValue)
			{
				return false;
			}
			IReadOnlyList<FormulaExpression> value = optional.Value;
			FormulaExpression formulaExpression2 = PowerQueryExpressionHelper.Func("List.RemoveMatchingItems", new FormulaExpression[]
			{
				PowerQueryExpressionHelper.Func("Table.Column", new FormulaExpression[] { formulaExpression, columnVariable }),
				new PowerQueryList(value)
			});
			FormulaExpression formulaExpression3;
			switch (fillMissingValues.fillMethod.Value)
			{
			case FillMethod.Mode:
				formulaExpression3 = PowerQueryExpressionHelper.Func("List.Mode", new FormulaExpression[] { formulaExpression2 });
				break;
			case FillMethod.Mean:
				formulaExpression3 = PowerQueryExpressionHelper.Func("List.Average", new FormulaExpression[] { formulaExpression2 });
				break;
			case FillMethod.RoundedMean:
				formulaExpression3 = PowerQueryExpressionHelper.Func("Number.Round", new FormulaExpression[] { PowerQueryExpressionHelper.Func("List.Average", new FormulaExpression[] { formulaExpression2 }) });
				break;
			default:
				formulaExpression3 = null;
				break;
			}
			FormulaExpression formulaExpression4 = formulaExpression3;
			if (formulaExpression4 == null)
			{
				return false;
			}
			string text;
			switch (fillMissingValues.fillMethod.Value)
			{
			case FillMethod.Mode:
				text = this._translationConstraint.LocalizedStrings.Mode;
				break;
			case FillMethod.Mean:
				text = this._translationConstraint.LocalizedStrings.Average;
				break;
			case FillMethod.RoundedMean:
				text = this._translationConstraint.LocalizedStrings.RoundedAverage;
				break;
			default:
				text = null;
				break;
			}
			string text2 = text;
			if (text2 == null)
			{
				return false;
			}
			FormulaExpression lastStepNameExpression = PowerQueryExpressionHelper.Variable(innerLet.LastStepName);
			FormulaExpression fillValueStepNameExpression = PowerQueryExpressionHelper.Variable(text2);
			innerLet.AddStep(text2, formulaExpression4, false);
			value.ForEach(delegate(FormulaExpression missingValue)
			{
				innerLet.AddStep(this._translationConstraint.LocalizedStrings.FilledMissingValues, new PowerQueryFunc(MTableFunctionName.ReplaceValue.QualifiedName, new FormulaExpression[]
				{
					lastStepNameExpression,
					missingValue,
					fillValueStepNameExpression,
					new PowerQueryConstant("Replacer.ReplaceValue"),
					new PowerQueryList(new FormulaExpression[] { columnVariable })
				}), false);
				lastStepNameExpression = PowerQueryExpressionHelper.Variable(innerLet.LastStepName);
			});
			if (this._translationConstraint.CombineMultiStepPrograms)
			{
				PowerQueryLet powerQueryLet = PowerQueryTranslator.WrapInCustomFunction(innerLet, fillMissingValues.sourceColumnName.Value, builder.LastStepName);
				builder.AddStep(this._translationConstraint.LocalizedStrings.FilledMissingValues, powerQueryLet, false);
				return true;
			}
			PowerQueryLet powerQueryLet2 = FormulaTransformVisitor.Transform(innerLet.CombineSteps(), delegate(FormulaExpression node)
			{
				PowerQueryVariable powerQueryVariable = node as PowerQueryVariable;
				if (powerQueryVariable != null)
				{
					string name = powerQueryVariable.Name;
					if (name == "table")
					{
						return PowerQueryExpressionHelper.Variable(builder.LastStepName);
					}
					if (name == "column")
					{
						return PowerQueryExpressionHelper.StringLiteral(fillMissingValues.sourceColumnName.Value);
					}
				}
				return node;
			}) as PowerQueryLet;
			builder.AddSteps(powerQueryLet2);
			return true;
		}

		// Token: 0x0600E4D3 RID: 58579 RVA: 0x00308548 File Offset: 0x00306748
		private static PowerQueryLet WrapInCustomFunction(PowerQueryLetExpressionBuilder let, string column, string lastStepName)
		{
			let.AddMetadata("AutoSuggested", PowerQueryExpressionHelper.BooleanLiteral(true));
			FormulaExpression formulaExpression = PowerQueryExpressionHelper.Lambda(new FormulaExpression[]
			{
				PowerQueryExpressionHelper.Parameter("table", "table"),
				PowerQueryExpressionHelper.Parameter("column", "text")
			}, let.CombineSteps());
			PowerQueryLetExpressionBuilder powerQueryLetExpressionBuilder = new PowerQueryLetExpressionBuilder(null, null);
			string text = "Fx";
			powerQueryLetExpressionBuilder.AddStep(text, formulaExpression, false);
			powerQueryLetExpressionBuilder.AddStep("Output", PowerQueryExpressionHelper.Func(text, new FormulaExpression[]
			{
				PowerQueryExpressionHelper.Variable(lastStepName),
				PowerQueryExpressionHelper.StringLiteral(column)
			}), false);
			return powerQueryLetExpressionBuilder.CombineSteps();
		}

		// Token: 0x040056CA RID: 22218
		private readonly Program _program;

		// Token: 0x040056CB RID: 22219
		private readonly Session _session;

		// Token: 0x040056CC RID: 22220
		private readonly OptimizeFor? _optimizeFor;

		// Token: 0x040056CD RID: 22221
		private readonly CancellationToken _cancellationToken;

		// Token: 0x040056CE RID: 22222
		private readonly Microsoft.ProgramSynthesis.Transformation.Table.Constraints.PowerQueryTranslationConstraint _translationConstraint;

		// Token: 0x040056CF RID: 22223
		private const string TableVariableString = "table";

		// Token: 0x040056D0 RID: 22224
		private const string ColumnVariableString = "column";

		// Token: 0x02001B36 RID: 6966
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040056D1 RID: 22225
			public static Func<string, StringRegion> <0>__CreateStringRegion;
		}
	}
}
