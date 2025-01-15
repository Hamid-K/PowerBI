using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Conditionals.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Conditionals
{
	// Token: 0x02000A0B RID: 2571
	public class Learner : ProgramLearner<Program, IEnumerable<string>, IEnumerable<IEnumerable<string>>>
	{
		// Token: 0x06003E01 RID: 15873 RVA: 0x000C1179 File Offset: 0x000BF379
		private Learner()
			: base(true, true)
		{
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06003E02 RID: 15874 RVA: 0x000C1193 File Offset: 0x000BF393
		public static Learner Instance { get; } = new Learner();

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06003E03 RID: 15875 RVA: 0x000C119A File Offset: 0x000BF39A
		public override Feature<double> ScoreFeature { get; } = new RankingScore(Language.Grammar);

		// Token: 0x06003E04 RID: 15876 RVA: 0x000C11A4 File Offset: 0x000BF3A4
		public override ProgramSet LearnAll(IEnumerable<Constraint<IEnumerable<string>, IEnumerable<IEnumerable<string>>>> constraints, IEnumerable<IEnumerable<string>> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			SynthesisEngine synthesisEngine;
			Witnesses.Options options;
			LogListener logListener;
			LearningTask learningTask = Learner.CreateLearningTask(constraints, additionalInputs, null, null, ProgramSamplingStrategy.UniformRandom, null, out synthesisEngine, out options, out logListener);
			ProgramSet programSet = synthesisEngine.Learn(learningTask, cancel);
			options.SaveLogToXMLIfEnabled(logListener, null);
			return programSet;
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x000C11E8 File Offset: 0x000BF3E8
		protected override ProgramCollection<Program, IEnumerable<string>, IEnumerable<IEnumerable<string>>, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<IEnumerable<string>, IEnumerable<IEnumerable<string>>>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<IEnumerable<string>> additionalInputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			SynthesisEngine synthesisEngine;
			Witnesses.Options options;
			LogListener logListener;
			LearningTask learningTask = Learner.CreateLearningTask(constraints, additionalInputs, new int?(k), numRandomProgramsToInclude, samplingStrategy, feature, out synthesisEngine, out options, out logListener);
			ProgramSetBuilder<output> programSetBuilder = Language.Build.Set.Cast.output(synthesisEngine.Learn(learningTask, cancel));
			options.SaveLogToXMLIfEnabled(logListener, null);
			if (programSetBuilder == null)
			{
				return ProgramCollection<Program, IEnumerable<string>, IEnumerable<IEnumerable<string>>, TFeatureValue>.Empty;
			}
			return ProgramCollection<Program, IEnumerable<string>, IEnumerable<IEnumerable<string>>, TFeatureValue>.From(programSetBuilder.Set as PrunedProgramSet, (ProgramNode p) => new Program(Language.Build.Node.Cast.output(p)), feature);
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x000C1274 File Offset: 0x000BF474
		private static LearningTask CreateLearningTask(IEnumerable<Constraint<IEnumerable<string>, IEnumerable<IEnumerable<string>>>> constraints, IEnumerable<IEnumerable<string>> additionalInputs, int? k, int? randomK, ProgramSamplingStrategy samplingStrategy, IFeature feature, out SynthesisEngine engine, out Witnesses.Options options, out LogListener logListener)
		{
			Grammar grammar = Language.Grammar;
			options = new Witnesses.Options();
			foreach (Constraint<IEnumerable<string>, IEnumerable<IEnumerable<string>>> constraint in constraints)
			{
				IOptionConstraint<Witnesses.Options> optionConstraint = constraint as IOptionConstraint<Witnesses.Options>;
				if (optionConstraint == null)
				{
					if (!(constraint is KnownProgram<IEnumerable<string>, IEnumerable<IEnumerable<string>>>))
					{
						throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Unsupported constraints {0}", new object[] { constraint.GetType() })), "constraints");
					}
				}
				else
				{
					optionConstraint.SetOptions(options);
				}
			}
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			Dictionary<string, Token> dictionary2 = (options.EnableMatchUnicode ? Token.NonDisjunctiveTokens : Token.NonDisjunctiveTokensAscii).ToDictionary<string, Token>();
			dictionary2.Remove("Alphanum");
			dictionary2.Remove("Line Separator");
			if (additionalInputs != null)
			{
				foreach (IEnumerable<string> enumerable in additionalInputs)
				{
					foreach (string text in enumerable)
					{
						LearningCacheSubstring learningCacheSubstring = ((text == null) ? null : new StringRegion(text, dictionary2));
						dictionary.GetOrAdd(State.CreateForLearning(grammar.InputSymbol, learningCacheSubstring), true);
					}
				}
			}
			Witnesses witnesses = new Witnesses(grammar, options);
			logListener = options.GetLogListenerIfEnabled(null);
			engine = new SynthesisEngine(grammar, new SynthesisEngine.Config
			{
				Strategies = new ISynthesisStrategy[]
				{
					new DeductiveSynthesis(witnesses, null)
				},
				CacheSize = int.MaxValue,
				UseThreads = false,
				LogListener = logListener,
				UseDynamicSoundnessCheck = options.UseDynamicSoundnessCheck
			}, null);
			ExampleSpec exampleSpec = new ExampleSpec(dictionary);
			return LearningTask.Create(grammar.StartSymbol, exampleSpec, randomK, samplingStrategy, k, feature, null);
		}
	}
}
