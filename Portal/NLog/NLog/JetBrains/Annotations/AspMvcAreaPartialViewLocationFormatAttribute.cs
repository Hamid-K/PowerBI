using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001D8 RID: 472
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class AspMvcAreaPartialViewLocationFormatAttribute : Attribute
	{
		// Token: 0x06001444 RID: 5188 RVA: 0x000368FB File Offset: 0x00034AFB
		public AspMvcAreaPartialViewLocationFormatAttribute([NotNull] string format)
		{
			this.Format = format;
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x0003690A File Offset: 0x00034B0A
		// (set) Token: 0x06001446 RID: 5190 RVA: 0x00036912 File Offset: 0x00034B12
		[NotNull]
		public string Format { get; private set; }
	}
}
