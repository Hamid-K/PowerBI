using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003E6 RID: 998
	public sealed class EmbeddedImage
	{
		// Token: 0x06001FC1 RID: 8129 RVA: 0x0007ECE6 File Offset: 0x0007CEE6
		public EmbeddedImage()
		{
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0007ED04 File Offset: 0x0007CF04
		public EmbeddedImage(string name, byte[] bytes, MIMEType.Enum mimetype)
		{
			this.m_name = name;
			this.m_imageData.Bytes = bytes;
			this.m_mimeType.Set(mimetype);
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x0007ED41 File Offset: 0x0007CF41
		// (set) Token: 0x06001FC4 RID: 8132 RVA: 0x0007ED49 File Offset: 0x0007CF49
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x0007ED52 File Offset: 0x0007CF52
		// (set) Token: 0x06001FC6 RID: 8134 RVA: 0x0007ED5A File Offset: 0x0007CF5A
		public MIMEType MIMEType
		{
			get
			{
				return this.m_mimeType;
			}
			set
			{
				this.m_mimeType = value;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x0007ED63 File Offset: 0x0007CF63
		// (set) Token: 0x06001FC8 RID: 8136 RVA: 0x0007ED6B File Offset: 0x0007CF6B
		public ImageData ImageData
		{
			get
			{
				return this.m_imageData;
			}
			set
			{
				this.m_imageData = value;
			}
		}

		// Token: 0x04000DD6 RID: 3542
		private string m_name;

		// Token: 0x04000DD7 RID: 3543
		private MIMEType m_mimeType = new MIMEType();

		// Token: 0x04000DD8 RID: 3544
		private ImageData m_imageData = new ImageData();
	}
}
