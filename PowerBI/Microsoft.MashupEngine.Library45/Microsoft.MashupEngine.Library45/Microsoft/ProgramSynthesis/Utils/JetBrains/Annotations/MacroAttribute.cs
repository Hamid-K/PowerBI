using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x0200055F RID: 1375
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = true)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class MacroAttribute : Attribute
	{
		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x000599AB File Offset: 0x00057BAB
		// (set) Token: 0x06001ED6 RID: 7894 RVA: 0x000599B3 File Offset: 0x00057BB3
		public string Expression { get; set; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x000599BC File Offset: 0x00057BBC
		// (set) Token: 0x06001ED8 RID: 7896 RVA: 0x000599C4 File Offset: 0x00057BC4
		public int Editable { get; set; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x000599CD File Offset: 0x00057BCD
		// (set) Token: 0x06001EDA RID: 7898 RVA: 0x000599D5 File Offset: 0x00057BD5
		public string Target { get; set; }
	}
}
