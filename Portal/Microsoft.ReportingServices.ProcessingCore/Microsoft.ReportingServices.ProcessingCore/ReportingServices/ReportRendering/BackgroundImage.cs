using System;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000071 RID: 113
	internal sealed class BackgroundImage : IImage
	{
		// Token: 0x06000700 RID: 1792 RVA: 0x0001ACEB File Offset: 0x00018EEB
		internal BackgroundImage(RenderingContext context, Image.SourceType imageSource, object imageValue, string mimeType)
		{
			this.m_internalImage = new InternalImage(imageSource, mimeType, imageValue, context);
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0001AD03 File Offset: 0x00018F03
		public byte[] ImageData
		{
			get
			{
				return this.m_internalImage.ImageData;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001AD10 File Offset: 0x00018F10
		public string MIMEType
		{
			get
			{
				return this.m_internalImage.MIMEType;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0001AD1D File Offset: 0x00018F1D
		public string StreamName
		{
			get
			{
				return this.m_internalImage.StreamName;
			}
		}

		// Token: 0x040001F4 RID: 500
		private InternalImage m_internalImage;
	}
}
