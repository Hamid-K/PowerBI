using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.DslLibrary.EntityDetectors;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Text.Description;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Visitors;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Exceptions;

namespace Microsoft.ProgramSynthesis.Transformation.Text
{
	// Token: 0x02001B81 RID: 7041
	public class Learner : ProgramLearner<Program, IRow, object>
	{
		// Token: 0x17002683 RID: 9859
		// (get) Token: 0x0600E6C7 RID: 59079 RVA: 0x0030DEC6 File Offset: 0x0030C0C6
		public static Learner Instance { get; } = new Learner(new RankingScore(Language.Grammar, new RankingScoreModel(), true, null));

		// Token: 0x17002684 RID: 9860
		// (get) Token: 0x0600E6C8 RID: 59080 RVA: 0x0030DECD File Offset: 0x0030C0CD
		public override Feature<double> ScoreFeature { get; }

		// Token: 0x17002685 RID: 9861
		// (get) Token: 0x0600E6C9 RID: 59081 RVA: 0x0030DED5 File Offset: 0x0030C0D5
		public Dictionary<ConcatLocation, LogListener> LastLearnAllSingleBranchLogs { get; } = new Dictionary<ConcatLocation, LogListener>();

		// Token: 0x17002686 RID: 9862
		// (get) Token: 0x0600E6CA RID: 59082 RVA: 0x0030DEDD File Offset: 0x0030C0DD
		public Dictionary<string, LogListener> LastLearnAllConditionalsLogs { get; } = new Dictionary<string, LogListener>();

		// Token: 0x0600E6CB RID: 59083 RVA: 0x0030DEE5 File Offset: 0x0030C0E5
		internal Learner(RankingScore scoreFeature)
			: base(true, true)
		{
			this.ScoreFeature = scoreFeature;
		}

		// Token: 0x0600E6CC RID: 59084 RVA: 0x0030DF0C File Offset: 0x0030C10C
		private static ProgramSetBuilder<@switch> LearnExternalEntityExtraction(ExternalEntityExtraction constraint)
		{
			externalExtractor externalExtractor = Learner.Rule.externalExtractor(constraint.Extractor);
			@switch @switch = Learner.Rule.SingleBranch(Learner.Rule.Transformation(Learner.Rule.Atom(Learner.Rule.LetColumnName(Learner.Rule.idx(constraint.ColumnName), Learner.Rule.LetX(Learner.Rule.ChooseInput(Learner.Var.vs, Learner.Var.columnName), Learner.Rule.SubString(Learner.Rule.SubStr(Learner.Var.x, Learner.Rule.ExternalExtractorPositionPair(Learner.Var.x, externalExtractor, Learner.Rule.k(constraint.K)))))))));
			return ProgramSetBuilder.List<@switch>(new @switch[] { @switch });
		}

		// Token: 0x0600E6CD RID: 59085 RVA: 0x0030DFE4 File Offset: 0x0030C1E4
		private ProgramSet LearnAllImpl(IEnumerable<Constraint<IRow, object>> trainingConstraints, out FeatureCalculationContext fcc, IEnumerable<IRow> additionalInputs = null, int? k = null, IFeature feature = null, int? randomK = null, ProgramSamplingStrategy samplingStrategy = ProgramSamplingStrategy.UniformRandom, CancellationToken cancel = default(CancellationToken))
		{
			this.LastLearnAllConditionalsLogs.Clear();
			this.LastLearnAllSingleBranchLogs.Clear();
			base.SpecsGeneratedFromLastLearn = null;
			Learner.PreprocessedConstraints preprocessedConstraints = new Learner.PreprocessedConstraints(trainingConstraints, additionalInputs);
			fcc = null;
			if (preprocessedConstraints.DoExternalEntityExtraction)
			{
				return Learner.LearnExternalEntityExtraction(preprocessedConstraints.Constraints.First<Constraint<IRow, object>>() as ExternalEntityExtraction).Set;
			}
			if (preprocessedConstraints.ReturnEmpty)
			{
				return ProgramSet.Empty(Language.Grammar.StartSymbol);
			}
			Spec spec = Learner.CreateSpec(preprocessedConstraints.VsrTrainingExamplesList, preprocessedConstraints.PrefixOfOutputConstraints, preprocessedConstraints.SampledAdditionalInputStates, preprocessedConstraints.AllowedTokens);
			Learner.CheckConstantOutput(preprocessedConstraints.VsrTrainingExamplesList, preprocessedConstraints.Options);
			IReadOnlyList<IRow> readOnlyList = preprocessedConstraints.SampledAdditionalInputStates.Select((State state) => state[Language.Grammar.InputSymbol] as IRow).ToList<IRow>();
			bool flag = (preprocessedConstraints.Options.AllowedTransformations & TransformationKind.IfThenElse) > TransformationKind.Unknown;
			preprocessedConstraints.Options.ForbidTransformations(new TransformationKind[] { TransformationKind.IfThenElse });
			LearningTask learningTask = this.CreateLearningTaskFor(Learner.Sym.@switch, spec, preprocessedConstraints.Options, preprocessedConstraints.SampledAdditionalInputStates, readOnlyList, preprocessedConstraints.FilterConstraints, preprocessedConstraints.TrainingConstraintsList, k, randomK, samplingStrategy, feature);
			fcc = learningTask.FeatureCalculationContext;
			ProgramSet programSet = this.LearnAllSingleBranch(learningTask, preprocessedConstraints.ColumnPriority, preprocessedConstraints.FilterConstraints, preprocessedConstraints.MergeColumns, preprocessedConstraints.Options, preprocessedConstraints.PluggableExternalTokens, cancel);
			if (preprocessedConstraints.PrefixOfOutputConstraints.Any<PrefixOfOutputConstraint>())
			{
				return programSet.AddConversionRules(Language.Grammar.StartSymbol);
			}
			if (flag && ProgramSet.IsNullOrEmpty(programSet))
			{
				if (preprocessedConstraints.MergeConstraints.All((MergeColumns c) => c.IsSoft))
				{
					preprocessedConstraints.Options.AllowTransformations(new TransformationKind[] { TransformationKind.IfThenElse });
					LearningTask learningTask2 = this.CreateLearningTaskFor(Learner.Sym.ite, spec, preprocessedConstraints.Options, preprocessedConstraints.AllAdditionalInputStates, preprocessedConstraints.AdditionalInputsList, preprocessedConstraints.FilterConstraints, preprocessedConstraints.TrainingConstraintsList, k, randomK, samplingStrategy, feature);
					programSet = this.LearnAllConditionals(learningTask2, preprocessedConstraints.ColumnPriority, preprocessedConstraints.FilterConstraints, preprocessedConstraints.Options, cancel);
				}
			}
			if (ProgramSet.IsNullOrEmpty(programSet))
			{
				programSet = this.LearnLookup(learningTask, preprocessedConstraints.ColumnPriority, preprocessedConstraints.FilterConstraints, preprocessedConstraints.Options, k, cancel);
			}
			if (ProgramSet.IsNullOrEmpty(programSet) && preprocessedConstraints.HasSoftConstraints)
			{
				cancel.ThrowIfCancellationRequested();
				return this.LearnAllImpl(preprocessedConstraints.HardConstraints, out fcc, preprocessedConstraints.AdditionalInputsList, k, feature, randomK, samplingStrategy, cancel);
			}
			return programSet.AddConversionRules(Language.Grammar.StartSymbol);
		}

