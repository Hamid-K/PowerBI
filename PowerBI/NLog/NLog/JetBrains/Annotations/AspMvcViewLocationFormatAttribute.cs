using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001DC RID: 476
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class AspMvcViewLocationFormatAttribute : Attribute
	{
		// Token: 0x06001450 RID: 5200 RVA: 0x0003697B File Offset: 0x00034B7B
		public AspMvcViewLocationFormatAttribute([NotNull] string format)
		{
			this.Format = format;
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x0003698A File Offset: 0x00034B8A
		// (set) Token: 0x06001452 RID: 5202 RVA: 0x00036992 File Offset: 0x00034B92
		[NotNull]
		public string Format { get; private set; }
	}
}
