using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001DB RID: 475
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class AspMvcPartialViewLocationFormatAttribute : Attribute
	{
		// Token: 0x0600144D RID: 5197 RVA: 0x0003695B File Offset: 0x00034B5B
		public AspMvcPartialViewLocationFormatAttribute([NotNull] string format)
		{
			this.Format = format;
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x0003696A File Offset: 0x00034B6A
		// (set) Token: 0x0600144F RID: 5199 RVA: 0x00036972 File Offset: 0x00034B72
		[NotNull]
		public string Format { get; private set; }
	}
}
