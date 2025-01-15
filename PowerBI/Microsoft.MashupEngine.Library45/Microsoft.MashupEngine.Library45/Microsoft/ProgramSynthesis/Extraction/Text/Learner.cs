using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Learning;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Text
{
	// Token: 0x02000EF6 RID: 3830
	public class Learner : ProgramLearner<Program, StringRegion, ITable<StringRegion>>
	{
		// Token: 0x06006835 RID: 26677 RVA: 0x001537B7 File Offset: 0x001519B7
		private Learner()
			: base(false, false)
		{
		}

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06006836 RID: 26678 RVA: 0x001537D1 File Offset: 0x001519D1
		public static Learner Instance { get; } = new Learner();

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06006837 RID: 26679 RVA: 0x001537D8 File Offset: 0x001519D8
		public override Feature<double> ScoreFeature { get; } = new RankingScore(Language.Grammar);

		// Token: 0x06006838 RID: 26680 RVA: 0x001537E0 File Offset: 0x001519E0
		protected override ProgramCollection<Program, StringRegion, ITable<StringRegion>, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<StringRegion, ITable<StringRegion>>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<StringRegion> additionalInputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			ProgramSet programSet = this.LearnImpl<TFeatureValue>(constraints, feature, new int?(k), numRandomProgramsToInclude, samplingStrategy, cancel);
			if (programSet == null || programSet.IsEmpty)
			{
				return ProgramCollection<Program, StringRegion, ITable<StringRegion>, TFeatureValue>.Empty;
			}
			return ProgramCollection<Program, StringRegion, ITable<StringRegion>, TFeatureValue>.From(programSet as PrunedProgramSet, (ProgramNode p) => new Program(p), feature);
		}

		// Token: 0x06006839 RID: 26681 RVA: 0x00153840 File Offset: 0x00151A40
		public override ProgramSet LearnAll(IEnumerable<Constraint<StringRegion, ITable<StringRegion>>> constraints, IEnumerable<StringRegion> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			return this.LearnImpl<double>(constraints, this.ScoreFeature, null, null, ProgramSamplingStrategy.UniformRandom, cancel);
		}

		// Token: 0x0600683A RID: 26682 RVA: 0x00153870 File Offset: 0x00151A70
		private ProgramSet LearnImpl<TFeatureValue>(IEnumerable<Constraint<StringRegion, ITable<StringRegion>>> constraints, Feature<TFeatureValue> feature, int? k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, CancellationToken cancel = default(CancellationToken))
		{
			Grammar grammar = Language.Grammar;
			PrefixTableSpec prefixTableSpec = new PrefixTableSpec((from e in constraints.OfType<Example>()
				group e by e.Input).ToDictionary((IGrouping<StringRegion, Example> g) => State.CreateForLearning(grammar.InputSymbol, g.Key), (IGrouping<StringRegion, Example> g) => Learner.MergeExamples(g.ToList<Example>())));
			Witnesses.Options options = new Witnesses.Options();
			Witnesses witnesses = new Witnesses(Language.Grammar, options);
			LogListener logListenerIfEnabled = options.GetLogListenerIfEnabled(null);
			SynthesisEngine synthesisEngine = new SynthesisEngine(Language.Grammar, new SynthesisEngine.Config
			{
				Strategies = new ISynthesisStrategy[]
				{
					new DeductiveSynthesis(witnesses, null)
				},
				UseThreads = false,
				LogListener = logListenerIfEnabled,
				UseDynamicSoundnessCheck = false
			}, null);
			LearningTask learningTask = ((k != null) ? LearningTask.Create(grammar.StartSymbol, prefixTableSpec, numRandomProgramsToInclude, samplingStrategy, new int?(k.Value), feature, null) : new LearningTask(grammar.StartSymbol, prefixTableSpec));
			ProgramSet programSet = synthesisEngine.Learn(learningTask, cancel);
			options.SaveLogToXMLIfEnabled(logListenerIfEnabled, null);
			if (k != null)
			{
				return programSet.Prune(new int?(k.Value), numRandomProgramsToInclude, feature, null, learningTask.FeatureCalculationContext, samplingStrategy, synthesisEngine.RandomNumberGenerator, synthesisEngine.Configuration.LogListener);
			}
			return programSet;
		}

		// Token: 0x0600683B RID: 26683 RVA: 0x001539F0 File Offset: 0x00151BF0
		private static ITable<object> MergeExamples(IReadOnlyList<Example> examples)
		{
			if (examples.Count == 1)
			{
				return examples.Single<Example>().Output;
			}
			IList<string> list = new List<string>();
			IList<List<ExampleCell>> list2 = new List<List<ExampleCell>>();
			foreach (Example example in examples)
			{
				IList<string> list3 = (example.Output.ColumnNames as IList<string>) ?? example.Output.ColumnNames.ToList<string>();
				for (int i = 0; i < Math.Min(list.Count, list3.Count); i++)
				{
					if (list[i] != list3[i])
					{
						throw new InconsistentColumnNameException(i, list[i], "Inconsistent columns " + list[i] + " and " + list3[i]);
					}
				}
				if (list3.Count > list.Count)
				{
					list = list3;
				}
			}
			foreach (Example example2 in examples)
			{
				ITable<ExampleCell> output = example2.Output;
				IList<IEnumerable<ExampleCell>> list4 = (output.Rows as IList<IEnumerable<ExampleCell>>) ?? output.Rows.ToList<IEnumerable<ExampleCell>>();
				for (int j = 0; j < Math.Min(list2.Count, list4.Count); j++)
				{
					IList<ExampleCell> list5 = list2[j];
					IList<ExampleCell> list6 = (list4[j] as IList<ExampleCell>) ?? list4[j].ToList<ExampleCell>();
					for (int k = 0; k < list6.Count; k++)
					{
						ExampleCell exampleCell = list5[k];
						ExampleCell exampleCell2 = list6[k];
						if (exampleCell.IsUserSpecified)
						{
							if (exampleCell2.IsUserSpecified && exampleCell2.Value != exampleCell.Value)
							{
								throw new InconsistentExampleException(j, k, exampleCell.Value ?? exampleCell2.Value, string.Format("Inconsistent example at ({0}, {1})", j, k));
							}
						}
						else if (exampleCell2.IsUserSpecified)
						{
							list5[k] = exampleCell2;
						}
						else if (exampleCell2.Value != exampleCell.Value)
						{
							string text = ((exampleCell2.Value != null && exampleCell.Value != null) ? null : (exampleCell2.Value ?? exampleCell.Value));
							list5[k] = new ExampleCell(text, false);
						}
					}
				}
				if (list4.Count > list2.Count)
				{
					for (int l = list2.Count; l < list4.Count; l++)
					{
						List<ExampleCell> list7 = list4[l].ToList<ExampleCell>();
						for (int m = list7.Count; m < list.Count; m++)
						{
							list7.Add(new ExampleCell(null, false));
						}
						list2.Add(list7);
					}
				}
			}
			return new Table<ExampleCell>(list, list2, null);
		}
	}
}