		// Token: 0x0600E6CE RID: 59086 RVA: 0x0030E285 File Offset: 0x0030C485
		public Learner.PreprocessedConstraints PreprocessConstraints(IEnumerable<Constraint<IRow, object>> trainingConstraints, IEnumerable<IRow> additionalInputs)
		{
			return new Learner.PreprocessedConstraints(trainingConstraints, additionalInputs);
		}

		// Token: 0x0600E6CF RID: 59087 RVA: 0x0030E290 File Offset: 0x0030C490
		private static Spec CreateSpec(List<Example<IRow, object>> vsrTrainingExamplesList, IReadOnlyList<PrefixOfOutputConstraint> prefixOfOutputConstraints, IReadOnlyList<State> onlyAdditionalInputStates, IReadOnlyDictionary<string, Token> allowedTokens)
		{
			Spec spec;
			if (vsrTrainingExamplesList.Any<Example<IRow, object>>())
			{
				Dictionary<State, object> dictionary = vsrTrainingExamplesList.ToDictionary((Example<IRow, object> t) => t.Input.AsStateForLearning(), delegate(Example<IRow, object> t)
				{
					string text = (string)t.Output;
					IReadOnlyDictionary<string, Token> allowedTokens2 = allowedTokens;
					return ValueSubstring.Create(text, null, null, null, allowedTokens2);
				});
				foreach (PrefixOfOutputConstraint prefixOfOutputConstraint in prefixOfOutputConstraints)
				{
					State state = prefixOfOutputConstraint.Input.AsStateForLearning();
					if (!dictionary.ContainsKey(prefixOfOutputConstraint.Input.AsStateForLearning()))
					{
						dictionary[state] = prefixOfOutputConstraint.OutputPrefixes;
					}
				}
				spec = new ExampleSpec(dictionary);
			}
			else
			{
				Optional<State> optional = onlyAdditionalInputStates.MaybeFirst<State>();
				if (optional.HasValue)
				{
					spec = new OutputNotNullSpec(new State[] { optional.Value });
				}
				else
				{
					spec = TopSpec.Instance;
				}
			}
			return spec;
		}

		// Token: 0x0600E6D0 RID: 59088 RVA: 0x0030E388 File Offset: 0x0030C588
		private static void CheckConstantOutput(List<Example<IRow, object>> vsrTrainingExamplesList, Witnesses.Options options)
		{
			if (vsrTrainingExamplesList.Any<Example<IRow, object>>() && options.ForbidConstantProgram)
			{
				string onlyOutput = vsrTrainingExamplesList[0].Output as string;
				if (vsrTrainingExamplesList.Skip(1).Any((Example<IRow, object> ex) => ex.Output as string != onlyOutput))
				{
					onlyOutput = null;
				}
				if (onlyOutput != null)
				{
					options.ForbiddenConstants = options.ForbiddenConstants.Add(onlyOutput);
					options.ForbidConstantProgram = true;
					return;
				}
				options.ForbidConstantProgram = false;
			}
		}

		// Token: 0x0600E6D1 RID: 59089 RVA: 0x0030E414 File Offset: 0x0030C614
		private LearningTask CreateLearningTaskFor(Symbol symbol, Spec spec, Witnesses.Options options, IReadOnlyList<State> additionalInputStates, IReadOnlyList<IRow> additionalInputs, IReadOnlyList<Constraint<IRow, object>> filterConstraints, IReadOnlyList<Constraint<IRow, object>> allConstraints, int? k, int? randomK, ProgramSamplingStrategy samplingStrategy, IFeature feature)
		{
			int? num = k;
			int? num2 = randomK;
			if (filterConstraints.Any<Constraint<IRow, object>>())
			{
				num += 100;
				num2 += 100;
			}
			else if (options.ForbidConstantProgram)
			{
				num++;
				num2++;
			}
			LearningTask learningTask = LearningTask.Create(symbol, spec, num2, samplingStrategy, num, feature, this.GetFeatureOptionsFor(allConstraints, additionalInputs));
			learningTask.AdditionalInputs = additionalInputStates;
			return learningTask;
		}

