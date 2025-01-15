using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000565 RID: 1381
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspMvcViewLocationFormatAttribute : Attribute
	{
		// Token: 0x06001EE6 RID: 7910 RVA: 0x00059A51 File Offset: 0x00057C51
		public AspMvcViewLocationFormatAttribute(string format)
		{
			this.Format = format;
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x00059A60 File Offset: 0x00057C60
		public string Format { get; }
	}
}
