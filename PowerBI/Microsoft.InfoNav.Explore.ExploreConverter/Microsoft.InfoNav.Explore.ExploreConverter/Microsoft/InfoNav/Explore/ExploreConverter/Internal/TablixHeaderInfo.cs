using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000A4 RID: 164
	internal sealed class TablixHeaderInfo
	{
		// Token: 0x0600032C RID: 812 RVA: 0x0000D21D File Offset: 0x0000B41D
		internal TablixHeaderInfo(float width, float minWidth, float maxWidth)
		{
			this.Width = width;
			this.MinWidth = minWidth;
			this.MaxWidth = maxWidth;
			this.ChildHeaderInfos = new List<TablixHeaderInfo>();
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000D245 File Offset: 0x0000B445
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000D24D File Offset: 0x0000B44D
		public float Width { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000D256 File Offset: 0x0000B456
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000D25E File Offset: 0x0000B45E
		public float MinWidth { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000D267 File Offset: 0x0000B467
		// (set) Token: 0x06000332 RID: 818 RVA: 0x0000D26F File Offset: 0x0000B46F
		public float MaxWidth { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000D278 File Offset: 0x0000B478
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000D280 File Offset: 0x0000B480
		public List<TablixHeaderInfo> ChildHeaderInfos { get; set; }
	}
}
