using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000560 RID: 1376
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspMvcAreaMasterLocationFormatAttribute : Attribute
	{
		// Token: 0x06001EDC RID: 7900 RVA: 0x000599DE File Offset: 0x00057BDE
		public AspMvcAreaMasterLocationFormatAttribute(string format)
		{
			this.Format = format;
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x000599ED File Offset: 0x00057BED
		public string Format { get; }
	}
}
