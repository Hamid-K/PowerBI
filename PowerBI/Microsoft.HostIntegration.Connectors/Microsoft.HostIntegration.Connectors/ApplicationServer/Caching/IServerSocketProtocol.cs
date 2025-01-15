using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200015D RID: 349
	internal interface IServerSocketProtocol
	{
		// Token: 0x06000AC1 RID: 2753
		IVelocityResponsePacket CreateResponsePacket(VelocityPacketType type, int packetId);

		// Token: 0x06000AC2 RID: 2754
		IReplyContext CreateReplyContext(ITcpChannel tcpChannel, IList<IVelocityPacket> packets, VelocityPacketException exception);

		// Token: 0x06000AC3 RID: 2755
		IVelocityRequestPacket CreateEmptyRequestPacket();

		// Token: 0x06000AC4 RID: 2756
		IEnumerable<ArraySegment<byte>> GetReadRequestBuffer(IList<IVelocityPacket> packets, IBufferManager bufferManager);

		// Token: 0x06000AC5 RID: 2757
		IList<ArraySegment<byte>> GetWriteResponseBuffer(IVelocityResponsePacket packet, IBufferManager bufferManager);

		// Token: 0x06000AC6 RID: 2758
		IList<ArraySegment<byte>> GetWriteResponseBuffer(ICollection<IVelocityResponsePacket> packets, IBufferManager bufferManager);
	}
}
