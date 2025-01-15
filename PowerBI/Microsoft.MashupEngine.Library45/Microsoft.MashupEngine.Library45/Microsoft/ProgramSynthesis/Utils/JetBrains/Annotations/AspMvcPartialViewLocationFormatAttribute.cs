using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000564 RID: 1380
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspMvcPartialViewLocationFormatAttribute : Attribute
	{
		// Token: 0x06001EE4 RID: 7908 RVA: 0x00059A3A File Offset: 0x00057C3A
		public AspMvcPartialViewLocationFormatAttribute(string format)
		{
			this.Format = format;
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x00059A49 File Offset: 0x00057C49
		public string Format { get; }
	}
}
