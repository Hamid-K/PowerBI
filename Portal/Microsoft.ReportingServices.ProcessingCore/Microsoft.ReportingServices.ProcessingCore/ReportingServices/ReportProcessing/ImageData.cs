using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200072D RID: 1837
	[Serializable]
	internal sealed class ImageData
	{
		// Token: 0x06006636 RID: 26166 RVA: 0x00191156 File Offset: 0x0018F356
		internal ImageData(byte[] data, string mimeType)
		{
			this.m_data = data;
			this.m_MIMEType = mimeType;
		}

		// Token: 0x17002422 RID: 9250
		// (get) Token: 0x06006637 RID: 26167 RVA: 0x0019116C File Offset: 0x0018F36C
		internal string MIMEType
		{
			get
			{
				return this.m_MIMEType;
			}
		}

		// Token: 0x17002423 RID: 9251
		// (get) Token: 0x06006638 RID: 26168 RVA: 0x00191174 File Offset: 0x0018F374
		internal byte[] Data
		{
			get
			{
				return this.m_data;
			}
		}

		// Token: 0x040032EB RID: 13035
		private byte[] m_data;

		// Token: 0x040032EC RID: 13036
		private string m_MIMEType;
	}
}
