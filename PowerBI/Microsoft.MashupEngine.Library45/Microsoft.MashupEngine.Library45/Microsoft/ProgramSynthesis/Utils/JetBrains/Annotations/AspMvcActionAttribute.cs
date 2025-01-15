using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000566 RID: 1382
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspMvcActionAttribute : Attribute
	{
		// Token: 0x06001EE8 RID: 7912 RVA: 0x0000957E File Offset: 0x0000777E
		public AspMvcActionAttribute()
		{
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x00059A68 File Offset: 0x00057C68
		public AspMvcActionAttribute(string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x00059A77 File Offset: 0x00057C77
		public string AnonymousProperty { get; }
	}
}
