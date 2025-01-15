using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B2E RID: 2862
	public abstract class MqHeaderWithData : MqHeader
	{
		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x06005A2C RID: 23084 RVA: 0x00173E94 File Offset: 0x00172094
		internal override int SendLength
		{
			get
			{
				return base.SendLength + ((this.preparedBytes == null) ? 0 : this.preparedBytes.Length);
			}
		}

		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x06005A2D RID: 23085
		protected abstract int ConsumedBytesLength { get; }

		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x06005A2E RID: 23086 RVA: 0x00173EB0 File Offset: 0x001720B0
		internal override int BytesConsumed
		{
			get
			{
				return base.BytesConsumed + this.ConsumedBytesLength;
			}
		}

		// Token: 0x06005A2F RID: 23087 RVA: 0x00173EBF File Offset: 0x001720BF
		protected MqHeaderWithData(MqHeaderType headerType, OrderedMqHeaderType orderedHeaderType, string name, string formatString, int fixedHeaderLength)
			: base(headerType, orderedHeaderType, name, formatString, fixedHeaderLength)
		{
		}

		// Token: 0x04004748 RID: 18248
		protected byte[] preparedBytes;
	}
}
