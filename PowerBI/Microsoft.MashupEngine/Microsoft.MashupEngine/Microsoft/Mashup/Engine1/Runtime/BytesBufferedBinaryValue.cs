using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200128F RID: 4751
	internal class BytesBufferedBinaryValue : BufferedBinaryValue
	{
		// Token: 0x06007CD8 RID: 31960 RVA: 0x001AC73D File Offset: 0x001AA93D
		public BytesBufferedBinaryValue(byte[] bytes)
		{
			this.bytes = bytes;
		}

		// Token: 0x170021F4 RID: 8692
		// (get) Token: 0x06007CD9 RID: 31961 RVA: 0x001AC74C File Offset: 0x001AA94C
		public sealed override byte[] AsBytes
		{
			get
			{
				return this.bytes;
			}
		}

		// Token: 0x040044D9 RID: 17625
		private readonly byte[] bytes;
	}
}
