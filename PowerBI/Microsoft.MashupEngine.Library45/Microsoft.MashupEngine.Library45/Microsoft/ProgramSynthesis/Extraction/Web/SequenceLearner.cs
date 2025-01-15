using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Learning;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FC7 RID: 4039
	public class SequenceLearner : ProgramLearner<SequenceProgram, IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>>
	{
		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x06006F6B RID: 28523 RVA: 0x0016C014 File Offset: 0x0016A214
		public static SequenceLearner Instance { get; } = new SequenceLearner();

		// Token: 0x06006F6C RID: 28524 RVA: 0x0016C01B File Offset: 0x0016A21B
		private SequenceLearner()
			: base(true, true)
		{
		}

		// Token: 0x06006F6D RID: 28525 RVA: 0x0016C028 File Offset: 0x0016A228
		private SubsequenceSpec ParseSequenceSpec(IEnumerable<Constraint<IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>>> constraints)
		{
			List<Constraint<IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>>> list = constraints.ToList<Constraint<IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>>>();
			Dictionary<WebRegion, List<WebRegion>> dictionary = (from p in list.OfType<MemberSubset<WebRegion, WebRegion>>()
				group p by p.InputMember).ToDictionary((IGrouping<WebRegion, MemberSubset<WebRegion, WebRegion>> g) => g.Key, (IGrouping<WebRegion, MemberSubset<WebRegion, WebRegion>> g) => g.SelectMany((MemberSubset<WebRegion, WebRegion> e) => e.OutputMember).ToList<WebRegion>());
			Dictionary<WebRegion, List<WebRegion>> dictionary2 = (from n in list.OfType<NegativeMemberSubset<WebRegion, WebRegion>>()
				group n by n.InputMember).ToDictionary((IGrouping<WebRegion, NegativeMemberSubset<WebRegion, WebRegion>> g) => g.Key, (IGrouping<WebRegion, NegativeMemberSubset<WebRegion, WebRegion>> g) => g.SelectMany((NegativeMemberSubset<WebRegion, WebRegion> e) => e.OutputMember).ToList<WebRegion>());
			Dictionary<State, Record<IEnumerable<object>, IEnumerable<object>>> dictionary3 = new Dictionary<State, Record<IEnumerable<object>, IEnumerable<object>>>();
			foreach (KeyValuePair<WebRegion, List<WebRegion>> keyValuePair in dictionary)
			{
				WebRegion key = keyValuePair.Key;
				IEnumerable<IDomNode> allChildrenAndSelf = key.GetAllChildrenAndSelf();
				List<WebRegion> value = keyValuePair.Value;
				List<WebRegion> list2;
				if (!dictionary2.TryGetValue(key, out list2))
				{
					list2 = new List<WebRegion>();
				}
				State state = State.CreateForLearning(Language.Grammar.InputSymbol, allChildrenAndSelf);
				dictionary3[state] = new Record<IEnumerable<object>, IEnumerable<object>>(value, list2);
			}
			return new SubsequenceSpec(dictionary3);
		}

		// Token: 0x06006F6E RID: 28526 RVA: 0x0016C1B0 File Offset: 0x0016A3B0
		protected override ProgramCollection<SequenceProgram, IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<IEnumerable<WebRegion>> additionalInputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			SubsequenceSpec subsequenceSpec = this.ParseSequenceSpec(constraints);
			Witnesses.Options options;
			LogListener logListener;
			SynthesisEngine synthesisEngine = this.CreateDeductiveSynthesisEngine(constraints, out options, out logListener);
			ProgramCollection<SequenceProgram, IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>, TFeatureValue> programCollection;
			using (new DumbDownTokenScoreForWebLearning())
			{
				ProgramSet programSet = synthesisEngine.LearnSymbolTopK(SequenceLearner.SequenceStartSymbol, subsequenceSpec, feature, k, numRandomProgramsToInclude, samplingStrategy, cancel, null);
				if (programSet == null || programSet.IsEmpty)
				{
					programCollection = ProgramCollection<SequenceProgram, IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>, TFeatureValue>.Empty;
				}
				else
				{
					PrunedProgramSet prunedProgramSet = programSet as PrunedProgramSet;
					options.SaveLogToXMLIfEnabled(logListener, null);
					programCollection = ProgramCollection<SequenceProgram, IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>, TFeatureValue>.From(prunedProgramSet, (ProgramNode x) => new SequenceProgram(x, ReferenceKind.Parent, new double?(x.GetFeatureValue<double>(this.ScoreFeature, null))), feature);
				}
			}
			return programCollection;
		}

		// Token: 0x06006F6F RID: 28527 RVA: 0x0016C248 File Offset: 0x0016A448
		public override ProgramSet LearnAll(IEnumerable<Constraint<IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>>> constraints, IEnumerable<IEnumerable<WebRegion>> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			SubsequenceSpec subsequenceSpec = this.ParseSequenceSpec(constraints);
			Witnesses.Options options;
			LogListener logListener;
			SynthesisEngine synthesisEngine = this.CreateDeductiveSynthesisEngine(constraints, out options, out logListener);
			ProgramSet programSet2;
			using (new DumbDownTokenScoreForWebLearning())
			{
				ProgramSet programSet = synthesisEngine.LearnSymbol(SequenceLearner.SequenceStartSymbol, subsequenceSpec, cancel);
				options.SaveLogToXMLIfEnabled(logListener, null);
				programSet2 = programSet;
			}
			return programSet2;
		}

		// Token: 0x06006F70 RID: 28528 RVA: 0x0016C2A8 File Offset: 0x0016A4A8
		internal SynthesisEngine CreateDeductiveSynthesisEngine(IEnumerable<Constraint<IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>>> constraints, out Witnesses.Options options, out LogListener logListener)
		{
			IEnumerable<IOptionConstraint<Witnesses.Options>> enumerable = constraints.OfType<IOptionConstraint<Witnesses.Options>>().ToList<IOptionConstraint<Witnesses.Options>>();
			options = new Witnesses.Options();
			foreach (IOptionConstraint<Witnesses.Options> optionConstraint in enumerable)
			{
				optionConstraint.SetOptions(options);
			}
			logListener = options.GetLogListenerIfEnabled(null);
			Grammar grammar = Language.Grammar;
			SynthesisEngine.Config config = new SynthesisEngine.Config();
			SynthesisEngine.Config config2 = config;
			DeductiveSynthesis[] array = new DeductiveSynthesis[1];
			int num = 0;
			DomainLearningLogic domainLearningLogic = new Witnesses(Language.Grammar, this.ScoreFeature, options, null);
			DeductiveSynthesis.Config config3 = new DeductiveSynthesis.Config();
			config3.PrereqTopProgramsThreshold = (int k) => new int?(1);
			config3.PrereqRandomProgramsThreshold = (int k) => new int?(1);
			array[num] = new DeductiveSynthesis(domainLearningLogic, config3);
			ISynthesisStrategy[] array2 = array;
			config2.Strategies = array2;
			config.UseThreads = false;
			config.LogListener = logListener;
			config.UseDynamicSoundnessCheck = options.UseDynamicSoundnessCheck;
			return new SynthesisEngine(grammar, config, null);
		}

		// Token: 0x06006F71 RID: 28529 RVA: 0x0000A5FD File Offset: 0x000087FD
		internal bool ShouldSequenceExampleBePositive(WebRegion expectedOutput, WebRegion actualOutput)
		{
			return true;
		}

		// Token: 0x06006F72 RID: 28530 RVA: 0x0016C3B8 File Offset: 0x0016A5B8
		public virtual IEnumerable<Constraint<IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>>> SelectStartingConstraints(int numStartingConstraints, IReadOnlyList<Example<IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>>> trainingExamples)
		{
			Example<IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>> example = trainingExamples.First<Example<IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>>>();
			yield return new MemberSubset<WebRegion, WebRegion>(example.Input.First<WebRegion>(), example.Output.First<IEnumerable<WebRegion>>().Take(numStartingConstraints).ToList<WebRegion>(), false);
			yield break;
		}

		// Token: 0x06006F73 RID: 28531 RVA: 0x0016C3CF File Offset: 0x0016A5CF
		public override Constraint<IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>> BuildPositiveConstraint(IEnumerable<WebRegion> input, IEnumerable<IEnumerable<WebRegion>> output, bool isSoft)
		{
			return new MemberSubset<WebRegion, WebRegion>(input.Single<WebRegion>(), output.Single<IEnumerable<WebRegion>>(), isSoft);
		}

		// Token: 0x06006F74 RID: 28532 RVA: 0x0016C3E3 File Offset: 0x0016A5E3
		public override Constraint<IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>> BuildNegativeConstraint(IEnumerable<WebRegion> input, IEnumerable<IEnumerable<WebRegion>> output, bool isSoft)
		{
			return new NegativeMemberSubset<WebRegion, WebRegion>(input.Single<WebRegion>(), output.Single<IEnumerable<WebRegion>>(), isSoft);
		}

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x06006F75 RID: 28533 RVA: 0x0016B102 File Offset: 0x00169302
		public override Feature<double> ScoreFeature
		{
			get
			{
				return ExtractionLearner.Instance.ScoreFeature;
			}
		}

		// Token: 0x04003074 RID: 12404
		internal static readonly Symbol SequenceStartSymbol = Language.Build.Symbol.resultSequence;
	}
}
