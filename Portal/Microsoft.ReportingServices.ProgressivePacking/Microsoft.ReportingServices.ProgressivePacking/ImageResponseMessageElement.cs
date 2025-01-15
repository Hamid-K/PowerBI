using System;
using System.IO;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x0200000A RID: 10
	internal sealed class ImageResponseMessageElement : ImageMessageElement
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000024FF File Offset: 0x000006FF
		public ImageResponseMessageElement()
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002507 File Offset: 0x00000707
		public ImageResponseMessageElement(string imageUrl, string imageWidth, string imageHeight, byte[] imageBytes, string serverErrorCode)
			: base(imageUrl, imageWidth, imageHeight)
		{
			this.ServerErrorCode = serverErrorCode;
			this.ImageBytes = imageBytes;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002522 File Offset: 0x00000722
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000252A File Offset: 0x0000072A
		public string ServerErrorCode { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002533 File Offset: 0x00000733
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000253B File Offset: 0x0000073B
		public byte[] ImageBytes { get; private set; }

		// Token: 0x06000027 RID: 39 RVA: 0x00002544 File Offset: 0x00000744
		public void Write(BinaryWriter writer)
		{
			this.WriteStringValue(base.ImageUrl, writer);
			this.WriteStringValue(base.ImageWidth, writer);
			this.WriteStringValue(base.ImageHeight, writer);
			this.WriteStringValue(this.ServerErrorCode, writer);
			if (this.ImageBytes != null)
			{
				writer.Write(this.ImageBytes.Length);
				writer.Write(this.ImageBytes);
				return;
			}
			writer.Write(0);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025AF File Offset: 0x000007AF
		private void WriteStringValue(string value, BinaryWriter writer)
		{
			if (value == null)
			{
				writer.Write(string.Empty);
				return;
			}
			writer.Write(value);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025C8 File Offset: 0x000007C8
		public void Read(BinaryReader reader)
		{
			base.ImageUrl = reader.ReadString();
			base.ImageWidth = reader.ReadString();
			base.ImageHeight = reader.ReadString();
			this.ServerErrorCode = reader.ReadString();
			int num = reader.ReadInt32();
			this.ImageBytes = reader.ReadBytes(num);
		}
	}
}
