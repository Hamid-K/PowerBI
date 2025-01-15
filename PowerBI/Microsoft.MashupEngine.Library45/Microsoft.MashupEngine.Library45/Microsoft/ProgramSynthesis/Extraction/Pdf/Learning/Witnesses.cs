using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build;
using Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Semantics;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Learning
{
	// Token: 0x02000C27 RID: 3111
	[NullableContext(1)]
	[Nullable(0)]
	internal class Witnesses : DomainLearningLogic
	{
		// Token: 0x0600505E RID: 20574 RVA: 0x000FC88F File Offset: 0x000FAA8F
		public Witnesses(Grammar grammar, Witnesses.Options options)
			: base(grammar)
		{
			this._options = options;
			this._builders = GrammarBuilders.Instance(grammar);
		}

		// Token: 0x0600505F RID: 20575 RVA: 0x000FC8AC File Offset: 0x000FAAAC
		[WitnessFunction("SnapToGlyphs", 0)]
		[return: Nullable(2)]
		public DisjunctiveBoundsExampleSpec Witness_SnapToGlyphs(BlackBoxRule rule, ExampleSpec spec)
		{
			Dictionary<State, DisjunctiveBoundsExampleSpec.PossibleBounds> dictionary = new Dictionary<State, DisjunctiveBoundsExampleSpec.PossibleBounds>(spec.Examples.Count);
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				State state;
				object obj;
				keyValuePair.Deconstruct(out state, out obj);
				State state2 = state;
				BoundsOnPdfPage boundsOnPdfPage = obj as BoundsOnPdfPage;
				if (boundsOnPdfPage == null || !Semantics.SnapToGlyphs(boundsOnPdfPage).Equals(boundsOnPdfPage))
				{
					return null;
				}
				dictionary[state2] = new DisjunctiveBoundsExampleSpec.SnapToGlyphsBounds(boundsOnPdfPage);
			}
			return new DisjunctiveBoundsExampleSpec(dictionary);
		}

		// Token: 0x06005060 RID: 20576 RVA: 0x0003B2A8 File Offset: 0x000394A8
		[WitnessFunction("LetBetweenAxis", 0)]
		public TopSpec Witness_Between_axis(LetRule rule, Spec spec)
		{
			return TopSpec.Instance;
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x000FC948 File Offset: 0x000FAB48
		[WitnessFunction("LetBetweenAxis", 1, DependsOnParameters = new int[] { 0 })]
		[WitnessFunction("LetBetweenBefore", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveBoundsExampleSpec Witness_LetBetween_body(LetRule rule, DisjunctiveBoundsExampleSpec spec, ExampleSpec varSpec)
		{
			return new DisjunctiveBoundsExampleSpec(spec.DisjunctiveExamples.ToDictionary((KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => kv.Key.Bind(rule.Variable, varSpec.Examples[kv.Key]), (KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => kv.Value));
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x000FC9A4 File Offset: 0x000FABA4
		[WitnessFunction("LetBetweenBefore", 0)]
		[return: Nullable(2)]
		public DisjunctiveBoundsExampleSpec Witness_Between_before(LetRule rule, DisjunctiveBoundsExampleSpec spec)
		{
			if (spec.DisjunctiveExamples.Values.Any((DisjunctiveBoundsExampleSpec.PossibleBounds b) => !(b is DisjunctiveBoundsExampleSpec.ConcretePossibleBounds)))
			{
				return null;
			}
			return new DisjunctiveBoundsExampleSpec(spec.DisjunctiveExamples.ToDictionary((KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => kv.Key, (KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => new DisjunctiveBoundsExampleSpec.EdgeOfPossibleBounds((DisjunctiveBoundsExampleSpec.ConcretePossibleBounds)kv.Value, ((Axis)kv.Key[this._builders.Symbol.betweenAxis]).DecreasingDirection())));
		}

		// Token: 0x06005063 RID: 20579 RVA: 0x000FCA20 File Offset: 0x000FAC20
		[WitnessFunction("Between", 2, DependsOnParameters = new int[] { 0 })]
		[return: Nullable(2)]
		public DisjunctiveBoundsExampleSpec Witness_Between_after(BlackBoxRule rule, DisjunctiveBoundsExampleSpec spec, ExampleSpec axisSpec)
		{
			if (spec.DisjunctiveExamples.Values.Any((DisjunctiveBoundsExampleSpec.PossibleBounds b) => !(b is DisjunctiveBoundsExampleSpec.ConcretePossibleBounds)))
			{
				return null;
			}
			return new DisjunctiveBoundsExampleSpec(spec.DisjunctiveExamples.ToDictionary((KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => kv.Key, (KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => new DisjunctiveBoundsExampleSpec.EdgeOfPossibleBounds((DisjunctiveBoundsExampleSpec.ConcretePossibleBounds)kv.Value, ((Axis)axisSpec.Examples[kv.Key]).IncreasingDirection())));
		}

		// Token: 0x06005064 RID: 20580 RVA: 0x000FCAA8 File Offset: 0x000FACA8
		[WitnessFunction("NextSeparator", 1)]
		public Spec Witness_NextSeparator_dir(BlackBoxRule rule, Spec spec)
		{
			DisjunctiveBoundsExampleSpec disjunctiveBoundsExampleSpec = spec as DisjunctiveBoundsExampleSpec;
			if (disjunctiveBoundsExampleSpec == null)
			{
				return TopSpec.Instance;
			}
			return DisjunctiveExamplesSpec.From(disjunctiveBoundsExampleSpec.DisjunctiveExamples.ToDictionary((KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => kv.Key, delegate(KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv)
			{
				DisjunctiveBoundsExampleSpec.EdgeOfPossibleBounds edgeOfPossibleBounds = kv.Value as DisjunctiveBoundsExampleSpec.EdgeOfPossibleBounds;
				if (edgeOfPossibleBounds != null)
				{
					return edgeOfPossibleBounds.Edge.AlignedAxis().AlignedDirections().Cast<object>();
				}
				return DirectionUtilities.Directions.Cast<object>();
			}));
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x000FCB14 File Offset: 0x000FAD14
		[WitnessFunction("NextSeparator", 0, DependsOnParameters = new int[] { 1 })]
		[return: Nullable(2)]
		public DisjunctiveBoundsExampleSpec Witness_NextSeparator_baseBounds(BlackBoxRule rule, DisjunctiveBoundsExampleSpec spec, ExampleSpec dirSpec)
		{
			if (spec.DisjunctiveExamples.Values.Any((DisjunctiveBoundsExampleSpec.PossibleBounds b) => b is DisjunctiveBoundsExampleSpec.DirectionFromPossibleBounds))
			{
				return null;
			}
			return new DisjunctiveBoundsExampleSpec(spec.DisjunctiveExamples.ToDictionary((KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => kv.Key, (KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => new DisjunctiveBoundsExampleSpec.DirectionFromPossibleBounds(kv.Value, ((Direction)dirSpec.Examples[kv.Key]).Opposite())));
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x000FCB9C File Offset: 0x000FAD9C
		[WitnessFunction("NextSeparator", 2, DependsOnParameters = new int[] { 0, 1 })]
		[WitnessFunction("NextSeparator_beforeRelative", 2, DependsOnParameters = new int[] { 0, 1 })]
		public DisjunctiveExamplesSpec Witness_NextSeparator_k(BlackBoxRule rule, DisjunctiveBoundsExampleSpec spec, ExampleSpec baseBoundsSpec, ExampleSpec dirSpec)
		{
			return DisjunctiveExamplesSpec.From(spec.DisjunctiveExamples.ToDictionary((KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => kv.Key, delegate(KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv)
			{
				BoundsOnPdfPage boundsOnPdfPage = (BoundsOnPdfPage)baseBoundsSpec.Examples[kv.Key];
				Direction direction = (Direction)dirSpec.Examples[kv.Key];
				return boundsOnPdfPage.PageData.BuildSeparatorCollection().Separators[direction.AlignedAxis().Perpendicular()].OverlappingElements(boundsOnPdfPage.Bounds.With(direction, boundsOnPdfPage.PageData.GetPageBounds()[direction])).OrderByClosest(direction).Enumerate<Separator>()
					.Where2((int _, Separator separator) => kv.Value.Contains(separator.PixelBounds))
					.Select2((int k, Separator _) => k)
					.ToList<int>()
					.Cast<object>();
			}));
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x000FCBF9 File Offset: 0x000FADF9
		[WitnessFunction("NextSameWidthSeparator", 1)]
		[WitnessFunction("NextSeparator_beforeRelative", 1)]
		[WitnessFunction("NextFontSizeDecrease", 1, Verify = true)]
		public ExampleSpec Witness_NextSameWidthSeparator_dir(BlackBoxRule rule, Spec spec)
		{
			return new ExampleSpec(spec.ProvidedInputs.ToDictionary((State input) => input, (State input) => ((Axis)input[this._builders.Symbol.betweenAxis]).IncreasingDirection()));
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x000FCC38 File Offset: 0x000FAE38
		[WitnessFunction("NextSameWidthSeparator", 3)]
		public ExampleSpec Witness_NextSameWidthSeparator_tolerance(BlackBoxRule rule, Spec spec)
		{
			return new ExampleSpec(spec.ProvidedInputs.ToDictionary((State input) => input, (State _) => 5));
		}

		// Token: 0x06005069 RID: 20585 RVA: 0x000FCC94 File Offset: 0x000FAE94
		[WitnessFunction("NextSameWidthSeparator", 2, DependsOnParameters = new int[] { 0, 1, 3 })]
		public DisjunctiveExamplesSpec Witness_NextSameWidthSeparator_k(BlackBoxRule rule, DisjunctiveBoundsExampleSpec spec, ExampleSpec baseBoundsSpec, ExampleSpec dirSpec, ExampleSpec toleranceSpec)
		{
			return DisjunctiveExamplesSpec.From(spec.DisjunctiveExamples.ToDictionary((KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => kv.Key, delegate(KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv)
			{
				BoundsOnPdfPage boundsOnPdfPage = (BoundsOnPdfPage)baseBoundsSpec.Examples[kv.Key];
				Direction direction = (Direction)dirSpec.Examples[kv.Key];
				int tolerance = (int)toleranceSpec.Examples[kv.Key];
				int baseBoundsSize = boundsOnPdfPage.Bounds[direction.AlignedAxis().Perpendicular()].Size();
				return (from separator in boundsOnPdfPage.PageData.BuildSeparatorCollection().Separators[direction.AlignedAxis().Perpendicular()].OverlappingElements(boundsOnPdfPage.Bounds.With(direction, boundsOnPdfPage.PageData.GetPageBounds()[direction]))
					where MathUtils.WithinTolerance(baseBoundsSize, separator.Line.Range.Size(), tolerance)
					select separator).OrderByClosest(direction).Enumerate<Separator>().Where2((int _, Separator separator) => kv.Value.Contains(separator.PixelBounds))
					.Select2((int k, Separator _) => k)
					.ToList<int>()
					.Cast<object>();
			}));
		}

		// Token: 0x0600506A RID: 20586 RVA: 0x000FCCF9 File Offset: 0x000FAEF9
		[WitnessFunction("CombineBounds", 0)]
		public DisjunctiveBoundsExampleSpec Witness_CombineBounds_horizontal(BlackBoxRule rule, DisjunctiveBoundsExampleSpec spec)
		{
			return spec.Select(([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => new DisjunctiveBoundsExampleSpec.IgnoreAxisPossibleBounds(kv.Value, Axis.Vertical));
		}

		// Token: 0x0600506B RID: 20587 RVA: 0x000FCD20 File Offset: 0x000FAF20
		[WitnessFunction("CombineBounds", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveBoundsExampleSpec Witness_CombineBounds_vertical(BlackBoxRule rule, DisjunctiveBoundsExampleSpec spec, ExampleSpec horizontalSpec)
		{
			return spec.Select(([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => new DisjunctiveBoundsExampleSpec.ReplaceAxisPossibleBounds(kv.Value, Axis.Horizontal, ((BoundsOnPdfPage)horizontalSpec.Examples[kv.Key]).Bounds.Horizontal));
		}

		// Token: 0x04002365 RID: 9061
		private readonly GrammarBuilders _builders;

		// Token: 0x04002366 RID: 9062
		private readonly Witnesses.Options _options;

		// Token: 0x04002367 RID: 9063
		private const int DefaultTolerance = 5;

		// Token: 0x02000C28 RID: 3112
		[NullableContext(0)]
		internal class Options : DSLOptions
		{
		}
	}
}
