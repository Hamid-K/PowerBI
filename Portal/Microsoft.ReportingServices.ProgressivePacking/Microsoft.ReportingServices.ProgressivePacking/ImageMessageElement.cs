using System;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x02000004 RID: 4
	internal abstract class ImageMessageElement
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002077 File Offset: 0x00000277
		public ImageMessageElement()
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000207F File Offset: 0x0000027F
		public ImageMessageElement(string imageUrl, string imageWidth, string imageHeight)
		{
			this.ImageUrl = imageUrl;
			this.ImageWidth = imageWidth;
			this.ImageHeight = imageHeight;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000209C File Offset: 0x0000029C
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020A4 File Offset: 0x000002A4
		public string ImageUrl { get; protected set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020AD File Offset: 0x000002AD
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020B5 File Offset: 0x000002B5
		public string ImageWidth { get; protected set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020C6 File Offset: 0x000002C6
		public string ImageHeight { get; protected set; }

		// Token: 0x0600000E RID: 14 RVA: 0x000020D0 File Offset: 0x000002D0
		public override bool Equals(object obj)
		{
			ImageMessageElement imageMessageElement = obj as ImageMessageElement;
			return imageMessageElement != null && (string.Compare(this.ImageUrl, imageMessageElement.ImageUrl, StringComparison.Ordinal) == 0 && string.Compare(this.ImageWidth, imageMessageElement.ImageWidth, StringComparison.Ordinal) == 0) && string.Compare(this.ImageHeight, imageMessageElement.ImageHeight, StringComparison.Ordinal) == 0;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002128 File Offset: 0x00000328
		public override int GetHashCode()
		{
			return ((this.ImageUrl != null) ? this.ImageUrl.GetHashCode() : 0) ^ ((this.ImageWidth != null) ? this.ImageWidth.GetHashCode() : 0) ^ ((this.ImageHeight != null) ? this.ImageHeight.GetHashCode() : 0);
		}
	}
}
