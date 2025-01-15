using System;
using System.IO;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x02000006 RID: 6
	internal class ImageResponseMessageWriter
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002230 File Offset: 0x00000430
		public ImageResponseMessageWriter(IMessageWriter writer)
		{
			this.m_writer = writer;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002240 File Offset: 0x00000440
		public void WriteElement(ImageResponseMessageElement messageElement)
		{
			using (Stream stream = this.m_writer.CreateWritableStream("getExternalImagesResponse"))
			{
				BinaryWriter binaryWriter = new BinaryWriter(stream, MessageUtil.StringEncoding);
				messageElement.Write(binaryWriter);
				binaryWriter.Flush();
			}
		}

		// Token: 0x04000005 RID: 5
		private IMessageWriter m_writer;
	}
}
