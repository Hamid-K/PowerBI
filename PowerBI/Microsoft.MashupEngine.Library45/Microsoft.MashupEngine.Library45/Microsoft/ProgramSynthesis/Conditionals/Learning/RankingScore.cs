using System;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Learning.Clustering;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Conditionals.Learning
{
	// Token: 0x02000A59 RID: 2649
	public class RankingScore : Feature<double>
	{
		// Token: 0x060041C8 RID: 16840 RVA: 0x000CDAB4 File Offset: 0x000CBCB4
		public RankingScore(Grammar grammar)
			: base(grammar, "Score", false, false, null, Feature<double>.FeatureInfoResolution.Declared)
		{
			this._featureRanking = new RankingFeatureCollector(grammar);
		}

		// Token: 0x060041C9 RID: 16841 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("Disjunction", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("Conjunction", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("Not", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("IsNullOrWhiteSpace", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("IsNull", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("IsWhiteSpace", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("StartsWithString", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("StartsWithDigit", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("StartsWithLetter", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("EndsWithString", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("EndsWithDigit", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("EndsWithLetter", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("ContainsString", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("Matches", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("StartsWith", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("EndsWith", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("Contains", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("True", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("k", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("str", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("r", Method = CalculationMethod.FromProgramNode)]
		public static double NoScore(ProgramNode _)
		{
			return 0.0;
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x000CDAD2 File Offset: 0x000CBCD2
		[FeatureCalculator("Start", Method = CalculationMethod.FromProgramNode)]
		public double Score(ProgramNode node)
		{
			return this._featureRanking.Calculate(node, null).Sum((PredicateFeature f) => f.Score);
		}

		// Token: 0x060041CB RID: 16843 RVA: 0x0001AF59 File Offset: 0x00019159
		protected override double GetFeatureValueForVariable(VariableNode variable)
		{
			return 0.0;
		}

		// Token: 0x04001D94 RID: 7572
		private readonly RankingFeatureCollector _featureRanking;
	}
}