		// Token: 0x0600E6D2 RID: 59090 RVA: 0x0030E4F0 File Offset: 0x0030C6F0
		private bool InputAppearsInOutput(LearningTask task)
		{
			ExampleSpec exampleSpec = task.Spec as ExampleSpec;
			if (exampleSpec == null)
			{
				return false;
			}
			foreach (KeyValuePair<State, object> keyValuePair in exampleSpec.Examples)
			{
				ValueSubstringRow valueSubstringRow = (ValueSubstringRow)keyValuePair.Key[Language.Grammar.InputSymbol];
				ValueSubstring valueSubstring = keyValuePair.Value as ValueSubstring;
				if (valueSubstring != null)
				{
					bool flag = false;
					foreach (string text in valueSubstringRow.ColumnNames)
					{
						object obj = valueSubstringRow[text];
						if (obj != null)
						{
							ValueSubstring valueSubstring2 = obj as ValueSubstring;
							if (valueSubstring2 != null && valueSubstring2.Length > 1U && valueSubstring.Value.Contains(valueSubstring2.Value))
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600E6D3 RID: 59091 RVA: 0x0030E608 File Offset: 0x0030C808
		private ProgramSet LearnUsingEngine(SynthesisEngine engine, LearningTask task, CancellationToken cancel)
		{
			ProgramSet programSet = engine.Learn(task, cancel);
			base.SpecsGeneratedFromLastLearn = ((base.SpecsGeneratedFromLastLearn == null) ? engine.GetCachedSpecs() : base.SpecsGeneratedFromLastLearn.Concat(engine.GetCachedSpecs()));
			return programSet;
		}

		// Token: 0x0600E6D4 RID: 59092 RVA: 0x0030E63C File Offset: 0x0030C83C
		private ProgramSet LearnAllSingleBranch(LearningTask task, IEnumerable<IEnumerable<string>> columnPriority, List<Constraint<IRow, object>> filterConstraints, MergeColumns mergeColumns, Witnesses.Options options, IEnumerable<ExternalEntityToken> pluggableExternalTokens, CancellationToken cancel)
		{
			Grammar grammar = Language.Grammar;
			HashSet<string> learningColumns = new HashSet<string>();
			foreach (IEnumerable<string> enumerable in columnPriority)
			{
				HashSet<string> hashSet = enumerable.Except(learningColumns).ConvertToHashSet<string>();
				learningColumns.AddRange(hashSet);
				DynamicTokenExtractor.InitializeLearningCache(task.ProvidedInputs.Select((State state) => (ValueSubstringRow)state[Language.Grammar.InputSymbol]).ToList<ValueSubstringRow>(), hashSet);
				if (pluggableExternalTokens != null && pluggableExternalTokens.Any<ExternalEntityToken>())
				{
					this.AddEntityDetectorsTokensToLearningCache(task.ProvidedInputs.Select((State state) => (ValueSubstringRow)state[Language.Grammar.InputSymbol]).ToList<ValueSubstringRow>(), hashSet, pluggableExternalTokens);
				}
				if (!options.RequiredColumns.Except(learningColumns).Any<string>())
				{
					Spec spec = task.Spec;
					ConcatLocation[] array = new ConcatLocation[]
					{
						ConcatLocation.Nowhere | ConcatLocation.AtTokenBoundaries,
						ConcatLocation.Anywhere
					};
					if (!task.RequiresPruning)
					{
						array = new ConcatLocation[] { ConcatLocation.Anywhere };
					}
					else if (options.OutputType is IType<PartialDateTime> || options.OutputType is IType<decimal> || (options.AllowedTransformations & TransformationKind.Concat) == TransformationKind.Unknown)
					{
						array = new ConcatLocation[] { ConcatLocation.Nowhere };
					}
					else
					{
						if (options.OutputType != null && !(options.OutputType is UnknownType))
						{
							string text = "Unsupported output type: ";
							IType outputType = options.OutputType;
							throw new NotImplementedException(text + ((outputType != null) ? outputType.ToString() : null));
						}
						if (this.InputAppearsInOutput(task))
						{
							array = new ConcatLocation[]
							{
								ConcatLocation.Nowhere | ConcatLocation.AtTokenBoundariesExceptInputs,
								ConcatLocation.AtTokenBoundaries,
								ConcatLocation.Anywhere
							};
						}
						else if (mergeColumns != null)
						{
							array = new ConcatLocation[] { ConcatLocation.Anywhere };
						}
					}
					if (options.DisableConcatHeuristics)
					{
						array = new ConcatLocation[] { options.ConcatLocation };
					}
					ProgramSet programSet = null;
					ConcatLocation[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						ConcatLocation concatLocation = array2[i];
						cancel.ThrowIfCancellationRequested();
						options.ConcatLocation = concatLocation;
						Witnesses witnesses = new Witnesses(grammar, this.ScoreFeature, learningColumns.ToList<string>(), options);
						LogListener logListenerIfEnabled = options.GetLogListenerIfEnabled(null);
						if (logListenerIfEnabled != null)
						{
							this.LastLearnAllSingleBranchLogs[concatLocation] = logListenerIfEnabled;
						}
						Grammar grammar2 = grammar;
						SynthesisEngine.Config config = new SynthesisEngine.Config();
						SynthesisEngine.Config config2 = config;
						ISynthesisStrategy[] array3 = new DeductiveSynthesis[]
						{
							new DeductiveSynthesis(witnesses, null)
						};
						config2.Strategies = array3;
						config.UseThreads = false;
						config.CacheSize = int.MaxValue;
						config.LogListener = logListenerIfEnabled;
						config.UseDynamicSoundnessCheck = options.UseDynamicSoundnessCheck;
						SynthesisEngine synthesisEngine = new SynthesisEngine(grammar2, config, new int?(options.RandomSeed));
						ProgramSet programSet2;
						if (!(mergeColumns != null))
						{
							programSet2 = this.LearnUsingEngine(synthesisEngine, task, cancel);
						}
						else
						{
							ProgramSetBuilder<e> programSetBuilder = mergeColumns.BuildASTs(task, learningColumns, cancel);
							programSet2 = ((programSetBuilder != null) ? programSetBuilder.Set.AddConversionRules(task.Symbol) : null);
						}
						programSet = programSet2;
						options.SaveLogToXMLIfEnabled(logListenerIfEnabled, () => string.Format("{0}ex.columns-{1}.concatLocation-{2}", task.Spec.ProvidedInputs.Count<State>(), string.Join("-", learningColumns.OrderBy((string c) => c)), concatLocation.ToString()));
						programSet = Learner.FilterPrograms(programSet, filterConstraints, !task.RequiresPruning);
						if (!ProgramSet.IsNullOrEmpty(programSet))
						{
							return programSet;
						}
					}
					if (programSet != null && !programSet.IsEmpty)
					{
						return programSet;
					}
				}
			}
			return ProgramSet.Empty(Language.Grammar.StartSymbol);
		}

		// Token: 0x0600E6D5 RID: 59093 RVA: 0x0030EA2C File Offset: 0x0030CC2C
		private void AddEntityDetectorsTokensToLearningCache(IReadOnlyList<ValueSubstringRow> allInputs, IEnumerable<string> columns, IEnumerable<ExternalEntityToken> pluggableExternalTokens)
		{
			using (IEnumerator<string> enumerator = columns.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string column = enumerator.Current;
					Func<ValueSubstringRow, object> func;
					Func<ValueSubstringRow, object> <>9__0;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (ValueSubstringRow input) => input[column]);
					}
					foreach (LearningCacheSubstring learningCacheSubstring in allInputs.Select(func).OfType<LearningCacheSubstring>())
					{
						if (learningCacheSubstring != null)
						{
							learningCacheSubstring.Cache.AddTokens(pluggableExternalTokens);
						}
					}
				}
			}
		}

		// Token: 0x0600E6D6 RID: 59094 RVA: 0x0030EAE4 File Offset: 0x0030CCE4
		private ProgramSet LearnAllConditionals(LearningTask task, IEnumerable<IEnumerable<string>> columnPriority, List<Constraint<IRow, object>> filterConstraints, Witnesses.Options options, CancellationToken cancel)
		{
			if (!options.DisableConcatHeuristics)
			{
				options.ConcatLocation = ConcatLocation.Nowhere | ConcatLocation.Anywhere;
			}
			ProgramSet programSet = ProgramSet.Empty(task.Symbol);
			HashSet<string> hashSet = new HashSet<string>();
			foreach (IEnumerable<string> enumerable in columnPriority)
			{
				using (IEnumerator<string> enumerator2 = enumerable.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						string column = enumerator2.Current;
						cancel.ThrowIfCancellationRequested();
						if (!hashSet.Contains(column))
						{
							hashSet.Add(column);
							options.ForbidConstantProgram = false;
							Witnesses witnesses = new Witnesses(Language.Grammar, this.ScoreFeature, new string[] { column }, options);
							LogListener logListenerIfEnabled = options.GetLogListenerIfEnabled(null);
							if (logListenerIfEnabled != null)
							{
								this.LastLearnAllConditionalsLogs[column] = logListenerIfEnabled;
							}
							Grammar grammar = Language.Grammar;
							SynthesisEngine.Config config = new SynthesisEngine.Config();
							SynthesisEngine.Config config2 = config;
							ISynthesisStrategy[] array = new DeductiveSynthesis[]
							{
								new DeductiveSynthesis(witnesses, null)
							};
							config2.Strategies = array;
							config.UseThreads = false;
							config.CacheSize = int.MaxValue;
							config.LogListener = logListenerIfEnabled;
							config.UseDynamicSoundnessCheck = options.UseDynamicSoundnessCheck;
							SynthesisEngine synthesisEngine = new SynthesisEngine(grammar, config, new int?(options.RandomSeed));
							ProgramSet programSet2 = this.LearnUsingEngine(synthesisEngine, task, cancel);
							programSet2 = Language.Build.Set.UnnamedConversion.switch_ite(Language.Build.Set.Cast.ite(programSet2)).Set;
							ProgramSet programSet3 = Learner.FilterPrograms(programSet2, filterConstraints, !task.RequiresPruning);
							options.SaveLogToXMLIfEnabled(logListenerIfEnabled, () => string.Format("{0}ex.columns-{1}.conditionals", task.Spec.ProvidedInputs.Count<State>(), column));
							if (!ProgramSet.IsNullOrEmpty(programSet3))
							{
								if (task.K.HasValue && task.PruningRequest.RandomK == null)
								{
									PrunedProgramSet prunedProgramSet = new ProgramSet[] { programSet, programSet3 }.NormalizedUnion().Prune(task.PruningRequest, task.FeatureCalculationContext, new Random(options.RandomSeed), options.GetLogListenerIfEnabled(null));
									options.MaxBranchCount = new int?(prunedProgramSet.TopPrograms.Max((ProgramNode node) => node.AcceptVisitor<int>(Learner.BranchCount)));
									programSet = prunedProgramSet;
								}
								else if (task.PruningRequest.RandomK != null)
								{
									programSet = new ProgramSet[] { programSet, programSet3 }.NormalizedUnion();
									options.MaxBranchCount = new int?(programSet.TopK(this.ScoreFeature, task.K.Value, task.FeatureCalculationContext, null).Max((ProgramNode node) => node.AcceptVisitor<int>(Learner.BranchCount)));
								}
								else
								{
									programSet = new ProgramSet[] { programSet, programSet3 }.NormalizedUnion();
								}
							}
						}
					}
				}
				if (task.PruningRequest.RandomK != null)
				{
					if ((programSet = programSet.Prune(task.PruningRequest, task.FeatureCalculationContext, new Random(options.RandomSeed), options.GetLogListenerIfEnabled(null))).TopPrograms.Count >= task.K.Value)
					{
						break;
					}
				}
				else if (task.K.HasValue && programSet.Size >= (long)task.K.Value)
				{
					break;
				}
			}
			return programSet;
		}

		// Token: 0x0600E6D7 RID: 59095 RVA: 0x0030EF60 File Offset: 0x0030D160
		private ProgramSet LearnLookup(LearningTask task, IEnumerable<IEnumerable<string>> columnPriority, IReadOnlyList<Constraint<IRow, object>> filterConstraints, Witnesses.Options options, int? k, CancellationToken cancel)
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (IEnumerable<string> enumerable in columnPriority)
			{
				cancel.ThrowIfCancellationRequested();
				HashSet<string> hashSet2 = enumerable.Except(hashSet).ConvertToHashSet<string>();
				hashSet.AddRange(hashSet2);
				LookupFallbackMode lookupFallbackMode = options.LookupFallbackMode;
				bool flag;
				if (lookupFallbackMode > LookupFallbackMode.Always)
				{
					if (lookupFallbackMode != LookupFallbackMode.Never)
					{
						throw new NotImplementedException("Unknown LookupFallbackMode: " + options.LookupFallbackMode.ToString());
					}
					flag = false;
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					Witnesses.Options options2 = new Witnesses.Options
					{
						AllowedTransformations = TransformationKind.Lookup
					};
					Witnesses witnesses = new Witnesses(Language.Grammar, this.ScoreFeature, hashSet.ToList<string>(), options2);
					Grammar grammar = Language.Grammar;
					SynthesisEngine.Config config = new SynthesisEngine.Config();
					SynthesisEngine.Config config2 = config;
					ISynthesisStrategy[] array = new DeductiveSynthesis[]
					{
						new DeductiveSynthesis(witnesses, null)
					};
					config2.Strategies = array;
					config.UseThreads = false;
					config.CacheSize = int.MaxValue;
					SynthesisEngine synthesisEngine = new SynthesisEngine(grammar, config, new int?(options.RandomSeed));
					return Learner.FilterPrograms(this.LearnUsingEngine(synthesisEngine, task, cancel), filterConstraints, !task.RequiresPruning);
				}
			}
			return ProgramSet.Empty(task.Symbol);
		}

