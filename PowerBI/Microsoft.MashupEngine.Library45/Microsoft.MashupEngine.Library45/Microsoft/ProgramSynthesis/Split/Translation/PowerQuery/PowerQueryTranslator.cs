using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Split.Text.Build;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Formula;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Split.Translation.PowerQuery
{
	// Token: 0x02001412 RID: 5138
	internal class PowerQueryTranslator : Session.Translator<PowerQueryLet>
	{
		// Token: 0x06009E98 RID: 40600 RVA: 0x0021A330 File Offset: 0x00218530
		private PowerQueryTranslator(Session session, SplitProgram program = null)
			: base(session, program)
		{
			this._constraint = session.Constraints.OfType<PowerQueryTranslationConstraint>().OnlyOrDefault<PowerQueryTranslationConstraint>() ?? new PowerQueryTranslationConstraint("Source", "input", PowerQueryTranslator.DefaultLocalizedStrings.Instance, PowerQueryTranslator.DefaultEscapePowerQueryM.Instance, null, null, true);
			this._builder = new PowerQueryLetExpressionBuilder(this._constraint.InputStepName, this._constraint._forbiddenStepNames);
			this._splitFunctionBuilder = new PowerQueryLetExpressionBuilder(PowerQueryTranslator.SplitFunctionParam, null);
		}

		// Token: 0x06009E99 RID: 40601 RVA: 0x0021A3B8 File Offset: 0x002185B8
		public static ITranslation<SplitProgram, FormulaExpression> Translate(Session session, SplitProgram program, OptimizeFor? optimizeFor, CancellationToken cancellationToken)
		{
			return new PowerQueryTranslator(session, program).Translate(optimizeFor, cancellationToken);
		}

		// Token: 0x17001AE9 RID: 6889
		// (get) Token: 0x06009E9A RID: 40602 RVA: 0x0021A3C8 File Offset: 0x002185C8
		public override string InputColumnName
		{
			get
			{
				return this._constraint.InputColumnName;
			}
		}

		// Token: 0x17001AEA RID: 6890
		// (get) Token: 0x06009E9B RID: 40603 RVA: 0x0021A3D5 File Offset: 0x002185D5
		public override string OutputColumnPrefix
		{
			get
			{
				return this._constraint.OutputColumnPrefix;
			}
		}

		// Token: 0x17001AEB RID: 6891
		// (get) Token: 0x06009E9C RID: 40604 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override int FirstOutputColumnIndex
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06009E9D RID: 40605 RVA: 0x0021A3E4 File Offset: 0x002185E4
		protected override PowerQueryLet BuildCombinedTFormulaProgram()
		{
			FormulaExpression formulaExpression = FormulaTransformVisitor.Transform(this._splitFunctionBuilder.CombineSteps(), delegate(FormulaExpression node)
			{
				PowerQueryVariable powerQueryVariable = node as PowerQueryVariable;
				FormulaExpression formulaExpression2;
				if (powerQueryVariable == null)
				{
					PowerQueryLookup powerQueryLookup = node as PowerQueryLookup;
					if (powerQueryLookup == null)
					{
						formulaExpression2 = node;
					}
					else
					{
						formulaExpression2 = ((powerQueryLookup.Name == this._constraint.InputColumnName) ? PowerQueryExpressionHelper.Variable(PowerQueryTranslator.SplitFunctionParam) : powerQueryLookup);
					}
				}
				else
				{
					formulaExpression2 = ((powerQueryVariable.Name == this._constraint.InputColumnName) ? PowerQueryExpressionHelper.Variable(PowerQueryTranslator.SplitFunctionParam) : powerQueryVariable);
				}
				return formulaExpression2;
			});
			PowerQueryLambdaFunction splitFunction = new PowerQueryLambdaFunction(new List<FormulaExpression>(new PowerQueryVariable[]
			{
				new PowerQueryVariable(PowerQueryTranslator.SplitFunctionParam)
			}), formulaExpression);
			string columnToSplit = this.InputColumnName;
			if (!this._constraint.RemoveOriginalColumn)
			{
				columnToSplit = ((this.OutputColumnPrefix == this.InputColumnName) ? (this.OutputColumnPrefix + ".split") : this.OutputColumnPrefix);
				this._builder.AddStep(this._constraint.LocalizedStrings.AddedColumn, (PowerQueryVariable lastStepVar) => new PowerQueryFunc(MTableFunctionName.AddColumn.QualifiedName, new FormulaExpression[]
				{
					lastStepVar,
					new PowerQueryStringLiteral(columnToSplit),
					new PowerQueryLambdaFunction(null, new PowerQueryLookup(null, this._constraint.InputColumnName))
				}));
			}
			this._builder.AddStep(this._constraint.LocalizedStrings.SplitColumn, delegate(PowerQueryVariable lastStepVar)
			{
				string qualifiedName = MTableFunctionName.SplitColumn.QualifiedName;
				FormulaExpression[] array = new FormulaExpression[4];
				array[0] = lastStepVar;
				array[1] = new PowerQueryStringLiteral(columnToSplit);
				array[2] = splitFunction;
				array[3] = new PowerQueryList(this._splitFunctionOutputColumns.Select((string c) => new PowerQueryStringLiteral(c)));
				return new PowerQueryFunc(qualifiedName, array);
			});
			return this._builder.CombineSteps();
		}

		// Token: 0x06009E9E RID: 40606 RVA: 0x0021A4E4 File Offset: 0x002186E4
		protected override bool TryRegisterTFormulaProgram(Session tfSession, Microsoft.ProgramSynthesis.Transformation.Formula.Program tfProgram, string outputColumnPrefix, int outputColumnNumber, int newColumnIndex, CancellationToken cancellationToken, out string outputColumnName)
		{
			outputColumnName = null;
			tfSession.Constraints.AddOrReplace(new PowerQueryTranslationConstraint
			{
				OutputType = OutputType.InnerExpression
			});
			FormulaTranslation formulaTranslation = tfSession.Translate(TargetLanguage.PowerQueryM, tfProgram, cancellationToken);
			FormulaExpression formulaExpression = ((formulaTranslation != null) ? ((ITranslation<Microsoft.ProgramSynthesis.Transformation.Formula.Program, FormulaExpression>)formulaTranslation).TranslatedExpression : null);
			if (formulaExpression == null)
			{
				return false;
			}
			string text = this.ResolveOutputColumnName(outputColumnPrefix + "." + outputColumnNumber.ToString());
			this._splitFunctionBuilder.AddStep(text + " text", formulaExpression, true);
			this._splitFunctionOutputColumns.Add(text);
			outputColumnName = text;
			return true;
		}

		// Token: 0x06009E9F RID: 40607 RVA: 0x0021A570 File Offset: 0x00218770
		protected override bool TryTranslateSplit(out PowerQueryLet translatedProgram, out IEnumerable<string> outputColumnNames)
		{
			string columnToSplit = this.InputColumnName;
			outputColumnNames = null;
			if (!this._constraint.RemoveOriginalColumn)
			{
				columnToSplit = ((this.OutputColumnPrefix == this.InputColumnName) ? (this.OutputColumnPrefix + ".split") : this.OutputColumnPrefix);
				this._builder.AddStep(this._constraint.LocalizedStrings.AddedColumn, (PowerQueryVariable lastStepVar) => new PowerQueryFunc(MTableFunctionName.AddColumn.QualifiedName, new FormulaExpression[]
				{
					lastStepVar,
					new PowerQueryStringLiteral(columnToSplit),
					new PowerQueryLambdaFunction(null, new PowerQueryLookup(null, this._constraint.InputColumnName))
				}));
			}
			if (!this.TryTranslateSplitInternal(columnToSplit, out translatedProgram, out outputColumnNames))
			{
				this._builder.Reset();
				return false;
			}
			translatedProgram = this._builder.CombineSteps();
			return true;
		}

		// Token: 0x06009EA0 RID: 40608 RVA: 0x0021A62C File Offset: 0x0021882C
		private bool TryTranslateSplitInternal(string columnToSplit, out PowerQueryLet translatedProgram, out IEnumerable<string> outputColumnNames)
		{
			translatedProgram = null;
			ProgramNode programNode = this._program.ProgramNode;
			SplitRegion splitRegion;
			if (!Microsoft.ProgramSynthesis.Split.Text.Language.Build.Node.IsRule.SplitRegion(programNode, out splitRegion))
			{
				outputColumnNames = null;
				return false;
			}
			if (splitRegion.ignoreIndexes.Value.Length != 0)
			{
				outputColumnNames = null;
				return false;
			}
			int value = splitRegion.numSplits.Value;
			FormulaExpression columnNamesOrNumber;
			if (columnToSplit == this.OutputColumnPrefix)
			{
				IEnumerable<string> forbiddenColumnNames = this._constraint.ForbiddenColumnNames;
				if (forbiddenColumnNames == null || !forbiddenColumnNames.Any(delegate(string n)
				{
					int num;
					return n.StartsWith(this.OutputColumnPrefix + ".") && int.TryParse(n.Substring(this.OutputColumnPrefix.Length + 1), out num);
				}))
				{
					columnNamesOrNumber = new PowerQueryNumberLiteral((double)value);
					outputColumnNames = from i in Enumerable.Range(1, value)
						select this.ResolveOutputColumnName(string.Format("{0}.{1}", this.OutputColumnPrefix, i));
					goto IL_0117;
				}
			}
			outputColumnNames = from i in Enumerable.Range(1, value)
				select this.ResolveOutputColumnName(string.Format("{0}.{1}", this.OutputColumnPrefix, i));
			columnNamesOrNumber = new PowerQueryList(outputColumnNames.Select((string n) => new PowerQueryStringLiteral(n)));
			IL_0117:
			splitMatches_constantDelimiterMatches splitMatches_constantDelimiterMatches;
			splitMatches_multipleMatches splitMatches_multipleMatches;
			if (Microsoft.ProgramSynthesis.Split.Text.Language.Build.Node.IsRule.splitMatches_constantDelimiterMatches(splitRegion.splitMatches.Node, out splitMatches_constantDelimiterMatches))
			{
				ConstantDelimiter constantDelimiter;
				if (splitMatches_constantDelimiterMatches.constantDelimiterMatches.Is_ConstantDelimiter(Microsoft.ProgramSynthesis.Split.Text.Language.Build, out constantDelimiter))
				{
					string value2 = constantDelimiter.s.Value;
					FormulaExpression delimiterList2 = ((value > 2) ? new PowerQueryFunc("List.Repeat", new FormulaExpression[]
					{
						new PowerQueryList(new FormulaExpression[]
						{
							new PowerQueryStringLiteral(value2)
						}),
						new PowerQueryNumberLiteral((double)(value - 1))
					}) : new PowerQueryList(new FormulaExpression[]
					{
						new PowerQueryStringLiteral(value2)
					}));
					this._builder.AddStep(this._constraint.LocalizedStrings.SplitColumnByDelimiter, (PowerQueryVariable lastStepVar) => new PowerQueryFunc(MTableFunctionName.SplitColumn.QualifiedName, new FormulaExpression[]
					{
						lastStepVar,
						new PowerQueryStringLiteral(columnToSplit),
						new PowerQueryFunc(MSplitterFunctionName.SplitTextByEachDelimiter.QualifiedName, new FormulaExpression[] { delimiterList2 }),
						columnNamesOrNumber
					}));
					return true;
				}
			}
			else if (Microsoft.ProgramSynthesis.Split.Text.Language.Build.Node.IsRule.splitMatches_multipleMatches(splitRegion.splitMatches.Node, out splitMatches_multipleMatches))
			{
				Optional<IReadOnlyList<Delimiter>> optional = DelimiterCollector.MaybeCollectConstantDelimiters(splitMatches_multipleMatches.Node);
				if (optional.HasValue)
				{
					IReadOnlyList<Delimiter> delimiters = optional.Value;
					if (delimiters.Any((Delimiter delimiter) => delimiter.IsRegex))
					{
						return false;
					}
					if (splitRegion.delimiterStart.Value || splitRegion.delimiterEnd.Value)
					{
						return false;
					}
					if (delimiters.Count == 1)
					{
						string delimiterString = delimiters[0].DelimiterString;
						FormulaExpression delimiterList3 = ((value > 2) ? new PowerQueryFunc("List.Repeat", new FormulaExpression[]
						{
							new PowerQueryList(new FormulaExpression[]
							{
								new PowerQueryStringLiteral(delimiterString)
							}),
							new PowerQueryNumberLiteral((double)(value - 1))
						}) : new PowerQueryList(new FormulaExpression[]
						{
							new PowerQueryStringLiteral(delimiterString)
						}));
						this._builder.AddStep(this._constraint.LocalizedStrings.SplitColumnByDelimiter, (PowerQueryVariable lastStepVar) => new PowerQueryFunc(MTableFunctionName.SplitColumn.QualifiedName, new FormulaExpression[]
						{
							lastStepVar,
							new PowerQueryStringLiteral(columnToSplit),
							new PowerQueryFunc(MSplitterFunctionName.SplitTextByEachDelimiter.QualifiedName, new FormulaExpression[] { delimiterList3 }),
							columnNamesOrNumber
						}));
					}
					else
					{
						this._builder.AddStep(this._constraint.LocalizedStrings.SplitColumnByDelimiter, delegate(PowerQueryVariable lastStepVar)
						{
							string qualifiedName = MTableFunctionName.SplitColumn.QualifiedName;
							FormulaExpression[] array = new FormulaExpression[4];
							array[0] = lastStepVar;
							array[1] = new PowerQueryStringLiteral(columnToSplit);
							int num2 = 2;
							string qualifiedName2 = MSplitterFunctionName.SplitTextByAnyDelimiter.QualifiedName;
							FormulaExpression[] array2 = new FormulaExpression[1];
							array2[0] = new PowerQueryList(delimiters.Select((Delimiter delimiter) => new PowerQueryStringLiteral(delimiter.DelimiterString)));
							array[num2] = new PowerQueryFunc(qualifiedName2, array2);
							array[3] = columnNamesOrNumber;
							return new PowerQueryFunc(qualifiedName, array);
						});
					}
					return true;
				}
				else if (splitRegion.numSplits.Value == 2)
				{
					GrammarBuilders grammarBuilders = GrammarBuilders.Instance(splitRegion.Node.Grammar);
					GrammarBuilders.Nodes.NodeRules rule = grammarBuilders.Node.Rule;
					GrammarBuilders.Nodes.NodeHoles hole = grammarBuilders.Node.Hole;
					v v = grammarBuilders.Node.Variable.v;
					d d = rule.LookAround(rule.Concat(grammarBuilders.Node.UnnamedConversion.r_regexMatch(rule.RegexMatch(v, rule.regex(new RegularExpression(new Token[] { Token.Tokens["Line Separator"] }, 0)))), rule.RegexMatch(v, hole.regex)), rule.ConstStr(v, hole.s), rule.Empty(v));
					ProgramNode programNode2 = splitMatches_multipleMatches.multipleMatches.Node.Children.FirstOrDefault<ProgramNode>();
					if (programNode2 == null)
					{
						return false;
					}
					IReadOnlyDictionary<Hole, ProgramNode> readOnlyDictionary = ProgramSetRewriter.ExtractMappings(programNode2, d.Node);
					if (readOnlyDictionary == null)
					{
						return false;
					}
					LiteralNode literalNode = readOnlyDictionary[grammarBuilders.Hole.s] as LiteralNode;
					if (literalNode == null)
					{
						return false;
					}
					LiteralNode literalNode2 = readOnlyDictionary[grammarBuilders.Hole.regex] as LiteralNode;
					if (literalNode2 == null)
					{
						return false;
					}
					string delimiter = literalNode.Value as string;
					if (string.IsNullOrEmpty(delimiter))
					{
						return false;
					}
					RegularExpression regularExpression = literalNode2.Value as RegularExpression;
					if (regularExpression == null)
					{
						return false;
					}
					Regex regex = regularExpression.Regex;
					FormulaExpression delimiterList = new PowerQueryList(new FormulaExpression[]
					{
						new PowerQueryStringLiteral(delimiter)
					});
					FormulaExpression startAtEnd = null;
					if (base.ProgramOutputs.All(delegate(SplitCell[] splitCells)
					{
						StringRegion cellValue = splitCells.First<SplitCell>().CellValue;
						return ((cellValue != null) ? new bool?(!cellValue.Value.Contains(delimiter)) : null) ?? false;
					}))
					{
						startAtEnd = new PowerQueryBooleanLiteral(false);
					}
					else
					{
						if (!base.ProgramOutputs.All(delegate(SplitCell[] splitCells)
						{
							StringRegion cellValue2 = splitCells.Last<SplitCell>().CellValue;
							return ((cellValue2 != null) ? new bool?(!cellValue2.Value.Contains(delimiter)) : null) ?? false;
						}))
						{
							return false;
						}
						startAtEnd = new PowerQueryBooleanLiteral(true);
					}
					this._builder.AddStep(this._constraint.LocalizedStrings.SplitColumnByDelimiter, (PowerQueryVariable lastStepVar) => new PowerQueryFunc(MTableFunctionName.SplitColumn.QualifiedName, new FormulaExpression[]
					{
						lastStepVar,
						new PowerQueryStringLiteral(columnToSplit),
						new PowerQueryFunc(MSplitterFunctionName.SplitTextByEachDelimiter.QualifiedName, new FormulaExpression[]
						{
							delimiterList,
							new PowerQueryVariable("QuoteStyle.None"),
							startAtEnd
						}),
						columnNamesOrNumber
					}));
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009EA1 RID: 40609 RVA: 0x0021AC14 File Offset: 0x00218E14
		private string ResolveOutputColumnName(string outputColumnName)
		{
			int num = 1;
			string resolved = outputColumnName;
			Func<string, bool> <>9__0;
			for (;;)
			{
				IEnumerable<string> forbiddenColumnNames = this._constraint.ForbiddenColumnNames;
				if (forbiddenColumnNames == null)
				{
					break;
				}
				IEnumerable<string> enumerable = forbiddenColumnNames;
				Func<string, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (string n) => n.Equals(resolved));
				}
				if (!enumerable.Any(func))
				{
					break;
				}
				resolved = string.Format("{0}.{1}", outputColumnName, num++);
			}
			return resolved;
		}

		// Token: 0x06009EA2 RID: 40610 RVA: 0x0021AC89 File Offset: 0x00218E89
		protected override ITranslation<SplitProgram, FormulaExpression> WrapTranslated(PowerQueryLet translated, IEnumerable<string> outputColumnNames)
		{
			return new PowerQueryTranslation(this._program, translated, this._constraint, new Metadata(outputColumnNames));
		}

		// Token: 0x0400402D RID: 16429
		private readonly PowerQueryTranslationConstraint _constraint;

		// Token: 0x0400402E RID: 16430
		private readonly PowerQueryLetExpressionBuilder _builder;

		// Token: 0x0400402F RID: 16431
		private readonly PowerQueryLetExpressionBuilder _splitFunctionBuilder;

		// Token: 0x04004030 RID: 16432
		private readonly List<string> _splitFunctionOutputColumns = new List<string>();

		// Token: 0x04004031 RID: 16433
		private static readonly string SplitFunctionParam = "cellText";

		// Token: 0x02001413 RID: 5139
		internal class DefaultLocalizedStrings : ILocalizedPowerQueryMStrings, ILocalizedPowerQueryMStrings
		{
			// Token: 0x17001AEC RID: 6892
			// (get) Token: 0x06009EA4 RID: 40612 RVA: 0x0021ACAF File Offset: 0x00218EAF
			public static ILocalizedPowerQueryMStrings Instance { get; } = new PowerQueryTranslator.DefaultLocalizedStrings();

			// Token: 0x06009EA5 RID: 40613 RVA: 0x00002130 File Offset: 0x00000330
			private DefaultLocalizedStrings()
			{
			}

			// Token: 0x17001AED RID: 6893
			// (get) Token: 0x06009EA6 RID: 40614 RVA: 0x0021ACB6 File Offset: 0x00218EB6
			public string AddedColumn
			{
				get
				{
					return "Added Column";
				}
			}

			// Token: 0x17001AEE RID: 6894
			// (get) Token: 0x06009EA7 RID: 40615 RVA: 0x0021ACBD File Offset: 0x00218EBD
			public string RemovedColumn
			{
				get
				{
					return "Removed Column";
				}
			}

			// Token: 0x17001AEF RID: 6895
			// (get) Token: 0x06009EA8 RID: 40616 RVA: 0x0021ACC4 File Offset: 0x00218EC4
			public string Source
			{
				get
				{
					return "Source";
				}
			}

			// Token: 0x17001AF0 RID: 6896
			// (get) Token: 0x06009EA9 RID: 40617 RVA: 0x0021ACCB File Offset: 0x00218ECB
			public string SplitColumnByDelimiter
			{
				get
				{
					return "Split Column by Delimiter";
				}
			}

			// Token: 0x17001AF1 RID: 6897
			// (get) Token: 0x06009EAA RID: 40618 RVA: 0x0021ACD2 File Offset: 0x00218ED2
			public string SplitColumn
			{
				get
				{
					return "Split Column";
				}
			}
		}

		// Token: 0x02001414 RID: 5140
		internal class DefaultEscapePowerQueryM : IEscapePowerQueryM
		{
			// Token: 0x17001AF2 RID: 6898
			// (get) Token: 0x06009EAC RID: 40620 RVA: 0x0021ACE5 File Offset: 0x00218EE5
			public static IEscapePowerQueryM Instance { get; } = new PowerQueryTranslator.DefaultEscapePowerQueryM();

			// Token: 0x06009EAD RID: 40621 RVA: 0x00002130 File Offset: 0x00000330
			private DefaultEscapePowerQueryM()
			{
			}

			// Token: 0x06009EAE RID: 40622 RVA: 0x0021ACEC File Offset: 0x00218EEC
			public string EscapeFieldIdentifier(string fieldIdentifier)
			{
				return PowerQueryLookup.EscapeLookupIdentifier(fieldIdentifier);
			}

			// Token: 0x06009EAF RID: 40623 RVA: 0x0021ACF4 File Offset: 0x00218EF4
			public string EscapeIdentifier(string identifier)
			{
				return PowerQueryVariable.EscapeVariableName(identifier);
			}

			// Token: 0x06009EB0 RID: 40624 RVA: 0x0021ACFC File Offset: 0x00218EFC
			public string EscapeString(string s)
			{
				return PowerQueryStringLiteral.EscapeString(s);
			}
		}
	}
}
