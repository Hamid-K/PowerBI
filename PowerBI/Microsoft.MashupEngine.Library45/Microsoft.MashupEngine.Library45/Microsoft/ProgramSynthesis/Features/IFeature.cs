using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Rules;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007C2 RID: 1986
	public interface IFeature
	{
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002A4D RID: 10829
		FeatureInfo Info { get; }

		// Token: 0x06002A4E RID: 10830
		IFeature GetExternFeature(GrammarRule rule, int parameter);

		// Token: 0x06002A4F RID: 10831
		object GetFeatureValueForVariable(VariableNode variable);

		// Token: 0x06002A50 RID: 10832
		object Calculate(ProgramNode program, LearningInfo learningInfo);

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002A51 RID: 10833
		bool UseComputedInputsForFccEquality { get; }
	}
}
