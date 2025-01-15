using System;

namespace Microsoft.ProgramSynthesis.Transformation.Text
{
	// Token: 0x02001BA4 RID: 7076
	internal class RankingScoreModelModified : RankingScoreModel
	{
		// Token: 0x0600E7E1 RID: 59361 RVA: 0x003123E9 File Offset: 0x003105E9
		public override double ConstStr(double bias_ConstStr, double score_ConstStr_s, double base_ConstantStringLength, double base_LogConstantStringLength, double base_IsCommonDelimiter, double base_ExampleCount, double base_AllInputsCount, double base_ConstantInInput, double base_ConstantinInputPenalty, double base_ConditionalTokenCounts)
		{
			return 1800.0;
		}
	}
}
