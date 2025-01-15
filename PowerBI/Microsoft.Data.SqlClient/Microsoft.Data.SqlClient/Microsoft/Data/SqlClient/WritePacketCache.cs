using System;
using System.Collections.Generic;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000B7 RID: 183
	internal sealed class WritePacketCache : IDisposable
	{
		// Token: 0x06000CE5 RID: 3301 RVA: 0x0002769F File Offset: 0x0002589F
		public WritePacketCache()
		{
			this._disposed = false;
			this._packets = new Stack<SNIPacket>();
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x000276BC File Offset: 0x000258BC
		public SNIPacket Take(SNIHandle sniHandle)
		{
			SNIPacket snipacket;
			if (this._packets.Count > 0)
			{
				snipacket = this._packets.Pop();
				SNINativeMethodWrapper.SNIPacketReset(sniHandle, SNINativeMethodWrapper.IOType.WRITE, snipacket, SNINativeMethodWrapper.ConsumerNumber.SNI_Consumer_SNI);
			}
			else
			{
				snipacket = new SNIPacket(sniHandle);
			}
			return snipacket;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x000276F6 File Offset: 0x000258F6
		public void Add(SNIPacket packet)
		{
			if (!this._disposed)
			{
				this._packets.Push(packet);
				return;
			}
			packet.Dispose();
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00027713 File Offset: 0x00025913
		public void Clear()
		{
			while (this._packets.Count > 0)
			{
				this._packets.Pop().Dispose();
			}
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00027735 File Offset: 0x00025935
		public void Dispose()
		{
			if (!this._disposed)
			{
				this._disposed = true;
				this.Clear();
			}
		}

		// Token: 0x0400057A RID: 1402
		private bool _disposed;

		// Token: 0x0400057B RID: 1403
		private Stack<SNIPacket> _packets;
	}
}
