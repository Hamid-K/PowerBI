using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000561 RID: 1377
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspMvcAreaPartialViewLocationFormatAttribute : Attribute
	{
		// Token: 0x06001EDE RID: 7902 RVA: 0x000599F5 File Offset: 0x00057BF5
		public AspMvcAreaPartialViewLocationFormatAttribute(string format)
		{
			this.Format = format;
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001EDF RID: 7903 RVA: 0x00059A04 File Offset: 0x00057C04
		public string Format { get; }
	}
}
