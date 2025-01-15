using System;
using System.Collections.Immutable;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Matching.Text.Build;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Matching.Text.Learning
{
	// Token: 0x02001205 RID: 4613
	public class LabelFeature : Feature<ProgramNode>
	{
		// Token: 0x06008B19 RID: 35609 RVA: 0x001D206F File Offset: 0x001D026F
		public LabelFeature(Grammar grammar)
			: base(grammar, "Label", false, false, null, Feature<ProgramNode>.FeatureInfoResolution.Declared)
		{
			this._build = GrammarBuilders.Instance(grammar);
		}

		// Token: 0x06008B1A RID: 35610 RVA: 0x001D2090 File Offset: 0x001D0290
		[FeatureCalculator("LetResult", Method = CalculationMethod.FromChildrenNodes)]
		public ProgramNode ResultLabel(ProgramNode sRegion, ProgramNode disjunctiveMatch)
		{
			return this._build.Node.Rule.LetResult(inputSRegion.CreateSafe(this._build, sRegion).Value, this._build.Node.Unsafe.disjunctive_match(disjunctiveMatch.GetFeatureValue<ProgramNode>(this, null))).Node;
		}

		// Token: 0x06008B1B RID: 35611 RVA: 0x001D20EC File Offset: 0x001D02EC
		[FeatureCalculator("LetMultiResult", Method = CalculationMethod.FromChildrenNodes)]
		public ProgramNode MultiResultLabel(ProgramNode sRegions, ProgramNode multiResultMatches)
		{
			return this._build.Node.Rule.LetMultiResult(this._build.Node.Cast.inputSRegions(sRegions), this._build.Node.Unsafe.multi_result_matches(multiResultMatches.GetFeatureValue<ProgramNode>(this, null))).Node;
		}

		// Token: 0x06008B1C RID: 35612 RVA: 0x001D214C File Offset: 0x001D034C
		[FeatureCalculator("Disjunction", Method = CalculationMethod.FromChildrenNodes)]
		public ProgramNode Label_Disjunction(ProgramNode match, ProgramNode disjunctiveMatch)
		{
			return this._build.Node.Rule.IfThenElse(this._build.Node.Cast.match(match), this._build.Node.Rule.label(match.AcceptVisitor<MatchingLabel>(new MatchingLabelCollector(this._build))), this._build.Node.Cast.labelled_disjunction(disjunctiveMatch.GetFeatureValue<ProgramNode>(this, null))).Node;
		}

		// Token: 0x06008B1D RID: 35613 RVA: 0x001D21D0 File Offset: 0x001D03D0
		[FeatureCalculator("NoMatch")]
		public ProgramNode Label_NoMatch()
		{
			return this._build.Node.UnnamedConversion.labelled_disjunction_label(this._build.Node.Rule.label(MatchingLabel.NoMatch)).Node;
		}

		// Token: 0x06008B1E RID: 35614 RVA: 0x001D2214 File Offset: 0x001D0414
		[FeatureCalculator("Nil", Method = CalculationMethod.FromChildrenNodes)]
		public ProgramNode Label_Nil(ProgramNode inputSRegions)
		{
			return this._build.Node.UnnamedConversion.labelled_multi_result_nil_label(this._build.Node.Rule.nil_label(ImmutableList<MatchingLabel>.Empty)).Node;
		}

		// Token: 0x06008B1F RID: 35615 RVA: 0x001D2258 File Offset: 0x001D0458
		[FeatureCalculator("MatchColumns", Method = CalculationMethod.FromChildrenNodes)]
		public ProgramNode Label_MatchColumns(ProgramNode disjunctiveMatch, ProgramNode multiResult)
		{
			return this._build.Node.Rule.LabelledMatchColumns(this._build.Node.Cast.labelled_disjunction(disjunctiveMatch.GetFeatureValue<ProgramNode>(this, null)), this._build.Node.Unsafe.labelled_multi_result(multiResult.GetFeatureValue<ProgramNode>(this, null))).Node;
		}

		// Token: 0x06008B20 RID: 35616 RVA: 0x001D22BC File Offset: 0x001D04BC
		[FeatureCalculator("LetHead", Method = CalculationMethod.FromChildrenNodes)]
		public ProgramNode HeadLabel(ProgramNode letValueNode, ProgramNode letBodyNode)
		{
			return this._build.Node.Rule.LetHead(this._build.Node.Cast._LetB3(letValueNode), this._build.Node.Cast._LetB4(letBodyNode.GetFeatureValue<ProgramNode>(this, null))).Node;
		}

		// Token: 0x06008B21 RID: 35617 RVA: 0x001D231C File Offset: 0x001D051C
		[FeatureCalculator("LetTail", Method = CalculationMethod.FromChildrenNodes)]
		public ProgramNode TailLabel(ProgramNode letValueNode, ProgramNode letBodyNode)
		{
			return this._build.Node.Rule.LetTail(this._build.Node.Cast._LetB1(letValueNode), this._build.Node.Unsafe._LetB2(letBodyNode.GetFeatureValue<ProgramNode>(this, null))).Node;
		}

		// Token: 0x040038BA RID: 14522
		private readonly GrammarBuilders _build;
	}
}
