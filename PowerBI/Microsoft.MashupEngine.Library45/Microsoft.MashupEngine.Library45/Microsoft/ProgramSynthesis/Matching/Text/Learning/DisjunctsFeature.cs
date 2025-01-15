using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Matching.Text.Learning
{
	// Token: 0x02001204 RID: 4612
	public class DisjunctsFeature : Feature<IEnumerable<ProgramNode>>
	{
		// Token: 0x06008B15 RID: 35605 RVA: 0x001D2036 File Offset: 0x001D0236
		public DisjunctsFeature(Grammar grammar)
			: base(grammar, "Disjuncts", false, false, null, Feature<IEnumerable<ProgramNode>>.FeatureInfoResolution.Declared)
		{
		}

		// Token: 0x06008B16 RID: 35606 RVA: 0x001D2048 File Offset: 0x001D0248
		[FeatureCalculator("Disjunction", Method = CalculationMethod.FromChildrenNodes)]
		public IEnumerable<ProgramNode> Disjuncts_Disjunction(ProgramNode match, ProgramNode disjunctiveMatch)
		{
			return this.Calculate(disjunctiveMatch, null).PrependItem(match);
		}

		// Token: 0x06008B17 RID: 35607 RVA: 0x001D2058 File Offset: 0x001D0258
		[FeatureCalculator("IfThenElse", Method = CalculationMethod.FromChildrenNodes)]
		public IEnumerable<ProgramNode> Disjuncts_IfThenElse(ProgramNode match, ProgramNode label, ProgramNode labelledDisjunction)
		{
			return this.Calculate(labelledDisjunction, null).PrependItem(match);
		}

		// Token: 0x06008B18 RID: 35608 RVA: 0x001D2068 File Offset: 0x001D0268
		[FeatureCalculator("NoMatch")]
		public static IEnumerable<ProgramNode> Disjuncts_NoMatch()
		{
			return Enumerable.Empty<ProgramNode>();
		}
	}
}
