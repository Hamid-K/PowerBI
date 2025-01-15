using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000551 RID: 1361
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class LocalizationRequiredAttribute : Attribute
	{
		// Token: 0x06001EB6 RID: 7862 RVA: 0x000598A7 File Offset: 0x00057AA7
		public LocalizationRequiredAttribute()
			: this(true)
		{
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x000598B0 File Offset: 0x00057AB0
		public LocalizationRequiredAttribute(bool required)
		{
			this.Required = required;
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x000598BF File Offset: 0x00057ABF
		public bool Required { get; }
	}
}
