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
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet
{
	// Token: 0x02000DD4 RID: 3540
	public class MLearner : ProgramLearner<MProgram, ISpreadsheetPair, SpreadsheetArea>
	{
		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x060059D4 RID: 22996 RVA: 0x0011D766 File Offset: 0x0011B966
		public static MLearner Instance { get; } = new MLearner();

		// Token: 0x060059D5 RID: 22997 RVA: 0x0011D76D File Offset: 0x0011B96D
		private MLearner()
			: base(true, true)
		{
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x060059D6 RID: 22998 RVA: 0x0011D788 File Offset: 0x0011B988
		public override Feature<double> ScoreFeature { get; } = new RankingScore(Language.Grammar, true);

		// Token: 0x060059D7 RID: 22999 RVA: 0x0011D790 File Offset: 0x0011B990
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

		// Token: 0x060059D8 RID: 23000 RVA: 0x0011D7E8 File Offset: 0x0011B9E8
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
			IReadOnlyList<Example<ISpreadsheetPair, SpreadsheetArea>> readOnlyList4 = readOnlyList.OfType<Example<ISpreadsheetPair, SpreadsheetArea>>().ToList<Example<ISpreadsheetPair, SpreadsheetArea>>();
			Spec spec;
			Func<ComponentBasedSynthesis.LearnerState, Spec, bool> func;
			if (readOnlyList4.Any<Example<ISpreadsheetPair, SpreadsheetArea>>())
			{
				ExampleSpec exSpec = new ExampleSpec(readOnlyList4.ToDictionary((Example<ISpreadsheetPair, SpreadsheetArea> c) => State.CreateForLearning(Language.Grammar.InputSymbol, c.Input), (Example<ISpreadsheetPair, SpreadsheetArea> c) => c.Output));
				spec = exSpec;
				IReadOnlyList<SpreadsheetArea> outputs = spec.ProvidedInputs.Select((State input) => exSpec.Examples[input]).Cast<SpreadsheetArea>().ToList<SpreadsheetArea>();
				func = delegate(ComponentBasedSynthesis.LearnerState learningState, Spec _)
				{
					if (learningState.Program.Symbol != Language.Build.Symbol.mTable)
					{
						return true;
					}
					return learningState.Values.Cast<SpreadsheetArea>().ZipWith(outputs).All2((SpreadsheetArea intermediateTable, SpreadsheetArea output) => intermediateTable.Span.Contains(output.Span));
				};
			}
			else
			{
				spec = new WithInputTopSpec(readOnlyList3);
				readOnlyList3 = null;
				func = null;
			}
			return Record.Create<SynthesisEngine, Spec, IReadOnlyList<State>>(new SynthesisEngine(Language.Grammar, new SynthesisEngine.Config
			{
				Strategies = new ISynthesisStrategy[]
				{
					new ComponentBasedSynthesis(new Symbol[]
					{
						Language.Build.Symbol.index,
						Language.Build.Symbol.wholeSheet,
						Language.Build.Symbol.wholeSheetFull,
						Language.Build.Symbol.trimTop,
						Language.Build.Symbol.area,
						Language.Build.Symbol.uncleanedSheetSection,
						Language.Build.Symbol.sheet
					}, func, null, null)
				},
				LogListener = logListenerIfEnabled,
				UseDynamicSoundnessCheck = options.UseDynamicSoundnessCheck
			}, null), spec, readOnlyList3);
		}

		// Token: 0x060059D9 RID: 23001 RVA: 0x0011D9D8 File Offset: 0x0011BBD8
		protected override ProgramCollection<MProgram, ISpreadsheetPair, SpreadsheetArea, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<ISpreadsheetPair, SpreadsheetArea>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<ISpreadsheetPair> additionalInputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			SynthesisEngine synthesisEngine;
			Spec spec;
			IReadOnlyList<State> readOnlyList;
			this.BuildEngineAndSpec(constraints, additionalInputs).Deconstruct(out synthesisEngine, out spec, out readOnlyList);
			SynthesisEngine synthesisEngine2 = synthesisEngine;
			Spec spec2 = spec;
			IReadOnlyList<State> readOnlyList2 = readOnlyList;
			LearningTask learningTask = new LearningTask(Language.Build.Symbol.mProgram, spec2, k, feature, null)
			{
				AdditionalInputs = readOnlyList2
			};
			ProgramSet programSet = synthesisEngine2.Learn(learningTask, cancel);
			PrunedProgramSet prunedProgramSet = programSet as PrunedProgramSet;
			return ProgramCollection<MProgram, ISpreadsheetPair, SpreadsheetArea, TFeatureValue>.From(prunedProgramSet, delegate(ProgramNode node)
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
				return new MProgram(node, num2);
			}, feature);
		}

		// Token: 0x060059DA RID: 23002 RVA: 0x0011DA68 File Offset: 0x0011BC68
		public override ProgramSet LearnAll(IEnumerable<Constraint<ISpreadsheetPair, SpreadsheetArea>> constraints, IEnumerable<ISpreadsheetPair> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			SynthesisEngine synthesisEngine;
			Spec spec;
			IReadOnlyList<State> readOnlyList;
			this.BuildEngineAndSpec(constraints, additionalInputs).Deconstruct(out synthesisEngine, out spec, out readOnlyList);
			SynthesisEngine synthesisEngine2 = synthesisEngine;
			Spec spec2 = spec;
			IReadOnlyList<State> readOnlyList2 = readOnlyList;
			LearningTask learningTask = new LearningTask(Language.Build.Symbol.mProgram, spec2)
			{
				AdditionalInputs = readOnlyList2
			};
			return synthesisEngine2.Learn(learningTask, cancel);
		}

		// Token: 0x060059DB RID: 23003 RVA: 0x0011DAB4 File Offset: 0x0011BCB4
		public MProgram LearnBestTranslation(Program p, IEnumerable<ISpreadsheetPair> inputs, bool cleanup, CancellationToken cancel = default(CancellationToken))
		{
			IReadOnlyList<Example<ISpreadsheetPair, SpreadsheetArea>> readOnlyList = inputs.Select(delegate(ISpreadsheetPair input)
			{
				SpreadsheetArea spreadsheetArea = p.Run(input);
				if (cleanup)
				{
					spreadsheetArea = Semantics.Trim(p.Run(input));
				}
				spreadsheetArea = spreadsheetArea.WithSpreadsheet(input.WithoutFormatting);
				return new Example<ISpreadsheetPair, SpreadsheetArea>(input, spreadsheetArea, false);
			}).ToList<Example<ISpreadsheetPair, SpreadsheetArea>>();
			return base.Learn(readOnlyList, null, cancel);
		}

		// Token: 0x060059DC RID: 23004 RVA: 0x0011DAF7 File Offset: 0x0011BCF7
		public MProgram LearnBestTranslation(Program p, ISpreadsheetPair input, bool cleanup, CancellationToken cancel = default(CancellationToken))
		{
			return this.LearnBestTranslation(p, new ISpreadsheetPair[] { input }, cleanup, cancel);
		}
	}
}
