using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002CA RID: 714
	internal sealed class SocketState
	{
		// Token: 0x06001A74 RID: 6772 RVA: 0x00050309 File Offset: 0x0004E509
		public SocketState(Socket socket)
		{
			this._socket = socket;
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x00050318 File Offset: 0x0004E518
		// (set) Token: 0x06001A76 RID: 6774 RVA: 0x00050320 File Offset: 0x0004E520
		public string CacheName { get; set; }

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x00050329 File Offset: 0x0004E529
		public Socket Socket
		{
			get
			{
				return this._socket;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x00050331 File Offset: 0x0004E531
		// (set) Token: 0x06001A79 RID: 6777 RVA: 0x00050339 File Offset: 0x0004E539
		public IVelocityPacket Packet { get; set; }

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x00050342 File Offset: 0x0004E542
		// (set) Token: 0x06001A7B RID: 6779 RVA: 0x0005034A File Offset: 0x0004E54A
		public IEnumerator<IList<ArraySegment<byte>>> ActiveEnumerator { get; set; }

		// Token: 0x04000E15 RID: 3605
		private readonly Socket _socket;
	}
}