		// Token: 0x0600E6D8 RID: 59096 RVA: 0x0030F0C4 File Offset: 0x0030D2C4
		private static ProgramSet FilterPrograms(ProgramSet res, IReadOnlyList<Constraint<IRow, object>> filterConstraints, bool learnAll)
		{
			if (!filterConstraints.Any<Constraint<IRow, object>>() || (res == null || res.IsEmpty))
			{
				return res;
			}
			if (!learnAll)
			{
				IEnumerable<ProgramNode> enumerable = res.RealizedPrograms.Where(delegate(ProgramNode p)
				{
					Program program = new Program(Language.Build.Node.Cast.@switch(p), new double?(0.0));
					return filterConstraints.All((Constraint<IRow, object> c) => c.IsSoft || c.Valid(program));
				});
				return ProgramSet.List(res.Symbol, enumerable);
			}
			Func<ProgramNode, bool> <>9__3;
			foreach (Constraint<IRow, object> constraint in filterConstraints.Where((Constraint<IRow, object> c) => !c.IsSoft))
			{
				DoesNotEqual<IRow, object> doesNotEqual = constraint as DoesNotEqual<IRow, object>;
				if (doesNotEqual != null)
				{
					Dictionary<State, object> dictionary = new Dictionary<State, object>();
					State state = doesNotEqual.Input.AsStateForLearning();
					dictionary[state] = ValueSubstring.Create(doesNotEqual.Output as string, null, null, null, null);
					ExampleSpec exampleSpec = new ExampleSpec(dictionary);
					ProgramSet programSet;
					ProgramSet programSet2;
					res.PartitionByValidity(exampleSpec, out programSet, out programSet2);
					res = programSet2;
				}
				else
				{
					Example<IRow, object> example = constraint as Example<IRow, object>;
					if (example != null && example.Output == null)
					{
						Dictionary<State, object> dictionary2 = new Dictionary<State, object>();
						State state = example.Input.AsStateForLearning();
						dictionary2[state] = Bottom.Value;
						ExampleSpec exampleSpec2 = new ExampleSpec(dictionary2);
						res = res.Filter(exampleSpec2);
					}
					else
					{
						if (!(res.Size < 100L))
						{
							string text = ".LearnAll() does not support constraints of type ";
							Type type = constraint.GetType();
							throw new NotImplementedException(text + ((type != null) ? type.ToString() : null));
						}
						IEnumerable<ProgramNode> realizedPrograms = res.RealizedPrograms;
						Func<ProgramNode, bool> func;
						if ((func = <>9__3) == null)
						{
							func = (<>9__3 = delegate(ProgramNode p)
							{
								Program program = new Program(Language.Build.Node.Cast.@switch(p), new double?(0.0));
								return filterConstraints.All((Constraint<IRow, object> c) => c.IsSoft || c.Valid(program));
							});
						}
						IEnumerable<ProgramNode> enumerable2 = realizedPrograms.Where(func);
						res = ProgramSet.List(res.Symbol, enumerable2);
					}
				}
			}
			return res;
		}

		// Token: 0x0600E6D9 RID: 59097 RVA: 0x0030F2B8 File Offset: 0x0030D4B8
		public override ProgramSet LearnAll(IEnumerable<Constraint<IRow, object>> constraints, IEnumerable<IRow> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			FeatureCalculationContext featureCalculationContext;
			return this.LearnAllImpl(constraints, out featureCalculationContext, additionalInputs, null, null, null, ProgramSamplingStrategy.UniformRandom, cancel);
		}

