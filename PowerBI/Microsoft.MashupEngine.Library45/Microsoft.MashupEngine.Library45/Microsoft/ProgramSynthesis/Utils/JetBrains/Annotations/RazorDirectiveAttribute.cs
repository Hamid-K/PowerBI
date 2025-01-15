using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x0200058B RID: 1419
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class RazorDirectiveAttribute : Attribute
	{
		// Token: 0x06001F1D RID: 7965 RVA: 0x00059B9A File Offset: 0x00057D9A
		public RazorDirectiveAttribute(string directive)
		{
			this.Directive = directive;
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x00059BA9 File Offset: 0x00057DA9
		public string Directive { get; }
	}
}
