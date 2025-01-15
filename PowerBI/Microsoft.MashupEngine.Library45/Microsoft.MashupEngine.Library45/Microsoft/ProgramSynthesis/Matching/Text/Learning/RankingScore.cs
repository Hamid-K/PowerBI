using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Matching.Text.Build;
using Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Matching.Text.Learning
{
	// Token: 0x02001206 RID: 4614
	public class RankingScore : Feature<double>
	{
		// Token: 0x06008B22 RID: 35618 RVA: 0x001D2379 File Offset: 0x001D0579
		public RankingScore(Grammar grammar)
			: base(grammar, "Score", false, false, null, Feature<double>.FeatureInfoResolution.Declared)
		{
			this._build = GrammarBuilders.Instance(grammar);
		}

		// Token: 0x06008B23 RID: 35619 RVA: 0x0001AF59 File Offset: 0x00019159
		protected override double GetFeatureValueForVariable(VariableNode variable)
		{
			return 0.0;
		}

		// Token: 0x06008B24 RID: 35620 RVA: 0x0000E945 File Offset: 0x0000CB45
		[FeatureCalculator("LetResult")]
		public static double ResultScore(double sRegionScore, double disjunctiveMatchScore)
		{
			return disjunctiveMatchScore;
		}

		// Token: 0x06008B25 RID: 35621 RVA: 0x001D2398 File Offset: 0x001D0598
		[FeatureCalculator("LetSplit", Method = CalculationMethod.FromChildrenNodes, SupportsLearningInfo = true)]
		public double SplitScore(LearningInfo info, ProgramNode prefixNode, ProgramNode suffixNode)
		{
			LetSplit letSplit;
			if (this._build.Node.IsRule.LetSplit(suffixNode, out letSplit))
			{
				IToken value = this._build.Node.Cast._LetB0(prefixNode).Cast_SuffixAfterTokenMatch().token.Value;
				IToken value2 = letSplit._LetB0.Cast_SuffixAfterTokenMatch().token.Value;
				if (value is ConstantToken && value2 is ConstantToken)
				{
					return double.NegativeInfinity;
				}
			}
			return prefixNode.GetFeatureValue<double>(this, info) + suffixNode.GetFeatureValue<double>(this, (info != null) ? info.ForChild(1) : null);
		}

		// Token: 0x06008B26 RID: 35622 RVA: 0x001D2448 File Offset: 0x001D0648
		[FeatureCalculator("token", Method = CalculationMethod.FromLiteral, SupportsLearningInfo = true)]
		public double Score_Token(LearningInfo info, IToken token)
		{
			double? num = ((info != null) ? (from state in info.FeatureCalculationContext.MaterializeSpecInputs()
				select state[this._build.Symbol.sRegion]).Cast<SuffixRegion>().Average(delegate(SuffixRegion sRegion)
			{
				double num4 = sRegion.PrefixMatchLength(token);
				string source = sRegion.Source;
				int? num5 = ((source != null) ? new int?(source.Length) : null);
				double? num6 = ((num5 != null) ? new double?((double)num5.GetValueOrDefault()) : null);
				if (num6 == null)
				{
					return null;
				}
				return new double?(num4 / num6.GetValueOrDefault());
			}) : null);
			IToken token2 = token;
			double num2 = ((token2 != null) ? token2.Score : double.NegativeInfinity);
			double? num3 = num * num2;
			if (num3 == null)
			{
				return num2;
			}
			return num3.GetValueOrDefault();
		}

		// Token: 0x06008B27 RID: 35623 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("NoMatch")]
		public static double Score_NoMatch()
		{
			return 0.0;
		}

		// Token: 0x06008B28 RID: 35624 RVA: 0x0000E945 File Offset: 0x0000CB45
		[FeatureCalculator("SuffixAfterTokenMatch")]
		public static double Score_SuffixAfterTokenMatch(double sRegionScore, double tokenScore)
		{
			return tokenScore;
		}

		// Token: 0x06008B29 RID: 35625 RVA: 0x001D2500 File Offset: 0x001D0700
		[FeatureCalculator("Disjunction", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double Score_Disjunction(LearningInfo info, ProgramNode program)
		{
			if (info == null)
			{
				Disjunction disjunction = this._build.Node.CastRule.Disjunction(program);
				return disjunction.match.Node.GetFeatureValue<double>(this, null) + disjunction.disjunctive_match.Node.GetFeatureValue<double>(this, null);
			}
			IEnumerable<State> specInputs = info.SpecInputs;
			List<Record<ProgramNode, List<State>>> list = (from p in program.GetFeatureValue<IEnumerable<ProgramNode>>(new DisjunctsFeature(base.Grammar), null)
				select Record.Create<ProgramNode, List<State>>(p, new List<State>(15))).ToList<Record<ProgramNode, List<State>>>();
			using (IEnumerator<State> enumerator = specInputs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					State input = enumerator.Current;
					int num = list.FindIndex((Record<ProgramNode, List<State>> t) => (bool)(t.Item1.Invoke(input) ?? false));
					if (num >= 0 && list[num].Item2.Count < 15)
					{
						list[num].Item2.Add(input);
						if (list.All((Record<ProgramNode, List<State>> t) => t.Item2.Count >= 15))
						{
							break;
						}
					}
				}
			}
			return list.Aggregate(0.0, delegate(double acc, Record<ProgramNode, List<State>> t)
			{
				LearningInfo learningInfo = new LearningInfo(FeatureCalculationContext.Create(t.Item2, null, info.Options), t.Item1);
				return acc + t.Item1.GetFeatureValue<double>(this, learningInfo);
			});
		}

		// Token: 0x06008B2A RID: 35626 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("IsNull")]
		public static double Score_IsNull(double sRegion)
		{
			return 0.0;
		}

		// Token: 0x06008B2B RID: 35627 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("EndOf")]
		public static double Score_EndOf(double sRegion)
		{
			return 0.0;
		}

		// Token: 0x040038BB RID: 14523
		private readonly GrammarBuilders _build;

		// Token: 0x040038BC RID: 14524
		private const int DisjunctStateSampleSize = 15;
	}
}
