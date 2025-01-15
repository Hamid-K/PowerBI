using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B2F RID: 2863
	public abstract class MqStructuredHeader : MqHeaderWithData
	{
		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x06005A30 RID: 23088 RVA: 0x00173ECE File Offset: 0x001720CE
		protected override int ConsumedBytesLength
		{
			get
			{
				return this.consumedBytesLength;
			}
		}

		// Token: 0x06005A31 RID: 23089 RVA: 0x00173ED6 File Offset: 0x001720D6
		protected MqStructuredHeader(MqHeaderType headerType, OrderedMqHeaderType orderedHeaderType, string name, string formatString, int fixedHeaderLength)
			: base(headerType, orderedHeaderType, name, formatString, fixedHeaderLength)
		{
		}

		// Token: 0x06005A32 RID: 23090 RVA: 0x00173EE5 File Offset: 0x001720E5
		internal override void Prepare()
		{
			this.preparedBytes = this.ConvertStructureToBytes();
		}

		// Token: 0x06005A33 RID: 23091
		protected abstract byte[] ConvertStructureToBytes();

		// Token: 0x04004749 RID: 18249
		protected int consumedBytesLength;
	}
}
