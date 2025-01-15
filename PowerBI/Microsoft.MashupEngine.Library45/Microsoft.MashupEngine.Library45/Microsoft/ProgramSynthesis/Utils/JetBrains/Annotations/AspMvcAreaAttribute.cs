using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000567 RID: 1383
	[AttributeUsage(AttributeTargets.Parameter)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspMvcAreaAttribute : Attribute
	{
		// Token: 0x06001EEB RID: 7915 RVA: 0x0000957E File Offset: 0x0000777E
		public AspMvcAreaAttribute()
		{
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x00059A7F File Offset: 0x00057C7F
		public AspMvcAreaAttribute(string anonymousProperty)
		{
			this.AnonymousProperty = anonymousProperty;
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x00059A8E File Offset: 0x00057C8E
		public string AnonymousProperty { get; }
	}
}
