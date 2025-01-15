using System;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x0200001B RID: 27
	public sealed class PidInfo
	{
		// Token: 0x06000074 RID: 116 RVA: 0x000038D6 File Offset: 0x00001AD6
		public PidInfo(string productId, byte[] digitalProductId, SkuType edition)
		{
			this.productId = productId;
			this.digitalProductId = digitalProductId;
			this.edition = edition;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000038F3 File Offset: 0x00001AF3
		public string ProductId
		{
			get
			{
				return this.productId;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000038FB File Offset: 0x00001AFB
		public byte[] DigitalProductId
		{
			get
			{
				return this.digitalProductId;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003903 File Offset: 0x00001B03
		public SkuType Edition
		{
			get
			{
				return this.edition;
			}
		}

		// Token: 0x04000083 RID: 131
		private readonly string productId;

		// Token: 0x04000084 RID: 132
		private readonly byte[] digitalProductId;

		// Token: 0x04000085 RID: 133
		private readonly SkuType edition;
	}
}