		// Token: 0x0600E6DA RID: 59098 RVA: 0x0030F2E8 File Offset: 0x0030D4E8
		private static ProgramCollection<Program, IRow, object, TFeatureValue> ProgramCollectionFromProgramSet<TFeatureValue>(ProgramSet programSet, Feature<TFeatureValue> feature, IFeatureOptions featureOptions, int k, int? numRandomProgramsToSample, ProgramSamplingStrategy samplingStrategy, FeatureCalculationContext fcc, bool skipPruning) where TFeatureValue : IComparable
		{
			if (programSet == null || programSet.IsEmpty)
			{
				return ProgramCollection<Program, IRow, object, TFeatureValue>.Empty;
			}
			if (skipPruning)
			{
				return new ProgramCollection<Program, IRow, object, TFeatureValue>(programSet.RealizedPrograms.Select((ProgramNode p) => new Program(Language.Build.Node.Cast.@switch(p), null)), null, null, null);
			}
			return ProgramCollection<Program, IRow, object, TFeatureValue>.From(programSet.Prune(new int?(k), numRandomProgramsToSample, feature, featureOptions, fcc, samplingStrategy, new Random(0), null), delegate(ProgramNode p)
			{
				@switch @switch = Language.Build.Node.Cast.@switch(p);
				Feature<double> feature2 = feature as Feature<double>;
				return new Program(@switch, (feature2 != null) ? new double?(p.GetFeatureValue<double>(feature2, fcc.WithProgramNode(p))) : null);
			}, feature);
		}

		// Token: 0x0600E6DB RID: 59099 RVA: 0x0030F390 File Offset: 0x0030D590
		protected override ProgramCollection<Program, IRow, object, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<IRow, object>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<IRow> additionalInputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			constraints = constraints.Memoize<Constraint<IRow, object>>();
			additionalInputs = ((additionalInputs != null) ? additionalInputs.Memoize<IRow>() : null);
			FeatureCalculationContext featureCalculationContext;
			return Learner.ProgramCollectionFromProgramSet<TFeatureValue>(this.LearnAllImpl(constraints, out featureCalculationContext, additionalInputs, new int?(k), feature, numRandomProgramsToInclude, samplingStrategy, cancel), feature, this.GetFeatureOptionsFor(constraints, additionalInputs), k, numRandomProgramsToInclude, samplingStrategy, featureCalculationContext, constraints.Any((Constraint<IRow, object> c) => c is MergeColumns));
		}

		// Token: 0x0600E6DC RID: 59100 RVA: 0x0030F408 File Offset: 0x0030D608
		public override IFeatureOptions GetFeatureOptionsFor(IEnumerable<Constraint<IRow, object>> constraints, IEnumerable<IRow> additionalInputs = null)
		{
			bool flag = constraints.OfType<Example<IRow, object>>().Any((Example<IRow, object> ex) => ex.Output == null);
			bool flag2 = constraints.OfType<AvoidImperialDateTimeFormat>().Any<AvoidImperialDateTimeFormat>();
			return new TransformationTextFeatureOptions(flag, flag2, null);
		}

		// Token: 0x17002687 RID: 9863
		// (get) Token: 0x0600E6DD RID: 59101 RVA: 0x0030F452 File Offset: 0x0030D652
		public override SpecSerializationContext SpecSerializationContext
		{
			get
			{
				return TransformationTextSpecSerializationContext.Instance;
			}
		}

		// Token: 0x040057BD RID: 22461
		private static readonly GrammarBuilders.GrammarSymbols Sym = Language.Build.Symbol;

		// Token: 0x040057BE RID: 22462
		private static readonly GrammarBuilders.Nodes.NodeRules Rule = Language.Build.Node.Rule;

		// Token: 0x040057BF RID: 22463
		private static readonly GrammarBuilders.Nodes.NodeVariables Var = Language.Build.Node.Variable;

		// Token: 0x040057C0 RID: 22464
		private static readonly BranchCount BranchCount = BranchCount.Instance(Language.Grammar);

		// Token: 0x02001B82 RID: 7042
		public class PreprocessedConstraints
		{
			// Token: 0x17002688 RID: 9864
			// (get) Token: 0x0600E6DF RID: 59103 RVA: 0x0030F4D2 File Offset: 0x0030D6D2
			public bool DoNormalLearn
			{
				get
				{
					return !this.DoExternalEntityExtraction && !this.ReturnEmpty && !this.PrefixOfOutputConstraints.Any<PrefixOfOutputConstraint>();
				}
			}

			// Token: 0x17002689 RID: 9865
			// (get) Token: 0x0600E6E0 RID: 59104 RVA: 0x0030F4F4 File Offset: 0x0030D6F4
			internal bool DoExternalEntityExtraction { get; }

			// Token: 0x1700268A RID: 9866
			// (get) Token: 0x0600E6E1 RID: 59105 RVA: 0x0030F4FC File Offset: 0x0030D6FC
			internal bool ReturnEmpty { get; }

			// Token: 0x1700268B RID: 9867
			// (get) Token: 0x0600E6E2 RID: 59106 RVA: 0x0030F504 File Offset: 0x0030D704
			internal Witnesses.Options Options { get; }

			// Token: 0x1700268C RID: 9868
			// (get) Token: 0x0600E6E3 RID: 59107 RVA: 0x0030F50C File Offset: 0x0030D70C
			internal IReadOnlyList<Constraint<IRow, object>> TrainingConstraintsList { get; }

			// Token: 0x1700268D RID: 9869
			// (get) Token: 0x0600E6E4 RID: 59108 RVA: 0x0030F514 File Offset: 0x0030D714
			internal IReadOnlyList<IRow> AdditionalInputsList { get; }

			// Token: 0x1700268E RID: 9870
			// (get) Token: 0x0600E6E5 RID: 59109 RVA: 0x0030F51C File Offset: 0x0030D71C
			internal HashSet<Constraint<IRow, object>> Constraints { get; }

			// Token: 0x1700268F RID: 9871
			// (get) Token: 0x0600E6E6 RID: 59110 RVA: 0x0030F524 File Offset: 0x0030D724
			internal HashSet<Constraint<IRow, object>> HardConstraints { get; }

			// Token: 0x17002690 RID: 9872
			// (get) Token: 0x0600E6E7 RID: 59111 RVA: 0x0030F52C File Offset: 0x0030D72C
			internal bool HasSoftConstraints { get; }

			// Token: 0x17002691 RID: 9873
			// (get) Token: 0x0600E6E8 RID: 59112 RVA: 0x0030F534 File Offset: 0x0030D734
			internal List<Constraint<IRow, object>> FilterConstraints { get; }

			// Token: 0x17002692 RID: 9874
			// (get) Token: 0x0600E6E9 RID: 59113 RVA: 0x0030F53C File Offset: 0x0030D73C
			internal List<MergeColumns> MergeConstraints { get; }

			// Token: 0x17002693 RID: 9875
			// (get) Token: 0x0600E6EA RID: 59114 RVA: 0x0030F544 File Offset: 0x0030D744
			internal MergeColumns MergeColumns { get; }

			// Token: 0x17002694 RID: 9876
			// (get) Token: 0x0600E6EB RID: 59115 RVA: 0x0030F54C File Offset: 0x0030D74C
			internal IReadOnlyList<IEnumerable<string>> ColumnPriority { get; }

			// Token: 0x17002695 RID: 9877
			// (get) Token: 0x0600E6EC RID: 59116 RVA: 0x0030F554 File Offset: 0x0030D754
			internal IReadOnlyList<PrefixOfOutputConstraint> PrefixOfOutputConstraints { get; }

			// Token: 0x17002696 RID: 9878
			// (get) Token: 0x0600E6ED RID: 59117 RVA: 0x0030F55C File Offset: 0x0030D75C
			public List<Example<IRow, object>> VsrTrainingExamplesList { get; }

