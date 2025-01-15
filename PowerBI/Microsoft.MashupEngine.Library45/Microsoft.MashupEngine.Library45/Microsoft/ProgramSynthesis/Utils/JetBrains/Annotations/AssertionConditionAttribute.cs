using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x0200057A RID: 1402
	[AttributeUsage(AttributeTargets.Parameter)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AssertionConditionAttribute : Attribute
	{
		// Token: 0x06001F05 RID: 7941 RVA: 0x00059AF2 File Offset: 0x00057CF2
		public AssertionConditionAttribute(AssertionConditionType conditionType)
		{
			this.ConditionType = conditionType;
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x00059B01 File Offset: 0x00057D01
		public AssertionConditionType ConditionType { get; }
	}
}
