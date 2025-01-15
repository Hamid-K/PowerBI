using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200032E RID: 814
	public abstract class BackgroundImageInstance : BaseInstance, IImageInstance
	{
		// Token: 0x06001E6B RID: 7787 RVA: 0x000763C6 File Offset: 0x000745C6
		internal BackgroundImageInstance(IReportScope reportScope)
			: base(reportScope)
		{
		}

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x06001E6C RID: 7788
		public abstract byte[] ImageData { get; }

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x06001E6D RID: 7789
		public abstract string StreamName { get; }

		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x06001E6E RID: 7790
		public abstract string MIMEType { get; }

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x06001E6F RID: 7791
		public abstract BackgroundRepeatTypes BackgroundRepeat { get; }

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06001E70 RID: 7792
		public abstract Positions Position { get; }

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06001E71 RID: 7793
		public abstract ReportColor TransparentColor { get; }
	}
}
