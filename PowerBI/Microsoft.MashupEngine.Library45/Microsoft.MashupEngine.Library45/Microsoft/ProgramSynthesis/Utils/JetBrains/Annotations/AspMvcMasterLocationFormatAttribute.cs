using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000563 RID: 1379
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspMvcMasterLocationFormatAttribute : Attribute
	{
		// Token: 0x06001EE2 RID: 7906 RVA: 0x00059A23 File Offset: 0x00057C23
		public AspMvcMasterLocationFormatAttribute(string format)
		{
			this.Format = format;
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x00059A32 File Offset: 0x00057C32
		public string Format { get; }
	}
}
