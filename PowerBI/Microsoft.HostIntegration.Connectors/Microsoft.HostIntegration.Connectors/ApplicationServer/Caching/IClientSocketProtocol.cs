using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200015C RID: 348
	internal interface IClientSocketProtocol
	{
		// Token: 0x06000ABC RID: 2748
		IVelocityRequestPacket CreateRequestPacket(VelocityPacketType type);

		// Token: 0x06000ABD RID: 2749
		IVelocityResponsePacket CreateEmptyResponsePacket();

		// Token: 0x06000ABE RID: 2750
		IEnumerable<ArraySegment<byte>> GetReadResponseBuffer(IList<IVelocityPacket> packets, IBufferManager bufferManager);

		// Token: 0x06000ABF RID: 2751
		IList<ArraySegment<byte>> GetWriteRequestBuffer(IVelocityRequestPacket packet, IBufferManager bufferManager);

		// Token: 0x06000AC0 RID: 2752
		IVelocityRequestPacket GetInitializationPacket(string cacheName);
	}
}
