using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001D9 RID: 473
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class AspMvcAreaViewLocationFormatAttribute : Attribute
	{
		// Token: 0x06001447 RID: 5191 RVA: 0x0003691B File Offset: 0x00034B1B
		public AspMvcAreaViewLocationFormatAttribute([NotNull] string format)
		{
			this.Format = format;
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0003692A File Offset: 0x00034B2A
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x00036932 File Offset: 0x00034B32
		[NotNull]
		public string Format { get; private set; }
	}
}
