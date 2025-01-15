using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x0200055B RID: 1371
	[AttributeUsage(AttributeTargets.Method)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class MustUseReturnValueAttribute : Attribute
	{
		// Token: 0x06001ECD RID: 7885 RVA: 0x0000957E File Offset: 0x0000777E
		public MustUseReturnValueAttribute()
		{
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x0005997D File Offset: 0x00057B7D
		public MustUseReturnValueAttribute(string justification)
		{
			this.Justification = justification;
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x0005998C File Offset: 0x00057B8C
		public string Justification { get; }
	}
}
