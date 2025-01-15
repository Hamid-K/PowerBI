using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Formula.Exceptions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Contracts;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Features;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Transformation.Formula
{
	// Token: 0x020014D8 RID: 5336
	internal class Learner : ProgramLearner<Program, IRow, object>
	{
		// Token: 0x0600A35C RID: 41820 RVA: 0x002297EA File Offset: 0x002279EA
		internal Learner(LearnDebugTrace debugTrace, RankingDebugTrace rankingDebugTrace, ILogger logger)
			: base(true, true)
		{
			this._debugTrace = debugTrace;
			this._rankingDebugTrace = rankingDebugTrace;
			this._logger = logger;
			this.ScoreFeature = new RankingScoreFeature(Language.Grammar, this._rankingDebugTrace);
		}

		// Token: 0x0600A35D RID: 41821 RVA: 0x0022981F File Offset: 0x00227A1F
		internal Learner()
			: this(null, null, null)
		{
		}

		// Token: 0x17001C8E RID: 7310
		// (get) Token: 0x0600A35E RID: 41822 RVA: 0x0022982A File Offset: 0x00227A2A
		// (set) Token: 0x0600A35F RID: 41823 RVA: 0x00229832 File Offset: 0x00227A32
		public LearnConfidenceResult LearnConfidence { get; private set; }

		// Token: 0x17001C8F RID: 7311
		// (get) Token: 0x0600A360 RID: 41824 RVA: 0x0022983B File Offset: 0x00227A3B
		public override Feature<double> ScoreFeature { get; }

		// Token: 0x0600A361 RID: 41825 RVA: 0x00229844 File Offset: 0x00227A44
		public override IFeatureOptions GetFeatureOptionsFor(IEnumerable<Constraint<IRow, object>> constraints, IEnumerable<IRow> additionalInputs = null)
		{
			List<Constraint<IRow, object>> list = constraints.ToList<Constraint<IRow, object>>();
			if (list.OfType<IOptionConstraint<LearnOptions>>().None<IOptionConstraint<LearnOptions>>())
			{
				list.Add(new LearnConstraint());
			}
			IReadOnlyList<Example> readOnlyList = list.OfType<Example>().ToList<Example>();
			IReadOnlyList<IRow> readOnlyList2 = additionalInputs.ToReadOnlyList<IRow>();
			LearnOptions learnOptions = Learner.ResolveLearnOptions(list, readOnlyList2);
			return new RankingScoreFeatureOptions
			{
				ColumnNamePriority = (learnOptions.EnableDefaultColumnNamePriority ? Learner.ResolveColumnNamePriority(readOnlyList, readOnlyList2, learnOptions.ColumnNamePriority) : learnOptions.ColumnNamePriority),
				DataCultures = learnOptions.DataCultures,
				Examples = readOnlyList,
				Inputs = readOnlyList2
			};
		}

		// Token: 0x0600A362 RID: 41826 RVA: 0x002298D0 File Offset: 0x00227AD0
		public override ProgramSet LearnAll(IEnumerable<Constraint<IRow, object>> constraints, IEnumerable<IRow> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			IReadOnlyList<Constraint<IRow, object>> readOnlyList = constraints.ToReadOnlyList<Constraint<IRow, object>>();
			IReadOnlyList<IRow> readOnlyList2 = additionalInputs.ToReadOnlyList<IRow>();
			return this.LearnCore(readOnlyList, this.ScoreFeature, null, null, ProgramSamplingStrategy.UniformRandom, cancel, readOnlyList2);
		}

		// Token: 0x0600A363 RID: 41827 RVA: 0x00229910 File Offset: 0x00227B10
		protected override ProgramCollection<Program, IRow, object, TScoreFeatureValue> LearnTopKUnchecked<TScoreFeatureValue>(IEnumerable<Constraint<IRow, object>> constraints, Feature<TScoreFeatureValue> scoreFeature, int programCount, int? randomProgramCount, ProgramSamplingStrategy samplingStrategy, IEnumerable<IRow> additionalInputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			Learner.<>c__DisplayClass15_0<TScoreFeatureValue> CS$<>8__locals1 = new Learner.<>c__DisplayClass15_0<TScoreFeatureValue>();
			CS$<>8__locals1.scoreFeature = scoreFeature;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cancel = cancel;
			CS$<>8__locals1.inputs = additionalInputs.ToReadOnlyList<IRow>();
			CS$<>8__locals1.iconstraints = constraints.ToReadOnlyList<Constraint<IRow, object>>();
			CS$<>8__locals1.programSet = this.LearnCore(CS$<>8__locals1.iconstraints, CS$<>8__locals1.scoreFeature, new int?(programCount), randomProgramCount, samplingStrategy, CS$<>8__locals1.cancel, CS$<>8__locals1.inputs) as PrunedProgramSet;
			if (CS$<>8__locals1.programSet == null || CS$<>8__locals1.programSet.IsEmpty)
			{
				return ProgramCollection<Program, IRow, object, TScoreFeatureValue>.Empty;
			}
			if (programCount != 1)
			{
				return new ProgramCollection<Program, IRow, object, TScoreFeatureValue>(CS$<>8__locals1.<LearnTopKUnchecked>g__TransformPrograms|1(CS$<>8__locals1.programSet.TopPrograms), CS$<>8__locals1.<LearnTopKUnchecked>g__TransformPrograms|1(CS$<>8__locals1.programSet.RandomlySampledPrograms), CS$<>8__locals1.scoreFeature, CS$<>8__locals1.programSet.FeatureCalculationContext);
			}
			if (CS$<>8__locals1.programSet.TopPrograms.FirstOrDefault<ProgramNode>() == null)
			{
				return ProgramCollection<Program, IRow, object, TScoreFeatureValue>.Empty;
			}
			return ProgramCollection<Program, IRow, object, TScoreFeatureValue>.From(CS$<>8__locals1.programSet, new Func<ProgramNode, Program>(CS$<>8__locals1.<LearnTopKUnchecked>g__TransformProgram|0), CS$<>8__locals1.scoreFeature);
		}

		// Token: 0x0600A364 RID: 41828 RVA: 0x00229A18 File Offset: 0x00227C18
		private ProgramSet LearnCore(IEnumerable<Constraint<IRow, object>> constraints, IFeature feature, int? programCount, int? randomProgramCount, ProgramSamplingStrategy samplingStrategy, CancellationToken cancel = default(CancellationToken), IEnumerable<IRow> additionalInputs = null)
		{
			IReadOnlyList<IRow> iadditionalInputs = (additionalInputs as IReadOnlyList<IRow>) ?? ((additionalInputs != null) ? additionalInputs.ToList<IRow>() : null);
			List<Constraint<IRow, object>> list = constraints.ToList<Constraint<IRow, object>>();
			List<Example> list2 = list.OfType<Example>().ToList<Example>();
			IReadOnlyList<LearnConstraint> readOnlyList = list.OfType<LearnConstraint>().ToReadOnlyList<LearnConstraint>();
			if (readOnlyList.Count > 1)
			{
				throw new TooManyLearnConstraintsException(string.Format("{0}: {1}", "LearnConstraint", readOnlyList.Count));
			}
			IReadOnlyList<ConditionalLearnConstraint> readOnlyList2 = list.OfType<ConditionalLearnConstraint>().ToReadOnlyList<ConditionalLearnConstraint>();
			if (readOnlyList2.Count > 1)
			{
				throw new TooManyLearnConstraintsException(string.Format("{0}: {1}", "ConditionalLearnConstraint", readOnlyList2.Count));
			}
			if (!list2.Any<Example>())
			{
				throw new NoExamplesProvidedException();
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			ProgramSet programSet2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Learner", "LearnCore", false, true) : null))
			{
				object obj = readOnlyList.None<LearnConstraint>() && readOnlyList2.Any<ConditionalLearnConstraint>();
				Func<IEnumerable<Example<IRow, object>>, ConditionalBranchMeta> func = null;
				object obj2 = obj;
				if (obj2 == null)
				{
					LearnConstraint learnConstraint = readOnlyList.FirstOrDefault<LearnConstraint>();
					if (learnConstraint == null)
					{
						learnConstraint = new LearnConstraint();
						list.Add(learnConstraint);
					}
					func = (IEnumerable<Example<IRow, object>> clusterExamples) => this.LearnCoditionalBranch(learnConstraint.Conditional ?? new ConditionalLearnConstraint(learnConstraint), clusterExamples, iadditionalInputs, cancel);
				}
				LearnOptions learnOptions = Learner.ResolveLearnOptions(list, iadditionalInputs);
				Recognition recognition = new Recognition(list2, learnOptions, this._debugTrace, cancel);
				bool flag = obj2 == 0;
				if (flag)
				{
					LearnConfidenceBehavior learnConfidence = learnOptions.LearnConfidence;
					bool flag2 = learnConfidence - LearnConfidenceBehavior.Predict <= 1;
					flag = flag2;
				}
				if (flag)
				{
					this.LearnConfidence = LearnConfidenceFactory.Compute(recognition, new CancellationToken?(cancel), this._debugTrace);
					double? score = this.LearnConfidence.Score;
					LearnConfidenceReason reason = this.LearnConfidence.Reason;
					if (learnOptions.LearnConfidence == LearnConfidenceBehavior.Suppress)
					{
						if (score != null)
						{
							double? num = score;
							double minLearnConfidence = learnOptions.MinLearnConfidence;
							if ((num.GetValueOrDefault() < minLearnConfidence) & (num != null))
							{
								return ProgramSet.Empty(Learner._grammar.StartSymbol);
							}
						}
						if (score == null)
						{
							if (reason == LearnConfidenceReason.OutputType && !learnOptions.EnableConditional)
							{
								return ProgramSet.Empty(Learner._grammar.StartSymbol);
							}
							flag = reason - LearnConfidenceReason.TooManyColumns <= 1;
							if (flag)
							{
								return ProgramSet.Empty(Learner._grammar.StartSymbol);
							}
						}
					}
				}
				Witness witness = new Witness(Learner._grammar, learnOptions, recognition, list2, iadditionalInputs, cancel, func, this._debugTrace);
				SynthesisEngine synthesisEngine = new SynthesisEngine(Learner._grammar, new SynthesisEngine.Config
				{
					Strategies = new ISynthesisStrategy[]
					{
						new DeductiveSynthesis(witness, null)
					},
					UseThreads = false
				}, null);
				IFeatureOptions featureOptionsFor = this.GetFeatureOptionsFor(list, iadditionalInputs);
				Dictionary<State, object> dictionary = new Dictionary<State, object>();
				foreach (Example example in list2)
				{
					State state = State.CreateForLearning(Learner._grammar.InputSymbol, example.Input);
					if (!dictionary.ContainsKey(state))
					{
						dictionary.Add(state, example.Output);
					}
				}
				ExampleSpec exampleSpec = new ExampleSpec(dictionary);
				LearningTask learningTask = LearningTask.Create(Learner._grammar.StartSymbol, exampleSpec, randomProgramCount, samplingStrategy, programCount, feature, featureOptionsFor);
				if (additionalInputs != null)
				{
					learningTask.AdditionalInputs = iadditionalInputs.Select((IRow input) => State.CreateForLearning(Learner._grammar.InputSymbol, input)).ToList<State>();
				}
				ProgramSet programSet = synthesisEngine.Learn(learningTask, cancel);
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				LogListener logListener = synthesisEngine.Configuration.LogListener;
				if (logListener != null)
				{
					logListener.SaveLogToXML("log.xml");
				}
				if (programCount != null)
				{
					programSet2 = programSet.Prune(new int?(programCount.Value), randomProgramCount, feature, featureOptionsFor, learningTask.FeatureCalculationContext, samplingStrategy, synthesisEngine.RandomNumberGenerator, synthesisEngine.Configuration.LogListener);
				}
				else
				{
					programSet2 = programSet;
				}
			}
			return programSet2;
		}

		// Token: 0x0600A365 RID: 41829 RVA: 0x00229E84 File Offset: 0x00228084
		private static IReadOnlyList<string> ResolveColumnNamePriority(IEnumerable<Example> examples, IEnumerable<IRow> inputs, IEnumerable<string> learnColumnNamePriority)
		{
			List<ColumnDetail> list = examples.Select((Example e) => e.Input).Concat(inputs).InputColumnDetails(null)
				.ToList<ColumnDetail>();
			List<string> list2;
			if ((list2 = ((learnColumnNamePriority != null) ? learnColumnNamePriority.ToList<string>() : null)) == null)
			{
				list2 = (from d in list
					select d.Name into name
					orderby name
					select name).ToList<string>();
			}
			List<string> inconsistentColumnNames = (from detail in list
				let emptyCount = detail.Values.Count(delegate(object value)
				{
					if (value != null)
					{
						string text = value as string;
						if (text == null || !(text == ""))
						{
							return false;
						}
					}
					return true;
				})
				where emptyCount > 0
				orderby emptyCount
				select detail.Name).ToList<string>();
			List<string> list3 = list2;
			list3.RemoveAll((string c) => inconsistentColumnNames.Contains(c));
			list3.AddRange(inconsistentColumnNames);
			return list3;
		}

		// Token: 0x0600A366 RID: 41830 RVA: 0x00229FE8 File Offset: 0x002281E8
		private ConditionalBranchMeta LearnCoditionalBranch(Constraint<IRow, object> conditionalLearnConstraint, IEnumerable<Example<IRow, object>> clusterExamples, IEnumerable<IRow> additionalInputs, CancellationToken cancellationToken)
		{
			IEnumerable<Constraint<IRow, object>> enumerable = clusterExamples.AppendItem(conditionalLearnConstraint).ToReadOnlyList<Constraint<IRow, object>>();
			Program program = base.Learn(enumerable, additionalInputs, cancellationToken);
			if (program == null)
			{
				return null;
			}
			IReadOnlyList<LetX> readOnlyList = program.OfType<LetX>().ToReadOnlyList<LetX>();
			IEnumerable<ConditionalBranchDelimiterInfo> enumerable2 = from letNode in readOnlyList
				let fromStr = letNode.Descendents(program).FirstOrDefault<FromStr>()
				where !fromStr.IsDefault<FromStr>()
				let split = letNode.Descendents(program).FirstOrDefault<Split>()
				where !split.IsDefault<Split>()
				let instance = split.splitInstance.Value
				select new ConditionalBranchDelimiterInfo
				{
					ColumnName = fromStr.columnName.Value,
					Value = split.splitDelimiter.Value,
					MinimumCount = Math.Abs(instance) - 1
				};
			IEnumerable<ConditionalBranchDelimiterInfo> enumerable3 = from letNode in readOnlyList
				let fromStr = letNode.Descendents(program).FirstOrDefault<FromStr>()
				where !fromStr.IsDefault<FromStr>()
				let find = letNode.Descendents(program).FirstOrDefault<Find>()
				where !find.IsDefault<Find>()
				select new ConditionalBranchDelimiterInfo
				{
					ColumnName = fromStr.columnName.Value,
					Value = find.findDelimiter.Value,
					MinimumCount = Math.Abs(find.findInstance.Value)
				};
			return new ConditionalBranchMeta
			{
				Program = program,
				HasWholeColumnOutput = program.Meta.WholeColumnOutput.GetValueOrDefault(),
				UsedColumnNames = program.Meta.ColumnsUsed,
				Delimiters = enumerable2.Union(enumerable3).ToList<ConditionalBranchDelimiterInfo>()
			};
		}

		// Token: 0x0600A367 RID: 41831 RVA: 0x0022A1D4 File Offset: 0x002283D4
		internal static LearnOptions ResolveLearnOptions(IEnumerable<Constraint<IRow, object>> constraints, IEnumerable<IRow> additionalInputs)
		{
			IReadOnlyList<Constraint<IRow, object>> readOnlyList = constraints.ToReadOnlyList<Constraint<IRow, object>>();
			IReadOnlyList<Example> readOnlyList2 = readOnlyList.OfType<Example>().ToList<Example>();
			IReadOnlyList<IRow> readOnlyList3 = additionalInputs.ToReadOnlyList<IRow>();
			LearnOptions learnOptions = new LearnOptions();
			IOptionConstraint<LearnOptions> optionConstraint = readOnlyList.OfType<IOptionConstraint<LearnOptions>>().SingleOrDefault<IOptionConstraint<LearnOptions>>();
			if (optionConstraint == null)
			{
				return null;
			}
			optionConstraint.SetOptions(learnOptions);
			learnOptions.ColumnNamePriority = (learnOptions.EnableDefaultColumnNamePriority ? Learner.ResolveColumnNamePriority(readOnlyList2, readOnlyList3, learnOptions.ColumnNamePriority) : learnOptions.ColumnNamePriority);
			return learnOptions;
		}

		// Token: 0x0400423A RID: 16954
		private static readonly Grammar _grammar = Language.Grammar;

		// Token: 0x0400423B RID: 16955
		private readonly RankingDebugTrace _rankingDebugTrace;

		// Token: 0x0400423C RID: 16956
		private readonly LearnDebugTrace _debugTrace;

		// Token: 0x0400423E RID: 16958
		private readonly ILogger _logger;
	}
}
