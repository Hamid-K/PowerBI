using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D3F RID: 7487
	internal abstract class ConstantStringFeature : RankingFeature
	{
		// Token: 0x0600FC6D RID: 64621 RVA: 0x0035E03D File Offset: 0x0035C23D
		protected ConstantStringFeature(Grammar grammar, string name, double learnedCoefficient = 0.0)
			: base(grammar, name, learnedCoefficient, true)
		{
		}

		// Token: 0x0600FC6E RID: 64622 RVA: 0x0035E049 File Offset: 0x0035C249
		[FeatureCalculator("ConstStr", Method = CalculationMethod.FromChildrenNodes)]
		public double Calculate(LiteralNode s)
		{
			return this.Calculate(s.Value as string);
		}

		// Token: 0x0600FC6F RID: 64623
		public abstract double Calculate(string s);

		// Token: 0x04005E48 RID: 24136
		internal static readonly HashSet<char> CommonDelimiters = new HashSet<char>(" ,;-/.\\()'\"<>");
	}
}
