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
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FB2 RID: 4018
	public class FieldProgramLearner : ProgramLearner<FieldProgram, WebRegion, string[]>
	{
		// Token: 0x06006EFB RID: 28411 RVA: 0x0016B0F1 File Offset: 0x001692F1
		private FieldProgramLearner()
			: base(true, true)
		{
		}

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x06006EFC RID: 28412 RVA: 0x0016B0FB File Offset: 0x001692FB
		public static FieldProgramLearner Instance { get; } = new FieldProgramLearner();

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x06006EFD RID: 28413 RVA: 0x0016B102 File Offset: 0x00169302
		public override Feature<double> ScoreFeature
		{
			get
			{
				return ExtractionLearner.Instance.ScoreFeature;
			}
		}

		// Token: 0x06006EFE RID: 28414 RVA: 0x0016B110 File Offset: 0x00169310
		private FieldExtractionSpec GetSpecification(IEnumerable<Constraint<WebRegion, string[]>> examples)
		{
			Dictionary<State, Record<object, IEnumerable<object>>> dictionary = new Dictionary<State, Record<object, IEnumerable<object>>>();
			Dictionary<State, int[]> dictionary2 = new Dictionary<State, int[]>();
			foreach (FieldExtractionExample fieldExtractionExample in examples.OfType<FieldExtractionExample>())
			{
				IEnumerable<IDomNode> allChildrenAndSelf = fieldExtractionExample.Input.GetAllChildrenAndSelf();
				State state = State.CreateForLearning(Language.Grammar.InputSymbol, allChildrenAndSelf);
				dictionary2[state] = fieldExtractionExample.NodeIndexes;
				dictionary[state] = new Record<object, IEnumerable<object>>(fieldExtractionExample.Output, new List<object>());
			}
			return new FieldExtractionSpec(dictionary, dictionary2);
		}

		// Token: 0x06006EFF RID: 28415 RVA: 0x0016B1B0 File Offset: 0x001693B0
		protected override ProgramCollection<FieldProgram, WebRegion, string[], TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<WebRegion, string[]>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<WebRegion> additionalReferences = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			FieldExtractionSpec specification = this.GetSpecification(constraints);
			Witnesses.Options options;
			LogListener logListener;
			SynthesisEngine synthesisEngine = this.CreateDeductiveSynthesisEngine(constraints, out options, out logListener);
			ProgramCollection<FieldProgram, WebRegion, string[], TFeatureValue> programCollection;
			using (new DumbDownTokenScoreForWebLearning())
			{
				ProgramSet programSet = synthesisEngine.LearnSymbolTopK(FieldProgram.ProgramSymbol, specification, feature, k, numRandomProgramsToInclude, samplingStrategy, cancel, null);
				if (programSet == null || programSet.IsEmpty)
				{
					programCollection = ProgramCollection<FieldProgram, WebRegion, string[], TFeatureValue>.Empty;
				}
				else
				{
					PrunedProgramSet prunedProgramSet = programSet as PrunedProgramSet;
					options.SaveLogToXMLIfEnabled(logListener, null);
					programCollection = ProgramCollection<FieldProgram, WebRegion, string[], TFeatureValue>.From(prunedProgramSet, (ProgramNode p) => new FieldProgram(p), feature);
				}
			}
			return programCollection;
		}

		// Token: 0x06006F00 RID: 28416 RVA: 0x0016B25C File Offset: 0x0016945C
		public override ProgramSet LearnAll(IEnumerable<Constraint<WebRegion, string[]>> examples, IEnumerable<WebRegion> additionalReferences = null, CancellationToken cancel = default(CancellationToken))
		{
			FieldExtractionSpec specification = this.GetSpecification(examples);
			Witnesses.Options options;
			LogListener logListener;
			SynthesisEngine synthesisEngine = this.CreateDeductiveSynthesisEngine(examples, out options, out logListener);
			ProgramSet programSet2;
			using (new DumbDownTokenScoreForWebLearning())
			{
				ProgramSet programSet = synthesisEngine.LearnSymbol(FieldProgram.ProgramSymbol, specification, cancel);
				options.SaveLogToXMLIfEnabled(logListener, null);
				programSet2 = programSet;
			}
			return programSet2;
		}

		// Token: 0x06006F01 RID: 28417 RVA: 0x0016B2BC File Offset: 0x001694BC
		internal SynthesisEngine CreateDeductiveSynthesisEngine(IEnumerable<Constraint<WebRegion, string[]>> constraints, out Witnesses.Options options, out LogListener logListener)
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
	}
}
