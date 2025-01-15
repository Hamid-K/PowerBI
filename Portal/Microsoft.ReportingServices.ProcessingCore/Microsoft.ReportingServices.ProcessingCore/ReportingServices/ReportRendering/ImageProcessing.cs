using System;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000074 RID: 116
	internal sealed class ImageProcessing : ImageBase
	{
		// Token: 0x06000727 RID: 1831 RVA: 0x0001B6F8 File Offset: 0x000198F8
		internal ImageProcessing DeepClone()
		{
			ImageProcessing imageProcessing = new ImageProcessing();
			if (this.m_imageData != null)
			{
				imageProcessing.m_imageData = new byte[this.m_imageData.Length];
				this.m_imageData.CopyTo(imageProcessing.m_imageData, 0);
			}
			if (this.m_mimeType != null)
			{
				imageProcessing.m_mimeType = string.Copy(this.m_mimeType);
			}
			imageProcessing.m_sizing = this.m_sizing;
			return imageProcessing;
		}

		// Token: 0x04000202 RID: 514
		internal byte[] m_imageData;

		// Token: 0x04000203 RID: 515
		internal string m_mimeType;

		// Token: 0x04000204 RID: 516
		internal Image.Sizings m_sizing;
	}
}
