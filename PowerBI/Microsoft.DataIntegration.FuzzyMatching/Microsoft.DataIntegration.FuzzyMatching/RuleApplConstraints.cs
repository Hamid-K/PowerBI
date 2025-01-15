using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000AB RID: 171
	[Serializable]
	internal sealed class RuleApplConstraints
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x0001D80C File Offset: 0x0001BA0C
		public void Reset(int maxPos, int maxRuleId)
		{
			if (this.RuleAtPos.Length < maxPos)
			{
				Array.Resize<int>(ref this.RuleAtPos, (int)((double)maxPos * 1.5));
				Array.Resize<int>(ref this.FreeRulesAtPos, (int)((double)maxPos * 1.5));
			}
			if (this.RuleStatus.Length < maxRuleId)
			{
				Array.Resize<int>(ref this.RuleStatus, (int)((double)maxRuleId * 1.5));
			}
			Array.Clear(this.FreeRulesAtPos, 0, maxPos);
			for (int i = 0; i < maxPos; i++)
			{
				this.RuleAtPos[i] = -1;
			}
			Array.Clear(this.RuleStatus, 0, maxRuleId);
		}

		// Token: 0x04000263 RID: 611
		public int[] RuleStatus = new int[0];

		// Token: 0x04000264 RID: 612
		public int[] RuleAtPos = new int[0];

		// Token: 0x04000265 RID: 613
		public int[] FreeRulesAtPos = new int[0];
	}
}
