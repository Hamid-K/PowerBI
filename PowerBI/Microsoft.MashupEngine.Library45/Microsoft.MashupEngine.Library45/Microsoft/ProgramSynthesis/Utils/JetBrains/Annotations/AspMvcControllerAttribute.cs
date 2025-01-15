using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000568 RID: 1384
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspMvcControllerAttribute : Attribute
	{
		// Token: 0x06001EEE RID: 7918 RVA: 0x0000957E File Offset: 0x0000777E
		public AspMvcControllerAttribute()
		{
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00059A96 File Offset: 0x00057C96
		public AspMvcControllerAttribute(string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x00059AA5 File Offset: 0x00057CA5
		public string AnonymousProperty { get; }
	}
}
