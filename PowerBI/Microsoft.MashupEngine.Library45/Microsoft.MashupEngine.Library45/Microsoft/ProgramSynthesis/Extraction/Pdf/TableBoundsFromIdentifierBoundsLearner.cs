using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Learning;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Semantics;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf
{
	// Token: 0x02000BC7 RID: 3015
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1, 1 })]
	internal class TableBoundsFromIdentifierBoundsLearner : ProgramLearner<TableBoundsFromIdentifierBoundsProgram, PdfRegion, PdfRegion>
	{
		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06004C7F RID: 19583 RVA: 0x000F0D91 File Offset: 0x000EEF91
		public static TableBoundsFromIdentifierBoundsLearner Instance { get; } = new TableBoundsFromIdentifierBoundsLearner();

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06004C80 RID: 19584 RVA: 0x000F0D98 File Offset: 0x000EEF98
		public override Feature<double> ScoreFeature { get; } = new RankingScore(Language.Grammar);

		// Token: 0x06004C81 RID: 19585 RVA: 0x000F0DA0 File Offset: 0x000EEFA0
		public TableBoundsFromIdentifierBoundsLearner()
			: base(false, false)
		{
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x000F0DBC File Offset: 0x000EEFBC
		private Witnesses.Options InterpretConstraints(IEnumerable<Constraint<PdfRegion, PdfRegion>> constraints)
		{
			Witnesses.Options options = new Witnesses.Options();
			foreach (Constraint<PdfRegion, PdfRegion> constraint in constraints)
			{
				IOptionConstraint<Witnesses.Options> optionConstraint = constraint as IOptionConstraint<Witnesses.Options>;
				if (optionConstraint != null)
				{
					optionConstraint.SetOptions(options);
				}
			}
			return options;
		}

		// Token: 0x06004C83 RID: 19587 RVA: 0x000F0E14 File Offset: 0x000EF014
		[return: Nullable(new byte[] { 0, 1, 1 })]
		private Record<SynthesisEngine, Spec> BuildEngineAndSpec(IEnumerable<Constraint<PdfRegion, PdfRegion>> constraints, [Nullable(new byte[] { 2, 1 })] IEnumerable<PdfRegion> additionalInputs = null)
		{
			List<Constraint<PdfRegion, PdfRegion>> list = constraints.ToList<Constraint<PdfRegion, PdfRegion>>();
			Witnesses.Options options = this.InterpretConstraints(list);
			Witnesses witnesses = new Witnesses(Language.Grammar, options);
			LogListener logListenerIfEnabled = options.GetLogListenerIfEnabled(null);
			SynthesisEngine synthesisEngine = new SynthesisEngine(Language.Grammar, new SynthesisEngine.Config
			{
				Strategies = new ISynthesisStrategy[]
				{
					new DeductiveSynthesis(witnesses, null)
				},
				LogListener = logListenerIfEnabled,
				UseDynamicSoundnessCheck = options.UseDynamicSoundnessCheck
			}, null);
			bool flag = list.OfType<MatchesSameGlyphs>().Any<MatchesSameGlyphs>();
			bool flag2 = list.OfType<Example<PdfRegion, PdfRegion>>().Any<Example<PdfRegion, PdfRegion>>();
			if (flag == flag2)
			{
				throw new ArgumentException("Constraints must include either MatchesSameGlyphs or Example constraint.", "constraints");
			}
			Spec spec;
			if (flag2)
			{
				spec = new ExampleSpec(list.OfType<Example<PdfRegion, PdfRegion>>().ToDictionary((Example<PdfRegion, PdfRegion> c) => State.CreateForLearning(Language.Grammar.InputSymbol, c.Input), (Example<PdfRegion, PdfRegion> c) => c.Output));
			}
			else
			{
				spec = new DisjunctiveBoundsExampleSpec(list.OfType<MatchesSameGlyphs>().ToDictionary((MatchesSameGlyphs c) => State.CreateForLearning(Language.Grammar.InputSymbol, c.Input), (MatchesSameGlyphs c) => new DisjunctiveBoundsExampleSpec.SnapToGlyphsBounds(c.Output)));
			}
			return Record.Create<SynthesisEngine, Spec>(synthesisEngine, spec);
		}

		// Token: 0x06004C84 RID: 19588 RVA: 0x000F0F68 File Offset: 0x000EF168
		protected override ProgramCollection<TableBoundsFromIdentifierBoundsProgram, PdfRegion, PdfRegion, TFeatureValue> LearnTopKUnchecked<[Nullable(0)] TFeatureValue>(IEnumerable<Constraint<PdfRegion, PdfRegion>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, [Nullable(new byte[] { 2, 1 })] IEnumerable<PdfRegion> additionalInputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			SynthesisEngine synthesisEngine;
			Spec spec;
			this.BuildEngineAndSpec(constraints, additionalInputs).Deconstruct(out synthesisEngine, out spec);
			SynthesisEngine synthesisEngine2 = synthesisEngine;
			Spec spec2 = spec;
			LearningTask learningTask = new LearningTask(Language.Grammar.StartSymbol, spec2, k, feature, null);
			ProgramSet programSet = synthesisEngine2.Learn(learningTask, cancel);
			PrunedProgramSet prunedProgramSet = programSet as PrunedProgramSet;
			return ProgramCollection<TableBoundsFromIdentifierBoundsProgram, PdfRegion, PdfRegion, TFeatureValue>.From(prunedProgramSet, delegate(ProgramNode node)
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
				return new TableBoundsFromIdentifierBoundsProgram(node, num2, null);
			}, feature);
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x000F0FE4 File Offset: 0x000EF1E4
		public override ProgramSet LearnAll(IEnumerable<Constraint<PdfRegion, PdfRegion>> constraints, [Nullable(new byte[] { 2, 1 })] IEnumerable<PdfRegion> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			SynthesisEngine synthesisEngine;
			Spec spec;
			this.BuildEngineAndSpec(constraints, additionalInputs).Deconstruct(out synthesisEngine, out spec);
			SynthesisEngine synthesisEngine2 = synthesisEngine;
			Spec spec2 = spec;
			LearningTask learningTask = new LearningTask(Language.Grammar.StartSymbol, spec2);
			return synthesisEngine2.Learn(learningTask, cancel);
		}
	}
}
