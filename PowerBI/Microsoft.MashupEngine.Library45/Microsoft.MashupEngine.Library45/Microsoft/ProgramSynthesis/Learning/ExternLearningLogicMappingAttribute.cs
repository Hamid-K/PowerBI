using System;

namespace Microsoft.ProgramSynthesis.Learning
{
	// Token: 0x020006B9 RID: 1721
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	public class ExternLearningLogicMappingAttribute : Attribute
	{
		// Token: 0x0600253E RID: 9534 RVA: 0x00067853 File Offset: 0x00065A53
		public ExternLearningLogicMappingAttribute(string rule)
		{
			this.Rule = rule;
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x0600253F RID: 9535 RVA: 0x00067862 File Offset: 0x00065A62
		// (set) Token: 0x06002540 RID: 9536 RVA: 0x0006786A File Offset: 0x00065A6A
		public string Rule { get; set; }
	}
}
