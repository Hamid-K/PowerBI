using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000587 RID: 1415
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspRequiredAttributeAttribute : Attribute
	{
		// Token: 0x06001F14 RID: 7956 RVA: 0x00059B2F File Offset: 0x00057D2F
		public AspRequiredAttributeAttribute(string attribute)
		{
			this.Attribute = attribute;
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x00059B3E File Offset: 0x00057D3E
		public string Attribute { get; }
	}
}