			// Token: 0x17002697 RID: 9879
			// (get) Token: 0x0600E6EE RID: 59118 RVA: 0x0030F564 File Offset: 0x0030D764
			internal IReadOnlyList<State> AllAdditionalInputStates { get; }

			// Token: 0x17002698 RID: 9880
			// (get) Token: 0x0600E6EF RID: 59119 RVA: 0x0030F56C File Offset: 0x0030D76C
			public IReadOnlyList<State> SampledAdditionalInputStates { get; }

			// Token: 0x17002699 RID: 9881
			// (get) Token: 0x0600E6F0 RID: 59120 RVA: 0x0030F574 File Offset: 0x0030D774
			public IReadOnlyList<ExternalEntityToken> PluggableExternalTokens { get; }

			// Token: 0x1700269A RID: 9882
			// (get) Token: 0x0600E6F1 RID: 59121 RVA: 0x0030F57C File Offset: 0x0030D77C
			public IReadOnlyDictionary<string, Token> AllowedTokens { get; }

			// Token: 0x0600E6F2 RID: 59122 RVA: 0x0030F584 File Offset: 0x0030D784
			internal PreprocessedConstraints(IEnumerable<Constraint<IRow, object>> trainingConstraints, IEnumerable<IRow> additionalInputs)
			{
				Witnesses.Options options = new Witnesses.Options();
				bool flag = false;
				IReadOnlyList<Constraint<IRow, object>> readOnlyList = (trainingConstraints as IReadOnlyList<Constraint<IRow, object>>) ?? trainingConstraints.ToList<Constraint<IRow, object>>();
				IReadOnlyList<IRow> readOnlyList2 = (additionalInputs as IReadOnlyList<IRow>) ?? ((additionalInputs != null) ? additionalInputs.ToList<IRow>() : null);
				HashSet<Constraint<IRow, object>> hashSet = readOnlyList.ConvertToHashSet<Constraint<IRow, object>>();
				HashSet<Constraint<IRow, object>> hashSet2 = hashSet.Where((Constraint<IRow, object> c) => !c.IsSoft).ConvertToHashSet<Constraint<IRow, object>>();
				bool flag2 = hashSet.Count != hashSet2.Count;
				if (hashSet.Count == 1 && hashSet.First<Constraint<IRow, object>>() is ExternalEntityExtraction)
				{
					this.DoExternalEntityExtraction = true;
					this.Constraints = hashSet;
					return;
				}
				IReadOnlyList<Example<IRow, object>> readOnlyList3 = Learner.PreprocessedConstraints.ExtractTrainingExamples(hashSet);
				IReadOnlyList<PrefixOfOutputConstraint> readOnlyList4 = Learner.PreprocessedConstraints.ExtractPrefixOfOutputConstraints(hashSet, readOnlyList3);
				ColumnPriority columnPriority = Learner.PreprocessedConstraints.ExtractColumnPriorityConstraint(hashSet);
				OutputIs<IRow, object> outputIs = Learner.PreprocessedConstraints.ExtractOutputIsConstraint(options, hashSet);
				IReadOnlyDictionary<string, Token> allowedTokens = Learner.PreprocessedConstraints.ExtractRegexTokenConstraint(hashSet);
				List<Constraint<IRow, object>> list = Learner.PreprocessedConstraints.ExtractFilterConstraints(hashSet);
				List<MergeColumns> list2;
				MergeColumns mergeColumns;
				if (!Learner.PreprocessedConstraints.ExtractMergeConstraints(hashSet, readOnlyList3, outputIs, ref flag, out list2, out mergeColumns))
				{
					this.ReturnEmpty = true;
					return;
				}
				Learner.PreprocessedConstraints.ExtractOptionConstraints(options, hashSet);
				hashSet.RemoveWhere((Constraint<IRow, object> c) => c is KnownProgram<IRow, object>);
				hashSet.ExceptWith(list);
				hashSet.RemoveWhere((Constraint<IRow, object> c) => c is AvoidImperialDateTimeFormat);
				if (hashSet.Any<Constraint<IRow, object>>())
				{
					throw new InvalidConstraintException("Unsupported constraints:\\n" + string.Join<Constraint<IRow, object>>("\n", hashSet));
				}
				List<IRow> list3 = (additionalInputs ?? Enumerable.Empty<IRow>()).Concat(from c in list.OfType<ConstraintOnInput<IRow, object>>()
					select c.Input).ToList<IRow>();
				HashSet<string> allColumns;
				IReadOnlyList<IEnumerable<string>> readOnlyList5;
				Learner.PreprocessedConstraints.ProcessColumnPriority(flag, readOnlyList3, columnPriority, mergeColumns, list3, out readOnlyList5, out allColumns);
				if (allColumns.Count == 0)
				{
					this.ReturnEmpty = true;
					return;
				}
				List<Example<IRow, object>> list4 = readOnlyList3.Select((Example<IRow, object> ex) => new Example<IRow, object>(new ValueSubstringRow(ex.Input, allColumns, allowedTokens), ex.Output, ex.IsSoft)).ToList<Example<IRow, object>>();
				List<PrefixOfOutputConstraint> list5 = readOnlyList4.Select((PrefixOfOutputConstraint c) => new PrefixOfOutputConstraint(new ValueSubstringRow(c.Input, allColumns, allowedTokens), c.OutputPrefixes, c.IsSoft)).ToList<PrefixOfOutputConstraint>();
				Learner.PreprocessedConstraints.CheckRepeatedInputs(list4, "trainingConstraints");
				IReadOnlyList<Example<IRow, object>> readOnlyList6 = Learner.PreprocessedConstraints.ExtractNullExamples(list, list4);
				if (!list4.Any<Example<IRow, object>>() && options.OutputType is UnknownType && mergeColumns == null)
				{
					this.ReturnEmpty = true;
					return;
				}
				IReadOnlyList<State> readOnlyList7;
				IReadOnlyList<State> readOnlyList8;
				Learner.PreprocessedConstraints.ExtractAdditionalInputStates(list3, allColumns, list4, readOnlyList6, out readOnlyList7, out readOnlyList8);
				IReadOnlyList<ExternalEntityToken> readOnlyList9;
				Learner.PreprocessedConstraints.ExtractExternalPluggableTokens(options.EntityDetectorsMap, out readOnlyList9);
				this.Options = options;
				this.TrainingConstraintsList = readOnlyList;
				this.AdditionalInputsList = readOnlyList2;
				this.Constraints = hashSet;
				this.HardConstraints = hashSet2;
				this.HasSoftConstraints = flag2;
				this.FilterConstraints = list;
				this.MergeConstraints = list2;
				this.MergeColumns = mergeColumns;
				this.ColumnPriority = readOnlyList5;
				this.VsrTrainingExamplesList = list4;
				this.AllAdditionalInputStates = readOnlyList7;
				this.SampledAdditionalInputStates = readOnlyList8.RandomlySampleToList(options.RandomSeed, options.AdditionalInputsSampleSize);
				this.PrefixOfOutputConstraints = list5;
				this.PluggableExternalTokens = readOnlyList9;
				this.AllowedTokens = allowedTokens;
			}

