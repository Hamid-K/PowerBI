using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007DD RID: 2013
	public static class LearningInfoUtils
	{
		// Token: 0x06002AE9 RID: 10985 RVA: 0x00078424 File Offset: 0x00076624
		public static LearningInfo WithProgramNode(this FeatureCalculationContext context, ProgramNode programNode)
		{
			return new LearningInfo(context, programNode);
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x0007842D File Offset: 0x0007662D
		public static LearningInfo WithProgramNode(this LearningInfo learningInfo, ProgramNode programNode)
		{
			if (learningInfo == null)
			{
				return null;
			}
			if (learningInfo.ProgramNode != programNode)
			{
				return new LearningInfo(learningInfo.FeatureCalculationContext, programNode);
			}
			return learningInfo;
		}
	}
}
