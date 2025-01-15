using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Learning.Clustering;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Conditionals.Learning
{
	// Token: 0x02000A57 RID: 2647
	public class RankingFeatureCollector : Feature<IReadOnlyList<PredicateFeature>>
	{
		// Token: 0x060041AF RID: 16815 RVA: 0x000CD8C8 File Offset: 0x000CBAC8
		public RankingFeatureCollector(Grammar grammar)
			: base(grammar, "FeatureScore", false, false, null, Feature<IReadOnlyList<PredicateFeature>>.FeatureInfoResolution.Declared)
		{
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x000CD8DA File Offset: 0x000CBADA
		[FeatureCalculator("Conjunction")]
		public static IReadOnlyList<PredicateFeature> ConjunctionScore(IReadOnlyList<PredicateFeature> pred, IReadOnlyList<PredicateFeature> baseConjunct)
		{
			return pred.Concat(baseConjunct).ToList<PredicateFeature>();
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("Conjunct")]
		public static IReadOnlyList<PredicateFeature> ConjunctScore(IReadOnlyList<PredicateFeature> baseConjunct)
		{
			return baseConjunct;
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x000CD8E8 File Offset: 0x000CBAE8
		[FeatureCalculator("Contains", Method = CalculationMethod.FromChildrenNodes)]
		public static IReadOnlyList<PredicateFeature> ContainsScore(VariableNode s, LiteralNode r, LiteralNode k)
		{
			return new RegexPredicateFeature[]
			{
				new RegexPredicateFeature((RegularExpression)r.Value, PredicateType.Contains, false, (int)k.Value)
			};
		}

		// Token: 0x060041B3 RID: 16819 RVA: 0x000CD911 File Offset: 0x000CBB11
		[FeatureCalculator("ContainsString", Method = CalculationMethod.FromChildrenNodes)]
		public static IReadOnlyList<PredicateFeature> ContainsStringScore(VariableNode s, LiteralNode str, LiteralNode k)
		{
			return new StringPredicateFeature[]
			{
				new StringPredicateFeature((string)str.Value, PredicateType.ContainsString, false, (int)k.Value)
			};
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x000CD8DA File Offset: 0x000CBADA
		[FeatureCalculator("Disjunction")]
		public static IReadOnlyList<PredicateFeature> DisjunctionScore(IReadOnlyList<PredicateFeature> conjunct, IReadOnlyList<PredicateFeature> disjunct)
		{
			return conjunct.Concat(disjunct).ToList<PredicateFeature>();
		}

		// Token: 0x060041B5 RID: 16821 RVA: 0x000CD93A File Offset: 0x000CBB3A
		[FeatureCalculator("EndsWithDigit")]
		public static IReadOnlyList<PredicateFeature> EndsWithDigitScore(IReadOnlyList<PredicateFeature> s)
		{
			return new BasicPredicateFeature[]
			{
				new BasicPredicateFeature(PredicateType.EndsWithDigit, false, 1)
			};
		}

		// Token: 0x060041B6 RID: 16822 RVA: 0x000CD94D File Offset: 0x000CBB4D
		[FeatureCalculator("EndsWithLetter")]
		public static IReadOnlyList<PredicateFeature> EndsWithLetterScore(IReadOnlyList<PredicateFeature> s)
		{
			return new BasicPredicateFeature[]
			{
				new BasicPredicateFeature(PredicateType.EndsWithLetter, false, 1)
			};
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x000CD960 File Offset: 0x000CBB60
		[FeatureCalculator("EndsWith", Method = CalculationMethod.FromChildrenNodes)]
		public static IReadOnlyList<PredicateFeature> EndsWithScore(VariableNode s, LiteralNode r)
		{
			return new RegexPredicateFeature[]
			{
				new RegexPredicateFeature((RegularExpression)r.Value, PredicateType.EndsWith, false, 1)
			};
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x000CD97F File Offset: 0x000CBB7F
		[FeatureCalculator("EndsWithString", Method = CalculationMethod.FromChildrenNodes)]
		public static IReadOnlyList<PredicateFeature> EndsWithStringScore(VariableNode s, LiteralNode str)
		{
			return new StringPredicateFeature[]
			{
				new StringPredicateFeature((string)str.Value, PredicateType.EndsWithString, false, 1)
			};
		}

		// Token: 0x060041B9 RID: 16825 RVA: 0x000CD99D File Offset: 0x000CBB9D
		[FeatureCalculator("IsNull")]
		public static IReadOnlyList<PredicateFeature> IsNull(IReadOnlyList<PredicateFeature> s)
		{
			return new BasicPredicateFeature[]
			{
				new BasicPredicateFeature(PredicateType.IsNull, false, 1)
			};
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x000CD9B0 File Offset: 0x000CBBB0
		[FeatureCalculator("IsNullOrWhiteSpace")]
		public static IReadOnlyList<PredicateFeature> IsNullOrWhiteSpaceScore(IReadOnlyList<PredicateFeature> s)
		{
			return new BasicPredicateFeature[]
			{
				new BasicPredicateFeature(PredicateType.IsNullOrWhiteSpace, false, 1)
			};
		}

		// Token: 0x060041BB RID: 16827 RVA: 0x000CD9C3 File Offset: 0x000CBBC3
		[FeatureCalculator("IsWhiteSpace")]
		public static IReadOnlyList<PredicateFeature> IsWhiteSpaceScore(IReadOnlyList<PredicateFeature> s)
		{
			return new BasicPredicateFeature[]
			{
				new BasicPredicateFeature(PredicateType.IsWhiteSpace, false, 1)
			};
		}

		// Token: 0x060041BC RID: 16828 RVA: 0x000CD9D6 File Offset: 0x000CBBD6
		[FeatureCalculator("k")]
		[FeatureCalculator("str")]
		[FeatureCalculator("r")]
		public static IReadOnlyList<PredicateFeature> LiteralScore()
		{
			return new PredicateFeature[0];
		}

		// Token: 0x060041BD RID: 16829 RVA: 0x000CD9DE File Offset: 0x000CBBDE
		[FeatureCalculator("Matches", Method = CalculationMethod.FromChildrenNodes)]
		public static IReadOnlyList<PredicateFeature> MatchesScore(VariableNode s, LiteralNode r)
		{
			return new RegexPredicateFeature[]
			{
				new RegexPredicateFeature((RegularExpression)r.Value, PredicateType.Matches, false, 1)
			};
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x000CD9FD File Offset: 0x000CBBFD
		[FeatureCalculator("Not")]
		public static IReadOnlyList<PredicateFeature> NotScore(IReadOnlyList<PredicateFeature> x)
		{
			return x.Select((PredicateFeature e) => e.Negate()).ToList<PredicateFeature>();
		}

		// Token: 0x060041BF RID: 16831 RVA: 0x000CDA29 File Offset: 0x000CBC29
		[FeatureCalculator("StartsWithDigit")]
		public static IReadOnlyList<PredicateFeature> StartsWithDigitScore(IReadOnlyList<PredicateFeature> s)
		{
			return new BasicPredicateFeature[]
			{
				new BasicPredicateFeature(PredicateType.StartsWithDigit, false, 1)
			};
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x000CDA3C File Offset: 0x000CBC3C
		[FeatureCalculator("StartsWithLetter")]
		public static IReadOnlyList<PredicateFeature> StartsWithLetterScore(IReadOnlyList<PredicateFeature> s)
		{
			return new BasicPredicateFeature[]
			{
				new BasicPredicateFeature(PredicateType.StartsWithLetter, false, 1)
			};
		}

		// Token: 0x060041C1 RID: 16833 RVA: 0x000CDA4F File Offset: 0x000CBC4F
		[FeatureCalculator("StartsWith", Method = CalculationMethod.FromChildrenNodes)]
		public static IReadOnlyList<PredicateFeature> StartsWithScore(VariableNode s, LiteralNode r)
		{
			return new RegexPredicateFeature[]
			{
				new RegexPredicateFeature((RegularExpression)r.Value, PredicateType.StartsWith, false, 1)
			};
		}

		// Token: 0x060041C2 RID: 16834 RVA: 0x000CDA6E File Offset: 0x000CBC6E
		[FeatureCalculator("StartsWithString", Method = CalculationMethod.FromChildrenNodes)]
		public static IReadOnlyList<PredicateFeature> StartsWithStringScore(VariableNode s, LiteralNode str)
		{
			return new StringPredicateFeature[]
			{
				new StringPredicateFeature((string)str.Value, PredicateType.StartsWithString, false, 1)
			};
		}

		// Token: 0x060041C3 RID: 16835 RVA: 0x000CDA8C File Offset: 0x000CBC8C
		[FeatureCalculator("True")]
		public static IReadOnlyList<PredicateFeature> TrueScore()
		{
			return new BasicPredicateFeature[]
			{
				new BasicPredicateFeature(PredicateType.True, false, 1)
			};
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x000CDA8C File Offset: 0x000CBC8C
		protected override IReadOnlyList<PredicateFeature> GetFeatureValueForVariable(VariableNode variable)
		{
			return new BasicPredicateFeature[]
			{
				new BasicPredicateFeature(PredicateType.True, false, 1)
			};
		}
	}
}
