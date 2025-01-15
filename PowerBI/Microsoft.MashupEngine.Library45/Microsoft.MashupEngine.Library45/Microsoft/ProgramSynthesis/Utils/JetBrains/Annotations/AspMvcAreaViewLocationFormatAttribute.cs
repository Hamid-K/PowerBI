using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000562 RID: 1378
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspMvcAreaViewLocationFormatAttribute : Attribute
	{
		// Token: 0x06001EE0 RID: 7904 RVA: 0x00059A0C File Offset: 0x00057C0C
		public AspMvcAreaViewLocationFormatAttribute(string format)
		{
			this.Format = format;
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x00059A1B File Offset: 0x00057C1B
		public string Format { get; }
	}
}