			// Token: 0x0600E6F3 RID: 59123 RVA: 0x0030F8B0 File Offset: 0x0030DAB0
			private static void ExtractExternalPluggableTokens(EntityDetectorsMap entityDetectorMap, out IReadOnlyList<ExternalEntityToken> pluggableExternalTokens)
			{
				IEnumerable<EntityDetector> enumerable = ((entityDetectorMap != null) ? entityDetectorMap.EmployedEntityDetectors.Values : null);
				if (enumerable != null && enumerable.Any<EntityDetector>())
				{
					pluggableExternalTokens = enumerable.Select((EntityDetector entityDetector) => new ExternalEntityToken(entityDetector)).ToList<ExternalEntityToken>();
					return;
				}
				pluggableExternalTokens = null;
			}

			// Token: 0x0600E6F4 RID: 59124 RVA: 0x0030F90C File Offset: 0x0030DB0C
			private static IReadOnlyList<PrefixOfOutputConstraint> ExtractPrefixOfOutputConstraints(HashSet<Constraint<IRow, object>> constraints, IReadOnlyList<Example<IRow, object>> trainingExamples)
			{
				IReadOnlyList<PrefixOfOutputConstraint> readOnlyList = constraints.OfType<PrefixOfOutputConstraint>().ToList<PrefixOfOutputConstraint>();
				if (readOnlyList.Any<PrefixOfOutputConstraint>() && !constraints.All((Constraint<IRow, object> c) => c is PrefixOfOutputConstraint || c is IOptionConstraint<Witnesses.Options> || !trainingExamples.Any<Example<IRow, object>>()))
				{
					throw new InvalidConstraintException(FormattableString.Invariant(FormattableStringFactory.Create("{0} constraints can only be used with one or more {1} constraints (and possibly some {2}s).", new object[]
					{
						typeof(PrefixOfOutputConstraint),
						typeof(Example<IRow, object>),
						typeof(IOptionConstraint<>)
					})));
				}
				constraints.ExceptWith(readOnlyList);
				return readOnlyList;
			}

			// Token: 0x0600E6F5 RID: 59125 RVA: 0x0030F998 File Offset: 0x0030DB98
			private static IReadOnlyList<Example<IRow, object>> ExtractTrainingExamples(HashSet<Constraint<IRow, object>> constraints)
			{
				IReadOnlyList<Example<IRow, object>> readOnlyList = constraints.OfType<Example<IRow, object>>().ToList<Example<IRow, object>>();
				foreach (Example<IRow, object> example in readOnlyList)
				{
					if (example.Output != null && !(example.Output is string))
					{
						throw new InvalidConstraintException(FormattableString.Invariant(FormattableStringFactory.Create("Output is of unsupported type: {0} (supported types: string) in constraint {1}.", new object[]
						{
							example.Output.GetType(),
							example
						})));
					}
				}
				constraints.ExceptWith(readOnlyList);
				return readOnlyList;
			}

			// Token: 0x0600E6F6 RID: 59126 RVA: 0x0030FA30 File Offset: 0x0030DC30
			private static ColumnPriority ExtractColumnPriorityConstraint(HashSet<Constraint<IRow, object>> constraints)
			{
				HashSet<ColumnPriority> hashSet = constraints.OfType<ColumnPriority>().ConvertToHashSet<ColumnPriority>();
				if (hashSet.Count > 1)
				{
					throw new InvalidConstraintException("May not have multiple different ColumnPriority constraints: " + string.Join<ColumnPriority>(", ", hashSet));
				}
				ColumnPriority columnPriority = hashSet.SingleOrDefault<ColumnPriority>();
				constraints.Remove(columnPriority);
				return columnPriority;
			}

			// Token: 0x0600E6F7 RID: 59127 RVA: 0x0030FA80 File Offset: 0x0030DC80
			private static OutputIs<IRow, object> ExtractOutputIsConstraint(Witnesses.Options options, HashSet<Constraint<IRow, object>> constraints)
			{
				HashSet<OutputIs<IRow, object>> hashSet = constraints.OfType<OutputIs<IRow, object>>().ConvertToHashSet<OutputIs<IRow, object>>();
				if (hashSet.Count > 1)
				{
					throw new InvalidConstraintException("May not have multiple different OutputIs constraints: " + string.Join<OutputIs<IRow, object>>(", ", hashSet));
				}
				OutputIs<IRow, object> outputIs = hashSet.SingleOrDefault<OutputIs<IRow, object>>();
				constraints.Remove(outputIs);
				IType type = ((outputIs != null) ? outputIs.Type : null);
				if (type != null)
				{
					options.OutputType = type;
				}
				return outputIs;
			}

			// Token: 0x0600E6F8 RID: 59128 RVA: 0x0030FAE4 File Offset: 0x0030DCE4
			private static IReadOnlyDictionary<string, Token> ExtractRegexTokenConstraint(HashSet<Constraint<IRow, object>> constraints)
			{
				HashSet<RegexTokenConstraint> hashSet = constraints.OfType<RegexTokenConstraint>().ConvertToHashSet<RegexTokenConstraint>();
				if (hashSet.Count > 1)
				{
					throw new InvalidConstraintException("May not have multiple different regex tokens constraints.");
				}
				RegexTokenConstraint regexTokenConstraint = hashSet.SingleOrDefault<RegexTokenConstraint>();
				constraints.Remove(regexTokenConstraint);
				if (regexTokenConstraint == null)
				{
					return null;
				}
				return regexTokenConstraint.AllowedTokens;
			}

			// Token: 0x0600E6F9 RID: 59129 RVA: 0x0030FB2C File Offset: 0x0030DD2C
			private static List<Constraint<IRow, object>> ExtractFilterConstraints(HashSet<Constraint<IRow, object>> constraints)
			{
				List<Constraint<IRow, object>> list = new List<Constraint<IRow, object>>(constraints.OfType<DoesNotEqual<IRow, object>>());
				foreach (DoesNotEqual<IRow, object> doesNotEqual in list.OfType<DoesNotEqual<IRow, object>>())
				{
					if (doesNotEqual.Output != null && !(doesNotEqual.Output is string))
					{
						throw new InvalidConstraintException(FormattableString.Invariant(FormattableStringFactory.Create("Output is of unsupported type: {0} (supported types: string) in constraint {1}.", new object[]
						{
							doesNotEqual.Output.GetType(),
							doesNotEqual
						})));
					}
				}
				return list;
			}

