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
using Microsoft.ProgramSynthesis.Transformation.Json.Learning;
using Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json
{
	// Token: 0x020019F5 RID: 6645
	public class Learner : ProgramLearner<Program, Newtonsoft.Json.Linq.JToken, Newtonsoft.Json.Linq.JToken>
	{
		// Token: 0x1700243B RID: 9275
		// (get) Token: 0x0600D88E RID: 55438 RVA: 0x002DF226 File Offset: 0x002DD426
		public static Learner Instance { get; } = new Learner();

		// Token: 0x0600D88F RID: 55439 RVA: 0x002DF22D File Offset: 0x002DD42D
		private Learner()
			: base(true, true)
		{
		}

		// Token: 0x1700243C RID: 9276
		// (get) Token: 0x0600D890 RID: 55440 RVA: 0x002DF247 File Offset: 0x002DD447
		public override Feature<double> ScoreFeature { get; } = new RankingScore(Language.Grammar);

		// Token: 0x0600D891 RID: 55441 RVA: 0x002DF250 File Offset: 0x002DD450
		private ProgramSet LearnImpl<TFeatureValue>(IEnumerable<Constraint<Newtonsoft.Json.Linq.JToken, Newtonsoft.Json.Linq.JToken>> constraints, Feature<TFeatureValue> feature, int? k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, CancellationToken cancel = default(CancellationToken))
		{
			Grammar grammar = Language.Grammar;
			ExampleSpec exampleSpec = new ExampleSpec(constraints.OfType<Example<Newtonsoft.Json.Linq.JToken, Newtonsoft.Json.Linq.JToken>>().ToDictionary((Example<Newtonsoft.Json.Linq.JToken, Newtonsoft.Json.Linq.JToken> e) => State.CreateForLearning(grammar.InputSymbol, Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken.From(e.Input)), (Example<Newtonsoft.Json.Linq.JToken, Newtonsoft.Json.Linq.JToken> e) => Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken.From(e.Output)));
			Witnesses witnesses = new Witnesses(Language.Grammar);
			SynthesisEngine synthesisEngine = new SynthesisEngine(Language.Grammar, new SynthesisEngine.Config
			{
				Strategies = new ISynthesisStrategy[]
				{
					new DeductiveSynthesis(witnesses, null)
				},
				UseThreads = false
			}, null);
			LearningTask learningTask = ((k != null) ? LearningTask.Create(grammar.StartSymbol, exampleSpec, numRandomProgramsToInclude, samplingStrategy, new int?(k.Value + 9), feature, null) : new LearningTask(grammar.StartSymbol, exampleSpec));
			ProgramSet programSet = synthesisEngine.Learn(learningTask, cancel);
			LogListener logListener = synthesisEngine.Configuration.LogListener;
			if (logListener != null)
			{
				logListener.SaveLogToXML("log.xml");
			}
			if (k != null)
			{
				return programSet.Prune(new int?(k.Value), numRandomProgramsToInclude, feature, null, learningTask.FeatureCalculationContext, samplingStrategy, synthesisEngine.RandomNumberGenerator, synthesisEngine.Configuration.LogListener);
			}
			return programSet;
		}

		// Token: 0x0600D892 RID: 55442 RVA: 0x002DF39C File Offset: 0x002DD59C
		protected override ProgramCollection<Program, Newtonsoft.Json.Linq.JToken, Newtonsoft.Json.Linq.JToken, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<Newtonsoft.Json.Linq.JToken, Newtonsoft.Json.Linq.JToken>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<Newtonsoft.Json.Linq.JToken> additionalInputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			ProgramSet programSet = this.LearnImpl<TFeatureValue>(constraints, feature, new int?(k), numRandomProgramsToInclude, samplingStrategy, cancel);
			if (programSet == null || programSet.IsEmpty)
			{
				return ProgramCollection<Program, Newtonsoft.Json.Linq.JToken, Newtonsoft.Json.Linq.JToken, TFeatureValue>.Empty;
			}
			return ProgramCollection<Program, Newtonsoft.Json.Linq.JToken, Newtonsoft.Json.Linq.JToken, TFeatureValue>.From(programSet as PrunedProgramSet, (ProgramNode p) => new Program(p), feature);
		}

		// Token: 0x0600D893 RID: 55443 RVA: 0x002DF3FC File Offset: 0x002DD5FC
		public override ProgramSet LearnAll(IEnumerable<Constraint<Newtonsoft.Json.Linq.JToken, Newtonsoft.Json.Linq.JToken>> constraints, IEnumerable<Newtonsoft.Json.Linq.JToken> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			return this.LearnImpl<double>(constraints, this.ScoreFeature, null, null, ProgramSamplingStrategy.UniformRandom, cancel);
		}

		// Token: 0x0600D894 RID: 55444 RVA: 0x0000A5FD File Offset: 0x000087FD
		internal virtual bool ShouldSequenceExampleBePositive(Example<Newtonsoft.Json.Linq.JToken, Newtonsoft.Json.Linq.JToken> expectedOutput, Example<Newtonsoft.Json.Linq.JToken, Newtonsoft.Json.Linq.JToken> actualOutput)
		{
			return true;
		}

		// Token: 0x04005330 RID: 21296
		private const int KMargin = 9;
	}
}
