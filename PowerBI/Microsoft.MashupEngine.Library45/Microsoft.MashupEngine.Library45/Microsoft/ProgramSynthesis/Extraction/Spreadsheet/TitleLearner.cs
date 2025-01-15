using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Learning;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet
{
	// Token: 0x02000DE2 RID: 3554
	public class TitleLearner : ProgramLearner<TitleProgram, ISpreadsheetPair, SpreadsheetArea>
	{
		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x06005A1D RID: 23069 RVA: 0x0011F161 File Offset: 0x0011D361
		public static TitleLearner Instance { get; } = new TitleLearner();

		// Token: 0x06005A1E RID: 23070 RVA: 0x0011F168 File Offset: 0x0011D368
		private TitleLearner()
			: base(true, true)
		{
		}

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x06005A1F RID: 23071 RVA: 0x0011F183 File Offset: 0x0011D383
		public override Feature<double> ScoreFeature { get; } = new RankingScore(Language.Grammar, false);

		// Token: 0x06005A20 RID: 23072 RVA: 0x0011F18C File Offset: 0x0011D38C
		private Witnesses.Options InterpretConstraints(IEnumerable<Constraint<ISpreadsheetPair, SpreadsheetArea>> constraints)
		{
			Witnesses.Options options = new Witnesses.Options();
			foreach (Constraint<ISpreadsheetPair, SpreadsheetArea> constraint in constraints)
			{
				IOptionConstraint<Witnesses.Options> optionConstraint = constraint as IOptionConstraint<Witnesses.Options>;
				if (optionConstraint != null)
				{
					optionConstraint.SetOptions(options);
				}
			}
			return options;
		}

		// Token: 0x06005A21 RID: 23073 RVA: 0x0011F1E4 File Offset: 0x0011D3E4
		private Record<SynthesisEngine, Spec, IReadOnlyList<State>> BuildEngineAndSpec(IEnumerable<Constraint<ISpreadsheetPair, SpreadsheetArea>> constraints, IEnumerable<ISpreadsheetPair> additionalInputs)
		{
			IReadOnlyList<Constraint<ISpreadsheetPair, SpreadsheetArea>> readOnlyList = constraints.ToList<Constraint<ISpreadsheetPair, SpreadsheetArea>>();
			Witnesses.Options options = this.InterpretConstraints(readOnlyList);
			LogListener logListenerIfEnabled = options.GetLogListenerIfEnabled(null);
			IReadOnlyList<State> readOnlyList2;
			if (additionalInputs == null)
			{
				readOnlyList2 = null;
			}
			else
			{
				readOnlyList2 = additionalInputs.Select((ISpreadsheetPair input) => State.CreateForLearning(Language.Grammar.InputSymbol, input)).ToList<State>();
			}
			IReadOnlyList<State> readOnlyList3 = readOnlyList2;
			if (readOnlyList.OfType<Example<ISpreadsheetPair, SpreadsheetArea>>().ToList<Example<ISpreadsheetPair, SpreadsheetArea>>().Any<Example<ISpreadsheetPair, SpreadsheetArea>>())
			{
				throw new NotImplementedException("Examples do not make sense for TitleLearner.");
			}
			Spec spec = new WithInputTopSpec(readOnlyList3);
			readOnlyList3 = null;
			IReadOnlyList<ProgramNode> initialPrograms = (from t in readOnlyList.OfType<TitleLearner.TitleForConstraint>()
				select t.Program.ProgramNode).ToList<ProgramNode>();
			Grammar grammar = Language.Grammar;
			SynthesisEngine.Config config = new SynthesisEngine.Config();
			SynthesisEngine.Config config2 = config;
			ISynthesisStrategy[] array = new ISynthesisStrategy[1];
			int num = 0;
			IEnumerable<Symbol> enumerable = new Symbol[]
			{
				Language.Build.Symbol.index,
				Language.Build.Symbol.wholeSheet,
				Language.Build.Symbol.wholeSheetFull,
				Language.Build.Symbol.trim,
				Language.Build.Symbol.trimTop,
				Language.Build.Symbol.trimBottom,
				Language.Build.Symbol.trimLeft,
				Language.Build.Symbol.area,
				Language.Build.Symbol.uncleanedSheetSection,
				Language.Build.Symbol.sheetSection,
				Language.Build.Symbol.sheetSplits,
				Language.Build.Symbol.horizontalSheetSection,
				Language.Build.Symbol.horizontalSheetSplits,
				Language.Build.Symbol.verticalSheetSection,
				Language.Build.Symbol.verticalSheetSplits,
				Language.Build.Symbol.sheet,
				Language.Build.Symbol.sheetPair,
				Language.Build.Symbol.output
			};
			Func<ComponentBasedSynthesis.LearnerState, Spec, bool> func = null;
			Dictionary<TerminalRule, Func<object, IEnumerable<object>>> dictionary = new Dictionary<TerminalRule, Func<object, IEnumerable<object>>>();
			TerminalRule terminalRule = Language.Build.Symbol.styleFilter.TerminalRule;
			dictionary[terminalRule] = delegate(object obj)
			{
				IEnumerable<StyleFilter> enumerable2 = Learner.GenerateStyleFilters((ISpreadsheetPair)obj);
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				string text = null;
				AxisAligned<string> axisAligned = new AxisAligned<string>("center", null);
				return enumerable2.AppendItem(new StyleFilter(flag, flag2, flag3, flag4, flag5, text, null, axisAligned));
			};
			array[num] = new ComponentBasedSynthesis(enumerable, func, dictionary, (Spec _) => initialPrograms);
			config2.Strategies = array;
			config.LogListener = logListenerIfEnabled;
			config.UseDynamicSoundnessCheck = options.UseDynamicSoundnessCheck;
			return Record.Create<SynthesisEngine, Spec, IReadOnlyList<State>>(new SynthesisEngine(grammar, config, null), spec, readOnlyList3);
		}

		// Token: 0x06005A22 RID: 23074 RVA: 0x0011F494 File Offset: 0x0011D694
		public override ProgramSet LearnAll(IEnumerable<Constraint<ISpreadsheetPair, SpreadsheetArea>> constraints, IEnumerable<ISpreadsheetPair> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			SynthesisEngine synthesisEngine;
			Spec spec;
			IReadOnlyList<State> readOnlyList;
			this.BuildEngineAndSpec(constraints, additionalInputs).Deconstruct(out synthesisEngine, out spec, out readOnlyList);
			SynthesisEngine synthesisEngine2 = synthesisEngine;
			Spec spec2 = spec;
			IReadOnlyList<State> readOnlyList2 = readOnlyList;
			LearningTask learningTask = new LearningTask(Language.Build.Symbol.startTitle, spec2)
			{
				AdditionalInputs = readOnlyList2
			};
			return synthesisEngine2.Learn(learningTask, cancel);
		}

		// Token: 0x06005A23 RID: 23075 RVA: 0x0011F4E0 File Offset: 0x0011D6E0
		protected override ProgramCollection<TitleProgram, ISpreadsheetPair, SpreadsheetArea, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<ISpreadsheetPair, SpreadsheetArea>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<ISpreadsheetPair> additionalInputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			SynthesisEngine synthesisEngine;
			Spec spec;
			IReadOnlyList<State> readOnlyList;
			this.BuildEngineAndSpec(constraints, additionalInputs).Deconstruct(out synthesisEngine, out spec, out readOnlyList);
			SynthesisEngine synthesisEngine2 = synthesisEngine;
			Spec spec2 = spec;
			IReadOnlyList<State> readOnlyList2 = readOnlyList;
			LearningTask learningTask = new LearningTask(Language.Build.Symbol.startTitle, spec2, k, feature, null)
			{
				AdditionalInputs = readOnlyList2
			};
			ProgramSet programSet = synthesisEngine2.Learn(learningTask, cancel);
			PrunedProgramSet prunedProgramSet = programSet as PrunedProgramSet;
			return ProgramCollection<TitleProgram, ISpreadsheetPair, SpreadsheetArea, TFeatureValue>.From(prunedProgramSet, delegate(ProgramNode node)
			{
				Feature<TFeatureValue> feature2 = feature;
				PrunedProgramSet prunedProgramSet2 = prunedProgramSet;
				TFeatureValue featureValue = node.GetFeatureValue<TFeatureValue>(feature2, (prunedProgramSet2 != null) ? prunedProgramSet2.FeatureCalculationContext.WithProgramNode(node) : null);
				double num2;
				if (featureValue is double)
				{
					double num = featureValue as double;
					num2 = num;
				}
				else
				{
					num2 = 0.0;
				}
				return new TitleProgram(node, num2, null);
			}, feature);
		}

		// Token: 0x06005A24 RID: 23076 RVA: 0x0011F56F File Offset: 0x0011D76F
		public IEnumerable<TitleProgram> LearnAllFor(Program program, ISpreadsheetPair input, CancellationToken cancel = default(CancellationToken))
		{
			return base.LearnTopK(new TitleLearner.TitleForConstraint[]
			{
				new TitleLearner.TitleForConstraint(program)
			}, 1000, new ISpreadsheetPair[] { input }, cancel).TopPrograms;
		}

		// Token: 0x06005A25 RID: 23077 RVA: 0x0011F59C File Offset: 0x0011D79C
		public TitleProgram LearnFor(Program program, ISpreadsheetPair input, CancellationToken cancel = default(CancellationToken))
		{
			TitleProgram titleProgram = base.Learn(new TitleLearner.TitleForConstraint[]
			{
				new TitleLearner.TitleForConstraint(program)
			}, new ISpreadsheetPair[] { input }, cancel);
			if (titleProgram == null || titleProgram.Score < 0.0)
			{
				return null;
			}
			return titleProgram;
		}

		// Token: 0x04002A43 RID: 10819
		private const int MaxTitleProgramsToLearn = 1000;

		// Token: 0x02000DE3 RID: 3555
		private class TitleForConstraint : Constraint<ISpreadsheetPair, SpreadsheetArea>
		{
			// Token: 0x17001078 RID: 4216
			// (get) Token: 0x06005A27 RID: 23079 RVA: 0x0011F5F3 File Offset: 0x0011D7F3
			public Program Program { get; }

			// Token: 0x06005A28 RID: 23080 RVA: 0x0011F5FB File Offset: 0x0011D7FB
			public TitleForConstraint(Program program)
			{
				this.Program = program;
			}

			// Token: 0x06005A29 RID: 23081 RVA: 0x0011F60A File Offset: 0x0011D80A
			public override bool ConflictsWith(Constraint<ISpreadsheetPair, SpreadsheetArea> other)
			{
				return other is TitleLearner.TitleForConstraint && !other.Equals(this);
			}

			// Token: 0x06005A2A RID: 23082 RVA: 0x0011F620 File Offset: 0x0011D820
			public override bool Equals(Constraint<ISpreadsheetPair, SpreadsheetArea> other)
			{
				TitleLearner.TitleForConstraint titleForConstraint = other as TitleLearner.TitleForConstraint;
				return titleForConstraint != null && titleForConstraint.Program.Equals(this.Program);
			}

			// Token: 0x06005A2B RID: 23083 RVA: 0x0011F64A File Offset: 0x0011D84A
			public override int GetHashCode()
			{
				return this.Program.GetHashCode();
			}

			// Token: 0x06005A2C RID: 23084 RVA: 0x0011F657 File Offset: 0x0011D857
			public override bool Valid(Program<ISpreadsheetPair, SpreadsheetArea> program)
			{
				return program.ProgramNode.ContainsSubProgram(this.Program.ProgramNode);
			}
		}
	}
}