			// Token: 0x0600E6FA RID: 59130 RVA: 0x0030FBC4 File Offset: 0x0030DDC4
			private static bool ExtractMergeConstraints(HashSet<Constraint<IRow, object>> constraints, IReadOnlyList<Example<IRow, object>> trainingExamplesList, OutputIs<IRow, object> outputIs, ref bool programDeterminedWithoutExamples, out List<MergeColumns> mergeConstraints, out MergeColumns mergeColumns)
			{
				List<MergeColumns> mergeConstraintsList = constraints.OfType<MergeColumns>().Distinct<MergeColumns>().ToList<MergeColumns>();
				mergeColumns = null;
				if (mergeConstraintsList.Count > 0)
				{
					List<MergeColumns> list = mergeConstraintsList.Where((MergeColumns c) => mergeConstraintsList.Any((MergeColumns other) => other != c && c.ConflictsWith(other))).ToList<MergeColumns>();
					if (list.Any<MergeColumns>())
					{
						throw new InvalidConstraintException("May not have multiple MergeColumns constraints with different details: " + string.Join<MergeColumns>(", ", list));
					}
					IReadOnlyDictionary<bool, List<MergeColumns>> readOnlyDictionary = (from c in mergeConstraintsList
						group c by c.IsSoft).ToDictionary((IGrouping<bool, MergeColumns> g) => g.Key, (IGrouping<bool, MergeColumns> g) => g.ToList<MergeColumns>());
					IEnumerable<Constraint<IRow, object>> possibleConflicts = trainingExamplesList.Concat(outputIs.SomeIfNotNull<OutputIs<IRow, object>>().AsEnumerable<OutputIs<IRow, object>>());
					Func<MergeColumns, bool> <>9__10;
					if (readOnlyDictionary.MaybeGet(false).Select(delegate(List<MergeColumns> hardMergeConstraints)
					{
						Func<MergeColumns, bool> func;
						if ((func = <>9__10) == null)
						{
							func = (<>9__10 = (MergeColumns c) => possibleConflicts.Any(new Func<Constraint<IRow, object>, bool>(c.ConflictsWith)));
						}
						return hardMergeConstraints.Any(func);
					}).OrElse(false))
					{
						mergeConstraints = null;
						return false;
					}
					Func<MergeColumns, bool> <>9__11;
					if (readOnlyDictionary.MaybeGet(true).Select(delegate(List<MergeColumns> softMergeConstraints)
					{
						Func<MergeColumns, bool> func2;
						if ((func2 = <>9__11) == null)
						{
							func2 = (<>9__11 = (MergeColumns c) => possibleConflicts.Any(new Func<Constraint<IRow, object>, bool>(c.ConflictsWith)));
						}
						return softMergeConstraints.Any(func2);
					}).OrElse(false))
					{
						mergeConstraintsList.RemoveAll((MergeColumns c) => c.IsSoft);
					}
					if (mergeConstraintsList.Any((MergeColumns c) => c.Columns != null))
					{
						programDeterminedWithoutExamples = true;
					}
					MergeColumns mergeColumns2;
					if (!mergeConstraintsList.Any<MergeColumns>())
					{
						mergeColumns2 = null;
					}
					else
					{
						mergeColumns2 = mergeConstraintsList.Aggregate((MergeColumns a, MergeColumns b) => a.CombineWith(b));
					}
					mergeColumns = mergeColumns2;
				}
				mergeConstraints = mergeConstraintsList;
				return true;
			}

			// Token: 0x0600E6FB RID: 59131 RVA: 0x0030FDB4 File Offset: 0x0030DFB4
			private static void ExtractOptionConstraints(Witnesses.Options options, HashSet<Constraint<IRow, object>> constraints)
			{
				foreach (IOptionConstraint<Witnesses.Options> optionConstraint in ((IEnumerable<IOptionConstraint<Witnesses.Options>>)constraints.OfType<IOptionConstraint<Witnesses.Options>>().ToList<IOptionConstraint<Witnesses.Options>>()))
				{
					optionConstraint.SetOptions(options);
					constraints.Remove((Constraint<IRow, object>)optionConstraint);
				}
			}

			// Token: 0x0600E6FC RID: 59132 RVA: 0x0030FE14 File Offset: 0x0030E014
			private static void ProcessColumnPriority(bool programDeterminedWithoutExamples, IReadOnlyList<Example<IRow, object>> trainingExamplesList, ColumnPriority columnPriorityConstraint, MergeColumns mergeColumns, List<IRow> allAdditionalInputs, out IReadOnlyList<IEnumerable<string>> columnPriority, out HashSet<string> allColumns)
			{
				IEnumerable<IEnumerable<string>> enumerable = ((columnPriorityConstraint != null) ? columnPriorityConstraint.Priority : null);
				if (enumerable == null)
				{
					IRow row = (from i in trainingExamplesList.Select((Example<IRow, object> t) => t.Input).Concat(allAdditionalInputs)
						where i != null
						select i).FirstOrDefault((IRow input) => input.ColumnNames != null);
					if (row == null && !programDeterminedWithoutExamples)
					{
						throw new InputsRequiredException("Transformation.Text must be given at least one input either as an example or as additional inputs or a constraint that defines a program.");
					}
					IEnumerable<string> enumerable2 = ((row != null) ? row.ColumnNames : null) ?? ((mergeColumns != null) ? mergeColumns.Columns : null);
					if (enumerable2 == null)
					{
						throw new InvalidConstraintException("Transformation.Text training examples must have a valid ColumnNames property.");
					}
					columnPriority = new HashSet<string>[] { enumerable2.ConvertToHashSet<string>() };
				}
				else
				{
					columnPriority = (enumerable as IReadOnlyList<IEnumerable<string>>) ?? enumerable.ToList<IEnumerable<string>>();
				}
				allColumns = columnPriority.Union<string>().ConvertToHashSet<string>();
			}

			// Token: 0x0600E6FD RID: 59133 RVA: 0x0030FF1C File Offset: 0x0030E11C
			private static void CheckRepeatedInputs(IEnumerable<Example<IRow, object>> vsrTrainingExamplesList, string argumentName)
			{
				IReadOnlyList<IRow> readOnlyList = (from t in vsrTrainingExamplesList
					group t by t.Input into g
					where g.Count<Example<IRow, object>>() > 1
					select g.Key).ToList<IRow>();
				if (readOnlyList.Any<IRow>())
				{
					throw new InvalidConstraintException("Transformation.Text examples may not have repeat inputs with different outputs. The following inputs are repeated: " + readOnlyList.DumpCollection(ObjectFormatting.Literal, "[", "]", ", ", null));
				}
			}

			// Token: 0x0600E6FE RID: 59134 RVA: 0x0030FFCC File Offset: 0x0030E1CC
			private static IReadOnlyList<Example<IRow, object>> ExtractNullExamples(List<Constraint<IRow, object>> filterConstraints, List<Example<IRow, object>> vsrTrainingExamplesList)
			{
				IReadOnlyList<Example<IRow, object>> readOnlyList = vsrTrainingExamplesList.Where((Example<IRow, object> ex) => ex.Output == null).ToList<Example<IRow, object>>();
				if (readOnlyList.Any<Example<IRow, object>>())
				{
					filterConstraints.AddRange(readOnlyList);
					vsrTrainingExamplesList.RemoveAll((Example<IRow, object> ex) => ex.Output == null);
				}
				return readOnlyList;
			}

			// Token: 0x0600E6FF RID: 59135 RVA: 0x0031003C File Offset: 0x0030E23C
			private static void ExtractAdditionalInputStates(List<IRow> allAdditionalInputs, HashSet<string> allColumns, List<Example<IRow, object>> vsrTrainingExamplesList, IReadOnlyList<Example<IRow, object>> nullExamples, out IReadOnlyList<State> allAdditionalInputStates, out IReadOnlyList<State> onlyAdditionalInputStates)
			{
				List<State> list = new List<State>();
				List<State> list2 = new List<State>();
				HashSet<IRow> hashSet = (from ex in vsrTrainingExamplesList.Concat(nullExamples)
					select ex.Input).ConvertToHashSet<IRow>();
				foreach (IRow row in allAdditionalInputs)
				{
					if (row != null)
					{
						State state = new ValueSubstringRow(row, allColumns, null).AsStateForLearning();
						list.Add(state);
						if (!hashSet.Contains(row))
						{
							hashSet.Add(row);
							list2.Add(state);
						}
					}
				}
				allAdditionalInputStates = list;
				onlyAdditionalInputStates = list2;
			}
		}
	}
}
