using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001F1 RID: 497
	[AttributeUsage(AttributeTargets.Parameter)]
	internal sealed class AssertionConditionAttribute : Attribute
	{
		// Token: 0x06001476 RID: 5238 RVA: 0x00036AE3 File Offset: 0x00034CE3
		public AssertionConditionAttribute(AssertionConditionType conditionType)
		{
			this.ConditionType = conditionType;
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x00036AF2 File Offset: 0x00034CF2
		// (set) Token: 0x06001478 RID: 5240 RVA: 0x00036AFA File Offset: 0x00034CFA
		public AssertionConditionType ConditionType { get; private set; }
	}
}
