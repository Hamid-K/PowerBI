using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
	// Token: 0x02000FBE RID: 4030
	public class RegionLearner : ProgramLearner<RegionProgram, IEnumerable<WebRegion>, IEnumerable<WebRegion>>
	{
		// Token: 0x06006F41 RID: 28481 RVA: 0x0016B9E3 File Offset: 0x00169BE3
		private RegionLearner()
			: base(true, true)
		{
		}

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x06006F42 RID: 28482 RVA: 0x0016B9ED File Offset: 0x00169BED
		public static RegionLearner Instance { get; } = new RegionLearner();

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x06006F43 RID: 28483 RVA: 0x0016B102 File Offset: 0x00169302
		public override Feature<double> ScoreFeature
		{
			get
			{
				return ExtractionLearner.Instance.ScoreFeature;
			}
		}

		// Token: 0x06006F44 RID: 28484 RVA: 0x0016B9F4 File Offset: 0x00169BF4
		private ExampleWithNegativesSpec ParseSubstringSpec(IEnumerable<Constraint<IEnumerable<WebRegion>, IEnumerable<WebRegion>>> examples)
		{
			List<Constraint<IEnumerable<WebRegion>, IEnumerable<WebRegion>>> list = examples.ToList<Constraint<IEnumerable<WebRegion>, IEnumerable<WebRegion>>>();
			Dictionary<WebRegion, List<object>> dictionary = (from n in list.OfType<CorrespondingMemberDoesNotEqual<WebRegion, WebRegion>>()
				group n by n.InputMember).ToDictionary((IGrouping<WebRegion, CorrespondingMemberDoesNotEqual<WebRegion, WebRegion>> g) => g.Key, (IGrouping<WebRegion, CorrespondingMemberDoesNotEqual<WebRegion, WebRegion>> g) => g.Cast<object>().ToList<object>());
			Dictionary<State, Record<object, IEnumerable<object>>> dictionary2 = new Dictionary<State, Record<object, IEnumerable<object>>>();
			foreach (CorrespondingMemberEquals<WebRegion, WebRegion> correspondingMemberEquals in list.OfType<CorrespondingMemberEquals<WebRegion, WebRegion>>())
			{
				WebRegion inputMember = correspondingMemberEquals.InputMember;
				IEnumerable<IDomNode> allChildrenAndSelf = inputMember.GetAllChildrenAndSelf();
				if (correspondingMemberEquals.OutputMember == null)
				{
					throw new InvalidOperationException("Substring example cannot be null!");
				}
				List<object> list2;
				if (!dictionary.TryGetValue(inputMember, out list2))
				{
					list2 = new List<object>();
				}
				State state = State.CreateForLearning(Language.Grammar.InputSymbol, allChildrenAndSelf);
				dictionary2[state] = new Record<object, IEnumerable<object>>(correspondingMemberEquals.OutputMember, list2);
			}
			return new ExampleWithNegativesSpec(dictionary2);
		}

		// Token: 0x06006F45 RID: 28485 RVA: 0x0016BB18 File Offset: 0x00169D18
		protected override ProgramCollection<RegionProgram, IEnumerable<WebRegion>, IEnumerable<WebRegion>, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<IEnumerable<WebRegion>, IEnumerable<WebRegion>>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<IEnumerable<WebRegion>> additionalReferences = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			RegionLearner.<>c__DisplayClass8_0<TFeatureValue> CS$<>8__locals1 = new RegionLearner.<>c__DisplayClass8_0<TFeatureValue>();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.feature = feature;
			ProgramNode[] array = this.LearnAll(constraints, null, default(CancellationToken)).RealizedPrograms.ToArray<ProgramNode>();
			IEnumerable<ProgramNode> enumerable = array;
			Func<ProgramNode, int> func;
			if ((func = RegionLearner.<>O.<0>__GetProgramSize) == null)
			{
				func = (RegionLearner.<>O.<0>__GetProgramSize = new Func<ProgramNode, int>(Utils.GetProgramSize));
			}
			IEnumerable<ProgramNode> enumerable2 = enumerable.OrderBy(func).ThenByDescending((ProgramNode p) => p.GetFeatureValue<TFeatureValue>(CS$<>8__locals1.feature, null)).Take(k);
			IEnumerable<ProgramNode> enumerable3 = array.RandomlySample(new Random(0), numRandomProgramsToInclude.GetValueOrDefault());
			return new ProgramCollection<RegionProgram, IEnumerable<WebRegion>, IEnumerable<WebRegion>, TFeatureValue>(enumerable2.Select(new Func<ProgramNode, RegionProgram>(CS$<>8__locals1.<LearnTopKUnchecked>g__ProgramFactory|0)), enumerable3.Select(new Func<ProgramNode, RegionProgram>(CS$<>8__locals1.<LearnTopKUnchecked>g__ProgramFactory|0)), CS$<>8__locals1.feature, null);
		}

		// Token: 0x06006F46 RID: 28486 RVA: 0x0016BBD0 File Offset: 0x00169DD0
		public override ProgramSet LearnAll(IEnumerable<Constraint<IEnumerable<WebRegion>, IEnumerable<WebRegion>>> examples, IEnumerable<IEnumerable<WebRegion>> additionalReferences = null, CancellationToken cancel = default(CancellationToken))
		{
			ExampleWithNegativesSpec exampleWithNegativesSpec = this.ParseSubstringSpec(examples);
			Witnesses.Options options;
			LogListener logListener;
			SynthesisEngine synthesisEngine = this.CreateDeductiveSynthesisEngine(examples, out options, out logListener);
			ProgramSet programSet2;
			using (new DumbDownTokenScoreForWebLearning())
			{
				ProgramSet programSet = synthesisEngine.LearnSymbol(RegionLearner.RegionStartSymbol, exampleWithNegativesSpec, cancel);
				options.SaveLogToXMLIfEnabled(logListener, null);
				programSet2 = programSet;
			}
			return programSet2;
		}

		// Token: 0x06006F47 RID: 28487 RVA: 0x0016BC30 File Offset: 0x00169E30
		internal SynthesisEngine CreateDeductiveSynthesisEngine(IEnumerable<Constraint<IEnumerable<WebRegion>, IEnumerable<WebRegion>>> constraints, out Witnesses.Options options, out LogListener logListener)
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

		// Token: 0x06006F48 RID: 28488 RVA: 0x000170F6 File Offset: 0x000152F6
		internal virtual bool ShouldSequenceExampleBePositive(Example<IEnumerable<WebRegion>, IEnumerable<WebRegion>> expectedOutput, Example<IEnumerable<WebRegion>, IEnumerable<WebRegion>> actualOutput)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006F49 RID: 28489 RVA: 0x0016BD40 File Offset: 0x00169F40
		public virtual IEnumerable<Constraint<IEnumerable<WebRegion>, IEnumerable<WebRegion>>> SelectStartingConstraints(int numStartingConstraints, IReadOnlyList<Example<IEnumerable<WebRegion>, IEnumerable<WebRegion>>> trainingExamples)
		{
			return trainingExamples.SelectMany((Example<IEnumerable<WebRegion>, IEnumerable<WebRegion>> ex) => ex.Input.Zip(ex.Output, (WebRegion i, WebRegion o) => new CorrespondingMemberEquals<WebRegion, WebRegion>(i, o, false))).Take(numStartingConstraints);
		}

		// Token: 0x06006F4A RID: 28490 RVA: 0x0016BD6D File Offset: 0x00169F6D
		public override Constraint<IEnumerable<WebRegion>, IEnumerable<WebRegion>> BuildNegativeConstraint(IEnumerable<WebRegion> input, IEnumerable<WebRegion> output, bool isSoft)
		{
			return new CorrespondingMemberDoesNotEqual<WebRegion, WebRegion>(input.Single<WebRegion>(), output.Single<WebRegion>(), isSoft);
		}

		// Token: 0x06006F4B RID: 28491 RVA: 0x0016BD81 File Offset: 0x00169F81
		public override Constraint<IEnumerable<WebRegion>, IEnumerable<WebRegion>> BuildPositiveConstraint(IEnumerable<WebRegion> input, IEnumerable<WebRegion> output, bool isSoft)
		{
			return new CorrespondingMemberEquals<WebRegion, WebRegion>(input.Single<WebRegion>(), output.Single<WebRegion>(), isSoft);
		}

		// Token: 0x04003064 RID: 12388
		internal static readonly Symbol RegionStartSymbol = Language.Build.Symbol.resultRegion;

		// Token: 0x02000FBF RID: 4031
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04003065 RID: 12389
			public static Func<ProgramNode, int> <0>__GetProgramSize;
		}
	}
}
