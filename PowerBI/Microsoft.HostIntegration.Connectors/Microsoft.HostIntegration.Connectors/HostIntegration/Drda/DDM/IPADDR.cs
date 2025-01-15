using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008E1 RID: 2273
	public class IPADDR
	{
		// Token: 0x0600480F RID: 18447 RVA: 0x00105D3C File Offset: 0x00103F3C
		public IPADDR(int ddmLength)
		{
			this._ddmLength = ddmLength;
		}

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x06004810 RID: 18448 RVA: 0x00105D53 File Offset: 0x00103F53
		public byte[] IPAddressBytes
		{
			get
			{
				return this.ipaddressbytes;
			}
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x06004811 RID: 18449 RVA: 0x00105D5B File Offset: 0x00103F5B
		public ushort Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x06004812 RID: 18450 RVA: 0x00105D64 File Offset: 0x00103F64
		public void Read(DdmReader reader)
		{
			this.ReadAsync(reader, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x00105D8C File Offset: 0x00103F8C
		public async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			byte[] array = await reader.ReadBytesAsync(this._ddmLength - 2, isAsync, cancellationToken);
			this.ipaddressbytes = array;
			this.port = await reader.ReadUInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
		}

		// Token: 0x0400349B RID: 13467
		private byte[] ipaddressbytes;

		// Token: 0x0400349C RID: 13468
		private int _ddmLength = 10;

		// Token: 0x0400349D RID: 13469
		private ushort port;
	}
}
