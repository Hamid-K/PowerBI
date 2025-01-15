using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B30 RID: 2864
	public abstract class MqUnstructuredHeader : MqHeaderWithData
	{
		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x06005A34 RID: 23092 RVA: 0x00173EF3 File Offset: 0x001720F3
		// (set) Token: 0x06005A35 RID: 23093 RVA: 0x00173EFB File Offset: 0x001720FB
		public byte[] Bytes
		{
			get
			{
				return this.bytes;
			}
			set
			{
				this.BytesBeingSet();
				this.bytes = value;
			}
		}

		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x06005A36 RID: 23094 RVA: 0x00173F0A File Offset: 0x0017210A
		protected override int ConsumedBytesLength
		{
			get
			{
				return this.Bytes.Length;
			}
		}

		// Token: 0x06005A37 RID: 23095 RVA: 0x00173ED6 File Offset: 0x001720D6
		protected MqUnstructuredHeader(MqHeaderType headerType, OrderedMqHeaderType orderedHeaderType, string name, string formatString, int fixedHeaderLength)
			: base(headerType, orderedHeaderType, name, formatString, fixedHeaderLength)
		{
		}

		// Token: 0x06005A38 RID: 23096 RVA: 0x00173F14 File Offset: 0x00172114
		internal override void Prepare()
		{
			this.preparedBytes = this.Bytes;
		}

		// Token: 0x06005A39 RID: 23097 RVA: 0x000036A9 File Offset: 0x000018A9
		protected virtual void BytesBeingSet()
		{
		}

		// Token: 0x0400474A RID: 18250
		private byte[] bytes;
	}
}
